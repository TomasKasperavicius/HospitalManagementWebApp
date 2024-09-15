using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebApp.Models
{
    public class Doctor : IUser
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Image { get; set; }
        public Role Role { get; set; }
        public int AddressID { get; set; }
        public Specialty Specialty { get; set; }
    }
    public enum Specialty
    {
        [Description("Family Medicine")]
        FamilyMedicine,

        [Description("Internal Medicine")]
        InternalMedicine,

        [Description("Pediatrics")]
        Pediatrics,

        [Description("Allergy and Immunology")]
        AllergyAndImmunology,

        [Description("Anesthesiology")]
        Anesthesiology,

        [Description("Dermatology")]
        Dermatology,

        [Description("Emergency Medicine")]
        EmergencyMedicine,

        [Description("Psychiatry")]
        Psychiatry,

        [Description("Neurology")]
        Neurology,

        [Description("Radiology")]
        Radiology,

        [Description("Pathology")]
        Pathology,

        [Description("Physical Medicine and Rehabilitation")]
        PhysicalMedicineAndRehabilitation,

        [Description("General Surgery")]
        GeneralSurgery,

        [Description("Orthopedic Surgery")]
        OrthopedicSurgery,

        [Description("Otolaryngology")]
        Otolaryngology,

        [Description("Cardiothoracic Surgery")]
        CardiothoracicSurgery,

        [Description("Plastic Surgery")]
        PlasticSurgery,

        [Description("Urology")]
        Urology,

        [Description("Gastroenterology")]
        Gastroenterology,

        [Description("Endocrinology")]
        Endocrinology,

        [Description("Hematology")]
        Hematology,

        [Description("Infectious Diseases")]
        InfectiousDiseases,

        [Description("Nephrology")]
        Nephrology,

        [Description("Pulmonology")]
        Pulmonology,

        [Description("Rheumatology")]
        Rheumatology,

        [Description("Sports Medicine")]
        SportsMedicine,

        [Description("Medical Genetics")]
        MedicalGenetics,

        [Description("Occupational Medicine")]
        OccupationalMedicine,

        [Description("Preventive Medicine")]
        PreventiveMedicine,

        [Description("Geriatrics")]
        Geriatrics,

        [Description("Hospital Medicine")]
        HospitalMedicine,

        [Description("Addiction Medicine")]
        AddictionMedicine,

        [Description("Pain Management")]
        PainManagement,

        [Description("Vascular Surgery")]
        VascularSurgery,

        [Description("Thoracic Surgery")]
        ThoracicSurgery,

        [Description("Dermatopathology")]
        Dermatopathology,

        [Description("Forensic Pathology")]
        ForensicPathology,

        [Description("Interventional Radiology")]
        InterventionalRadiology,

        [Description("Pediatric Surgery")]
        PediatricSurgery,

        [Description("Colon and Rectal Surgery")]
        ColonAndRectalSurgery,

        [Description("Transplant Surgery")]
        TransplantSurgery,

        [Description("Reproductive Endocrinology")]
        ReproductiveEndocrinology,

        [Description("Maternal-Fetal Medicine")]
        MaternalFetalMedicine,

        [Description("Child and Adolescent Psychiatry")]
        ChildAdolescentPsychiatry,

        [Description("Neuropsychiatry")]
        Neuropsychiatry,

        [Description("Sleep Medicine")]
        SleepMedicine,

        [Description("Allergy")]
        Allergy,

        [Description("Occupational Therapy")]
        OccupationalTherapy,

        [Description("Speech Pathology")]
        SpeechPathology
    }
}
