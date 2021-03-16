using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Cranks.Result
{
    public sealed record ReasonCollection
        : IReadOnlyList<IReason>
    {
        public ReasonCollection(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        public ReasonCollection(IEnumerable<IReason> reasons)
        {
            Reasons = reasons.ToImmutableList();
        }

        public int Count => Reasons.Count;

        private ImmutableList<IReason> Reasons { get; } = ImmutableList<IReason>.Empty;

        public IReason this[int index] => Reasons[index];

        public IEnumerator<IReason> GetEnumerator() => Reasons.GetEnumerator();

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
