using System.Collections.Generic;
using UnityEngine;

namespace Generators.Resource
{
    [CreateAssetMenu(menuName = "Resource/ResourceManager")]
    public class ResourceManager : ScriptableObject
    {
        [SerializeField] private List<ResourceData> resources;
        
        private Dictionary<uint, ResourceData> _resourceMap;

        public void Init()
        {
            _resourceMap = new Dictionary<uint, ResourceData>();
            resources.ForEach(resource =>
            {
                resource.staticID = (uint)_resourceMap.Count + 1;
                _resourceMap.Add(resource.staticID, resource);
            });
        }
        
        public ResourceData GetResourceById(uint id)
        {
            return _resourceMap[id];
        }
    }
}