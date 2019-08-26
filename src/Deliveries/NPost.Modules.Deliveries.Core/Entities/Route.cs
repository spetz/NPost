using System;
using System.Collections.Generic;

namespace NPost.Modules.Deliveries.Core.Entities
{
    internal class Route : IEquatable<Route>
    {
        public IEnumerable<string> Addresses { get; private set; }
        public double Distance { get; private set; }

        public Route(IEnumerable<string> addresses, double distance)
        {
            Addresses = addresses;
            Distance = distance;
        }

        public bool Equals(Route other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Addresses, other.Addresses) && Distance.Equals(other.Distance);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Route) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Addresses != null ? Addresses.GetHashCode() : 0) * 397) ^ Distance.GetHashCode();
            }
        }
    }
}