using System.Collections.Generic;

namespace patient_api.Data.dto
{
    public class AddressPagedResponse
    {
        public AddressPagedResponse() { }
 
        public IEnumerable<Address_dto> Data { get; set; }

        public int? TotalRecords { get; set; } = 0;

        public int? TotalPages { get; set; } = 0;

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public string NextPage { get; set; }

        public string PreviousPage { get; set; }

    }
}
