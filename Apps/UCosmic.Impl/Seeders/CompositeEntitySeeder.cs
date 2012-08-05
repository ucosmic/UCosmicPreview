namespace UCosmic.Impl.Seeders
{
    public class CompositeEntitySeeder : BaseDataSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CoreSqlSeeder _coreSqlSeeder;
        //private readonly LanguageSqlSeeder _languageSqlSeeder;
        //private readonly PlaceByGeoPlanetEntitySeeder _placeByGeoPlanetEntitySeeder;
        private readonly RoleEntitySeeder _roleEntitySeeder;
        private readonly EstablishmentEntitySeeder _establishmentEntitySeeder;
        private readonly EmailTemplateEntitySeeder _emailTemplateEntitySeeder;
        private readonly PersonEntitySeeder _personEntitySeeder;
        private readonly UserEntitySeeder _userEntitySeeder;
        private readonly MemberEntitySeeder _memberEntitySeeder;
        private readonly InstitutionalAgreementEntitySeeder _institutionalAgreementEntitySeeder;
        private readonly InstitutionalAgreementSettingsEntitySeeder _institutionalAgreementSettingsEntitySeeder;

        public CompositeEntitySeeder(IUnitOfWork unitOfWork
            , CoreSqlSeeder coreSqlSeeder
            //, LanguageSqlSeeder languageSqlSeeder
            //, PlaceByGeoPlanetEntitySeeder placeByGeoPlanetEntitySeeder
            , RoleEntitySeeder roleEntitySeeder
            , EstablishmentEntitySeeder establishmentEntitySeeder
            , EmailTemplateEntitySeeder emailTemplateEntitySeeder
            , PersonEntitySeeder personEntitySeeder
            , UserEntitySeeder userEntitySeeder
            , MemberEntitySeeder memberEntitySeeder
            , InstitutionalAgreementEntitySeeder institutionalAgreementEntitySeeder
            , InstitutionalAgreementSettingsEntitySeeder institutionalAgreementSettingsEntitySeeder
        )
        {
            _unitOfWork = unitOfWork;
            _coreSqlSeeder = coreSqlSeeder;
            //_languageSqlSeeder = languageSqlSeeder;
            //_placeByGeoPlanetEntitySeeder = placeByGeoPlanetEntitySeeder;
            _roleEntitySeeder = roleEntitySeeder;
            _establishmentEntitySeeder = establishmentEntitySeeder;
            _emailTemplateEntitySeeder = emailTemplateEntitySeeder;
            _personEntitySeeder = personEntitySeeder;
            _userEntitySeeder = userEntitySeeder;
            _memberEntitySeeder = memberEntitySeeder;
            _institutionalAgreementEntitySeeder = institutionalAgreementEntitySeeder;
            _institutionalAgreementSettingsEntitySeeder = institutionalAgreementSettingsEntitySeeder;
        }

        public override void Seed()
        {
            //_languageSqlSeeder.Seed();
            //_placeByGeoPlanetEntitySeeder.Seed();
            _coreSqlSeeder.Seed();

            _roleEntitySeeder.Seed();
            _establishmentEntitySeeder.Seed();
            _emailTemplateEntitySeeder.Seed();
            _personEntitySeeder.Seed();
            _userEntitySeeder.Seed();
            _memberEntitySeeder.Seed();
            _unitOfWork.SaveChanges();
            _institutionalAgreementEntitySeeder.Seed();
            _institutionalAgreementSettingsEntitySeeder.Seed();

            _unitOfWork.SaveChanges();
        }
    }
}
