using System.Collections.Generic;
using System.Linq;
using Generators.Level.Data;
using Level.Core;
using Unity.VisualScripting;

namespace Generators.Level
{
    public class LevelDataManager // reserved: 0 - starting level, 1 - empty level
    {
        private Dictionary<int, LevelData> _levels;
        private Dictionary<int, LevelRelationData> _relations;

        public LevelDataManager(IReadOnlyCollection<LevelData> levels, LevelData starting, LevelData emptyLevel)
        {
            _levels = new Dictionary<int, LevelData>();
            _relations = new Dictionary<int, LevelRelationData>();
            InitLevelMap(levels, starting, emptyLevel);
            InitRelations();
        }

        private void InitLevelMap(IReadOnlyCollection<LevelData> levels, LevelData starting, LevelData emptyLevel)
        {
            var l = new List<LevelData> { starting, emptyLevel };
            l.AddRange(levels);
            _levels = l.Select((level, i) => new { level, i })
                .ToDictionary(x => x.level.id = x.i, x => x.level);
        }

        private void InitRelations()
        {
            var allLevels = _levels.Values.ToList();
            var activeLevels = _levels.Values.Skip(2).ToList();
            _relations = allLevels.ToDictionary(level => level.id, _ => new LevelRelationData());
            allLevels.ForEach(level => FillRelations(level, _relations[level.id], activeLevels));
        }

        private void FillRelations(LevelData level, LevelRelationData relation, List<LevelData> levels)
        {
            if (level.Left.enabled && level.IsLeftUnique)
                relation.Left = level.Left.customNeighbourIds;
            if (level.Right.enabled && level.IsRightUnique)
                relation.Right = level.Right.customNeighbourIds;
            if (level.Top.enabled && level.IsTopUnique)
                relation.Top = level.Top.customNeighbourIds;
            if (level.Bottom.enabled && level.IsBottomUnique)
                relation.Bottom = level.Bottom.customNeighbourIds;
            
            levels.ForEach(_level =>
            {
                if (level.Left.enabled == _level.Right.enabled && !level.IsLeftUnique && !_level.IsRightUnique)
                    relation.Left.Add(_level.id);
                if (level.Right.enabled == _level.Left.enabled && !level.IsRightUnique && !_level.IsLeftUnique)
                    relation.Right.Add(_level.id);
                if (level.Top.enabled == _level.Bottom.enabled && !level.IsTopUnique && !_level.IsBottomUnique)
                    relation.Top.Add(_level.id);
                if (level.Bottom.enabled == _level.Top.enabled && !level.IsBottomUnique && !_level.IsTopUnique)
                    relation.Bottom.Add(_level.id);
            });
        }
        
        public LevelData GetLevelData(int id)
        {
            return _levels[id];
        }
        
        public LevelRelationData GetLevelRelations(int id)
        {
            return _relations[id];
        }

        public List<int> FindLevelsWithMatchingNeighbors(Dictionary<LevelSide, LevelBase> sides, bool excludeBottoms = false) // todo, I have to optimize this monstrosity for sure
        {
            var neighbourIds = sides
                .Where(side => side.Value)
                .Select(side => GetLevelRelations(side.Value.staticData.id).GetNeighbors(OppositeSide.Get(side.Key)).AsEnumerable());
            if (excludeBottoms)
            {
                neighbourIds = neighbourIds.Select(list => list.Where(id => !GetLevelData(id).Bottom.enabled));
            }
            var matchingIds = neighbourIds.First();
            if (neighbourIds.Count() == 1) return matchingIds.ToList();
            return neighbourIds.Skip(1).Aggregate(matchingIds, (current, ids) => current.Intersect(ids)).ToList();
        }

        public LevelData GetEmptyLevel()
        {
            return GetLevelData(1);
        }
    }
}