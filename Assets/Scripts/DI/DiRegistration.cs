using System;

namespace DI
{
    public class DiRegistration
    {
        public Func<MyDIContainer, object> Factory { get; set; }
        
        public bool IsSingleton { get; set; }

        public object Instance { get; set;}
    }
}