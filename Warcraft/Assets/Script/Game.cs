using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Script
{
    [Serializable()]
    class Game
    {
        List<Pawn> pawns;

        [XmlElement("Pawns")]
        public List<Pawn> Pawns
        {
            get
            {
                return pawns;
            }

            set
            {
                pawns = value;
            }
        }
    }
}
