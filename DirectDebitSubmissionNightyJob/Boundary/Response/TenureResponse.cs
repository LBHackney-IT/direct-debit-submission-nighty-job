using System;
using System.Collections.Generic;
using System.Text;

namespace DirectDebitSubmissionNightyJob.Boundary.Response
{
    public class TenureResponse
    {
        public string Id { get; set; }
        public string PaymentReference { get; set; }
        public List<HouseholdMember> HouseholdMembers { get; set; }
        public TenuredAsset TenuredAsset { get; set; }
        public Charges Charges { get; set; }
        public DateTime StartOfTenureDate { get; set; }
        public DateTime EndOfTenureDate { get; set; }
        public TenureType TenureType { get; set; }
        public bool IsActive { get; set; }
        public bool IsTenanted { get; set; }
        public Terminated Terminated { get; set; }
        public DateTime SuccessionDate { get; set; }
        public DateTime EvictionDate { get; set; }
        public DateTime PotentialEndDate { get; set; }
        public bool IsMutualExchange { get; set; }
        public bool InformHousingBenefitsForChanges { get; set; }
        public bool IsSublet { get; set; }
        public DateTime SubletEndDate { get; set; }
        public List<Notice> Notices { get; set; }
        public List<LegacyReference> LegacyReferences { get; set; }
        public AgreementType AgreementType { get; set; }
    }

    public class HouseholdMember
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string FullName { get; set; }
        public bool IsResponsible { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonTenureType { get; set; }
    }

    public class TenuredAsset
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string FullAddress { get; set; }
        public string Uprn { get; set; }
        public string PropertyReference { get; set; }
    }

    public class Charges
    {
        public int Rent { get; set; }
        public int CurrentBalance { get; set; }
        public string BillingFrequency { get; set; }
        public int ServiceCharge { get; set; }
        public int OtherCharges { get; set; }
        public int CombinedServiceCharges { get; set; }
        public int CombinedRentCharges { get; set; }
        public int TenancyInsuranceCharge { get; set; }
        public int OriginalRentCharge { get; set; }
        public int OriginalServiceCharge { get; set; }
    }

    public class TenureType
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class Terminated
    {
        public bool IsTerminated { get; set; }
        public string ReasonForTermination { get; set; }
    }

    public class Notice
    {
        public string Type { get; set; }
        public DateTime ServedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class LegacyReference
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class AgreementType
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
