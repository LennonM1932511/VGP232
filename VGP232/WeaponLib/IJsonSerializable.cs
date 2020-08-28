using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponLib
{
    public interface IJsonSerializable
    {
        bool LoadJSON(string path);
        bool SaveAsJSON(string path);
    }
}