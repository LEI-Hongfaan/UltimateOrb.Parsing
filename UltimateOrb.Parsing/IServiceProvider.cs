using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateOrb {

    public interface IServiceProvider {

        T GetService<T, TService>();
    }
}
