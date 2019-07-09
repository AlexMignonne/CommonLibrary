using System.Collections.Generic;

namespace CommonLibrary.SeedOfWork
{
    public sealed class PaginationModel<T> where T : class
    {
        public PaginationModel(
            IReadOnlyCollection<T> payload,
            int amountItem,
            long totalItem,
            long timeStamp,
            long totalPage)
        {
            Payload = payload;
            AmountItem = amountItem;
            TotalItem = totalItem;
            TimeStamp = timeStamp;
            TotalPage = totalPage;
        }

        public IReadOnlyCollection<T> Payload { get; }
        public int AmountItem { get; }
        public long TotalItem { get; }
        public long TimeStamp { get; }
        public long TotalPage { get; }
    }
}
