using System.ComponentModel.DataAnnotations;

namespace HospitalManagementWebApp.Models
{
    public class Doctor
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
        Role Role { get; set; }
        public List<WorkSchedule> WorkSchedule { get; set; }
        public int AddressID { get; set; }
        public Specialty Specialty { get; set; }
    }
    public enum Specialty
    {
        FamilyMedicine,
        InternalMedicine,
        Pediatrics,
        AllergyAndImmunology,
        Anesthesiology,
        Dermatology,
        EmergencyMedicine,
        Psychiatry,
        Neurology,
        Radiology,
        Pathology,
        PhysicalMedicineAndRehabilitation,
        GeneralSurgery,
        OrthopedicSurgery,
        Otolaryngology,
        CardiothoracicSurgery,
        PlasticSurgery,
        Urology,
        Gastroenterology,
        Endocrinology,
        Hematology,
        InfectiousDiseases,
        Nephrology,
        Pulmonology,
        Rheumatology,
        SportsMedicine,
        MedicalGenetics,
        OccupationalMedicine,
        PreventiveMedicine,
        Geriatrics,
        HospitalMedicine,
        AddictionMedicine,
        PainManagement,
        VascularSurgery,
        ThoracicSurgery,
        Dermatopathology,
        ForensicPathology,
        InterventionalRadiology,
        PediatricSurgery,
        ColonAndRectalSurgery,
        TransplantSurgery,
        ReproductiveEndocrinology,
        MaternalFetalMedicine,
        ChildAdolescentPsychiatry,
        Neuropsychiatry,
        SleepMedicine,
        Allergy,
        OccupationalTherapy,
        SpeechPathology
    }
}
