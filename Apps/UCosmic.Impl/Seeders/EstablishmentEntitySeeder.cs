using System.Collections.Generic;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Languages;
using UCosmic.Domain.Places;

namespace UCosmic.Impl.Seeders
{
    public class EstablishmentEntitySeeder : BaseDataSeeder
    {
        private readonly EstablishmentTypeAndCategorySeeder _typeAndCategorySeeder;
        private readonly EstablishmentSunyEntitySeeder _sunySeeder;
        private readonly EstablishmentUcEntitySeeder _ucSeeder;
        private readonly EstablishmentUsfEntitySeeder _usfSeeder;
        private readonly EstablishmentUmnEntitySeeder _umnSeeder;
        private readonly EstablishmentFoundingMemberEntitySeeder _foundingMemberSeeder;
        private readonly EstablishmentSamplePartnerEntitySeeder _samplePartnerSeeder;
        private readonly EstablishmentTestShibEntitySeeder _testShibSeeder;
        private readonly EstablishmentUcShibEntitySeeder _ucShibSeeder;
        private readonly EstablishmentRecruitmentAgencyEntitySeeder _agencySeeder;

        public EstablishmentEntitySeeder(EstablishmentTypeAndCategorySeeder typeAndCategorySeeder
            , EstablishmentSunyEntitySeeder sunySeeder
            , EstablishmentUcEntitySeeder ucSeeder
            , EstablishmentUmnEntitySeeder umnSeeder
            , EstablishmentUsfEntitySeeder usfSeeder
            , EstablishmentFoundingMemberEntitySeeder foundingMemberSeeder
            , EstablishmentSamplePartnerEntitySeeder samplePartnerSeeder
            , EstablishmentTestShibEntitySeeder testShibSeeder
            , EstablishmentUcShibEntitySeeder ucShibSeeder
            , EstablishmentRecruitmentAgencyEntitySeeder agencySeeder
        )
        {
            _typeAndCategorySeeder = typeAndCategorySeeder;
            _sunySeeder = sunySeeder;
            _ucSeeder = ucSeeder;
            _umnSeeder = umnSeeder;
            _usfSeeder = usfSeeder;
            _foundingMemberSeeder = foundingMemberSeeder;
            _samplePartnerSeeder = samplePartnerSeeder;
            _testShibSeeder = testShibSeeder;
            _ucShibSeeder = ucShibSeeder;
            _agencySeeder = agencySeeder;
        }

        public override void Seed()
        {
            _typeAndCategorySeeder.Seed();
            _sunySeeder.Seed();
            _ucSeeder.Seed();
            _umnSeeder.Seed();
            _usfSeeder.Seed();
            _foundingMemberSeeder.Seed();
            _samplePartnerSeeder.Seed();
            _testShibSeeder.Seed();
            //_ucShibSeeder.Seed();
            _agencySeeder.Seed();
        }
    }

    public class EstablishmentSunyEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public EstablishmentSunyEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            var suny = Seed(new CreateEstablishment
            {
                OfficialName = "State University of New York (SUNY)",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversitySystem.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.suny.edu",
                EmailDomains = new[] { "@suny.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Adirondack",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversitySystem.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyacc.edu",
                EmailDomains = new[] { "@sunyacc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "University at Albany (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.albany.edu",
                EmailDomains = new[] { "@albany.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Alfred State College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.alfredstate.edu",
                EmailDomains = new[] { "@alfredstate.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Alfred University (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.alfred.edu",
                EmailDomains = new[] { "@alfred.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Binghamtom University (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.binghamton.edu",
                EmailDomains = new[] { "@binghamton.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "The College at Brockport (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.brockport.edu",
                EmailDomains = new[] { "@brockport.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Broome Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunybroome.edu",
                EmailDomains = new[] { "@sunybroome.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "University at Buffalo (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.buffalo.edu",
                EmailDomains = new[] { "@buffalo.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Buffalo State College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.buffalostate.edu",
                EmailDomains = new[] { "@buffalostate.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Canton",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.canton.edu",
                EmailDomains = new[] { "@canton.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Cayuga Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cayuga-cc.edu",
                EmailDomains = new[] { "@cayuga-cc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Clinton Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.clinton.edu",
                EmailDomains = new[] { "@clinton.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Cobleskill",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cobleskill.edu",
                EmailDomains = new[] { "@cobleskill.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Columbia-Greene Community College",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunycgcc.edu",
                EmailDomains = new[] { "@sunycgcc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Cornell University College of Agriculture and Life Sciences",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cals.cornell.edu",
                EmailDomains = new[] { "@cals.cornell.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Cornell University College of Human Ecology",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.human.cornell.edu",
                EmailDomains = new[] { "@human.cornell.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Cornell University College of Veterinary Medicine",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.vet.cornell.edu",
                EmailDomains = new[] { "@vet.cornell.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Cornell University ILR School",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ilr.cornell.edu",
                EmailDomains = new[] { "@ilr.cornell.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Corning Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.corning-cc.edu",
                EmailDomains = new[] { "@corning-cc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Cortland",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cortland.edu",
                EmailDomains = new[] { "@cortland.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Delhi",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.delhi.edu",
                EmailDomains = new[] { "@delhi.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Downstate Medical Center",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.downstate.edu",
                EmailDomains = new[] { "@downstate.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Dutchess Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunydutchess.edu",
                EmailDomains = new[] { "@sunydutchess.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Empire State College",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.esc.edu",
                EmailDomains = new[] { "@esc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY College of Environmental Science and Forestry",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.esf.edu",
                EmailDomains = new[] { "@esf.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Erie Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ecc.edu",
                EmailDomains = new[] { "@ecc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Farmingdale State College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.farmingdale.edu",
                EmailDomains = new[] { "@farmingdale.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Fashion Institute of Technology (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.fitnyc.edu",
                EmailDomains = new[] { "@fitnyc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Finger Lakes Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.flcc.edu",
                EmailDomains = new[] { "@flcc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Fredonia",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.fredonia.edu",
                EmailDomains = new[] { "@fredonia.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Fulton-Montgomery Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "fmcc.suny.edu",
                EmailDomains = new[] { "@fmcc.suny.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Genesse Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.genesse.edu",
                EmailDomains = new[] { "@genesse.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Geneseo",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.geneseo.edu",
                EmailDomains = new[] { "@geneseo.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Herkimer County Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.herkimer.edu",
                EmailDomains = new[] { "@herkimer.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Hudson Valley Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.hvcc.edu",
                EmailDomains = new[] { "@hvcc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Jamestown Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyjcc.edu",
                EmailDomains = new[] { "@sunyjcc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Jefferson Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyjefferson.edu",
                EmailDomains = new[] { "@sunyjefferson.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Maritime College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunymaritime.edu",
                EmailDomains = new[] { "@sunymaritime.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Mohawk Valley Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.mvcc.edu",
                EmailDomains = new[] { "@mvcc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Monroe Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.monroecc.edu",
                EmailDomains = new[] { "@monroecc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Morrisville State College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.morrisville.edu",
                EmailDomains = new[] { "@morrisville.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Nassau Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ncc.edu",
                EmailDomains = new[] { "@ncc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "New Paltz (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.newpaltz.edu",
                EmailDomains = new[] { "@newpaltz.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Niagra County Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.niagaracc.suny.edu",
                EmailDomains = new[] { "@niagaracc.suny.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "North Country Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.nccc.edu",
                EmailDomains = new[] { "@nccc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "The College at Old Westbury (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.oldwestbury.edu",
                EmailDomains = new[] { "@oldwestbury.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY College at Oneonta",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.oneonta.edu",
                EmailDomains = new[] { "@oneonta.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Onondaga Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyocc.edu",
                EmailDomains = new[] { "@sunyocc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY College of Optometry",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyopt.edu",
                EmailDomains = new[] { "@sunyopt.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Orange",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyorange.edu",
                EmailDomains = new[] { "@sunyorange.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Oswego (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.oswego.edu",
                EmailDomains = new[] { "@oswego.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Plattsburgh",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.plattsburgh.edu",
                EmailDomains = new[] { "@plattsburgh.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Potsdam",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.potsdam.edu",
                EmailDomains = new[] { "@potsdam.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Purchase College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.purchase.edu",
                EmailDomains = new[] { "@purchase.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Rockland Community College",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyrockland.edu",
                EmailDomains = new[] { "@sunyrockland.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Schenectady County Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunysccc.edu",
                EmailDomains = new[] { "@sunysccc.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Stony Brook University (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.stonybrook.edu",
                EmailDomains = new[] { "@stonybrook.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Suffolk County Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunysuffolk.edu",
                EmailDomains = new[] { "@sunysuffolk.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Sullivan Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunysullivan.edu",
                EmailDomains = new[] { "@sunysullivan.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNYIT",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyit.edu",
                EmailDomains = new[] { "@sunyit.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Tompkins Cortland Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.tc3.edu",
                EmailDomains = new[] { "@tc3.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "SUNY Ulster",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunyulster.edu",
                EmailDomains = new[] { "@sunyulster.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Upstate Medical University (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.upstate.edu",
                EmailDomains = new[] { "@upstate.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Westchester Community College (SUNY)",
                IsMember = true,
                ParentId = suny.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.CommunityCollege.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sunywcc.edu",
                EmailDomains = new[] { "@sunywcc.edu" },
            });
        }
    }

    public class EstablishmentUcEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public EstablishmentUcEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            var uc = Seed(new CreateEstablishment
            {
                OfficialName = "University of Cincinnati",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uc.edu",
                EmailDomains = new[] { "@uc.edu", "@ucmail.uc.edu" },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 39.132084, Longitude = -84.516479, },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Allied Health Sciences, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cahs.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "McMicken College of Arts & Sciences, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.artsci.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Business, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.business.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College Conservatory of Music, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "ccm.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Design, Architecture, Art, and Planning, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.daap.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Education, Criminal Justice, and Human Services, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cech.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Engineering & Applied Sciences, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ceas.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Law, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.law.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Medicine, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.med.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Nursing, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "nursing.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "James L. Winkle College of Pharmacy, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "pharmacy.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "School of Social Work, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uc.edu/socialwork",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Raymond Walters College, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.rwc.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Clermont College, University of Cincinnati",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ucclermont.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "University of Cincinnati Graduate School",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.grad.uc.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "University of Cincinnati Honors Program",
                IsMember = true,
                ParentId = uc.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.AcademicProgram.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uc.edu/honors.html",
            });
        }
    }

    public class EstablishmentUmnEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public EstablishmentUmnEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            var umn = Seed(new CreateEstablishment
            {
                OfficialName = "University of Minnesota",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.umn.edu",
                EmailDomains = new[] { "@umn.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Center for Allied Health Programs, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cahp.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Biological Sciences, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cbs.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Continuing Education, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cce.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "School of Dentistry, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.dentistry.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Design, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.design.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Education & Human Development, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cehd.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "University of Minnesota Extension",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.extension.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Food, Agricultural and Natural Resource Sciences, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cfans.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "The Graduate School, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.grad.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "University of Minnesota Law School",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.law.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Liberal Arts, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cla.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Carlson School of Management, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.csom.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "University of Minnesota Medical School",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.med.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "School of Nursing, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.nursing.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Pharmacy, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.pharmacy.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Hubert H. Humphrey School of Public Affairs, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.hhh.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "School of Public Health, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sph.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Science & Engineering, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cse.umn.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "College of Veterinary Medicine, University of Minnesota",
                IsMember = true,
                ParentId = umn.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cvm.umn.edu",
            });
        }
    }

    public class EstablishmentUsfEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public EstablishmentUsfEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            Seed(new CreateEstablishment
            {
                OfficialName = "University of South Florida",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.usf.edu",
                EmailDomains = new[] { "@usf.edu", "@iac.usf.edu", "@mail.usf.edu" },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 28.061680, Longitude = -82.414803 },
            });
        }
    }

    public class EstablishmentTestShibEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<UpdateSamlSignOnInfoCommand> _updateSaml;
        private readonly IUnitOfWork _unitOfWork;

        public EstablishmentTestShibEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IHandleCommands<UpdateSamlSignOnInfoCommand> updateSaml
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
            _updateSaml = updateSaml;
            _unitOfWork = unitOfWork;
        }

        public override void Seed()
        {
            var testShib = Seed(new CreateEstablishment
            {
                OfficialName = "TestShib2",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.BusinessOrCorporation.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.testshib.org",
                EmailDomains = new[] { "@testshib.org" },
            });
            _updateSaml.Handle(new UpdateSamlSignOnInfoCommand
            {
                Establishment = testShib,
                EntityId = "https://idp.testshib.org/idp/shibboleth",
                MetadataUrl = "https://idp.testshib.org/idp/shibboleth",
            });
            _unitOfWork.SaveChanges();
        }
    }

    public class EstablishmentUcShibEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<UpdateSamlSignOnInfoCommand> _updateSaml;
        private readonly IUnitOfWork _unitOfWork;

        public EstablishmentUcShibEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IHandleCommands<UpdateSamlSignOnInfoCommand> updateSaml
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
            _updateSaml = updateSaml;
            _unitOfWork = unitOfWork;
        }

        public override void Seed()
        {
            var uc = _queryProcessor.Execute(new GetEstablishmentByUrlQuery("www.uc.edu"));
            _updateSaml.Handle(new UpdateSamlSignOnInfoCommand
            {
                Establishment = uc,
                EntityId = "https://qalogin.uc.edu/idp/shibboleth",
                MetadataUrl = "https://qalogin.uc.edu/idp/profile/Metadata/SAML",
            });
            _unitOfWork.SaveChanges();
        }
    }

    public class EstablishmentFoundingMemberEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public EstablishmentFoundingMemberEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            #region Lehigh

            var lehigh = Seed(new CreateEstablishment
            {
                OfficialName = "Lehigh University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.lehigh.edu",
                EmailDomains = new[] { "@lehigh.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Lehigh University College of Arts and Sciences",
                IsMember = true,
                ParentId = lehigh.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "cas.lehigh.edu",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Lehigh University College of Business and Economics",
                IsMember = true,
                ParentId = lehigh.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.lehigh.edu/business",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Lehigh University College of Education",
                IsMember = true,
                ParentId = lehigh.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.lehigh.edu/education",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Lehigh University P.C. Rossin College of Engineering and Applied Science",
                IsMember = true,
                ParentId = lehigh.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.lehigh.edu/engineering",
            });

            #endregion
            #region Manipal

            var manipalGlobal = Seed(new CreateEstablishment
            {
                OfficialName = "Manipal Education",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversitySystem.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.manipalglobal.com",
            });
            var manipalEdu = Seed(new CreateEstablishment
            {
                OfficialName = "Manipal University",
                IsMember = true,
                ParentId = manipalGlobal.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.manipal.edu",
                EmailDomains = new[] { "@manipal.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Melaka Manipal Medical College",
                IsMember = true,
                ParentId = manipalEdu.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.manipal.edu/Institutions/Medicine/MMMCMelaka",
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "ICICI Manipal Academy",
                IsMember = true,
                ParentId = manipalEdu.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ima.manipal.edu",
                EmailDomains = new[] { "@ima.manipal.edu" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "American University of Antigua",
                IsMember = true,
                ParentId = manipalGlobal.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.auamed.org",
                EmailDomains = new[] { "@auamed.org" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Manipal University Dubai Campus",
                IsMember = true,
                ParentId = manipalGlobal.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.manipaldubai.com",
                EmailDomains = new[] { "@manipaldubai.com" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Manipal College of Medical Sciences, Nepal",
                IsMember = true,
                ParentId = manipalGlobal.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.manipal.edu.np",
                EmailDomains = new[] { "@manipal.edu.np" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Sikkim Manipal University",
                IsMember = true,
                ParentId = manipalGlobal.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.smude.edu.in",
                EmailDomains = new[] { "@smude.edu.in" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Manipal International University",
                IsMember = true,
                ParentId = manipalGlobal.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.miu.edu.my",
                EmailDomains = new[] { "@miu.edu.my" },
            });

            #endregion
            #region Usil

            var usil = Seed(new CreateEstablishment
            {
                OfficialName = "Universidad San Ignacio de Loyola",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.usil.edu.pe",
                EmailDomains = new[] { "@usil.edu.pe" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad San Ignacio de Loyola Facultad de Ciencias Empresariales",
                IsMember = true,
                ParentId = usil.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad San Ignacio de Loyola Facultad de Educación",
                IsMember = true,
                ParentId = usil.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad San Ignacio de Loyola Facultad de Administración Hotelera, Turismo y Gastronomía",
                IsMember = true,
                ParentId = usil.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad San Ignacio de Loyola Facultad de  Humanidades",
                IsMember = true,
                ParentId = usil.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad San Ignacio de Loyola Ingeniería y Arquitectura",
                IsMember = true,
                ParentId = usil.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.College.AsSentenceFragment())).RevisionId,
            });

            #endregion
            #region Griffith

            var griffith = Seed(new CreateEstablishment
            {
                OfficialName = "Griffith University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.griffith.edu.au",
                EmailDomains = new[] { "@griffith.edu.au" },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Griffith University Gold Coast Campus",
                IsMember = true,
                ParentId = griffith.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversityCampus.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Griffith University Logan Campus",
                IsMember = true,
                ParentId = griffith.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversityCampus.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Griffith University Mt Gravatt Campus",
                IsMember = true,
                ParentId = griffith.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversityCampus.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Griffith University Nathan Campus",
                IsMember = true,
                ParentId = griffith.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversityCampus.AsSentenceFragment())).RevisionId,
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Griffith University South Bank Campus",
                IsMember = true,
                ParentId = griffith.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.UniversityCampus.AsSentenceFragment())).RevisionId,
            });

            #endregion
            #region Singles (Bjtu, Edinburgh, etc)

            Seed(new CreateEstablishment
            {
                OfficialName = "Beijing Jiaotong University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.bjtu.edu.cn",
                EmailDomains = new[] { "@bjtu.edu.cn", "@njtu.edu.cn" },
                NonOfficialUrls = new[]
                {
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "www.njtu.edu.cn",
                        IsDefunct = true,
                    }
                },
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Northern Jiaotong University",
                        IsDefunct = true,
                        TranslationToLanguageId = _queryProcessor.Execute(
                            new LanguageByIsoCode { IsoCode = "en" }).RevisionId,
                    }
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Edinburgh Napier University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.napier.ac.uk",
                EmailDomains = new[] { "@napier.ac.uk" },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Future University in Egypt",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.fue.edu.eg",
                EmailDomains = new[] { "@fue.edu.eg" },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "The University of New South Wales",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.unsw.edu.au",
                EmailDomains = new[] { "@unsw.edu.au" },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "The College Board",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.BusinessOrCorporation.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.collegeboard.org",
                EmailDomains = new[] { "@collegeboard.org" },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Institute of International Education (IIE)",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.Association.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.iie.org",
                EmailDomains = new[] { "@iie.org" },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Terra Dotta, LLC",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.BusinessOrCorporation.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.terradotta.com",
                EmailDomains = new[] { "@terradotta.com" },
            });

            #endregion
        }
    }

    public class EstablishmentSamplePartnerEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public EstablishmentSamplePartnerEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            var en = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "en" });
            var fr = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "fr" });
            Seed(new CreateEstablishment
            {
                OfficialName = "Jinan University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.jnu.edu.cn",
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 23.128067, Longitude = 113.347710, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Swinburne University of Technology",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.swinburne.edu.au",
                NonOfficialUrls = new[]
                {
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "www.swin.edu.au"
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = -37.851940, Longitude = 144.991974, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Fachhochschule Nordwestschweiz",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.fhnw.ch",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "University of Applied Sciences Northwestern Switzerland",
                        TranslationToLanguageId = en.RevisionId,
                    },
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Fachhochschule Beider Basel",
                        TranslationToLanguageId = en.RevisionId,
                        IsDefunct = true,
                    },
                },
                NonOfficialUrls = new[]
                {
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "www.fhbb.ch",
                        IsDefunct = true,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 47.484417, Longitude = 8.207265, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Johannes Kepler Universität Linz",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.jku.at",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Johannes Kepler University Linz",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 48.337395, Longitude = 14.317374, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Université catholique de Louvain",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uclouvain.be",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Catholic University of Louvain",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 50.673618, Longitude = 4.604945, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidade Federal do Paraná",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ufpr.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Federal University of Parana",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = -25.434137, Longitude = -49.267353, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidade Federal do Rio de Janeiro",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ufrj.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Federal University of Rio de Janeiro",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = -22.862494, Longitude = -43.223907, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "University of Florida",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ufl.edu",
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 29.643528, Longitude = -82.350685, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Instituto de Pesquisa e Planejamento Urbano de Curitiba",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ippuc.org.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Institute for Research and Urban Planning Curtiba",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = -25.414003, Longitude = -49.252010, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidade Positivo",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.up.com.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Positive University",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = -25.448196, Longitude = -49.355865, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidade de São Paulo",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.usp.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "University of Sao Paulo",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = -23.559305, Longitude = -46.715672, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad del Desarrollo",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.udd.cl",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "University of Development",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = -36.823036, Longitude = -73.036003, },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad Nacional del Nordeste",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.unne.edu.ar",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Northeast National University",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad de Flores",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uflo.edu.ar",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "University of Flores",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                NonOfficialUrls = new[]
                {
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "universidad.uflo.edu.ar",
                        IsDefunct = true,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidade Federal de Goiás",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ufg.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Federal University of Goias",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Fundação Getúlio Vargas",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.fgv.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Getulio Vargas Foundation",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
                NonOfficialUrls = new[]
                {
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "portal.fgv.br",
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidade Estadual do Ceara",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uece.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Ceara State University",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidade Estadual Paulista",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.unesp.br",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Paulista State University",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Université du Québec à Montréal",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uqam.ca",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "University of Quebec at Montreal",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad de Artes, Ciencias y Comunicación",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uniacc.cl",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "University of Arts, Sciences, and Communication",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad de Santiago de Chile",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.usach.cl",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "University of Santiago Chile",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Capital Normal University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cnu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Chang'an University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.xahu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "The China Conservatory",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ccmusic.edu.cn",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "China Conservatory of Music",
                        TranslationToLanguageId = en.RevisionId,
                        IsDefunct = true,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Dalian Jiaotong University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.djtu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "East China Jiaotong University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ecjtu.jx.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Environmental Management College of China",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.emcc.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Guangxi University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.gxu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Guangxi University of Technology",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.gxut.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Guilin University of Technology",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.glut.edu.cn",
                NonOfficialUrls = new[]
                {
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "www.glite.edu.cn",
                        IsDefunct = true,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Hebei University of Technology",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.hebut.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Chinese Academy of Sciences",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cas.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Institute of Psychology, Chinese Academy of Sciences",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.psych.cas.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Liaoning Normal University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.lnnu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Nankai University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.nankai.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Shandong Province",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.GovernmentAdministration.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sd.gov.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Shandong University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sdu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Shanghai Academy of Environmental Sciences",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.saes.sh.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Shanghai Jiao Tong University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sjtu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Soochow University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.suda.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "South China Normal University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.scnu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Southwestern University of Finance and Economics",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.swufe.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Sun Yat-Sen University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.sysu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Tongji University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.tongji.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Tsinghua University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.tsinghua.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Xian International Studies University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.xisu.edu.cn",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad Tecnológica de Bolívar",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.unitecnologica.edu.co",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Technology University of Bolivar",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad del Atlántico",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.uniatlantico.edu.co",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Atlantic University",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Universidad del Valle",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.univalle.edu.co",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Valle University",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Arcada",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.arcada.fi",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Tekniska Läroverkets Kamratförbund r.f",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.tlk.fi",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Swedish Institute of Technology",
                        TranslationToLanguageId = en.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Audencia Nantes School of Management",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.audencia.com",
                NonOfficialNames = new[]
                {
                    new CreateEstablishment.NonOfficialName
                    {
                        Text = "Ecole Supérieure de Commerce de Nantes",
                        TranslationToLanguageId = fr.RevisionId,
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Nanyang Technological University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.ntu.edu.sg",
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Chungnam National University",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.cnu.ac.kr",
                NonOfficialUrls = new[]
                {
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "plus.cnu.ac.kr",
                    },
                    new CreateEstablishment.NonOfficialUrl
                    {
                        Value = "ipsi.cnu.ac.kr",
                    },
                },
            });

            Seed(new CreateEstablishment
            {
                OfficialName = "Beijing University of Technology",
                IsMember = true,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.University.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.bjut.edu.cn",
            });
        }
    }

    public class EstablishmentRecruitmentAgencyEntitySeeder : BaseEstablishmentEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public EstablishmentRecruitmentAgencyEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createEstablishment, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            var en = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "en" });
            var zh = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "zh" });

            #region EduGlobal

            var eduGlobalHq = Seed(new CreateEstablishment
            {
                OfficialName = "EduGlobal Beijing",
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.RecruitmentAgency.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.eduglobalchina.com",
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 39.89871600001, Longitude = 116.4178770000002, },
                Addresses = new[]
                {
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = en.RevisionId,
                        Text = "7F North Office Tower, Beijing New World Centre\r\n3B Chongwenmenwai St\r\n100062 Beijing\r\nPR China",
                    },
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = zh.RevisionId,
                        Text = "中国北京崇文区崇文门外大街3号B\r\n北京新世界中心写字楼B座7层\r\n邮编：100062",
                    },
                },
                PublicContactInfo = new EstablishmentContactInfo
                {
                    Phone = "+86 (10) 6708 0808",
                    Fax = "+86 (10) 6708 2541",
                    Email = "infobeijing@eduglobal.com",
                },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "EduGlobal Changchun",
                ParentId = eduGlobalHq.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.RecruitmentAgency.AsSentenceFragment())).RevisionId,
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 43.8911290000001, Longitude = 125.310471, },
                Addresses = new[]
                {
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = en.RevisionId,
                        Text = "Songhuajiang University,\r\nNo.758 Qianjin Street, Changchun City,\r\nJilin Province\r\n130000, P.R.China",
                    },
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = zh.RevisionId,
                        Text = "吉林省长春市前进大街758号松花江大学\r\n邮编：130000",
                    },
                },
                PublicContactInfo = new EstablishmentContactInfo
                {
                    Phone = "+86 431 85111566",
                    Fax = "+86 431-85111566",
                    Email = "lina.wang@eduglobal.com",
                },
            });

            #endregion
            #region EIC Group

            var eicHq = Seed(new CreateEstablishment
            {
                OfficialName = "EIC Group Beijing",
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.RecruitmentAgency.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.eic.org.cn",
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 39.9059830001, Longitude = 116.4593730001, },
                Addresses = new[]
                {
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = en.RevisionId,
                        Text = "Room 1203, Block A, Jianwai SOHO\r\n39 East 3rd Ring Road\r\nChaoyang District, Beijing\r\nChina  100022",
                    },
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = zh.RevisionId,
                        Text = "北京市朝阳区东三环中路39号建外SOHO A座\r\n12,15层国贸办公区",
                    },
                },
                PublicContactInfo = new EstablishmentContactInfo
                {
                    Phone = "+86 (10) 5878 1616",
                    Fax = "+86 (10) 5869 4393",
                    Email = "beijing@eic.org.cn",
                },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "EIC Group Changsha",
                ParentId = eicHq.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.RecruitmentAgency.AsSentenceFragment())).RevisionId,
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 28.194132, Longitude = 112.976715, },
                Addresses = new[]
                {
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = en.RevisionId,
                        Text = "Floor 24, Pinghetang Business Mansion\r\nNo. 88 Huangxing Middle Road\r\nChangsha City, Hunan Province\r\nChina",
                    },
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = zh.RevisionId,
                        Text = "长沙市黄兴中路88号平和堂商务楼24楼启德教育\r\n中心",
                    },
                },
                PublicContactInfo = new EstablishmentContactInfo
                {
                    Phone = "+86 (731) 8448 8495",
                    Fax = "+86 (731) 8448 3835",
                    Email = "changsha@eic.org.cn",
                },
            });

            #endregion
            #region CanAchieve

            var canAchieveHq = Seed(new CreateEstablishment
            {
                OfficialName = "Can Achieve Group Beijing",
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.RecruitmentAgency.AsSentenceFragment())).RevisionId,
                OfficialWebsiteUrl = "www.can-achieve.com.cn",
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 39.905605, Longitude = 116.459831, },
                Addresses = new[]
                {
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = en.RevisionId,
                        Text = "802, Tower B, JianWai SOHO, Office Building\r\nChaoyang District\r\nBeijing China, 100022",
                    },
                },
                PublicContactInfo = new EstablishmentContactInfo
                {
                    Phone = "+86 (10) 5869 9445",
                    Fax = "+86 (10) 5869 4171",
                },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Can Achieve Group Nanjing",
                ParentId = canAchieveHq.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.RecruitmentAgency.AsSentenceFragment())).RevisionId,
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 32.044769, Longitude = 118.789917, },
                Addresses = new[]
                {
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = en.RevisionId,
                        Text = "A 12F Deji Mansion, No. 188 Changjiang Road\r\nNanjing, Jiangsu Province\r\nChina, 210018",
                    },
                },
                PublicContactInfo = new EstablishmentContactInfo
                {
                    Phone = "+86 (25) 8681 6111",
                },
            });
            Seed(new CreateEstablishment
            {
                OfficialName = "Can Achieve Group Guangzhou",
                ParentId = canAchieveHq.RevisionId,
                TypeId = _queryProcessor.Execute(new EstablishmentTypeByEnglishName(
                    KnownEstablishmentType.RecruitmentAgency.AsSentenceFragment())).RevisionId,
                FindPlacesByCoordinates = true,
                Coordinates = new Coordinates { Latitude = 23.13893700002, Longitude = 113.32875100002, },
                Addresses = new[]
                {
                    new CreateEstablishment.Address
                    {
                        TranslationToLanguageId = en.RevisionId,
                        Text = "Room 511, Nanfang Securities Building\r\nNo.140-148, Tiyu Dong Road",
                    },
                },
                PublicContactInfo = new EstablishmentContactInfo
                {
                    Phone = "+86 (20) 2222 0066",
                },
            });

            #endregion
        }
    }

    public abstract class BaseEstablishmentEntitySeeder : BaseDataSeeder
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<CreateEstablishment> _createEstablishment;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseEstablishmentEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishment> createEstablishment
            , IUnitOfWork unitOfWork
        )
        {
            _queryProcessor = queryProcessor;
            _createEstablishment = createEstablishment;
            _unitOfWork = unitOfWork;
        }

        protected Establishment Seed(CreateEstablishment command)
        {
            // make sure establishment does not already exist
            var establishment = _queryProcessor.Execute(new EstablishmentByOfficialName(command.OfficialName));
            if (!string.IsNullOrWhiteSpace(command.OfficialWebsiteUrl))
                establishment = _queryProcessor.Execute(new GetEstablishmentByUrlQuery(command.OfficialWebsiteUrl));
            if (establishment != null) return establishment;

            _createEstablishment.Handle(command);
            _unitOfWork.SaveChanges();
            return command.CreatedEstablishment;
        }
    }

    public class EstablishmentTypeAndCategorySeeder : BaseDataSeeder
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<CreateEstablishmentCategory> _createEstablishmentCategory;
        private readonly IHandleCommands<CreateEstablishmentType> _createEstablishmentType;
        private readonly IUnitOfWork _unitOfWork;

        public EstablishmentTypeAndCategorySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreateEstablishmentCategory> createEstablishmentCategory
            , IHandleCommands<CreateEstablishmentType> createEstablishmentType
            , IUnitOfWork unitOfWork
        )
        {
            _queryProcessor = queryProcessor;
            _createEstablishmentCategory = createEstablishmentCategory;
            _createEstablishmentType = createEstablishmentType;
            _unitOfWork = unitOfWork;
        }

        public override void Seed()
        {
            // first seed establishment categories and types
            var establishmentCategoryEntries = new Dictionary<string, string>
            {
                { EstablishmentCategoryCode.Inst, "Institution" },
                { EstablishmentCategoryCode.Corp, "Business or Corporation" },
                { EstablishmentCategoryCode.Govt, "Government" },
            };

            foreach (var establishmentCategoryEntry in establishmentCategoryEntries)
            {
                var establishmentCategory = _queryProcessor.Execute(
                    new EstablishmentCategoryByCode(establishmentCategoryEntry.Key));
                if (establishmentCategory == null)
                {
                    _createEstablishmentCategory.Handle(new CreateEstablishmentCategory
                    {
                        Code = establishmentCategoryEntry.Key,
                        EnglishName = establishmentCategoryEntry.Value,
                    });
                }
            }

            var establishmentTypeEntries = new Dictionary<KnownEstablishmentType, string>
            {
                { KnownEstablishmentType.UniversitySystem, EstablishmentCategoryCode.Inst },
                { KnownEstablishmentType.UniversityCampus, EstablishmentCategoryCode.Inst },
                { KnownEstablishmentType.University, EstablishmentCategoryCode.Inst },
                { KnownEstablishmentType.College, EstablishmentCategoryCode.Inst },
                { KnownEstablishmentType.CommunityCollege, EstablishmentCategoryCode.Inst },
                { KnownEstablishmentType.AcademicProgram, EstablishmentCategoryCode.Inst },
                { KnownEstablishmentType.GovernmentAdministration, EstablishmentCategoryCode.Govt },
                { KnownEstablishmentType.BusinessOrCorporation, EstablishmentCategoryCode.Corp },
                { KnownEstablishmentType.RecruitmentAgency, EstablishmentCategoryCode.Corp },
                { KnownEstablishmentType.Association, EstablishmentCategoryCode.Corp },
            };

            foreach (var establishmentTypeEntry in establishmentTypeEntries)
            {
                var establishmentType = _queryProcessor.Execute(
                    new EstablishmentTypeByEnglishName(establishmentTypeEntry.Key.AsSentenceFragment()));
                if (establishmentType == null)
                {
                    _createEstablishmentType.Handle(new CreateEstablishmentType
                    {
                        CategoryCode = establishmentTypeEntry.Value,
                        EnglishName = establishmentTypeEntry.Key.AsSentenceFragment(),
                    });
                }
            }
            _unitOfWork.SaveChanges();
        }
    }
}
