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
    }

    namespace Inspectors
    {
        public abstract class Inspector : MonoBehaviour
        {
            public abstract void Fill(IInspectable inspectable);
            public abstract void Clear();
        }

        public interface IInspectable
        {

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
