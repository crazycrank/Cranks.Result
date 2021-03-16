using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Cranks.Result
{
    /// <summary>
    /// An immutable collection of <see cref="IReason"/>s.
    /// </summary>
    public sealed record ReasonCollection
        : IReadOnlyList<IReason>
    {
        private readonly ImmutableList<IReason> _reasons = ImmutableList<IReason>.Empty;

        internal ReasonCollection(params IReason[] reasons)
            : this(reasons.AsEnumerable())
        {
        }

        internal ReasonCollection(IEnumerable<IReason> reasons)
        {
            _reasons = reasons.ToImmutableList();
        }

        /// <inheritdoc />
        public int Count => _reasons.Count;

        /// <inheritdoc />
        public IReason this[int index] => _reasons[index];

        /// <inheritdoc />
        public IEnumerator<IReason> GetEnumerator() => _reasons.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _reasons.GetEnumerator();

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = default(HashCode);
            foreach (var reason in _reasons)
            {
                hashCode.Add(reason);
            }

            return hashCode.ToHashCode();
        }

        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append(nameof(_reasons));
            builder.Append(" = [ ");
            builder.Append(string.Join(", ", _reasons));
            builder.Append(" ]");

            return true;
        }

        public bool Equals(ReasonCollection? other)
        {
            return other is not null
                   && EqualityContract == other.EqualityContract
                   && _reasons.SequenceEqual(other._reasons);
        }
    }
}
