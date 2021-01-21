using Microsoft.AspNetCore.WebUtilities;
using patient_api.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPatientUri(string PatientId)
        {
            return new Uri(_baseUri + $"/api/patient/GetPatient?Id={PatientId}");
        }
        
        public Uri GetPatientsPagedUri(PaginationQuery pagination)
        {
            var uri = new Uri(_baseUri + $"/api/patient/GetPatients");
            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(uri.ToString(), "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetPatientAddressUri(string AddressId)
        {
            return new Uri(_baseUri + $"/api/address/GetAddress?Id={AddressId}");
        }

        public Uri GetPatientAddressPagedUri(string PatientId, PaginationQuery pagination)
        {
            var uri = new Uri(_baseUri + $"/api/address/GetPatientAddresses?PatientId={PatientId}");
            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(uri.ToString(), "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
