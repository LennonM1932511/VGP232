using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponLib
{
    public interface IXmlSerializable
    {
        bool LoadXML(string path);
        bool SaveAsXML(string path);
    }
}