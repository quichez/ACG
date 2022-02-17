using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACG 
{
    namespace Resources
    {
        public abstract class Resource
        {
            public abstract string Name { get; }
            public abstract string Description { get; }
            public int Amount { get; protected set; }
            public int EffectiveAmount { get; private set; }

            public virtual void ChangeResourceAmount(int amt)
            {
                Amount += amt;
            }

            protected Resource() { Amount = 1; EffectiveAmount = Amount; }
            protected Resource(int amt = 1)
            {
                Amount = amt;
                EffectiveAmount = Amount;
            }

            //public abstract int GetEffectiveAmount();

            public void SetEffectiveAmount(int amt) => EffectiveAmount = amt;

            public override string ToString()
            {
                if (Amount == EffectiveAmount) return Name + "     " + Amount.ToString();
                else return Name + "     " + Amount.ToString() + " (" + EffectiveAmount.ToString() + ")";
            }

        }

        public interface IRenewableResource
        {
            int RenewalAmount { get; }

            void RenewResource();
            void RenewResourceByAmount(int amt);
            void ChangeRenewalAmountByAmount(int amt);        
        }

        public interface IExpenseResource
        {
            int Cost { get; }
            Type ResourceRequired { get; }
            //void PayForResource();
            void SetCostToAmount(int amt);

            void ChangeCostByAmount(int amt);     
            

        }

        public interface IFoodResource
        {

        }

        public interface IIndustryResource
        {

        }

        public static class ResourceFactory
        {
            private static Dictionary<string, Type> _resourceByName;
            private static bool IsInitialized => _resourceByName != null;

            private static void InitializeFactory()
            {
                if (IsInitialized) return;

                var resourceTypes = Assembly.GetAssembly(typeof(Resource)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Resource)));

                _resourceByName = new Dictionary<string, Type>();

                foreach (var type in resourceTypes)
                {
                    if (Activator.CreateInstance(type) is Resource temp) _resourceByName.Add(temp.Name, type);
                }
            }

            public static Resource GetResource(string resourceType)
            {
                InitializeFactory();
                if (!_resourceByName.ContainsKey(resourceType)) throw new ArgumentException("The resource " + resourceType + " has not been created!");

                var type = _resourceByName[resourceType];
                var res = Activator.CreateInstance(type) as Resource;
                return res;
            }

            public static List<Resource> GetResourceTypes()
            {
                InitializeFactory();
                List<Resource> temp = new List<Resource>(_resourceByName.Count);
                foreach (var item in _resourceByName.Values)
                {
                    temp.Add(Activator.CreateInstance(item) as Resource);                    
                }
                return temp;
            }

            public static IEnumerable<string> GetResourceNames()
            {
                InitializeFactory();
                return _resourceByName.Keys;
            }
        }

        public static class ResourceHelper
        {
            public static bool GetResource(this List<Resource> resources, Resource target, out Resource result)
            {
                result = resources.Find(x => x.GetType() == target.GetType());
                if (result is null) return false; else return true;
            }

            public static bool GetResource(this List<Resource> resources, Type target, out Resource result)
            {
                result = resources.Find(x => x.GetType() == target);
                if (result is null) return false; else return true;
            }

            public static int GetResourceAmount(this List<Resource> resources, Type target)
            {
                var result = resources.Find(x => x.GetType() == target);
                if (result is null) return 0; else return result.EffectiveAmount;
            }

            public static void AddToResourceList(this List<Resource> resources, Resource newResource)
            {
                if(resources.GetResource(newResource, out Resource res))
                {
                    res.ChangeResourceAmount(newResource.Amount);
                }
                else
                {
                    resources.Add(newResource);
                }
            }

            public static void AddRangeToResourceList(this List<Resource> resources, List<Resource> newResources)
            {
                foreach (var item in newResources)
                {
                    resources.AddToResourceList(item);
                }
            }

            public static void RemoveFromResourceList(this List<Resource> resources, Resource tgtResource)
            {
                if (resources.GetResource(tgtResource, out Resource res))
                {
                    if(res.Amount - tgtResource.Amount == 0)
                    {
                        resources.Remove(resources.Find(rs => rs.GetType() == res.GetType()));
                    }
                    else if(res.Amount - tgtResource.Amount < 0)
                    {
                        throw new Exception("You shouldn't have had as many resources as this!");
                    }
                    else
                    {
                        res.ChangeResourceAmount(-tgtResource.Amount);
                    }
                }
                else
                {
                    throw new Exception("This resource didn't exist!");
                }
            }

            public static void RemoveRangeFromResourceList(this List<Resource> resources, List<Resource> tgtResources)
            {
                foreach (var item in tgtResources)
                {
                    resources.RemoveFromResourceList(item);
                }
            }


        }
    }

    namespace Inspectors
    {
        public abstract class Inspector : MonoBehaviour
        {
            public abstract void FillPanels(IInspectable inspectable);
            public abstract void ClearPanels();
        }

        public abstract class Inspector2 : MonoBehaviour
        {
            public abstract void FillPanels(Unit unit);
            public abstract void ClearPanels();
        }

        public interface IInspectable
        {

        }
    }

    namespace Buildings
    {        
        [Serializable]
        public abstract class Building
        {

            public abstract string Name { get; }
            public abstract string Description { get; }
            public abstract Type SettlementType { get; }
            public abstract int purchaseCost { get; }
            public abstract int industryCost { get; }
            public bool Unlocked { get; protected set; } = false;
            public bool InProcessOfUnlock { get; protected set; }
            public abstract void Unlock(int amt);
            public void StartProcess()
            {
                InProcessOfUnlock = true;
            }

            internal void StopProcess()
            {
                InProcessOfUnlock = true;
                Unlocked = true;
            }
        }

        public static class BuildingFactory
        {
            private static Dictionary<string, Type> _buildingByName;
            private static bool IsInitialized => _buildingByName != null;

            private static void InitializeFactory()
            {
                if (IsInitialized) return;

                var buildingType = Assembly.GetAssembly(typeof(Building)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Building)));

                _buildingByName = new Dictionary<string, Type>();

                foreach (var type in buildingType)
                {
                    if (Activator.CreateInstance(type) is Building temp) _buildingByName.Add(temp.Name, type);
                }
            }

            public static Building GetBuilding(string buildingName)
            {
                InitializeFactory();
                if (!_buildingByName.ContainsKey(buildingName)) throw new ArgumentException("The building " + buildingName + " has not been created!");

                var type = _buildingByName[buildingName];
                var res = Activator.CreateInstance(type) as Building;
                return res;
            }

            public static List<Building> GetBuildingTypes(Type buildingType)
            {
                InitializeFactory();
                List<Building> temp = new List<Building>(_buildingByName.Count);
                foreach (var item in _buildingByName.Values)
                {
                    var building = Activator.CreateInstance(item) as Building;
                    if(building.SettlementType == buildingType) temp.Add(building);
                }
                return temp;
            }

            public static IEnumerable<string> GetBuildingNames()
            {
                InitializeFactory();
                return _buildingByName.Keys;
            }
        }
    }

    namespace CellGeneration
    {
        public static class CellGenerator
        {
            public static int[,] GenerateLand(Vector2Int dim, ICollection cells)
            {
                float random = UnityEngine.Random.Range(0.0f, 99999f);
                int[,] islandMap = new int[dim.x, dim.y] ;
                for (int i = 0; i < dim.y; i++)
                {
                    for (int j = 0; j < dim.x; j++)
                    {
                        islandMap[j, i] = Mathf.RoundToInt(Mathf.Clamp(Mathf.PerlinNoise((float)j / dim.x * 3.0f + random, (float)i / dim.y * 3.0f + random), 0.0f, 1.0f));
                    }                    
                }
                return islandMap;
            }

            public static void GenerateMountains(this GridManager gm, Cell[] cells, MountainCell mountain)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    float first = UnityEngine.Random.value;
                    if (first < 0.025f && cells[i].TryGetComponent(out FieldCell _))
                    {
                        UnityEngine.Object.Destroy(cells[i]);
                        cells[i] = UnityEngine.Object.Instantiate(mountain,cells[i].transform.position, cells[i].transform.rotation,gm.transform);
                    }
                }
            }

        }
    }
}
