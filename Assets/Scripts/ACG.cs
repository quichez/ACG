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
            public virtual void ChangeResourceAmount(int amt)
            {
                Amount += amt;
            }

            protected Resource() { Amount = 1; }
            protected Resource(int amt = 1)
            {
                Amount = amt;
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
}
