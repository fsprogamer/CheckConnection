using CheckConnection.Model;
using CheckConnectionWpf.Methods;
using System.Collections.Generic;
using System.Linq;

namespace CheckConnectionWpf.Data
{
    public class CompareConnection
    {
        public string Name { get; set; }
        public string Active { get; set; }
        public string History { get; set; }        
        //Active!=History = false
        //Active==History = true
        public bool Equal { get; set; } = true;
        public static List<CompareConnection> GetDifference(Connection first, Connection second)
        {
            CompareConnection cmp = new CompareConnection();

            if (first == null && second == null) return null;

            var query = from active in ReflectionProperties<Connection>.GetPropertiesValueList(first)
                        join history in ReflectionProperties<Connection>.GetPropertiesValueList(second)
                        on active.Name equals history.Name
                        where active.Name != "Index" 
                        select new CompareConnection() { Name = active.Name, Active = active.Value, History = history.Value, Equal = active.Value==history.Value };

            List<CompareConnection> compareConnections = query.ToList<CompareConnection>();
            return compareConnections;
        }
    }
}
