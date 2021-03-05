using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace ResultZ.Reasons
{
    public sealed record ReasonCollection
        : IReadOnlyList<Reason>
    {
        public ReasonCollection(params Reason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        public ReasonCollection(IEnumerable<Reason> reasons)
        {
            Reasons = reasons.ToImmutableList();
        }

        public int Count => Reasons.Count;

        public Reason this[int index] => Reasons[index];

        private ImmutableList<Reason> Reasons { get; } = ImmutableList<Reason>.Empty;

        public IEnumerator<Reason> GetEnumerator() => Reasons.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Reasons.GetEnumerator();

        public override int GetHashCode()
        {
            var hashCode = default(HashCode);
            foreach (var reason in Reasons)
            {
                hashCode.Add(reason);
            }

            return hashCode.ToHashCode();
        }

        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append(nameof(Reasons));
            builder.Append(" = [ ");
            builder.Append(string.Join(", ", Reasons));
            builder.Append(" ]");

            return true;
        }

        public bool Equals(ReasonCollection? other)
        {
            return other is not null
                   && EqualityContract == other.EqualityContract
                   && Reasons.SequenceEqual(other.Reasons);
        }
    }
}
