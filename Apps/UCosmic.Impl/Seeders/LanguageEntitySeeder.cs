//using System.Collections.Generic;
//using System.Linq;
//using UCosmic.Domain.Languages;

//namespace UCosmic.Impl.Seeders
//{
//    // Note this class has been deprecated. Use LanguageSqlSeeder instead.
//    public class LanguageEntitySeeder : BaseDataSeeder
//    {
//        private readonly IProcessQueries _queryProcessor;
//        private readonly IHandleCommands<CreateLanguage> _createLanguage;
//        private readonly IHandleCommands<CreateLanguageName> _createLanguageName;
//        private readonly IUnitOfWork _unitOfWork;

//        public LanguageEntitySeeder(IProcessQueries queryProcessor
//            , IHandleCommands<CreateLanguage> createLanguage
//            , IHandleCommands<CreateLanguageName> createLanguageName
//            , IUnitOfWork unitOfWork
//        )
//        {
//            _queryProcessor = queryProcessor;
//            _createLanguage = createLanguage;
//            _createLanguageName = createLanguageName;
//            _unitOfWork = unitOfWork;
//        }

//        public override void Seed()
//        {
//            if (_queryProcessor.Execute(new LanguagesUnfiltered()).Any()) return;

//            #region SIL Languages

//            var en = _queryProcessor.Execute(new LanguageByIsoCode("en"));
//            if (en == null)
//            {
//                var enCreate = new CreateLanguage { TwoLetterIsoCode = "en", ThreeLetterIsoCode = "eng", ThreeLetterIsoBibliographicCode = "eng", };
//                _createLanguage.Handle(enCreate);
//                en = enCreate.CreatedLanguage;
//            }

//            var es = _queryProcessor.Execute(new LanguageByIsoCode("es"));
//            if (es == null)
//            {
//                var esCreate = new CreateLanguage { TwoLetterIsoCode = "es", ThreeLetterIsoCode = "spa", ThreeLetterIsoBibliographicCode = "spa", };
//                _createLanguage.Handle(esCreate);
//                es = esCreate.CreatedLanguage;
//            }

//            var de = _queryProcessor.Execute(new LanguageByIsoCode("de"));
//            if (de == null)
//            {
//                var deCreate = new CreateLanguage { TwoLetterIsoCode = "de", ThreeLetterIsoCode = "deu", ThreeLetterIsoBibliographicCode = "ger", };
//                _createLanguage.Handle(deCreate);
//                de = deCreate.CreatedLanguage;
//            }

//            var ar = _queryProcessor.Execute(new LanguageByIsoCode("ar"));
//            if (ar == null)
//            {
//                var arCreate = new CreateLanguage { TwoLetterIsoCode = "ar", ThreeLetterIsoCode = "ara", ThreeLetterIsoBibliographicCode = "ara", };
//                _createLanguage.Handle(arCreate);
//                ar = arCreate.CreatedLanguage;
//            }

//            var aa = _queryProcessor.Execute(new LanguageByIsoCode("aa"));
//            if (aa == null)
//            {
//                var aaCreate = new CreateLanguage { TwoLetterIsoCode = "aa", ThreeLetterIsoCode = "aar", ThreeLetterIsoBibliographicCode = "aar", };
//                _createLanguage.Handle(aaCreate);
//                aa = aaCreate.CreatedLanguage;
//            }

//            var ab = _queryProcessor.Execute(new LanguageByIsoCode("ab"));
//            if (ab == null)
//            {
//                var abCreate = new CreateLanguage { TwoLetterIsoCode = "ab", ThreeLetterIsoCode = "abk", ThreeLetterIsoBibliographicCode = "abk", };
//                _createLanguage.Handle(abCreate);
//                ab = abCreate.CreatedLanguage;
//            }

//            var af = _queryProcessor.Execute(new LanguageByIsoCode("af"));
//            if (af == null)
//            {
//                var afCreate = new CreateLanguage { TwoLetterIsoCode = "af", ThreeLetterIsoCode = "afr", ThreeLetterIsoBibliographicCode = "afr", };
//                _createLanguage.Handle(afCreate);
//                af = afCreate.CreatedLanguage;
//            }

//            var ak = _queryProcessor.Execute(new LanguageByIsoCode("ak"));
//            if (ak == null)
//            {
//                var akCreate = new CreateLanguage { TwoLetterIsoCode = "ak", ThreeLetterIsoCode = "aka", ThreeLetterIsoBibliographicCode = "aka", };
//                _createLanguage.Handle(akCreate);
//                ak = akCreate.CreatedLanguage;
//            }

//            var am = _queryProcessor.Execute(new LanguageByIsoCode("am"));
//            if (am == null)
//            {
//                var amCreate = new CreateLanguage { TwoLetterIsoCode = "am", ThreeLetterIsoCode = "amh", ThreeLetterIsoBibliographicCode = "amh", };
//                _createLanguage.Handle(amCreate);
//                am = amCreate.CreatedLanguage;
//            }

//            var an = _queryProcessor.Execute(new LanguageByIsoCode("an"));
//            if (an == null)
//            {
//                var anCreate = new CreateLanguage { TwoLetterIsoCode = "an", ThreeLetterIsoCode = "arg", ThreeLetterIsoBibliographicCode = "arg", };
//                _createLanguage.Handle(anCreate);
//                an = anCreate.CreatedLanguage;
//            }

//            var asLanguage = _queryProcessor.Execute(new LanguageByIsoCode("as"));
//            if (asLanguage == null)
//            {
//                var asLanguageCreate = new CreateLanguage { TwoLetterIsoCode = "as", ThreeLetterIsoCode = "asm", ThreeLetterIsoBibliographicCode = "asm", };
//                _createLanguage.Handle(asLanguageCreate);
//                asLanguage = asLanguageCreate.CreatedLanguage;
//            }

//            var av = _queryProcessor.Execute(new LanguageByIsoCode("av"));
//            if (av == null)
//            {
//                var avCreate = new CreateLanguage { TwoLetterIsoCode = "av", ThreeLetterIsoCode = "ava", ThreeLetterIsoBibliographicCode = "ava", };
//                _createLanguage.Handle(avCreate);
//                av = avCreate.CreatedLanguage;
//            }

//            var ay = _queryProcessor.Execute(new LanguageByIsoCode("ay"));
//            if (ay == null)
//            {
//                var ayCreate = new CreateLanguage { TwoLetterIsoCode = "ay", ThreeLetterIsoCode = "aym", ThreeLetterIsoBibliographicCode = "aym", };
//                _createLanguage.Handle(ayCreate);
//                ay = ayCreate.CreatedLanguage;
//            }

//            var az = _queryProcessor.Execute(new LanguageByIsoCode("az"));
//            if (az == null)
//            {
//                var azCreate = new CreateLanguage { TwoLetterIsoCode = "az", ThreeLetterIsoCode = "aze", ThreeLetterIsoBibliographicCode = "aze", };
//                _createLanguage.Handle(azCreate);
//                az = azCreate.CreatedLanguage;
//            }

//            var ba = _queryProcessor.Execute(new LanguageByIsoCode("ba"));
//            if (ba == null)
//            {
//                var baCreate = new CreateLanguage { TwoLetterIsoCode = "ba", ThreeLetterIsoCode = "bak", ThreeLetterIsoBibliographicCode = "bak", };
//                _createLanguage.Handle(baCreate);
//                ba = baCreate.CreatedLanguage;
//            }

//            var be = _queryProcessor.Execute(new LanguageByIsoCode("be"));
//            if (be == null)
//            {
//                var beCreate = new CreateLanguage { TwoLetterIsoCode = "be", ThreeLetterIsoCode = "bel", ThreeLetterIsoBibliographicCode = "bel", };
//                _createLanguage.Handle(beCreate);
//                be = beCreate.CreatedLanguage;
//            }

//            var bg = _queryProcessor.Execute(new LanguageByIsoCode("bg"));
//            if (bg == null)
//            {
//                var bgCreate = new CreateLanguage { TwoLetterIsoCode = "bg", ThreeLetterIsoCode = "bul", ThreeLetterIsoBibliographicCode = "bul", };
//                _createLanguage.Handle(bgCreate);
//                bg = bgCreate.CreatedLanguage;
//            }

//            var bh = _queryProcessor.Execute(new LanguageByIsoCode("bh"));
//            if (bh == null)
//            {
//                var bhCreate = new CreateLanguage { TwoLetterIsoCode = "bh", ThreeLetterIsoCode = "bih", ThreeLetterIsoBibliographicCode = "bih", };
//                _createLanguage.Handle(bhCreate);
//                bh = bhCreate.CreatedLanguage;
//            }

//            var bi = _queryProcessor.Execute(new LanguageByIsoCode("bi"));
//            if (bi == null)
//            {
//                var biCreate = new CreateLanguage { TwoLetterIsoCode = "bi", ThreeLetterIsoCode = "bis", ThreeLetterIsoBibliographicCode = "bis", };
//                _createLanguage.Handle(biCreate);
//                bi = biCreate.CreatedLanguage;
//            }

//            var bm = _queryProcessor.Execute(new LanguageByIsoCode("bm"));
//            if (bm == null)
//            {
//                var bmCreate = new CreateLanguage { TwoLetterIsoCode = "bm", ThreeLetterIsoCode = "bam", ThreeLetterIsoBibliographicCode = "bam", };
//                _createLanguage.Handle(bmCreate);
//                bm = bmCreate.CreatedLanguage;
//            }

//            var bn = _queryProcessor.Execute(new LanguageByIsoCode("bn"));
//            if (bn == null)
//            {
//                var bnCreate = new CreateLanguage { TwoLetterIsoCode = "bn", ThreeLetterIsoCode = "ben", ThreeLetterIsoBibliographicCode = "ben", };
//                _createLanguage.Handle(bnCreate);
//                bn = bnCreate.CreatedLanguage;
//            }

//            var bo = _queryProcessor.Execute(new LanguageByIsoCode("bo"));
//            if (bo == null)
//            {
//                var boCreate = new CreateLanguage { TwoLetterIsoCode = "bo", ThreeLetterIsoCode = "bod", ThreeLetterIsoBibliographicCode = "tib", };
//                _createLanguage.Handle(boCreate);
//                bo = boCreate.CreatedLanguage;
//            }

//            var br = _queryProcessor.Execute(new LanguageByIsoCode("br"));
//            if (br == null)
//            {
//                var brCreate = new CreateLanguage { TwoLetterIsoCode = "br", ThreeLetterIsoCode = "bre", ThreeLetterIsoBibliographicCode = "bre", };
//                _createLanguage.Handle(brCreate);
//                br = brCreate.CreatedLanguage;
//            }

//            var bs = _queryProcessor.Execute(new LanguageByIsoCode("bs"));
//            if (bs == null)
//            {
//                var bsCreate = new CreateLanguage { TwoLetterIsoCode = "bs", ThreeLetterIsoCode = "bos", ThreeLetterIsoBibliographicCode = "bos", };
//                _createLanguage.Handle(bsCreate);
//                bs = bsCreate.CreatedLanguage;
//            }

//            var ca = _queryProcessor.Execute(new LanguageByIsoCode("ca"));
//            if (ca == null)
//            {
//                var caCreate = new CreateLanguage { TwoLetterIsoCode = "ca", ThreeLetterIsoCode = "cat", ThreeLetterIsoBibliographicCode = "cat", };
//                _createLanguage.Handle(caCreate);
//                ca = caCreate.CreatedLanguage;
//            }

//            var ce = _queryProcessor.Execute(new LanguageByIsoCode("ce"));
//            if (ce == null)
//            {
//                var ceCreate = new CreateLanguage { TwoLetterIsoCode = "ce", ThreeLetterIsoCode = "che", ThreeLetterIsoBibliographicCode = "che", };
//                _createLanguage.Handle(ceCreate);
//                ce = ceCreate.CreatedLanguage;
//            }

//            var ch = _queryProcessor.Execute(new LanguageByIsoCode("ch"));
//            if (ch == null)
//            {
//                var chCreate = new CreateLanguage { TwoLetterIsoCode = "ch", ThreeLetterIsoCode = "cha", ThreeLetterIsoBibliographicCode = "cha", };
//                _createLanguage.Handle(chCreate);
//                ch = chCreate.CreatedLanguage;
//            }

//            var co = _queryProcessor.Execute(new LanguageByIsoCode("co"));
//            if (co == null)
//            {
//                var coCreate = new CreateLanguage { TwoLetterIsoCode = "co", ThreeLetterIsoCode = "cos", ThreeLetterIsoBibliographicCode = "cos", };
//                _createLanguage.Handle(coCreate);
//                co = coCreate.CreatedLanguage;
//            }

//            var cr = _queryProcessor.Execute(new LanguageByIsoCode("cr"));
//            if (cr == null)
//            {
//                var crCreate = new CreateLanguage { TwoLetterIsoCode = "cr", ThreeLetterIsoCode = "cre", ThreeLetterIsoBibliographicCode = "cre", };
//                _createLanguage.Handle(crCreate);
//                cr = crCreate.CreatedLanguage;
//            }

//            var cs = _queryProcessor.Execute(new LanguageByIsoCode("cs"));
//            if (cs == null)
//            {
//                var csCreate = new CreateLanguage { TwoLetterIsoCode = "cs", ThreeLetterIsoCode = "ces", ThreeLetterIsoBibliographicCode = "cze", };
//                _createLanguage.Handle(csCreate);
//                cs = csCreate.CreatedLanguage;
//            }

//            var cu = _queryProcessor.Execute(new LanguageByIsoCode("cu"));
//            if (cu == null)
//            {
//                var cuCreate = new CreateLanguage { TwoLetterIsoCode = "cu", ThreeLetterIsoCode = "chu", ThreeLetterIsoBibliographicCode = "chu", };
//                _createLanguage.Handle(cuCreate);
//                cu = cuCreate.CreatedLanguage;
//            }

//            var cv = _queryProcessor.Execute(new LanguageByIsoCode("cv"));
//            if (cv == null)
//            {
//                var cvCreate = new CreateLanguage { TwoLetterIsoCode = "cv", ThreeLetterIsoCode = "chv", ThreeLetterIsoBibliographicCode = "chv", };
//                _createLanguage.Handle(cvCreate);
//                cv = cvCreate.CreatedLanguage;
//            }

//            var cy = _queryProcessor.Execute(new LanguageByIsoCode("cy"));
//            if (cy == null)
//            {
//                var cyCreate = new CreateLanguage { TwoLetterIsoCode = "cy", ThreeLetterIsoCode = "cym", ThreeLetterIsoBibliographicCode = "wel", };
//                _createLanguage.Handle(cyCreate);
//                cy = cyCreate.CreatedLanguage;
//            }

//            var da = _queryProcessor.Execute(new LanguageByIsoCode("da"));
//            if (da == null)
//            {
//                var daCreate = new CreateLanguage { TwoLetterIsoCode = "da", ThreeLetterIsoCode = "dan", ThreeLetterIsoBibliographicCode = "dan", };
//                _createLanguage.Handle(daCreate);
//                da = daCreate.CreatedLanguage;
//            }

//            var dv = _queryProcessor.Execute(new LanguageByIsoCode("dv"));
//            if (dv == null)
//            {
//                var dvCreate = new CreateLanguage { TwoLetterIsoCode = "dv", ThreeLetterIsoCode = "div", ThreeLetterIsoBibliographicCode = "div", };
//                _createLanguage.Handle(dvCreate);
//                dv = dvCreate.CreatedLanguage;
//            }

//            var dz = _queryProcessor.Execute(new LanguageByIsoCode("dz"));
//            if (dz == null)
//            {
//                var dzCreate = new CreateLanguage { TwoLetterIsoCode = "dz", ThreeLetterIsoCode = "dzo", ThreeLetterIsoBibliographicCode = "dzo", };
//                _createLanguage.Handle(dzCreate);
//                dz = dzCreate.CreatedLanguage;
//            }

//            var ee = _queryProcessor.Execute(new LanguageByIsoCode("ee"));
//            if (ee == null)
//            {
//                var eeCreate = new CreateLanguage { TwoLetterIsoCode = "ee", ThreeLetterIsoCode = "ewe", ThreeLetterIsoBibliographicCode = "ewe", };
//                _createLanguage.Handle(eeCreate);
//                ee = eeCreate.CreatedLanguage;
//            }

//            var el = _queryProcessor.Execute(new LanguageByIsoCode("el"));
//            if (el == null)
//            {
//                var elCreate = new CreateLanguage { TwoLetterIsoCode = "el", ThreeLetterIsoCode = "ell", ThreeLetterIsoBibliographicCode = "ell", };
//                _createLanguage.Handle(elCreate);
//                el = elCreate.CreatedLanguage;
//            }

//            var eo = _queryProcessor.Execute(new LanguageByIsoCode("eo"));
//            if (eo == null)
//            {
//                var eoCreate = new CreateLanguage { TwoLetterIsoCode = "eo", ThreeLetterIsoCode = "epo", ThreeLetterIsoBibliographicCode = "epo", };
//                _createLanguage.Handle(eoCreate);
//                eo = eoCreate.CreatedLanguage;
//            }

//            var et = _queryProcessor.Execute(new LanguageByIsoCode("et"));
//            if (et == null)
//            {
//                var etCreate = new CreateLanguage { TwoLetterIsoCode = "et", ThreeLetterIsoCode = "est", ThreeLetterIsoBibliographicCode = "est", };
//                _createLanguage.Handle(etCreate);
//                et = etCreate.CreatedLanguage;
//            }

//            var eu = _queryProcessor.Execute(new LanguageByIsoCode("eu"));
//            if (eu == null)
//            {
//                var euCreate = new CreateLanguage { TwoLetterIsoCode = "eu", ThreeLetterIsoCode = "eus", ThreeLetterIsoBibliographicCode = "baq", };
//                _createLanguage.Handle(euCreate);
//                eu = euCreate.CreatedLanguage;
//            }

//            var fa = _queryProcessor.Execute(new LanguageByIsoCode("fa"));
//            if (fa == null)
//            {
//                var faCreate = new CreateLanguage { TwoLetterIsoCode = "fa", ThreeLetterIsoCode = "fas", ThreeLetterIsoBibliographicCode = "per", };
//                _createLanguage.Handle(faCreate);
//                fa = faCreate.CreatedLanguage;
//            }

//            var ff = _queryProcessor.Execute(new LanguageByIsoCode("ff"));
//            if (ff == null)
//            {
//                var ffCreate = new CreateLanguage { TwoLetterIsoCode = "ff", ThreeLetterIsoCode = "ful", ThreeLetterIsoBibliographicCode = "ful", };
//                _createLanguage.Handle(ffCreate);
//                ff = ffCreate.CreatedLanguage;
//            }

//            var fj = _queryProcessor.Execute(new LanguageByIsoCode("fj"));
//            if (fj == null)
//            {
//                var fjCreate = new CreateLanguage { TwoLetterIsoCode = "fj", ThreeLetterIsoCode = "fij", ThreeLetterIsoBibliographicCode = "fij", };
//                _createLanguage.Handle(fjCreate);
//                fj = fjCreate.CreatedLanguage;
//            }

//            var fo = _queryProcessor.Execute(new LanguageByIsoCode("fo"));
//            if (fo == null)
//            {
//                var foCreate = new CreateLanguage { TwoLetterIsoCode = "fo", ThreeLetterIsoCode = "fao", ThreeLetterIsoBibliographicCode = "fao", };
//                _createLanguage.Handle(foCreate);
//                fo = foCreate.CreatedLanguage;
//            }

//            var fy = _queryProcessor.Execute(new LanguageByIsoCode("fy"));
//            if (fy == null)
//            {
//                var fyCreate = new CreateLanguage { TwoLetterIsoCode = "fy", ThreeLetterIsoCode = "fry", ThreeLetterIsoBibliographicCode = "fry", };
//                _createLanguage.Handle(fyCreate);
//                fy = fyCreate.CreatedLanguage;
//            }

//            var gd = _queryProcessor.Execute(new LanguageByIsoCode("gd"));
//            if (gd == null)
//            {
//                var gdCreate = new CreateLanguage { TwoLetterIsoCode = "gd", ThreeLetterIsoCode = "gla", ThreeLetterIsoBibliographicCode = "gla", };
//                _createLanguage.Handle(gdCreate);
//                gd = gdCreate.CreatedLanguage;
//            }

//            var fi = _queryProcessor.Execute(new LanguageByIsoCode("fi"));
//            if (fi == null)
//            {
//                var fiCreate = new CreateLanguage { TwoLetterIsoCode = "fi", ThreeLetterIsoCode = "fin", ThreeLetterIsoBibliographicCode = "fin", };
//                _createLanguage.Handle(fiCreate);
//                fi = fiCreate.CreatedLanguage;
//            }

//            var fr = _queryProcessor.Execute(new LanguageByIsoCode("fr"));
//            if (fr == null)
//            {
//                var frCreate = new CreateLanguage { TwoLetterIsoCode = "fr", ThreeLetterIsoCode = "fra", ThreeLetterIsoBibliographicCode = "fre", };
//                _createLanguage.Handle(frCreate);
//                fr = frCreate.CreatedLanguage;
//            }

//            var ga = _queryProcessor.Execute(new LanguageByIsoCode("ga"));
//            if (ga == null)
//            {
//                var gaCreate = new CreateLanguage { TwoLetterIsoCode = "ga", ThreeLetterIsoCode = "gle", ThreeLetterIsoBibliographicCode = "gle", };
//                _createLanguage.Handle(gaCreate);
//                ga = gaCreate.CreatedLanguage;
//            }

//            var gl = _queryProcessor.Execute(new LanguageByIsoCode("gl"));
//            if (gl == null)
//            {
//                var glCreate = new CreateLanguage { TwoLetterIsoCode = "gl", ThreeLetterIsoCode = "glg", ThreeLetterIsoBibliographicCode = "glg", };
//                _createLanguage.Handle(glCreate);
//                gl = glCreate.CreatedLanguage;
//            }

//            var gn = _queryProcessor.Execute(new LanguageByIsoCode("gn"));
//            if (gn == null)
//            {
//                var gnCreate = new CreateLanguage { TwoLetterIsoCode = "gn", ThreeLetterIsoCode = "grn", ThreeLetterIsoBibliographicCode = "grn", };
//                _createLanguage.Handle(gnCreate);
//                gn = gnCreate.CreatedLanguage;
//            }

//            var gu = _queryProcessor.Execute(new LanguageByIsoCode("gu"));
//            if (gu == null)
//            {
//                var guCreate = new CreateLanguage { TwoLetterIsoCode = "gu", ThreeLetterIsoCode = "guj", ThreeLetterIsoBibliographicCode = "guj", };
//                _createLanguage.Handle(guCreate);
//                gu = guCreate.CreatedLanguage;
//            }

//            var gv = _queryProcessor.Execute(new LanguageByIsoCode("gv"));
//            if (gv == null)
//            {
//                var gvCreate = new CreateLanguage { TwoLetterIsoCode = "gv", ThreeLetterIsoCode = "glv", ThreeLetterIsoBibliographicCode = "glv", };
//                _createLanguage.Handle(gvCreate);
//                gv = gvCreate.CreatedLanguage;
//            }

//            var ha = _queryProcessor.Execute(new LanguageByIsoCode("ha"));
//            if (ha == null)
//            {
//                var haCreate = new CreateLanguage { TwoLetterIsoCode = "ha", ThreeLetterIsoCode = "hau", ThreeLetterIsoBibliographicCode = "hau", };
//                _createLanguage.Handle(haCreate);
//                ha = haCreate.CreatedLanguage;
//            }

//            var he = _queryProcessor.Execute(new LanguageByIsoCode("he"));
//            if (he == null)
//            {
//                var heCreate = new CreateLanguage { TwoLetterIsoCode = "he", ThreeLetterIsoCode = "heb", ThreeLetterIsoBibliographicCode = "heb", };
//                _createLanguage.Handle(heCreate);
//                he = heCreate.CreatedLanguage;
//            }

//            var hi = _queryProcessor.Execute(new LanguageByIsoCode("hi"));
//            if (hi == null)
//            {
//                var hiCreate = new CreateLanguage { TwoLetterIsoCode = "hi", ThreeLetterIsoCode = "hin", ThreeLetterIsoBibliographicCode = "hin", };
//                _createLanguage.Handle(hiCreate);
//                hi = hiCreate.CreatedLanguage;
//            }

//            var ho = _queryProcessor.Execute(new LanguageByIsoCode("ho"));
//            if (ho == null)
//            {
//                var hoCreate = new CreateLanguage { TwoLetterIsoCode = "ho", ThreeLetterIsoCode = "hmo", ThreeLetterIsoBibliographicCode = "hmo", };
//                _createLanguage.Handle(hoCreate);
//                ho = hoCreate.CreatedLanguage;
//            }

//            var hr = _queryProcessor.Execute(new LanguageByIsoCode("hr"));
//            if (hr == null)
//            {
//                var hrCreate = new CreateLanguage { TwoLetterIsoCode = "hr", ThreeLetterIsoCode = "hrv", ThreeLetterIsoBibliographicCode = "hrv", };
//                _createLanguage.Handle(hrCreate);
//                hr = hrCreate.CreatedLanguage;
//            }

//            var ht = _queryProcessor.Execute(new LanguageByIsoCode("ht"));
//            if (ht == null)
//            {
//                var htCreate = new CreateLanguage { TwoLetterIsoCode = "ht", ThreeLetterIsoCode = "hat", ThreeLetterIsoBibliographicCode = "hat", };
//                _createLanguage.Handle(htCreate);
//                ht = htCreate.CreatedLanguage;
//            }

//            var hu = _queryProcessor.Execute(new LanguageByIsoCode("hu"));
//            if (hu == null)
//            {
//                var huCreate = new CreateLanguage { TwoLetterIsoCode = "hu", ThreeLetterIsoCode = "hun", ThreeLetterIsoBibliographicCode = "hun", };
//                _createLanguage.Handle(huCreate);
//                hu = huCreate.CreatedLanguage;
//            }

//            var hy = _queryProcessor.Execute(new LanguageByIsoCode("hy"));
//            if (hy == null)
//            {
//                var hyCreate = new CreateLanguage { TwoLetterIsoCode = "hy", ThreeLetterIsoCode = "hye", ThreeLetterIsoBibliographicCode = "arm", };
//                _createLanguage.Handle(hyCreate);
//                hy = hyCreate.CreatedLanguage;
//            }

//            var hz = _queryProcessor.Execute(new LanguageByIsoCode("hz"));
//            if (hz == null)
//            {
//                var hzCreate = new CreateLanguage { TwoLetterIsoCode = "hz", ThreeLetterIsoCode = "her", ThreeLetterIsoBibliographicCode = "her", };
//                _createLanguage.Handle(hzCreate);
//                hz = hzCreate.CreatedLanguage;
//            }

//            var ia = _queryProcessor.Execute(new LanguageByIsoCode("ia"));
//            if (ia == null)
//            {
//                var iaCreate = new CreateLanguage { TwoLetterIsoCode = "ia", ThreeLetterIsoCode = "ina", ThreeLetterIsoBibliographicCode = "ina", };
//                _createLanguage.Handle(iaCreate);
//                ia = iaCreate.CreatedLanguage;
//            }

//            var id = _queryProcessor.Execute(new LanguageByIsoCode("id"));
//            if (id == null)
//            {
//                var idCreate = new CreateLanguage { TwoLetterIsoCode = "id", ThreeLetterIsoCode = "ind", ThreeLetterIsoBibliographicCode = "ind", };
//                _createLanguage.Handle(idCreate);
//                id = idCreate.CreatedLanguage;
//            }

//            var ie = _queryProcessor.Execute(new LanguageByIsoCode("ie"));
//            if (ie == null)
//            {
//                var ieCreate = new CreateLanguage { TwoLetterIsoCode = "ie", ThreeLetterIsoCode = "ile", ThreeLetterIsoBibliographicCode = "ile", };
//                _createLanguage.Handle(ieCreate);
//                ie = ieCreate.CreatedLanguage;
//            }

//            var ig = _queryProcessor.Execute(new LanguageByIsoCode("ig"));
//            if (ig == null)
//            {
//                var igCreate = new CreateLanguage { TwoLetterIsoCode = "ig", ThreeLetterIsoCode = "ibo", ThreeLetterIsoBibliographicCode = "ibo", };
//                _createLanguage.Handle(igCreate);
//                ig = igCreate.CreatedLanguage;
//            }

//            var ii = _queryProcessor.Execute(new LanguageByIsoCode("ii"));
//            if (ii == null)
//            {
//                var iiCreate = new CreateLanguage { TwoLetterIsoCode = "ii", ThreeLetterIsoCode = "iii", ThreeLetterIsoBibliographicCode = "iii", };
//                _createLanguage.Handle(iiCreate);
//                ii = iiCreate.CreatedLanguage;
//            }

//            var io = _queryProcessor.Execute(new LanguageByIsoCode("io"));
//            if (io == null)
//            {
//                var ioCreate = new CreateLanguage { TwoLetterIsoCode = "io", ThreeLetterIsoCode = "ido", ThreeLetterIsoBibliographicCode = "ido", };
//                _createLanguage.Handle(ioCreate);
//                io = ioCreate.CreatedLanguage;
//            }

//            var isLanguage = _queryProcessor.Execute(new LanguageByIsoCode("is"));
//            if (isLanguage == null)
//            {
//                var isLanguageCreate = new CreateLanguage { TwoLetterIsoCode = "is", ThreeLetterIsoCode = "isl", ThreeLetterIsoBibliographicCode = "ice", };
//                _createLanguage.Handle(isLanguageCreate);
//                isLanguage = isLanguageCreate.CreatedLanguage;
//            }

//            var it = _queryProcessor.Execute(new LanguageByIsoCode("it"));
//            if (it == null)
//            {
//                var itCreate = new CreateLanguage { TwoLetterIsoCode = "it", ThreeLetterIsoCode = "ita", ThreeLetterIsoBibliographicCode = "ita", };
//                _createLanguage.Handle(itCreate);
//                it = itCreate.CreatedLanguage;
//            }

//            var iu = _queryProcessor.Execute(new LanguageByIsoCode("iu"));
//            if (iu == null)
//            {
//                var iuCreate = new CreateLanguage { TwoLetterIsoCode = "iu", ThreeLetterIsoCode = "iku", ThreeLetterIsoBibliographicCode = "iku", };
//                _createLanguage.Handle(iuCreate);
//                iu = iuCreate.CreatedLanguage;
//            }

//            var ja = _queryProcessor.Execute(new LanguageByIsoCode("ja"));
//            if (ja == null)
//            {
//                var jaCreate = new CreateLanguage { TwoLetterIsoCode = "ja", ThreeLetterIsoCode = "jpn", ThreeLetterIsoBibliographicCode = "jpn", };
//                _createLanguage.Handle(jaCreate);
//                ja = jaCreate.CreatedLanguage;
//            }


//            var jv = _queryProcessor.Execute(new LanguageByIsoCode("jv"));
//            if (jv == null)
//            {
//                var jvCreate = new CreateLanguage { TwoLetterIsoCode = "jv", ThreeLetterIsoCode = "jav", ThreeLetterIsoBibliographicCode = "jav", };
//                _createLanguage.Handle(jvCreate);
//                jv = jvCreate.CreatedLanguage;
//            }

//            var ka = _queryProcessor.Execute(new LanguageByIsoCode("ka"));
//            if (ka == null)
//            {
//                var kaCreate = new CreateLanguage { TwoLetterIsoCode = "ka", ThreeLetterIsoCode = "kat", ThreeLetterIsoBibliographicCode = "geo", };
//                _createLanguage.Handle(kaCreate);
//                ka = kaCreate.CreatedLanguage;
//            }

//            var kg = _queryProcessor.Execute(new LanguageByIsoCode("kg"));
//            if (kg == null)
//            {
//                var kgCreate = new CreateLanguage { TwoLetterIsoCode = "kg", ThreeLetterIsoCode = "kon", ThreeLetterIsoBibliographicCode = "kon", };
//                _createLanguage.Handle(kgCreate);
//                kg = kgCreate.CreatedLanguage;
//            }

//            var ki = _queryProcessor.Execute(new LanguageByIsoCode("ki"));
//            if (ki == null)
//            {
//                var kiCreate = new CreateLanguage { TwoLetterIsoCode = "ki", ThreeLetterIsoCode = "kik", ThreeLetterIsoBibliographicCode = "kik", };
//                _createLanguage.Handle(kiCreate);
//                ki = kiCreate.CreatedLanguage;
//            }

//            var kj = _queryProcessor.Execute(new LanguageByIsoCode("kj"));
//            if (kj == null)
//            {
//                var kjCreate = new CreateLanguage { TwoLetterIsoCode = "kj", ThreeLetterIsoCode = "kua", ThreeLetterIsoBibliographicCode = "kua", };
//                _createLanguage.Handle(kjCreate);
//                kj = kjCreate.CreatedLanguage;
//            }

//            var kk = _queryProcessor.Execute(new LanguageByIsoCode("kk"));
//            if (kk == null)
//            {
//                var kkCreate = new CreateLanguage { TwoLetterIsoCode = "kk", ThreeLetterIsoCode = "kaz", ThreeLetterIsoBibliographicCode = "kaz", };
//                _createLanguage.Handle(kkCreate);
//                kk = kkCreate.CreatedLanguage;
//            }

//            var kl = _queryProcessor.Execute(new LanguageByIsoCode("kl"));
//            if (kl == null)
//            {
//                var klCreate = new CreateLanguage { TwoLetterIsoCode = "kl", ThreeLetterIsoCode = "kal", ThreeLetterIsoBibliographicCode = "kal", };
//                _createLanguage.Handle(klCreate);
//                kl = klCreate.CreatedLanguage;
//            }

//            var km = _queryProcessor.Execute(new LanguageByIsoCode("km"));
//            if (km == null)
//            {
//                var kmCreate = new CreateLanguage { TwoLetterIsoCode = "km", ThreeLetterIsoCode = "khm", ThreeLetterIsoBibliographicCode = "khm", };
//                _createLanguage.Handle(kmCreate);
//                km = kmCreate.CreatedLanguage;
//            }

//            var kn = _queryProcessor.Execute(new LanguageByIsoCode("kn"));
//            if (kn == null)
//            {
//                var knCreate = new CreateLanguage { TwoLetterIsoCode = "kn", ThreeLetterIsoCode = "kan", ThreeLetterIsoBibliographicCode = "kan", };
//                _createLanguage.Handle(knCreate);
//                kn = knCreate.CreatedLanguage;
//            }

//            var ko = _queryProcessor.Execute(new LanguageByIsoCode("ko"));
//            if (ko == null)
//            {
//                var koCreate = new CreateLanguage { TwoLetterIsoCode = "ko", ThreeLetterIsoCode = "kor", ThreeLetterIsoBibliographicCode = "kor", };
//                _createLanguage.Handle(koCreate);
//                ko = koCreate.CreatedLanguage;
//            }

//            var ks = _queryProcessor.Execute(new LanguageByIsoCode("ks"));
//            if (ks == null)
//            {
//                var ksCreate = new CreateLanguage { TwoLetterIsoCode = "ks", ThreeLetterIsoCode = "kas", ThreeLetterIsoBibliographicCode = "kas", };
//                _createLanguage.Handle(ksCreate);
//                ks = ksCreate.CreatedLanguage;
//            }

//            var ku = _queryProcessor.Execute(new LanguageByIsoCode("ku"));
//            if (ku == null)
//            {
//                var kuCreate = new CreateLanguage { TwoLetterIsoCode = "ku", ThreeLetterIsoCode = "kur", ThreeLetterIsoBibliographicCode = "kur", };
//                _createLanguage.Handle(kuCreate);
//                ku = kuCreate.CreatedLanguage;
//            }

//            var kv = _queryProcessor.Execute(new LanguageByIsoCode("kv"));
//            if (kv == null)
//            {
//                var kvCreate = new CreateLanguage { TwoLetterIsoCode = "kv", ThreeLetterIsoCode = "kom", ThreeLetterIsoBibliographicCode = "kom", };
//                _createLanguage.Handle(kvCreate);
//                kv = kvCreate.CreatedLanguage;
//            }

//            var kw = _queryProcessor.Execute(new LanguageByIsoCode("kw"));
//            if (kw == null)
//            {
//                var kwCreate = new CreateLanguage { TwoLetterIsoCode = "kw", ThreeLetterIsoCode = "cor", ThreeLetterIsoBibliographicCode = "cor", };
//                _createLanguage.Handle(kwCreate);
//                kw = kwCreate.CreatedLanguage;
//            }

//            var ky = _queryProcessor.Execute(new LanguageByIsoCode("ky"));
//            if (ky == null)
//            {
//                var kyCreate = new CreateLanguage { TwoLetterIsoCode = "ky", ThreeLetterIsoCode = "kir", ThreeLetterIsoBibliographicCode = "kir", };
//                _createLanguage.Handle(kyCreate);
//                ky = kyCreate.CreatedLanguage;
//            }

//            var la = _queryProcessor.Execute(new LanguageByIsoCode("la"));
//            if (la == null)
//            {
//                var laCreate = new CreateLanguage { TwoLetterIsoCode = "la", ThreeLetterIsoCode = "lat", ThreeLetterIsoBibliographicCode = "lat", };
//                _createLanguage.Handle(laCreate);
//                la = laCreate.CreatedLanguage;
//            }

//            var lb = _queryProcessor.Execute(new LanguageByIsoCode("lb"));
//            if (lb == null)
//            {
//                var lbCreate = new CreateLanguage { TwoLetterIsoCode = "lb", ThreeLetterIsoCode = "ltz", ThreeLetterIsoBibliographicCode = "ltz", };
//                _createLanguage.Handle(lbCreate);
//                lb = lbCreate.CreatedLanguage;
//            }

//            var lg = _queryProcessor.Execute(new LanguageByIsoCode("lg"));
//            if (lg == null)
//            {
//                var lgCreate = new CreateLanguage { TwoLetterIsoCode = "lg", ThreeLetterIsoCode = "lug", ThreeLetterIsoBibliographicCode = "lug", };
//                _createLanguage.Handle(lgCreate);
//                lg = lgCreate.CreatedLanguage;
//            }

//            var li = _queryProcessor.Execute(new LanguageByIsoCode("li"));
//            if (li == null)
//            {
//                var liCreate = new CreateLanguage { TwoLetterIsoCode = "li", ThreeLetterIsoCode = "lim", ThreeLetterIsoBibliographicCode = "lim", };
//                _createLanguage.Handle(liCreate);
//                li = liCreate.CreatedLanguage;
//            }

//            var ln = _queryProcessor.Execute(new LanguageByIsoCode("ln"));
//            if (ln == null)
//            {
//                var lnCreate = new CreateLanguage { TwoLetterIsoCode = "ln", ThreeLetterIsoCode = "lin", ThreeLetterIsoBibliographicCode = "lin", };
//                _createLanguage.Handle(lnCreate);
//                ln = lnCreate.CreatedLanguage;
//            }

//            var lo = _queryProcessor.Execute(new LanguageByIsoCode("lo"));
//            if (lo == null)
//            {
//                var loCreate = new CreateLanguage { TwoLetterIsoCode = "lo", ThreeLetterIsoCode = "lao", ThreeLetterIsoBibliographicCode = "lao", };
//                _createLanguage.Handle(loCreate);
//                lo = loCreate.CreatedLanguage;
//            }

//            var lt = _queryProcessor.Execute(new LanguageByIsoCode("lt"));
//            if (lt == null)
//            {
//                var ltCreate = new CreateLanguage { TwoLetterIsoCode = "lt", ThreeLetterIsoCode = "lit", ThreeLetterIsoBibliographicCode = "lit", };
//                _createLanguage.Handle(ltCreate);
//                lt = ltCreate.CreatedLanguage;
//            }

//            var lv = _queryProcessor.Execute(new LanguageByIsoCode("lv"));
//            if (lv == null)
//            {
//                var lvCreate = new CreateLanguage { TwoLetterIsoCode = "lv", ThreeLetterIsoCode = "lav", ThreeLetterIsoBibliographicCode = "lav", };
//                _createLanguage.Handle(lvCreate);
//                lv = lvCreate.CreatedLanguage;
//            }

//            var mg = _queryProcessor.Execute(new LanguageByIsoCode("mg"));
//            if (mg == null)
//            {
//                var mgCreate = new CreateLanguage { TwoLetterIsoCode = "mg", ThreeLetterIsoCode = "mlg", ThreeLetterIsoBibliographicCode = "mlg", };
//                _createLanguage.Handle(mgCreate);
//                mg = mgCreate.CreatedLanguage;
//            }

//            var mh = _queryProcessor.Execute(new LanguageByIsoCode("mh"));
//            if (mh == null)
//            {
//                var mhCreate = new CreateLanguage { TwoLetterIsoCode = "mh", ThreeLetterIsoCode = "mah", ThreeLetterIsoBibliographicCode = "mah", };
//                _createLanguage.Handle(mhCreate);
//                mh = mhCreate.CreatedLanguage;
//            }

//            var mi = _queryProcessor.Execute(new LanguageByIsoCode("mi"));
//            if (mi == null)
//            {
//                var miCreate = new CreateLanguage { TwoLetterIsoCode = "mi", ThreeLetterIsoCode = "mri", ThreeLetterIsoBibliographicCode = "mao", };
//                _createLanguage.Handle(miCreate);
//                mi = miCreate.CreatedLanguage;
//            }

//            var mk = _queryProcessor.Execute(new LanguageByIsoCode("mk"));
//            if (mk == null)
//            {
//                var mkCreate = new CreateLanguage { TwoLetterIsoCode = "mk", ThreeLetterIsoCode = "mkd", ThreeLetterIsoBibliographicCode = "mac", };
//                _createLanguage.Handle(mkCreate);
//                mk = mkCreate.CreatedLanguage;
//            }

//            var ml = _queryProcessor.Execute(new LanguageByIsoCode("ml"));
//            if (ml == null)
//            {
//                var mlCreate = new CreateLanguage { TwoLetterIsoCode = "ml", ThreeLetterIsoCode = "mal", ThreeLetterIsoBibliographicCode = "mal", };
//                _createLanguage.Handle(mlCreate);
//                ml = mlCreate.CreatedLanguage;
//            }

//            var mn = _queryProcessor.Execute(new LanguageByIsoCode("mn"));
//            if (mn == null)
//            {
//                var mnCreate = new CreateLanguage { TwoLetterIsoCode = "mn", ThreeLetterIsoCode = "mon", ThreeLetterIsoBibliographicCode = "mon", };
//                _createLanguage.Handle(mnCreate);
//                mn = mnCreate.CreatedLanguage;
//            }

//            var mr = _queryProcessor.Execute(new LanguageByIsoCode("mr"));
//            if (mr == null)
//            {
//                var mrCreate = new CreateLanguage { TwoLetterIsoCode = "mr", ThreeLetterIsoCode = "mar", ThreeLetterIsoBibliographicCode = "mar", };
//                _createLanguage.Handle(mrCreate);
//                mr = mrCreate.CreatedLanguage;
//            }

//            var ms = _queryProcessor.Execute(new LanguageByIsoCode("ms"));
//            if (ms == null)
//            {
//                var msCreate = new CreateLanguage { TwoLetterIsoCode = "ms", ThreeLetterIsoCode = "msa", ThreeLetterIsoBibliographicCode = "may", };
//                _createLanguage.Handle(msCreate);
//                ms = msCreate.CreatedLanguage;
//            }

//            var mt = _queryProcessor.Execute(new LanguageByIsoCode("mt"));
//            if (mt == null)
//            {
//                var mtCreate = new CreateLanguage { TwoLetterIsoCode = "mt", ThreeLetterIsoCode = "mlt", ThreeLetterIsoBibliographicCode = "mlt", };
//                _createLanguage.Handle(mtCreate);
//                mt = mtCreate.CreatedLanguage;
//            }

//            var my = _queryProcessor.Execute(new LanguageByIsoCode("my"));
//            if (my == null)
//            {
//                var myCreate = new CreateLanguage { TwoLetterIsoCode = "my", ThreeLetterIsoCode = "mya", ThreeLetterIsoBibliographicCode = "bur", };
//                _createLanguage.Handle(myCreate);
//                my = myCreate.CreatedLanguage;
//            }

//            var na = _queryProcessor.Execute(new LanguageByIsoCode("na"));
//            if (na == null)
//            {
//                var naCreate = new CreateLanguage { TwoLetterIsoCode = "na", ThreeLetterIsoCode = "nau", ThreeLetterIsoBibliographicCode = "nau", };
//                _createLanguage.Handle(naCreate);
//                na = naCreate.CreatedLanguage;
//            }

//            var nb = _queryProcessor.Execute(new LanguageByIsoCode("nb"));
//            if (nb == null)
//            {
//                var nbCreate = new CreateLanguage { TwoLetterIsoCode = "nb", ThreeLetterIsoCode = "nob", ThreeLetterIsoBibliographicCode = "nob", };
//                _createLanguage.Handle(nbCreate);
//                nb = nbCreate.CreatedLanguage;
//            }

//            var ne = _queryProcessor.Execute(new LanguageByIsoCode("ne"));
//            if (ne == null)
//            {
//                var neCreate = new CreateLanguage { TwoLetterIsoCode = "ne", ThreeLetterIsoCode = "nep", ThreeLetterIsoBibliographicCode = "nep", };
//                _createLanguage.Handle(neCreate);
//                ne = neCreate.CreatedLanguage;
//            }

//            var ng = _queryProcessor.Execute(new LanguageByIsoCode("ng"));
//            if (ng == null)
//            {
//                var ngCreate = new CreateLanguage { TwoLetterIsoCode = "ng", ThreeLetterIsoCode = "ndo", ThreeLetterIsoBibliographicCode = "ndo", };
//                _createLanguage.Handle(ngCreate);
//                ng = ngCreate.CreatedLanguage;
//            }

//            var nl = _queryProcessor.Execute(new LanguageByIsoCode("nl"));
//            if (nl == null)
//            {
//                var nlCreate = new CreateLanguage { TwoLetterIsoCode = "nl", ThreeLetterIsoCode = "nld", ThreeLetterIsoBibliographicCode = "dut", };
//                _createLanguage.Handle(nlCreate);
//                nl = nlCreate.CreatedLanguage;
//            }

//            var nn = _queryProcessor.Execute(new LanguageByIsoCode("nn"));
//            if (nn == null)
//            {
//                var nnCreate = new CreateLanguage { TwoLetterIsoCode = "nn", ThreeLetterIsoCode = "nno", ThreeLetterIsoBibliographicCode = "nno", };
//                _createLanguage.Handle(nnCreate);
//                nn = nnCreate.CreatedLanguage;
//            }

//            var no = _queryProcessor.Execute(new LanguageByIsoCode("no"));
//            if (no == null)
//            {
//                var noCreate = new CreateLanguage { TwoLetterIsoCode = "no", ThreeLetterIsoCode = "nor", ThreeLetterIsoBibliographicCode = "nor", };
//                _createLanguage.Handle(noCreate);
//                no = noCreate.CreatedLanguage;
//            }

//            var nv = _queryProcessor.Execute(new LanguageByIsoCode("nv"));
//            if (nv == null)
//            {
//                var nvCreate = new CreateLanguage { TwoLetterIsoCode = "nv", ThreeLetterIsoCode = "nav", ThreeLetterIsoBibliographicCode = "nav", };
//                _createLanguage.Handle(nvCreate);
//                nv = nvCreate.CreatedLanguage;
//            }

//            var ny = _queryProcessor.Execute(new LanguageByIsoCode("ny"));
//            if (ny == null)
//            {
//                var nyCreate = new CreateLanguage { TwoLetterIsoCode = "ny", ThreeLetterIsoCode = "nya", ThreeLetterIsoBibliographicCode = "nya", };
//                _createLanguage.Handle(nyCreate);
//                ny = nyCreate.CreatedLanguage;
//            }

//            var oc = _queryProcessor.Execute(new LanguageByIsoCode("oc"));
//            if (oc == null)
//            {
//                var ocCreate = new CreateLanguage { TwoLetterIsoCode = "oc", ThreeLetterIsoCode = "oci", ThreeLetterIsoBibliographicCode = "oci", };
//                _createLanguage.Handle(ocCreate);
//                oc = ocCreate.CreatedLanguage;
//            }

//            var om = _queryProcessor.Execute(new LanguageByIsoCode("om"));
//            if (om == null)
//            {
//                var omCreate = new CreateLanguage { TwoLetterIsoCode = "om", ThreeLetterIsoCode = "orm", ThreeLetterIsoBibliographicCode = "orm", };
//                _createLanguage.Handle(omCreate);
//                om = omCreate.CreatedLanguage;
//            }

//            var or = _queryProcessor.Execute(new LanguageByIsoCode("or"));
//            if (or == null)
//            {
//                var orCreate = new CreateLanguage { TwoLetterIsoCode = "or", ThreeLetterIsoCode = "ori", ThreeLetterIsoBibliographicCode = "ori", };
//                _createLanguage.Handle(orCreate);
//                or = orCreate.CreatedLanguage;
//            }

//            var os = _queryProcessor.Execute(new LanguageByIsoCode("os"));
//            if (os == null)
//            {
//                var osCreate = new CreateLanguage { TwoLetterIsoCode = "os", ThreeLetterIsoCode = "oss", ThreeLetterIsoBibliographicCode = "oss", };
//                _createLanguage.Handle(osCreate);
//                os = osCreate.CreatedLanguage;
//            }

//            var pa = _queryProcessor.Execute(new LanguageByIsoCode("pa"));
//            if (pa == null)
//            {
//                var paCreate = new CreateLanguage { TwoLetterIsoCode = "pa", ThreeLetterIsoCode = "pan", ThreeLetterIsoBibliographicCode = "pan", };
//                _createLanguage.Handle(paCreate);
//                pa = paCreate.CreatedLanguage;
//            }

//            var pi = _queryProcessor.Execute(new LanguageByIsoCode("pi"));
//            if (pi == null)
//            {
//                var piCreate = new CreateLanguage { TwoLetterIsoCode = "pi", ThreeLetterIsoCode = "pli", ThreeLetterIsoBibliographicCode = "pli", };
//                _createLanguage.Handle(piCreate);
//                pi = piCreate.CreatedLanguage;
//            }

//            var pl = _queryProcessor.Execute(new LanguageByIsoCode("pl"));
//            if (pl == null)
//            {
//                var plCreate = new CreateLanguage { TwoLetterIsoCode = "pl", ThreeLetterIsoCode = "pol", ThreeLetterIsoBibliographicCode = "pol", };
//                _createLanguage.Handle(plCreate);
//                pl = plCreate.CreatedLanguage;
//            }

//            var ps = _queryProcessor.Execute(new LanguageByIsoCode("ps"));
//            if (ps == null)
//            {
//                var psCreate = new CreateLanguage { TwoLetterIsoCode = "ps", ThreeLetterIsoCode = "pus", ThreeLetterIsoBibliographicCode = "pus", };
//                _createLanguage.Handle(psCreate);
//                ps = psCreate.CreatedLanguage;
//            }

//            var pt = _queryProcessor.Execute(new LanguageByIsoCode("pt"));
//            if (pt == null)
//            {
//                var ptCreate = new CreateLanguage { TwoLetterIsoCode = "pt", ThreeLetterIsoCode = "por", ThreeLetterIsoBibliographicCode = "por", };
//                _createLanguage.Handle(ptCreate);
//                pt = ptCreate.CreatedLanguage;
//            }

//            var qu = _queryProcessor.Execute(new LanguageByIsoCode("qu"));
//            if (qu == null)
//            {
//                var quCreate = new CreateLanguage { TwoLetterIsoCode = "qu", ThreeLetterIsoCode = "que", ThreeLetterIsoBibliographicCode = "que", };
//                _createLanguage.Handle(quCreate);
//                qu = quCreate.CreatedLanguage;
//            }

//            var rm = _queryProcessor.Execute(new LanguageByIsoCode("rm"));
//            if (rm == null)
//            {
//                var rmCreate = new CreateLanguage { TwoLetterIsoCode = "rm", ThreeLetterIsoCode = "roh", ThreeLetterIsoBibliographicCode = "roh", };
//                _createLanguage.Handle(rmCreate);
//                rm = rmCreate.CreatedLanguage;
//            }

//            var rn = _queryProcessor.Execute(new LanguageByIsoCode("rn"));
//            if (rn == null)
//            {
//                var rnCreate = new CreateLanguage { TwoLetterIsoCode = "rn", ThreeLetterIsoCode = "run", ThreeLetterIsoBibliographicCode = "run", };
//                _createLanguage.Handle(rnCreate);
//                rn = rnCreate.CreatedLanguage;
//            }

//            var ro = _queryProcessor.Execute(new LanguageByIsoCode("ro"));
//            if (ro == null)
//            {
//                var roCreate = new CreateLanguage { TwoLetterIsoCode = "ro", ThreeLetterIsoCode = "ron", ThreeLetterIsoBibliographicCode = "rum", };
//                _createLanguage.Handle(roCreate);
//                ro = roCreate.CreatedLanguage;
//            }

//            var ru = _queryProcessor.Execute(new LanguageByIsoCode("ru"));
//            if (ru == null)
//            {
//                var ruCreate = new CreateLanguage { TwoLetterIsoCode = "ru", ThreeLetterIsoCode = "rus", ThreeLetterIsoBibliographicCode = "rus", };
//                _createLanguage.Handle(ruCreate);
//                ru = ruCreate.CreatedLanguage;
//            }

//            var rw = _queryProcessor.Execute(new LanguageByIsoCode("rw"));
//            if (rw == null)
//            {
//                var rwCreate = new CreateLanguage { TwoLetterIsoCode = "rw", ThreeLetterIsoCode = "kin", ThreeLetterIsoBibliographicCode = "kin", };
//                _createLanguage.Handle(rwCreate);
//                rw = rwCreate.CreatedLanguage;
//            }

//            var sa = _queryProcessor.Execute(new LanguageByIsoCode("sa"));
//            if (sa == null)
//            {
//                var saCreate = new CreateLanguage { TwoLetterIsoCode = "sa", ThreeLetterIsoCode = "san", ThreeLetterIsoBibliographicCode = "san", };
//                _createLanguage.Handle(saCreate);
//                sa = saCreate.CreatedLanguage;
//            }

//            var sc = _queryProcessor.Execute(new LanguageByIsoCode("sc"));
//            if (sc == null)
//            {
//                var scCreate = new CreateLanguage { TwoLetterIsoCode = "sc", ThreeLetterIsoCode = "srd", ThreeLetterIsoBibliographicCode = "srd", };
//                _createLanguage.Handle(scCreate);
//                sc = scCreate.CreatedLanguage;
//            }

//            var sd = _queryProcessor.Execute(new LanguageByIsoCode("sd"));
//            if (sd == null)
//            {
//                var sdCreate = new CreateLanguage { TwoLetterIsoCode = "sd", ThreeLetterIsoCode = "snd", ThreeLetterIsoBibliographicCode = "snd", };
//                _createLanguage.Handle(sdCreate);
//                sd = sdCreate.CreatedLanguage;
//            }

//            var se = _queryProcessor.Execute(new LanguageByIsoCode("se"));
//            if (se == null)
//            {
//                var seCreate = new CreateLanguage { TwoLetterIsoCode = "se", ThreeLetterIsoCode = "sme", ThreeLetterIsoBibliographicCode = "sme", };
//                _createLanguage.Handle(seCreate);
//                se = seCreate.CreatedLanguage;
//            }

//            var sg = _queryProcessor.Execute(new LanguageByIsoCode("sg"));
//            if (sg == null)
//            {
//                var sgCreate = new CreateLanguage { TwoLetterIsoCode = "sg", ThreeLetterIsoCode = "sag", ThreeLetterIsoBibliographicCode = "sag", };
//                _createLanguage.Handle(sgCreate);
//                sg = sgCreate.CreatedLanguage;
//            }

//            var sh = _queryProcessor.Execute(new LanguageByIsoCode("sh"));
//            if (sh == null)
//            {
//                var shCreate = new CreateLanguage { TwoLetterIsoCode = "sh", ThreeLetterIsoCode = "hbs", ThreeLetterIsoBibliographicCode = "hbs", };
//                _createLanguage.Handle(shCreate);
//                sh = shCreate.CreatedLanguage;
//            }

//            var si = _queryProcessor.Execute(new LanguageByIsoCode("si"));
//            if (si == null)
//            {
//                var siCreate = new CreateLanguage { TwoLetterIsoCode = "si", ThreeLetterIsoCode = "sin", ThreeLetterIsoBibliographicCode = "sin", };
//                _createLanguage.Handle(siCreate);
//                si = siCreate.CreatedLanguage;
//            }

//            var sk = _queryProcessor.Execute(new LanguageByIsoCode("sk"));
//            if (sk == null)
//            {
//                var skCreate = new CreateLanguage { TwoLetterIsoCode = "sk", ThreeLetterIsoCode = "slk", ThreeLetterIsoBibliographicCode = "slo", };
//                _createLanguage.Handle(skCreate);
//                sk = skCreate.CreatedLanguage;
//            }

//            var sl = _queryProcessor.Execute(new LanguageByIsoCode("sl"));
//            if (sl == null)
//            {
//                var slCreate = new CreateLanguage { TwoLetterIsoCode = "sl", ThreeLetterIsoCode = "slv", ThreeLetterIsoBibliographicCode = "slv", };
//                _createLanguage.Handle(slCreate);
//                sl = slCreate.CreatedLanguage;
//            }

//            var sm = _queryProcessor.Execute(new LanguageByIsoCode("sm"));
//            if (sm == null)
//            {
//                var smCreate = new CreateLanguage { TwoLetterIsoCode = "sm", ThreeLetterIsoCode = "smo", ThreeLetterIsoBibliographicCode = "smo", };
//                _createLanguage.Handle(smCreate);
//                sm = smCreate.CreatedLanguage;
//            }

//            var sn = _queryProcessor.Execute(new LanguageByIsoCode("sn"));
//            if (sn == null)
//            {
//                var snCreate = new CreateLanguage { TwoLetterIsoCode = "sn", ThreeLetterIsoCode = "sna", ThreeLetterIsoBibliographicCode = "sna", };
//                _createLanguage.Handle(snCreate);
//                sn = snCreate.CreatedLanguage;
//            }

//            var so = _queryProcessor.Execute(new LanguageByIsoCode("so"));
//            if (so == null)
//            {
//                var soCreate = new CreateLanguage { TwoLetterIsoCode = "so", ThreeLetterIsoCode = "som", ThreeLetterIsoBibliographicCode = "som", };
//                _createLanguage.Handle(soCreate);
//                so = soCreate.CreatedLanguage;
//            }

//            var sq = _queryProcessor.Execute(new LanguageByIsoCode("sq"));
//            if (sq == null)
//            {
//                var sqCreate = new CreateLanguage { TwoLetterIsoCode = "sq", ThreeLetterIsoCode = "sqi", ThreeLetterIsoBibliographicCode = "alb", };
//                _createLanguage.Handle(sqCreate);
//                sq = sqCreate.CreatedLanguage;
//            }

//            var sr = _queryProcessor.Execute(new LanguageByIsoCode("sr"));
//            if (sr == null)
//            {
//                var srCreate = new CreateLanguage { TwoLetterIsoCode = "sr", ThreeLetterIsoCode = "srp", ThreeLetterIsoBibliographicCode = "srp", };
//                _createLanguage.Handle(srCreate);
//                sr = srCreate.CreatedLanguage;
//            }

//            var ss = _queryProcessor.Execute(new LanguageByIsoCode("ss"));
//            if (ss == null)
//            {
//                var ssCreate = new CreateLanguage { TwoLetterIsoCode = "ss", ThreeLetterIsoCode = "ssw", ThreeLetterIsoBibliographicCode = "ssw", };
//                _createLanguage.Handle(ssCreate);
//                ss = ssCreate.CreatedLanguage;
//            }

//            var st = _queryProcessor.Execute(new LanguageByIsoCode("st"));
//            if (st == null)
//            {
//                var stCreate = new CreateLanguage { TwoLetterIsoCode = "st", ThreeLetterIsoCode = "sot", ThreeLetterIsoBibliographicCode = "sot", };
//                _createLanguage.Handle(stCreate);
//                st = stCreate.CreatedLanguage;
//            }

//            var su = _queryProcessor.Execute(new LanguageByIsoCode("su"));
//            if (su == null)
//            {
//                var suCreate = new CreateLanguage { TwoLetterIsoCode = "su", ThreeLetterIsoCode = "sun", ThreeLetterIsoBibliographicCode = "sun", };
//                _createLanguage.Handle(suCreate);
//                su = suCreate.CreatedLanguage;
//            }

//            var sv = _queryProcessor.Execute(new LanguageByIsoCode("sv"));
//            if (sv == null)
//            {
//                var svCreate = new CreateLanguage { TwoLetterIsoCode = "sv", ThreeLetterIsoCode = "swe", ThreeLetterIsoBibliographicCode = "swe", };
//                _createLanguage.Handle(svCreate);
//                sv = svCreate.CreatedLanguage;
//            }

//            var sw = _queryProcessor.Execute(new LanguageByIsoCode("sw"));
//            if (sw == null)
//            {
//                var swCreate = new CreateLanguage { TwoLetterIsoCode = "sw", ThreeLetterIsoCode = "swa", ThreeLetterIsoBibliographicCode = "swa", };
//                _createLanguage.Handle(swCreate);
//                sw = swCreate.CreatedLanguage;
//            }

//            var ta = _queryProcessor.Execute(new LanguageByIsoCode("ta"));
//            if (ta == null)
//            {
//                var taCreate = new CreateLanguage { TwoLetterIsoCode = "ta", ThreeLetterIsoCode = "tam", ThreeLetterIsoBibliographicCode = "tam", };
//                _createLanguage.Handle(taCreate);
//                ta = taCreate.CreatedLanguage;
//            }

//            var te = _queryProcessor.Execute(new LanguageByIsoCode("te"));
//            if (te == null)
//            {
//                var teCreate = new CreateLanguage { TwoLetterIsoCode = "te", ThreeLetterIsoCode = "tel", ThreeLetterIsoBibliographicCode = "tel", };
//                _createLanguage.Handle(teCreate);
//                te = teCreate.CreatedLanguage;
//            }

//            var tg = _queryProcessor.Execute(new LanguageByIsoCode("tg"));
//            if (tg == null)
//            {
//                var tgCreate = new CreateLanguage { TwoLetterIsoCode = "tg", ThreeLetterIsoCode = "tgk", ThreeLetterIsoBibliographicCode = "tgk", };
//                _createLanguage.Handle(tgCreate);
//                tg = tgCreate.CreatedLanguage;
//            }

//            var th = _queryProcessor.Execute(new LanguageByIsoCode("th"));
//            if (th == null)
//            {
//                var thCreate = new CreateLanguage { TwoLetterIsoCode = "th", ThreeLetterIsoCode = "tha", ThreeLetterIsoBibliographicCode = "tha", };
//                _createLanguage.Handle(thCreate);
//                th = thCreate.CreatedLanguage;
//            }

//            var ti = _queryProcessor.Execute(new LanguageByIsoCode("ti"));
//            if (ti == null)
//            {
//                var tiCreate = new CreateLanguage { TwoLetterIsoCode = "ti", ThreeLetterIsoCode = "tir", ThreeLetterIsoBibliographicCode = "tir", };
//                _createLanguage.Handle(tiCreate);
//                ti = tiCreate.CreatedLanguage;
//            }

//            var tk = _queryProcessor.Execute(new LanguageByIsoCode("tk"));
//            if (tk == null)
//            {
//                var tkCreate = new CreateLanguage { TwoLetterIsoCode = "tk", ThreeLetterIsoCode = "tuk", ThreeLetterIsoBibliographicCode = "tuk", };
//                _createLanguage.Handle(tkCreate);
//                tk = tkCreate.CreatedLanguage;
//            }

//            var tl = _queryProcessor.Execute(new LanguageByIsoCode("tl"));
//            if (tl == null)
//            {
//                var tlCreate = new CreateLanguage { TwoLetterIsoCode = "tl", ThreeLetterIsoCode = "tgl", ThreeLetterIsoBibliographicCode = "tgl", };
//                _createLanguage.Handle(tlCreate);
//                tl = tlCreate.CreatedLanguage;
//            }

//            var tn = _queryProcessor.Execute(new LanguageByIsoCode("tn"));
//            if (tn == null)
//            {
//                var tnCreate = new CreateLanguage { TwoLetterIsoCode = "tn", ThreeLetterIsoCode = "tsn", ThreeLetterIsoBibliographicCode = "tsn", };
//                _createLanguage.Handle(tnCreate);
//                tn = tnCreate.CreatedLanguage;
//            }

//            var to = _queryProcessor.Execute(new LanguageByIsoCode("to"));
//            if (to == null)
//            {
//                var toCreate = new CreateLanguage { TwoLetterIsoCode = "to", ThreeLetterIsoCode = "ton", ThreeLetterIsoBibliographicCode = "ton", };
//                _createLanguage.Handle(toCreate);
//                to = toCreate.CreatedLanguage;
//            }

//            var tr = _queryProcessor.Execute(new LanguageByIsoCode("tr"));
//            if (tr == null)
//            {
//                var trCreate = new CreateLanguage { TwoLetterIsoCode = "tr", ThreeLetterIsoCode = "tur", ThreeLetterIsoBibliographicCode = "tur", };
//                _createLanguage.Handle(trCreate);
//                tr = trCreate.CreatedLanguage;
//            }

//            var ts = _queryProcessor.Execute(new LanguageByIsoCode("ts"));
//            if (ts == null)
//            {
//                var tsCreate = new CreateLanguage { TwoLetterIsoCode = "ts", ThreeLetterIsoCode = "tso", ThreeLetterIsoBibliographicCode = "tso", };
//                _createLanguage.Handle(tsCreate);
//                ts = tsCreate.CreatedLanguage;
//            }

//            var tt = _queryProcessor.Execute(new LanguageByIsoCode("tt"));
//            if (tt == null)
//            {
//                var ttCreate = new CreateLanguage { TwoLetterIsoCode = "tt", ThreeLetterIsoCode = "tat", ThreeLetterIsoBibliographicCode = "tat", };
//                _createLanguage.Handle(ttCreate);
//                tt = ttCreate.CreatedLanguage;
//            }

//            var tw = _queryProcessor.Execute(new LanguageByIsoCode("tw"));
//            if (tw == null)
//            {
//                var twCreate = new CreateLanguage { TwoLetterIsoCode = "tw", ThreeLetterIsoCode = "twi", ThreeLetterIsoBibliographicCode = "twi", };
//                _createLanguage.Handle(twCreate);
//                tw = twCreate.CreatedLanguage;
//            }

//            var ty = _queryProcessor.Execute(new LanguageByIsoCode("ty"));
//            if (ty == null)
//            {
//                var tyCreate = new CreateLanguage { TwoLetterIsoCode = "ty", ThreeLetterIsoCode = "tah", ThreeLetterIsoBibliographicCode = "tah", };
//                _createLanguage.Handle(tyCreate);
//                ty = tyCreate.CreatedLanguage;
//            }

//            var ug = _queryProcessor.Execute(new LanguageByIsoCode("ug"));
//            if (ug == null)
//            {
//                var ugCreate = new CreateLanguage { TwoLetterIsoCode = "ug", ThreeLetterIsoCode = "uig", ThreeLetterIsoBibliographicCode = "uig", };
//                _createLanguage.Handle(ugCreate);
//                ug = ugCreate.CreatedLanguage;
//            }

//            var uk = _queryProcessor.Execute(new LanguageByIsoCode("uk"));
//            if (uk == null)
//            {
//                var ukCreate = new CreateLanguage { TwoLetterIsoCode = "uk", ThreeLetterIsoCode = "ukr", ThreeLetterIsoBibliographicCode = "ukr", };
//                _createLanguage.Handle(ukCreate);
//                uk = ukCreate.CreatedLanguage;
//            }

//            var ur = _queryProcessor.Execute(new LanguageByIsoCode("ur"));
//            if (ur == null)
//            {
//                var urCreate = new CreateLanguage { TwoLetterIsoCode = "ur", ThreeLetterIsoCode = "urd", ThreeLetterIsoBibliographicCode = "urd", };
//                _createLanguage.Handle(urCreate);
//                ur = urCreate.CreatedLanguage;
//            }

//            var uz = _queryProcessor.Execute(new LanguageByIsoCode("uz"));
//            if (uz == null)
//            {
//                var uzCreate = new CreateLanguage { TwoLetterIsoCode = "uz", ThreeLetterIsoCode = "uzb", ThreeLetterIsoBibliographicCode = "uzb", };
//                _createLanguage.Handle(uzCreate);
//                uz = uzCreate.CreatedLanguage;
//            }

//            var ve = _queryProcessor.Execute(new LanguageByIsoCode("ve"));
//            if (ve == null)
//            {
//                var veCreate = new CreateLanguage { TwoLetterIsoCode = "ve", ThreeLetterIsoCode = "ven", ThreeLetterIsoBibliographicCode = "ven", };
//                _createLanguage.Handle(veCreate);
//                ve = veCreate.CreatedLanguage;
//            }

//            var vi = _queryProcessor.Execute(new LanguageByIsoCode("vi"));
//            if (vi == null)
//            {
//                var viCreate = new CreateLanguage { TwoLetterIsoCode = "vi", ThreeLetterIsoCode = "vie", ThreeLetterIsoBibliographicCode = "vie", };
//                _createLanguage.Handle(viCreate);
//                vi = viCreate.CreatedLanguage;
//            }

//            var vo = _queryProcessor.Execute(new LanguageByIsoCode("vo"));
//            if (vo == null)
//            {
//                var voCreate = new CreateLanguage { TwoLetterIsoCode = "vo", ThreeLetterIsoCode = "vol", ThreeLetterIsoBibliographicCode = "vol", };
//                _createLanguage.Handle(voCreate);
//                vo = voCreate.CreatedLanguage;
//            }

//            var wa = _queryProcessor.Execute(new LanguageByIsoCode("wa"));
//            if (wa == null)
//            {
//                var waCreate = new CreateLanguage { TwoLetterIsoCode = "wa", ThreeLetterIsoCode = "wln", ThreeLetterIsoBibliographicCode = "wln", };
//                _createLanguage.Handle(waCreate);
//                wa = waCreate.CreatedLanguage;
//            }

//            var wo = _queryProcessor.Execute(new LanguageByIsoCode("wo"));
//            if (wo == null)
//            {
//                var woCreate = new CreateLanguage { TwoLetterIsoCode = "wo", ThreeLetterIsoCode = "wol", ThreeLetterIsoBibliographicCode = "wol", };
//                _createLanguage.Handle(woCreate);
//                wo = woCreate.CreatedLanguage;
//            }

//            var xh = _queryProcessor.Execute(new LanguageByIsoCode("xh"));
//            if (xh == null)
//            {
//                var xhCreate = new CreateLanguage { TwoLetterIsoCode = "xh", ThreeLetterIsoCode = "xho", ThreeLetterIsoBibliographicCode = "xho", };
//                _createLanguage.Handle(xhCreate);
//                xh = xhCreate.CreatedLanguage;
//            }

//            var yi = _queryProcessor.Execute(new LanguageByIsoCode("yi"));
//            if (yi == null)
//            {
//                var yiCreate = new CreateLanguage { TwoLetterIsoCode = "yi", ThreeLetterIsoCode = "yid", ThreeLetterIsoBibliographicCode = "yid", };
//                _createLanguage.Handle(yiCreate);
//                yi = yiCreate.CreatedLanguage;
//            }

//            var yo = _queryProcessor.Execute(new LanguageByIsoCode("yo"));
//            if (yo == null)
//            {
//                var yoCreate = new CreateLanguage { TwoLetterIsoCode = "yo", ThreeLetterIsoCode = "yor", ThreeLetterIsoBibliographicCode = "yor", };
//                _createLanguage.Handle(yoCreate);
//                yo = yoCreate.CreatedLanguage;
//            }

//            var za = _queryProcessor.Execute(new LanguageByIsoCode("za"));
//            if (za == null)
//            {
//                var zaCreate = new CreateLanguage { TwoLetterIsoCode = "za", ThreeLetterIsoCode = "zha", ThreeLetterIsoBibliographicCode = "zha", };
//                _createLanguage.Handle(zaCreate);
//                za = zaCreate.CreatedLanguage;
//            }

//            var zh = _queryProcessor.Execute(new LanguageByIsoCode("zh"));
//            if (zh == null)
//            {
//                var zhCreate = new CreateLanguage { TwoLetterIsoCode = "zh", ThreeLetterIsoCode = "zho", ThreeLetterIsoBibliographicCode = "chi", };
//                _createLanguage.Handle(zhCreate);
//                zh = zhCreate.CreatedLanguage;
//            }

//            var zu = _queryProcessor.Execute(new LanguageByIsoCode("zu"));
//            if (zu == null)
//            {
//                var zuCreate = new CreateLanguage { TwoLetterIsoCode = "zu", ThreeLetterIsoCode = "zul", ThreeLetterIsoBibliographicCode = "zul", };
//                _createLanguage.Handle(zuCreate);
//                zu = zuCreate.CreatedLanguage;
//            }

//            _unitOfWork.SaveChanges();

//            #endregion

//            #region Language Names

//            //Languages with 61 translations

//            if (!en.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "English", TranslationToLanguageId = en.Id, },          // #1  Language = "English"
//                    new CreateLanguageName { Text = "Inglés", TranslationToLanguageId = es.Id, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Englisch", TranslationToLanguageId = de.Id, },         // #3  Language = "German"
//                    new CreateLanguageName { Text = "الإنجليزية", TranslationToLanguageId = ar.Id, },       // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Engels", TranslationToLanguageId = af.Id, },            // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İngilis dili", TranslationToLanguageId = az.Id, },      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "англійская", TranslationToLanguageId = be.Id, },        // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "английски език", TranslationToLanguageId = bg.Id, },    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইংরেজি", TranslationToLanguageId = bn.Id, },             // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "anglès", TranslationToLanguageId = ca.Id, },            // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "angličtina", TranslationToLanguageId = cs.Id, },        // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Saesneg", TranslationToLanguageId = cy.Id, },           // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "engelsk", TranslationToLanguageId = da.Id, },           // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "inglise", TranslationToLanguageId = et.Id, },           // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Ingelesa", TranslationToLanguageId = eu.Id, },          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "انگلیسی", TranslationToLanguageId = fa.Id, },          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "englanti", TranslationToLanguageId = fi.Id, },          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "anglaise", TranslationToLanguageId = fr.Id, },          // #18 Language = "French"
//                    new CreateLanguageName { Text = "Béarla", TranslationToLanguageId = ga.Id, },            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Inglés", TranslationToLanguageId = gl.Id, },            // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઇંગલિશ", TranslationToLanguageId = gu.Id, },            // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אנגלית", TranslationToLanguageId = he.Id, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अंग्रेज़ी", TranslationToLanguageId = hi.Id, },             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "engleski", TranslationToLanguageId = hr.Id, },          // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "angle", TranslationToLanguageId = ht.Id, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "angol", TranslationToLanguageId = hu.Id, },             // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "անգլերեն", TranslationToLanguageId = hy.Id, },          // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bahasa Inggris", TranslationToLanguageId = id.Id, },    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Enska", TranslationToLanguageId = isLanguage.Id, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "inglese", TranslationToLanguageId = it.Id, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "英語を", TranslationToLanguageId = ja.Id, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ინგლისური", TranslationToLanguageId = ka.Id, },       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಇಂಗ್ಲೀಷ್", TranslationToLanguageId = kn.Id, },            // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "영어", TranslationToLanguageId = ko.Id, },              // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "English", TranslationToLanguageId = la.Id, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "anglų", TranslationToLanguageId = lt.Id, },             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "angļu", TranslationToLanguageId = lv.Id, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "англиски јазик", TranslationToLanguageId = mk.Id, },    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Inggeris", TranslationToLanguageId = ms.Id, },   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ingliż", TranslationToLanguageId = mt.Id, },            // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Engels", TranslationToLanguageId = nl.Id, },            // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "engelsk", TranslationToLanguageId = no.Id, },           // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "angielski", TranslationToLanguageId = pl.Id, },         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "Inglês", TranslationToLanguageId = pt.Id, },            // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "englez", TranslationToLanguageId = ro.Id, },            // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "английский", TranslationToLanguageId = ru.Id, },        // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "angličtina", TranslationToLanguageId = sk.Id, },        // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "angleški", TranslationToLanguageId = sl.Id, },          // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "anglisht", TranslationToLanguageId = sq.Id, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "енглески", TranslationToLanguageId = sr.Id, },          // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "engelska", TranslationToLanguageId = sv.Id, },          // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiingereza", TranslationToLanguageId = sw.Id, },        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஆங்கிலம்", TranslationToLanguageId = ta.Id, },        // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఆంగ్ల", TranslationToLanguageId = te.Id, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาอังกฤษ", TranslationToLanguageId = th.Id, },          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İngilizce", TranslationToLanguageId = tr.Id, },         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Англійська", TranslationToLanguageId = uk.Id, },        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "انگریزی", TranslationToLanguageId = ur.Id, },          // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Anh", TranslationToLanguageId = vi.Id, },         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ענגליש", TranslationToLanguageId = yi.Id, },           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "英语", TranslationToLanguageId = zh.Id, },              // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = en.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!es.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = en.Id, },           // #1  Language = "English"
//                    new CreateLanguageName { Text = "Español", TranslationToLanguageId = es.Id, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Spanisch", TranslationToLanguageId = de.Id, },          // #3  Language = "German"
//                    new CreateLanguageName { Text = "الاسبانية", TranslationToLanguageId = ar.Id, },         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = af.Id, },            // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "ispan", TranslationToLanguageId = az.Id, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Іспанская", TranslationToLanguageId = be.Id, },          // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "испански", TranslationToLanguageId = bg.Id, },           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্পেনসম্পর্কিত", TranslationToLanguageId = bn.Id, },         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "espanyol", TranslationToLanguageId = ca.Id, },           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "španělština", TranslationToLanguageId = cs.Id, },        // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Sbaeneg", TranslationToLanguageId = cy.Id, },            // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "spansk", TranslationToLanguageId = da.Id, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hispaania", TranslationToLanguageId = et.Id, },          // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Espainiako", TranslationToLanguageId = eu.Id, },         // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اسپانیایی", TranslationToLanguageId = fa.Id, },         // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "espanjalainen", TranslationToLanguageId = fi.Id, },      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "espagnole", TranslationToLanguageId = fr.Id, },          // #18 Language = "French"
//                    new CreateLanguageName { Text = "Spáinnis", TranslationToLanguageId = ga.Id, },           // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "castelán", TranslationToLanguageId = gl.Id, },           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્પેનિશ", TranslationToLanguageId = gu.Id, },              // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ספרדית", TranslationToLanguageId = he.Id, },            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्पेनिश", TranslationToLanguageId = hi.Id, },              // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Španjolski", TranslationToLanguageId = hr.Id, },         // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Panyòl", TranslationToLanguageId = ht.Id, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "spanyol", TranslationToLanguageId = hu.Id, },            // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իսպաներեն", TranslationToLanguageId = hy.Id, },         // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Spanyol", TranslationToLanguageId = id.Id, },            // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Spænska", TranslationToLanguageId = isLanguage.Id, },    // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "spagnolo", TranslationToLanguageId = it.Id, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スペイン", TranslationToLanguageId = ja.Id, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ესპანური", TranslationToLanguageId = ka.Id, },           // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ಪ್ಯಾನಿಷ್", TranslationToLanguageId = kn.Id, },             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "스페인의", TranslationToLanguageId = ko.Id, },            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = la.Id, },            // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Ispanijos", TranslationToLanguageId = lt.Id, },          // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "spāņu", TranslationToLanguageId = lv.Id, },              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "шпански", TranslationToLanguageId = mk.Id, },            // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Sepanyol", TranslationToLanguageId = ms.Id, },    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Spanjol", TranslationToLanguageId = mt.Id, },            // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Spaans", TranslationToLanguageId = nl.Id, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = no.Id, },            // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "hiszpański", TranslationToLanguageId = pl.Id, },         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "hiszpański", TranslationToLanguageId = pt.Id, },         // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "spaniol", TranslationToLanguageId = ro.Id, },            // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "испанский", TranslationToLanguageId = ru.Id, },          // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "španielčina", TranslationToLanguageId = sk.Id, },        // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "španski", TranslationToLanguageId = sl.Id, },            // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "spanjisht", TranslationToLanguageId = sq.Id, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "шпански", TranslationToLanguageId = sr.Id, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "spanska", TranslationToLanguageId = sv.Id, },            // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kihispania", TranslationToLanguageId = sw.Id, },         // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்பானிஷ்", TranslationToLanguageId = ta.Id, },        // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్పానిష్", TranslationToLanguageId = te.Id, },              // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาสเปน", TranslationToLanguageId = th.Id, },            // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İspanyolca", TranslationToLanguageId = tr.Id, },         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Іспанська", TranslationToLanguageId = uk.Id, },          // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہسپانوی", TranslationToLanguageId = ur.Id, },           // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tây Ban Nha", TranslationToLanguageId = vi.Id, },        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = yi.Id, },            // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "西班牙", TranslationToLanguageId = zh.Id, },             // #61 Language = "Chinese"
//                 }
//                .ForEach(n =>
//                {
//                    n.LanguageId = es.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!de.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "German", TranslationToLanguageId = en.Id, },                          // #1  Language = "English"
//                    new CreateLanguageName { Text = "Alemán", TranslationToLanguageId = es.Id, },                          // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Deutsch", TranslationToLanguageId = de.Id, },                         // #3  Language = "German"
//                    new CreateLanguageName { Text = "الألمانية", TranslationToLanguageId = ar.Id, },                       // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Duits", TranslationToLanguageId = af.Id, },                            // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "alman", TranslationToLanguageId = az.Id, },                             // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Нямецкі", TranslationToLanguageId = be.Id, },                          // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "немски", TranslationToLanguageId = bg.Id, },                           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "জার্মান", TranslationToLanguageId = bn.Id, },                             // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "alemany", TranslationToLanguageId = ca.Id, },                          // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Němec", TranslationToLanguageId = cs.Id, },                            // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Almaeneg", TranslationToLanguageId = cy.Id, },                         // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "tysk", TranslationToLanguageId = da.Id, },                             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "saksa", TranslationToLanguageId = et.Id, },                            // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Alemana", TranslationToLanguageId = eu.Id, },                          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آلمانی", TranslationToLanguageId = fa.Id, },                          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "saksa", TranslationToLanguageId = fi.Id, },                            // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "allemande", TranslationToLanguageId = fr.Id, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Gearmáinis", TranslationToLanguageId = ga.Id, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Alemán", TranslationToLanguageId = gl.Id, },                           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "જર્મન ભાષા", TranslationToLanguageId = gu.Id, },                        // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גרמנית", TranslationToLanguageId = he.Id, },                          // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "जर्मन", TranslationToLanguageId = hi.Id, },                             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "njemački", TranslationToLanguageId = hr.Id, },                         // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Alman", TranslationToLanguageId = ht.Id, },                            // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "német", TranslationToLanguageId = hu.Id, },                            // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "գերմաներեն", TranslationToLanguageId = hy.Id, },                      // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Jerman", TranslationToLanguageId = id.Id, },                           // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Þýska", TranslationToLanguageId = isLanguage.Id, },                    // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "tedesco", TranslationToLanguageId = it.Id, },                          // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ドイツ", TranslationToLanguageId = ja.Id, },                           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "გერმანული", TranslationToLanguageId = ka.Id, },                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ವಂತ", TranslationToLanguageId = kn.Id, },                             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "독일의", TranslationToLanguageId = ko.Id, },                           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Germanica", TranslationToLanguageId = la.Id, },                        // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Vokietijos", TranslationToLanguageId = lt.Id, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "vācu", TranslationToLanguageId = lv.Id, },                             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "германски", TranslationToLanguageId = mk.Id, },                        // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Jerman", TranslationToLanguageId = ms.Id, },                           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ġermaniż", TranslationToLanguageId = mt.Id, },                         // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Duits", TranslationToLanguageId = nl.Id, },                            // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "tyske", TranslationToLanguageId = no.Id, },                            // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "niemiecki", TranslationToLanguageId = pl.Id, },                        // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "alemão", TranslationToLanguageId = pt.Id, },                           // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "german", TranslationToLanguageId = ro.Id, },                           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "немецкий", TranslationToLanguageId = ru.Id, },                         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Nemec", TranslationToLanguageId = sk.Id, },                            // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "nemška", TranslationToLanguageId = sl.Id, },                           // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "gjermanisht", TranslationToLanguageId = sq.Id, },                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "немачки", TranslationToLanguageId = sr.Id, },                          // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "tyska", TranslationToLanguageId = sv.Id, },                            // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Ujerumani", TranslationToLanguageId = sw.Id, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஜெர்மனி நாட்டை சார்ந்தவர்", TranslationToLanguageId = ta.Id, },    // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "సన్నిహిత", TranslationToLanguageId = te.Id, },                          // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาเยอรมัน", TranslationToLanguageId = th.Id, },                         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Alman", TranslationToLanguageId = tr.Id, },                            // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "німецький", TranslationToLanguageId = uk.Id, },                        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "جرمن", TranslationToLanguageId = ur.Id, },                            // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Đức", TranslationToLanguageId = vi.Id, },                              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "דייַטש", TranslationToLanguageId = yi.Id, },                           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "德国", TranslationToLanguageId = zh.Id, },                             // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = de.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ar.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = en.Id, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "Árabe", TranslationToLanguageId = es.Id, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Arabisch", TranslationToLanguageId = de.Id, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "العربية", TranslationToLanguageId = ar.Id, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Arabies", TranslationToLanguageId = af.Id, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Ərəb", TranslationToLanguageId = az.Id, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Арабская", TranslationToLanguageId = be.Id, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "арабски", TranslationToLanguageId = bg.Id, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আরবি", TranslationToLanguageId = bn.Id, },                  // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "àrab", TranslationToLanguageId = ca.Id, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "arabština", TranslationToLanguageId = cs.Id, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Arabeg", TranslationToLanguageId = cy.Id, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "arabic", TranslationToLanguageId = da.Id, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "araabia", TranslationToLanguageId = et.Id, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = eu.Id, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "عربی", TranslationToLanguageId = fa.Id, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Arabia", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "arabe", TranslationToLanguageId = fr.Id, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Araibis", TranslationToLanguageId = ga.Id, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Árabe", TranslationToLanguageId = gl.Id, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "અરબી ભાષા", TranslationToLanguageId = gu.Id, },            // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ערבית", TranslationToLanguageId = he.Id, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अरबी भाषा", TranslationToLanguageId = hi.Id, },              // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "arapski", TranslationToLanguageId = hr.Id, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "arab", TranslationToLanguageId = ht.Id, },                  // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "arab", TranslationToLanguageId = hu.Id, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "արաբական", TranslationToLanguageId = hy.Id, },            // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Arab", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "arabíska", TranslationToLanguageId = isLanguage.Id, },      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "arabo", TranslationToLanguageId = it.Id, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アラビア", TranslationToLanguageId = ja.Id, },               // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "არაბული", TranslationToLanguageId = ka.Id, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅರಬ್ಬಿ ಭಾಷೆಯ", TranslationToLanguageId = kn.Id, },           // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아랍어", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = la.Id, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "arabų", TranslationToLanguageId = lt.Id, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "arābu", TranslationToLanguageId = lv.Id, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "арапски", TranslationToLanguageId = mk.Id, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Arab", TranslationToLanguageId = ms.Id, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Għarbi", TranslationToLanguageId = mt.Id, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Arabisch", TranslationToLanguageId = nl.Id, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "arabisk", TranslationToLanguageId = no.Id, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "arabski", TranslationToLanguageId = pl.Id, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "árabe", TranslationToLanguageId = pt.Id, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba arabă", TranslationToLanguageId = ro.Id, },           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "арабский", TranslationToLanguageId = ru.Id, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "arabčina", TranslationToLanguageId = sk.Id, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = sl.Id, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "arab", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "арапски", TranslationToLanguageId = sr.Id, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Arabiska", TranslationToLanguageId = sv.Id, },              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiarabu", TranslationToLanguageId = sw.Id, },               // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "அரபு", TranslationToLanguageId = ta.Id, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "అరబిక్", TranslationToLanguageId = te.Id, },                 // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาอาหรับ", TranslationToLanguageId = th.Id, },              // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Arapça", TranslationToLanguageId = tr.Id, },                // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Арабська", TranslationToLanguageId = uk.Id, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "عربی", TranslationToLanguageId = ur.Id, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Ả Rập", TranslationToLanguageId = vi.Id, },           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַראַביש", TranslationToLanguageId = yi.Id, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "阿拉伯语", TranslationToLanguageId = zh.Id, },               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ar.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (!af.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = en.Id, },                      // #1  Language = "English"
//                    new CreateLanguageName { Text = "africaans", TranslationToLanguageId = es.Id, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = de.Id, },                       // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأفريكانية", TranslationToLanguageId = ar.Id, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = af.Id, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = az.Id, },                       // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "афрыкаанс", TranslationToLanguageId = be.Id, },                        // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "африканс", TranslationToLanguageId = bg.Id, },                         // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আফ্রিকার অন্যতম সরকারি ভাষা", TranslationToLanguageId = bn.Id, },         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Africaans", TranslationToLanguageId = ca.Id, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "afrikánština", TranslationToLanguageId = cs.Id, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Affricaneg", TranslationToLanguageId = cy.Id, },                       // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = da.Id, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "afrikaani", TranslationToLanguageId = et.Id, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = eu.Id, },                        // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آفریکانس", TranslationToLanguageId = fa.Id, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = fi.Id, },                        // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = fr.Id, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = ga.Id, },                        // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = gl.Id, },                        // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આફ્રિકી", TranslationToLanguageId = gu.Id, },                            // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אפריקאנס", TranslationToLanguageId = he.Id, },                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "दक्षिण अफ्रीकी अथवा केप डच", TranslationToLanguageId = hi.Id, },          // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = hr.Id, },                        // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Kreyòl ayisyen", TranslationToLanguageId = ht.Id, },                   // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = hu.Id, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "աֆրիկանս", TranslationToLanguageId = hy.Id, },                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Afrikanas", TranslationToLanguageId = id.Id, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = isLanguage.Id, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = it.Id, },                        // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アフリカーンス", TranslationToLanguageId = ja.Id, },                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "აფრიკაანსი", TranslationToLanguageId = ka.Id, },                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಆಫ್ರಿಕಾನ್ಸ್", TranslationToLanguageId = kn.Id, },                          // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아프리카 어", TranslationToLanguageId = ko.Id, },                       // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Africanica", TranslationToLanguageId = la.Id, },                       // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "afrikanų", TranslationToLanguageId = lt.Id, },                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "afrikandu", TranslationToLanguageId = lv.Id, },                        // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "африканс", TranslationToLanguageId = mk.Id, },                         // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Afrikaan", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = mt.Id, },                        // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = nl.Id, },                        // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = no.Id, },                        // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = pl.Id, },                        // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = pt.Id, },                        // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = ro.Id, },                        // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "африкаанс", TranslationToLanguageId = ru.Id, },                        // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Afrikánčina", TranslationToLanguageId = sk.Id, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = sl.Id, },                        // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = sq.Id, },                        // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "африканс", TranslationToLanguageId = sr.Id, },                         // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = sv.Id, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiafrikana", TranslationToLanguageId = sw.Id, },                       // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஆஃப்ரிகான்ஸ்", TranslationToLanguageId = ta.Id, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఆఫ్రికాన్స్", TranslationToLanguageId = te.Id, },                           // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "แอฟริกา", TranslationToLanguageId = th.Id, },                            // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = tr.Id, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Африкаанс", TranslationToLanguageId = uk.Id, },                        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ايفريکانز", TranslationToLanguageId = ur.Id, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "thứ tiếng", TranslationToLanguageId = vi.Id, },                        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַפֿריקאַנס", TranslationToLanguageId = yi.Id, },                        // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "南非语", TranslationToLanguageId = zh.Id, },                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = af.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!az.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = en.Id, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "Azerbaiyán", TranslationToLanguageId = es.Id, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = de.Id, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأذربيجانية", TranslationToLanguageId = ar.Id, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Aserbeidjans", TranslationToLanguageId = af.Id, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Azərbaycan", TranslationToLanguageId = az.Id, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "азербайджанскі", TranslationToLanguageId = be.Id, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "азербайджански", TranslationToLanguageId = bg.Id, },              // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আজেরবাইজান", TranslationToLanguageId = bn.Id, },                  // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Azerbaidjan", TranslationToLanguageId = ca.Id, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "ázerbájdžánský", TranslationToLanguageId = cs.Id, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = cy.Id, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "aserbajdsjanske", TranslationToLanguageId = da.Id, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "aserbaidžaani", TranslationToLanguageId = et.Id, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = eu.Id, },                 // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آذربایجان", TranslationToLanguageId = fa.Id, },                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Azerbaidžanin", TranslationToLanguageId = fi.Id, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "azerbaïdjanais", TranslationToLanguageId = fr.Id, },              // #18 Language = "French"
//                    new CreateLanguageName { Text = "Asarbaiseáinis", TranslationToLanguageId = ga.Id, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Azerbaiano", TranslationToLanguageId = gl.Id, },                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "અઝરબૈજાની", TranslationToLanguageId = gu.Id, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אזרביג'אן", TranslationToLanguageId = he.Id, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आज़रबाइजानी", TranslationToLanguageId = hi.Id, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Azarbejdžanac", TranslationToLanguageId = hr.Id, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Azerbaydjan", TranslationToLanguageId = ht.Id, },                 // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "azerbajdzsáni", TranslationToLanguageId = hu.Id, },               // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Ադրբեջանի", TranslationToLanguageId = hy.Id, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Azerbaijan", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Aserbaídsjan", TranslationToLanguageId = isLanguage.Id, },        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "azero", TranslationToLanguageId = it.Id, },                       // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アゼルバイジャン語", TranslationToLanguageId = ja.Id, },           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "აზერბაიჯანის", TranslationToLanguageId = ka.Id, },               // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅಜರ್ಬೈಜಾನಿ", TranslationToLanguageId = kn.Id, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아제르바 이잔", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Azeriana", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Azerbaidžano", TranslationToLanguageId = lt.Id, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Azerbaidžānas", TranslationToLanguageId = lv.Id, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Азербејџан", TranslationToLanguageId = mk.Id, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Azerbaijan", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ażerbajġan", TranslationToLanguageId = mt.Id, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Azerbeidzjaanse", TranslationToLanguageId = nl.Id, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "aserbajdsjanske", TranslationToLanguageId = no.Id, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "azerbejdżański", TranslationToLanguageId = pl.Id, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "azerbaijano", TranslationToLanguageId = pt.Id, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Azerbaidjan", TranslationToLanguageId = ro.Id, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "азербайджанский", TranslationToLanguageId = ru.Id, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "azerbajdžanský", TranslationToLanguageId = sk.Id, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "azerbajdžanski", TranslationToLanguageId = sl.Id, },              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = sq.Id, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "азербејџански", TranslationToLanguageId = sr.Id, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "azerbajdzjanska", TranslationToLanguageId = sv.Id, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiazabaijani", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "அஜர்பைஜானி", TranslationToLanguageId = ta.Id, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బైజాన్", TranslationToLanguageId = te.Id, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวอาเซร์ไบจัน", TranslationToLanguageId = th.Id, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Azeri", TranslationToLanguageId = tr.Id, },                       // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "азербайджанський", TranslationToLanguageId = uk.Id, },            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آذربائیجان", TranslationToLanguageId = ur.Id, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Azerbaijan", TranslationToLanguageId = vi.Id, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַזערבייַדזאַניש", TranslationToLanguageId = yi.Id, },             // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "阿塞拜疆", TranslationToLanguageId = zh.Id, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = az.Id;
//                    _createLanguageName.Handle(n);
//                });



//            if (!be.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = en.Id, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "bielorruso", TranslationToLanguageId = es.Id, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "belarussischen", TranslationToLanguageId = de.Id, },            // #3  Language = "German"
//                    new CreateLanguageName { Text = "البيلاروسية", TranslationToLanguageId = ar.Id, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Wit", TranslationToLanguageId = af.Id, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "AzəBelarus", TranslationToLanguageId = az.Id, },                // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Беларуская", TranslationToLanguageId = be.Id, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "Беларус", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "বেলারুশীয়", TranslationToLanguageId = bn.Id, },                  // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "bielorús", TranslationToLanguageId = ca.Id, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "běloruské", TranslationToLanguageId = cs.Id, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Belarwseg", TranslationToLanguageId = cy.Id, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "belarussiske", TranslationToLanguageId = da.Id, },              // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Valgevene", TranslationToLanguageId = et.Id, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = eu.Id, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "بلاروس", TranslationToLanguageId = fa.Id, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Valko-Venäjän", TranslationToLanguageId = fi.Id, },             // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "biélorusses", TranslationToLanguageId = fr.Id, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Bealarúisis", TranslationToLanguageId = ga.Id, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = gl.Id, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "બેલારુસિયન", TranslationToLanguageId = gu.Id, },                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "בלארוסית", TranslationToLanguageId = he.Id, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बेलारूसी", TranslationToLanguageId = hi.Id, },                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "bjeloruski", TranslationToLanguageId = hr.Id, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Belarisyen", TranslationToLanguageId = ht.Id, },                // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "fehérorosz", TranslationToLanguageId = hu.Id, },                // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բելարուսերեն", TranslationToLanguageId = hy.Id, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = id.Id, },                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "hvítrússneska", TranslationToLanguageId = isLanguage.Id, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "bielorusso", TranslationToLanguageId = it.Id, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ベラルーシ語", TranslationToLanguageId = ja.Id, },               // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბელორუსის", TranslationToLanguageId = ka.Id, },               // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಬೆಲರೂಸಿಯನ್", TranslationToLanguageId = kn.Id, },                // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "벨라루스어", TranslationToLanguageId = ko.Id, },                 // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Belarusica", TranslationToLanguageId = la.Id, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Baltarusijos", TranslationToLanguageId = lt.Id, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Baltkrievijas", TranslationToLanguageId = lv.Id, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "белоруски", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = ms.Id, },                   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = mt.Id, },                   // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Wit-Russische", TranslationToLanguageId = nl.Id, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = no.Id, },                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Białorusi", TranslationToLanguageId = pl.Id, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = pt.Id, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = ro.Id, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "Белорусская", TranslationToLanguageId = ru.Id, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "bieloruskej", TranslationToLanguageId = sk.Id, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "beloruski", TranslationToLanguageId = sl.Id, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = sq.Id, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "беларусиан", TranslationToLanguageId = sr.Id, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "vitryska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kibelarusi", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பெலாரஷ்யன்", TranslationToLanguageId = ta.Id, },            // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బెలారుషియన్", TranslationToLanguageId = te.Id, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เบลารุส", TranslationToLanguageId = th.Id, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Belarusça", TranslationToLanguageId = tr.Id, },                 // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Білоруська", TranslationToLanguageId = uk.Id, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "بیلاروسی", TranslationToLanguageId = ur.Id, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = vi.Id, },                   // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "בעלאָרוסיש", TranslationToLanguageId = yi.Id, },                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "白俄罗斯", TranslationToLanguageId = zh.Id, },                   // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = be.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!bg.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = en.Id, },             // #1  Language = "English"
//                    new CreateLanguageName { Text = "búlgaro", TranslationToLanguageId = es.Id, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Bulgarisch", TranslationToLanguageId = de.Id, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "البلغارية", TranslationToLanguageId = ar.Id, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Bulgaars", TranslationToLanguageId = af.Id, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Bolqar", TranslationToLanguageId = az.Id, },                 // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Балгарская", TranslationToLanguageId = be.Id, },             // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "български", TranslationToLanguageId = bg.Id, },              // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "বুলগেরীয়", TranslationToLanguageId = bn.Id, },                // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Búlgar", TranslationToLanguageId = ca.Id, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "bulharský", TranslationToLanguageId = cs.Id, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Bwlgareg", TranslationToLanguageId = cy.Id, },               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "bulgarian", TranslationToLanguageId = da.Id, },              // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "bulgaaria", TranslationToLanguageId = et.Id, },              // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = eu.Id, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "بلغاری", TranslationToLanguageId = fa.Id, },                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = fi.Id, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "bulgares", TranslationToLanguageId = fr.Id, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Bulgáiris", TranslationToLanguageId = ga.Id, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Búlgaro", TranslationToLanguageId = gl.Id, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "બલ્ગેરિયન", TranslationToLanguageId = gu.Id, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "בולגרית", TranslationToLanguageId = he.Id, },               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बल्गेरियाई", TranslationToLanguageId = hi.Id, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "bugarski", TranslationToLanguageId = hr.Id, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "bulgarian", TranslationToLanguageId = ht.Id, },              // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "bolgár", TranslationToLanguageId = hu.Id, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բուլղարացի", TranslationToLanguageId = hy.Id, },             // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bulgaria", TranslationToLanguageId = id.Id, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Búlgaríu", TranslationToLanguageId = isLanguage.Id, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "bulgaro", TranslationToLanguageId = it.Id, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ブルガリア語", TranslationToLanguageId = ja.Id, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბულგარეთის", TranslationToLanguageId = ka.Id, },           // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅಲ್ಲಿಯ ಭಾಷೆ", TranslationToLanguageId = kn.Id, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "불가리아 사람", TranslationToLanguageId = ko.Id, },           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Bulgarica", TranslationToLanguageId = la.Id, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "bulgarų", TranslationToLanguageId = lt.Id, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Bulgārijas", TranslationToLanguageId = lv.Id, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Бугарија", TranslationToLanguageId = mk.Id, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bulgaria", TranslationToLanguageId = ms.Id, },               // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Bulgaru", TranslationToLanguageId = mt.Id, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "bulgarian", TranslationToLanguageId = nl.Id, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "bulgarske", TranslationToLanguageId = no.Id, },              // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "bułgarski", TranslationToLanguageId = pl.Id, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "búlgaro", TranslationToLanguageId = pt.Id, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba bulgară", TranslationToLanguageId = ro.Id, },          // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "болгарский", TranslationToLanguageId = ru.Id, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "bulharský", TranslationToLanguageId = sk.Id, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "bolgarski", TranslationToLanguageId = sl.Id, },              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "bullgare", TranslationToLanguageId = sq.Id, },               // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "бугарски", TranslationToLanguageId = sr.Id, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "bulgariska", TranslationToLanguageId = sv.Id, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = sw.Id, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பல்கேரியன்", TranslationToLanguageId = ta.Id, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బల్గేరియన్", TranslationToLanguageId = te.Id, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "บัลแกเรีย", TranslationToLanguageId = th.Id, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Bulgar", TranslationToLanguageId = tr.Id, },                 // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Болгарська", TranslationToLanguageId = uk.Id, },             // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "بلغارین", TranslationToLanguageId = ur.Id, },               // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Bun-ga-ri", TranslationToLanguageId = vi.Id, },              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "בולגאַריש", TranslationToLanguageId = yi.Id, },              // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "保加利亚语", TranslationToLanguageId = zh.Id, },              // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bg.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (!bn.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = en.Id, },            // #1  Language = "English"
//                    new CreateLanguageName { Text = "bengalí", TranslationToLanguageId = es.Id, },             // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = de.Id, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "بنغالي", TranslationToLanguageId = ar.Id, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = af.Id, },             // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Benqal", TranslationToLanguageId = az.Id, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "бенгальскі", TranslationToLanguageId = be.Id, },          // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "бенгалски", TranslationToLanguageId = bg.Id, },           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "বাঙ্গালী", TranslationToLanguageId = bn.Id, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "bengalí", TranslationToLanguageId = ca.Id, },             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "bengálský", TranslationToLanguageId = cs.Id, },           // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = cy.Id, },             // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = da.Id, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = et.Id, },             // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = eu.Id, },             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "بنگالی", TranslationToLanguageId = fa.Id, },             // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = fi.Id, },             // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = fr.Id, },             // #18 Language = "French"
//                    new CreateLanguageName { Text = "Beangáilis", TranslationToLanguageId = ga.Id, },          // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "bengalí", TranslationToLanguageId = gl.Id, },             // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "બંગાળનું", TranslationToLanguageId = gu.Id, },              // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "בנגלית", TranslationToLanguageId = he.Id, },             // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बंगाली", TranslationToLanguageId = hi.Id, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "bengalski", TranslationToLanguageId = hr.Id, },           // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = ht.Id, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "bengáli", TranslationToLanguageId = hu.Id, },             // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բենգալերեն", TranslationToLanguageId = hy.Id, },          // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = id.Id, },             // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = isLanguage.Id, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "bengalese", TranslationToLanguageId = it.Id, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ベンガル語", TranslationToLanguageId = ja.Id, },           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბენგალური", TranslationToLanguageId = ka.Id, },          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಬಂಗಾಳಿ", TranslationToLanguageId = kn.Id, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "벵골 사람", TranslationToLanguageId = ko.Id, },            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Bengalica", TranslationToLanguageId = la.Id, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "bengalų", TranslationToLanguageId = lt.Id, },             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "bengāļu", TranslationToLanguageId = lv.Id, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "бенгалски", TranslationToLanguageId = mk.Id, },           // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = ms.Id, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = mt.Id, },             // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Bengalees", TranslationToLanguageId = nl.Id, },           // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = no.Id, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "bengalski", TranslationToLanguageId = pl.Id, },           // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = pt.Id, },             // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = ro.Id, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "бенгальский", TranslationToLanguageId = ru.Id, },         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "bengálsky", TranslationToLanguageId = sk.Id, },           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = sl.Id, },             // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "bengalisht", TranslationToLanguageId = sq.Id, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "бенгалски", TranslationToLanguageId = sr.Id, },           // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = sv.Id, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kibengali", TranslationToLanguageId = sw.Id, },           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பெங்காலி", TranslationToLanguageId = ta.Id, },          // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బెంగాలీ", TranslationToLanguageId = te.Id, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เบงกาลี", TranslationToLanguageId = th.Id, },               // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Bengal", TranslationToLanguageId = tr.Id, },              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "бенгальська", TranslationToLanguageId = uk.Id, },         // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "بنگالی", TranslationToLanguageId = ur.Id, },             // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = vi.Id, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "בענגאַליש", TranslationToLanguageId = yi.Id, },           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "孟加拉", TranslationToLanguageId = zh.Id, },              // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bn.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (!ca.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Catalan", TranslationToLanguageId = en.Id, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "catalán", TranslationToLanguageId = es.Id, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Katalanisch", TranslationToLanguageId = de.Id, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "الكاتالونية", TranslationToLanguageId = ar.Id, },              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Katalaans", TranslationToLanguageId = af.Id, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = az.Id, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Каталонскі", TranslationToLanguageId = be.Id, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "каталонски", TranslationToLanguageId = bg.Id, },                 // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "কাটালান", TranslationToLanguageId = bn.Id, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "català", TranslationToLanguageId = ca.Id, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "katalánština", TranslationToLanguageId = cs.Id, },               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Catalaneg", TranslationToLanguageId = cy.Id, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "catalansk", TranslationToLanguageId = da.Id, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Katalaani", TranslationToLanguageId = et.Id, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Katalana", TranslationToLanguageId = eu.Id, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کاتالان", TranslationToLanguageId = fa.Id, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "katalaani", TranslationToLanguageId = fi.Id, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "catalane", TranslationToLanguageId = fr.Id, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Catalóinis", TranslationToLanguageId = ga.Id, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Catalán", TranslationToLanguageId = gl.Id, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કેટાલન", TranslationToLanguageId = gu.Id, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קטלאנית", TranslationToLanguageId = he.Id, },                   // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "कैटलन", TranslationToLanguageId = hi.Id, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Catalan", TranslationToLanguageId = hr.Id, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "katalán", TranslationToLanguageId = hu.Id, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "կատալոներեն", TranslationToLanguageId = hy.Id, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = id.Id, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "katalónska", TranslationToLanguageId = isLanguage.Id, },         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "catalano", TranslationToLanguageId = it.Id, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "カタロニア語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "კატალანური", TranslationToLanguageId = ka.Id, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕ್ಯಾಟಲಾನ್", TranslationToLanguageId = kn.Id, },                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "카탈로니아의", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Catalana", TranslationToLanguageId = la.Id, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "katalonų", TranslationToLanguageId = lt.Id, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Katalāņu", TranslationToLanguageId = lv.Id, },                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "каталонски", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Catalan", TranslationToLanguageId = ms.Id, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = mt.Id, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "catalan", TranslationToLanguageId = nl.Id, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "katalansk", TranslationToLanguageId = no.Id, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Kataloński", TranslationToLanguageId = pl.Id, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "catalão", TranslationToLanguageId = pt.Id, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "catalan", TranslationToLanguageId = ro.Id, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "каталонский", TranslationToLanguageId = ru.Id, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Katalánsky", TranslationToLanguageId = sk.Id, },                 // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Katalonski", TranslationToLanguageId = sl.Id, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Katalonje", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "каталонски", TranslationToLanguageId = sr.Id, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "katalanska", TranslationToLanguageId = sv.Id, },                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kikatalani", TranslationToLanguageId = sw.Id, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கடாலன்", TranslationToLanguageId = ta.Id, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "కాటలాన్", TranslationToLanguageId = te.Id, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "คาตาลัน", TranslationToLanguageId = th.Id, },                      // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = tr.Id, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Каталонський", TranslationToLanguageId = uk.Id, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کاٹالانين", TranslationToLanguageId = ur.Id, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Catalan", TranslationToLanguageId = vi.Id, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קאַטאַלאַניש", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "加泰罗尼亚", TranslationToLanguageId = zh.Id, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ca.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!cs.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = en.Id, },                                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "checo", TranslationToLanguageId = es.Id, },                                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Tschechisch", TranslationToLanguageId = de.Id, },                              // #3  Language = "German"
//                    new CreateLanguageName { Text = "التشيكية", TranslationToLanguageId = ar.Id, },                                // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Tsjeggies", TranslationToLanguageId = af.Id, },                                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Çex", TranslationToLanguageId = az.Id, },                                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Чэшскі", TranslationToLanguageId = be.Id, },                                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "чешки", TranslationToLanguageId = bg.Id, },                                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "চেকোশ্লোভাকিয়াবাসী স্লাভজাতির একটি শাখার লোক", TranslationToLanguageId = bn.Id, },    // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "txec", TranslationToLanguageId = ca.Id, },                                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "český", TranslationToLanguageId = cs.Id, },                                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Tsiec", TranslationToLanguageId = cy.Id, },                                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "tjekkisk", TranslationToLanguageId = da.Id, },                                 // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "tšehhi", TranslationToLanguageId = et.Id, },                                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Txekiar", TranslationToLanguageId = eu.Id, },                                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "چک", TranslationToLanguageId = fa.Id, },                                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Tšekin", TranslationToLanguageId = fi.Id, },                                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "tchèque", TranslationToLanguageId = fr.Id, },                                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "na Seice", TranslationToLanguageId = ga.Id, },                                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Checo", TranslationToLanguageId = gl.Id, },                                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ચેક", TranslationToLanguageId = gu.Id, },                                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "צ", TranslationToLanguageId = he.Id, },                                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "चेक", TranslationToLanguageId = hi.Id, },                                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "češki", TranslationToLanguageId = hr.Id, },                                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "czech", TranslationToLanguageId = ht.Id, },                                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "cseh", TranslationToLanguageId = hu.Id, },                                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "չեխ", TranslationToLanguageId = hy.Id, },                                      // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Ceko", TranslationToLanguageId = id.Id, },                                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Tékkland", TranslationToLanguageId = isLanguage.Id, },                         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ceco", TranslationToLanguageId = it.Id, },                                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "チェコ", TranslationToLanguageId = ja.Id, },                                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჩეხეთის", TranslationToLanguageId = ka.Id, },                                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಜೆಕ್", TranslationToLanguageId = kn.Id, },                                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "체코어", TranslationToLanguageId = ko.Id, },                                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Bohemica", TranslationToLanguageId = la.Id, },                                 // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Čekijos", TranslationToLanguageId = lt.Id, },                                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Čehijas", TranslationToLanguageId = lv.Id, },                                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Чешка", TranslationToLanguageId = mk.Id, },                                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = ms.Id, },                                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ċeka", TranslationToLanguageId = mt.Id, },                                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Tsjechisch", TranslationToLanguageId = nl.Id, },                               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "tsjekkiske", TranslationToLanguageId = no.Id, },                               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = pl.Id, },                                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "tcheco", TranslationToLanguageId = pt.Id, },                                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "ceh", TranslationToLanguageId = ro.Id, },                                      // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "чешский", TranslationToLanguageId = ru.Id, },                                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Český", TranslationToLanguageId = sk.Id, },                                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Češki", TranslationToLanguageId = sl.Id, },                                    // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "çek", TranslationToLanguageId = sq.Id, },                                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "чешки", TranslationToLanguageId = sr.Id, },                                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Tjeckien", TranslationToLanguageId = sv.Id, },                                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = sw.Id, },                                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "செக்", TranslationToLanguageId = ta.Id, },                                     // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "చెక్", TranslationToLanguageId = te.Id, },                                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "สาธารณรัฐเช็ก", TranslationToLanguageId = th.Id, },                                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Çek", TranslationToLanguageId = tr.Id, },                                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "чеський", TranslationToLanguageId = uk.Id, },                                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "چیک", TranslationToLanguageId = ur.Id, },                                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Séc", TranslationToLanguageId = vi.Id, },                                      // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טשעכיש", TranslationToLanguageId = yi.Id, },                                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "捷克", TranslationToLanguageId = zh.Id, },                                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cs.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (!cy.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = en.Id, },                                                // #1  Language = "English"
//                    new CreateLanguageName { Text = "galés", TranslationToLanguageId = es.Id, },                                                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Walisisch", TranslationToLanguageId = de.Id, },                                             // #3  Language = "German"
//                    new CreateLanguageName { Text = "تهرب من دفع الرهان", TranslationToLanguageId = ar.Id, },                                  // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Walliese", TranslationToLanguageId = af.Id, },                                              // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Uels", TranslationToLanguageId = az.Id, },                                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Валійская", TranslationToLanguageId = be.Id, },                                             // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "уелски", TranslationToLanguageId = bg.Id, },                                                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ত্তয়েল্স্দেশসম্বন্ধীয়", TranslationToLanguageId = bn.Id, },                                          // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Gal · lès", TranslationToLanguageId = ca.Id, },                                             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "velšský", TranslationToLanguageId = cs.Id, },                                               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Cymraeg", TranslationToLanguageId = cy.Id, },                                               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "walisisk", TranslationToLanguageId = da.Id, },                                              // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "võlga maksmata jätma", TranslationToLanguageId = et.Id, },                                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Galesera", TranslationToLanguageId = eu.Id, },                                              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زیر قول زدن", TranslationToLanguageId = fa.Id, },                                          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Walesin", TranslationToLanguageId = fi.Id, },                                               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Gallois", TranslationToLanguageId = fr.Id, },                                               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Breatnais", TranslationToLanguageId = ga.Id, },                                             // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Galés", TranslationToLanguageId = gl.Id, },                                                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કરારભંગ કરવો", TranslationToLanguageId = gu.Id, },                                           // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "וולשית", TranslationToLanguageId = he.Id, },                                               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "वेल्श", TranslationToLanguageId = hi.Id, },                                                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "velški", TranslationToLanguageId = hr.Id, },                                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "welsh", TranslationToLanguageId = ht.Id, },                                                 // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "walesi", TranslationToLanguageId = hu.Id, },                                                // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ուելսացի", TranslationToLanguageId = hy.Id, },                                              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = id.Id, },                                                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "velska", TranslationToLanguageId = isLanguage.Id, },                                        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "gallese", TranslationToLanguageId = it.Id, },                                               // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ウェールズ語", TranslationToLanguageId = ja.Id, },                                           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "უელსური", TranslationToLanguageId = ka.Id, },                                             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ವೇಲ್ಸಿನ ಜನರು ಯಾ ಭಾಷೆ", TranslationToLanguageId = kn.Id, },                                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "웨일스 사람", TranslationToLanguageId = ko.Id, },                                            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = la.Id, },                                                 // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Velso", TranslationToLanguageId = lt.Id, },                                                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "velsiešu", TranslationToLanguageId = lv.Id, },                                              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "велшкиот", TranslationToLanguageId = mk.Id, },                                              // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = ms.Id, },                                                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = mt.Id, },                                                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "van Wales", TranslationToLanguageId = nl.Id, },                                             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "walisisk", TranslationToLanguageId = no.Id, },                                              // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "walijski", TranslationToLanguageId = pl.Id, },                                              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "galês", TranslationToLanguageId = pt.Id, },                                                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = ro.Id, },                                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "валлийский", TranslationToLanguageId = ru.Id, },                                            // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "waleský", TranslationToLanguageId = sk.Id, },                                               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "valižanščina", TranslationToLanguageId = sl.Id, },                                          // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "shkel premtimin", TranslationToLanguageId = sq.Id, },                                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "велшки", TranslationToLanguageId = sr.Id, },                                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "walesiska", TranslationToLanguageId = sv.Id, },                                             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = sw.Id, },                                                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "குதிரை பந்தயத்தில் பணம் கொடுக்காது ஓடி விடு", TranslationToLanguageId = ta.Id, },    // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "వెల్ష్", TranslationToLanguageId = te.Id, },                                                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาชาวเวลส์", TranslationToLanguageId = th.Id, },                                             // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "şartları yerine getirmemek", TranslationToLanguageId = tr.Id, },                            // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Валлійська", TranslationToLanguageId = uk.Id, },                                            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ويلش", TranslationToLanguageId = ur.Id, },                                                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Wales", TranslationToLanguageId = vi.Id, },                                           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "וועלש", TranslationToLanguageId = yi.Id, },                                                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "威尔士", TranslationToLanguageId = zh.Id, },                                                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cy.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!da.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Danish", TranslationToLanguageId = en.Id, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "danés", TranslationToLanguageId = es.Id, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Dänisch", TranslationToLanguageId = de.Id, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "دانماركي", TranslationToLanguageId = ar.Id, },                // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Deens", TranslationToLanguageId = af.Id, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Danimarka", TranslationToLanguageId = az.Id, },                // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Дацкая", TranslationToLanguageId = be.Id, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "датски", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ডেনিশ", TranslationToLanguageId = bn.Id, },                    // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "danès", TranslationToLanguageId = ca.Id, },                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "dánský", TranslationToLanguageId = cs.Id, },                   // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Daneg", TranslationToLanguageId = cy.Id, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "danske", TranslationToLanguageId = da.Id, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "taani", TranslationToLanguageId = et.Id, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Danimarkako", TranslationToLanguageId = eu.Id, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "دانمارکی", TranslationToLanguageId = fa.Id, },                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "tanska", TranslationToLanguageId = fi.Id, },                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "danoises", TranslationToLanguageId = fr.Id, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Danmhairgis", TranslationToLanguageId = ga.Id, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Dinamarqués", TranslationToLanguageId = gl.Id, },              // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ડેનિશ ભાષા", TranslationToLanguageId = gu.Id, },                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "דני", TranslationToLanguageId = he.Id, },                      // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "डेनिश", TranslationToLanguageId = hi.Id, },                     // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "danski", TranslationToLanguageId = hr.Id, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Danwa", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "dán", TranslationToLanguageId = hu.Id, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "դանիերեն", TranslationToLanguageId = hy.Id, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Denmark", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Danska", TranslationToLanguageId = isLanguage.Id, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "danese", TranslationToLanguageId = it.Id, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "デンマーク", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "დანიის", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಡೆನ್ಮಾರ್ಕ್ ದೇಶದ ಭಾಷೆ", TranslationToLanguageId = kn.Id, },        // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "덴마크의", TranslationToLanguageId = ko.Id, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Danish", TranslationToLanguageId = la.Id, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Danijos", TranslationToLanguageId = lt.Id, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Dānijas", TranslationToLanguageId = lv.Id, },                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "данскиот", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Denmark", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "danish", TranslationToLanguageId = mt.Id, },                   // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Deens", TranslationToLanguageId = nl.Id, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "danske", TranslationToLanguageId = no.Id, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "duński", TranslationToLanguageId = pl.Id, },                   // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "dinamarquês", TranslationToLanguageId = pt.Id, },              // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "danez", TranslationToLanguageId = ro.Id, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "датский", TranslationToLanguageId = ru.Id, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "dánsky", TranslationToLanguageId = sk.Id, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Danish", TranslationToLanguageId = sl.Id, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "danez", TranslationToLanguageId = sq.Id, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "дански", TranslationToLanguageId = sr.Id, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "danska", TranslationToLanguageId = sv.Id, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Denmark", TranslationToLanguageId = sw.Id, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "டானிஷ்", TranslationToLanguageId = ta.Id, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "డేనిష్", TranslationToLanguageId = te.Id, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาเดนมาร์ก", TranslationToLanguageId = th.Id, },                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Danimarkalı", TranslationToLanguageId = tr.Id, },              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Данська", TranslationToLanguageId = uk.Id, },                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ڈينش", TranslationToLanguageId = ur.Id, },                    // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Đan Mạch", TranslationToLanguageId = vi.Id, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "דאַניש", TranslationToLanguageId = yi.Id, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "丹麦", TranslationToLanguageId = zh.Id, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = da.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (!et.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Estonian", TranslationToLanguageId = en.Id, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = es.Id, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Estnisch", TranslationToLanguageId = de.Id, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "الاستونية", TranslationToLanguageId = ar.Id, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Estnies", TranslationToLanguageId = af.Id, },                 // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "eston", TranslationToLanguageId = az.Id, },                   // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "эстонскі", TranslationToLanguageId = be.Id, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "естонски", TranslationToLanguageId = bg.Id, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "এস্তোনিয়াদেশ - সংক্রান্ত", TranslationToLanguageId = bn.Id, },       // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Estònia", TranslationToLanguageId = ca.Id, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "estonština", TranslationToLanguageId = cs.Id, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = cy.Id, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "estisk", TranslationToLanguageId = da.Id, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "eesti", TranslationToLanguageId = et.Id, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Estonian", TranslationToLanguageId = eu.Id, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان استونی", TranslationToLanguageId = fa.Id, },            // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "virolainen", TranslationToLanguageId = fi.Id, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "estonien", TranslationToLanguageId = fr.Id, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Eastóinis", TranslationToLanguageId = ga.Id, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Estoniano", TranslationToLanguageId = gl.Id, },               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "એસ્ટોનિયન", TranslationToLanguageId = gu.Id, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אסטוניה", TranslationToLanguageId = he.Id, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "एस्तोनियावासी", TranslationToLanguageId = hi.Id, },             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "estonski", TranslationToLanguageId = hr.Id, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Estonyen", TranslationToLanguageId = ht.Id, },                // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "észt", TranslationToLanguageId = hu.Id, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Էստոնիայի", TranslationToLanguageId = hy.Id, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = id.Id, },                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "eistneska", TranslationToLanguageId = isLanguage.Id, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "estonian", TranslationToLanguageId = it.Id, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "エストニア語", TranslationToLanguageId = ja.Id, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ესტონეთის", TranslationToLanguageId = ka.Id, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಎಸ್ಟೋನಿಯನ್", TranslationToLanguageId = kn.Id, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "에스 토니아 사람", TranslationToLanguageId = ko.Id, },         // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Estonica", TranslationToLanguageId = la.Id, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Estijos", TranslationToLanguageId = lt.Id, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Igaunijas", TranslationToLanguageId = lv.Id, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Естонија", TranslationToLanguageId = mk.Id, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = ms.Id, },                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Estonjan", TranslationToLanguageId = mt.Id, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Estlands", TranslationToLanguageId = nl.Id, },                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "estisk", TranslationToLanguageId = no.Id, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "estoński", TranslationToLanguageId = pl.Id, },                // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "estoniano", TranslationToLanguageId = pt.Id, },               // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba estonă", TranslationToLanguageId = ro.Id, },            // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "эстонский", TranslationToLanguageId = ru.Id, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "estónčina", TranslationToLanguageId = sk.Id, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "estonski", TranslationToLanguageId = sl.Id, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "estonez", TranslationToLanguageId = sq.Id, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "естонски", TranslationToLanguageId = sr.Id, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "estniska", TranslationToLanguageId = sv.Id, },                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiestonia", TranslationToLanguageId = sw.Id, },               // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "எஸ்தானியம்", TranslationToLanguageId = ta.Id, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఈస్టోనియను", TranslationToLanguageId = te.Id, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เอสโตเนีย", TranslationToLanguageId = th.Id, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Estonyalı", TranslationToLanguageId = tr.Id, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Естонська", TranslationToLanguageId = uk.Id, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "اسٹونين", TranslationToLanguageId = ur.Id, },                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Estonia", TranslationToLanguageId = vi.Id, },           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "עסטיש", TranslationToLanguageId = yi.Id, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "爱沙尼亚语", TranslationToLanguageId = zh.Id, },               // #61 Language = "Chinese"

//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = et.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!eu.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = en.Id, },                                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "vasco", TranslationToLanguageId = es.Id, },                                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Baskische", TranslationToLanguageId = de.Id, },                               // #3  Language = "German"
//                    new CreateLanguageName { Text = "الباسكي", TranslationToLanguageId = ar.Id, },                                // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Baskies", TranslationToLanguageId = af.Id, },                                 // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Bask", TranslationToLanguageId = az.Id, },                                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "басконскі", TranslationToLanguageId = be.Id, },                               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "баска", TranslationToLanguageId = bg.Id, },                                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্পেন ও ফ্রান্সের পিরেনিজ পর্বতাঞ্চলের অধিবাসী", TranslationToLanguageId = bn.Id, },     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Basc", TranslationToLanguageId = ca.Id, },                                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = cs.Id, },                                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Basgeg", TranslationToLanguageId = cy.Id, },                                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Baskerlandet", TranslationToLanguageId = da.Id, },                            // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "baski", TranslationToLanguageId = et.Id, },                                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Euskal", TranslationToLanguageId = eu.Id, },                                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "باسک", TranslationToLanguageId = fa.Id, },                                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "baski", TranslationToLanguageId = fi.Id, },                                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "basques", TranslationToLanguageId = fr.Id, },                                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Bascais", TranslationToLanguageId = ga.Id, },                                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = gl.Id, },                                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ટૂંકી ઝાલરવાળી ચોળી", TranslationToLanguageId = gu.Id, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הבסקים", TranslationToLanguageId = he.Id, },                                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बस्क", TranslationToLanguageId = hi.Id, },                                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "baskijski", TranslationToLanguageId = hr.Id, },                               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "basque", TranslationToLanguageId = ht.Id, },                                  // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "baszk", TranslationToLanguageId = hu.Id, },                                   // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բասկա", TranslationToLanguageId = hy.Id, },                                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = id.Id, },                                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "baskneska", TranslationToLanguageId = isLanguage.Id, },                       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "basco", TranslationToLanguageId = it.Id, },                                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "バスク", TranslationToLanguageId = ja.Id, },                                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბასკურ", TranslationToLanguageId = ka.Id, },                                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಬಾಸ್ಕ್", TranslationToLanguageId = kn.Id, },                                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "바스크 사람", TranslationToLanguageId = ko.Id, },                              // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Vasca", TranslationToLanguageId = la.Id, },                                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Baskų", TranslationToLanguageId = lt.Id, },                                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "basku", TranslationToLanguageId = lv.Id, },                                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Баскија", TranslationToLanguageId = mk.Id, },                                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = ms.Id, },                                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Bask", TranslationToLanguageId = mt.Id, },                                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "basque", TranslationToLanguageId = nl.Id, },                                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = no.Id, },                                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "baskijski", TranslationToLanguageId = pl.Id, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "basco", TranslationToLanguageId = pt.Id, },                                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "basc", TranslationToLanguageId = ro.Id, },                                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "баскский", TranslationToLanguageId = ru.Id, },                                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = sk.Id, },                                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = sl.Id, },                                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "bask", TranslationToLanguageId = sq.Id, },                                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "баскијски", TranslationToLanguageId = sr.Id, },                               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "baskiska", TranslationToLanguageId = sv.Id, },                                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = sw.Id, },                                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பஸ்க்", TranslationToLanguageId = ta.Id, },                                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బాస్క్", TranslationToLanguageId = te.Id, },                                    // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวแบสค์", TranslationToLanguageId = th.Id, },                                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Bask", TranslationToLanguageId = tr.Id, },                                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Баскська", TranslationToLanguageId = uk.Id, },                                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "باسکی", TranslationToLanguageId = ur.Id, },                                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = vi.Id, },                                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "באַסק", TranslationToLanguageId = yi.Id, },                                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "巴斯克", TranslationToLanguageId = zh.Id, },                                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = eu.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!fa.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = en.Id, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "persa", TranslationToLanguageId = es.Id, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Persisch", TranslationToLanguageId = de.Id, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "اللغة الفارسية", TranslationToLanguageId = ar.Id, },            // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Persiese", TranslationToLanguageId = af.Id, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "fars", TranslationToLanguageId = az.Id, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Фарсі", TranslationToLanguageId = be.Id, },                       // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "персийски", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "পারসিক", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "persa", TranslationToLanguageId = ca.Id, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "perský", TranslationToLanguageId = cs.Id, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Perseg", TranslationToLanguageId = cy.Id, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "perser", TranslationToLanguageId = da.Id, },                      // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Pärsia", TranslationToLanguageId = et.Id, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = eu.Id, },                     // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "فارسی", TranslationToLanguageId = fa.Id, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "persialainen", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Persique", TranslationToLanguageId = fr.Id, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Peirsis", TranslationToLanguageId = ga.Id, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Persa", TranslationToLanguageId = gl.Id, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ફારસી", TranslationToLanguageId = gu.Id, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פרסי", TranslationToLanguageId = he.Id, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "फ़ारसी", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Perzijski", TranslationToLanguageId = hr.Id, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Pèsik", TranslationToLanguageId = ht.Id, },                       // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "perzsa", TranslationToLanguageId = hu.Id, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "պարսիկ", TranslationToLanguageId = hy.Id, },                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = id.Id, },                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "persneska", TranslationToLanguageId = isLanguage.Id, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "persiano", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ペルシア語", TranslationToLanguageId = ja.Id, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სპარსეთის", TranslationToLanguageId = ka.Id, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಪರ್ಷಿಯಾಕ್ಕೆ ಸಂಬಂಧಿಸಿದ", TranslationToLanguageId = kn.Id, },           // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "페르시아의", TranslationToLanguageId = ko.Id, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Persae", TranslationToLanguageId = la.Id, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "persų", TranslationToLanguageId = lt.Id, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "persiešu", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Персискиот", TranslationToLanguageId = mk.Id, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Parsi", TranslationToLanguageId = ms.Id, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Persjan", TranslationToLanguageId = mt.Id, },                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Perzisch", TranslationToLanguageId = nl.Id, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = no.Id, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "perski", TranslationToLanguageId = pl.Id, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "persa", TranslationToLanguageId = pt.Id, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "persană", TranslationToLanguageId = ro.Id, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "персидский", TranslationToLanguageId = ru.Id, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "perzský", TranslationToLanguageId = sk.Id, },                     // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = sl.Id, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "persisht", TranslationToLanguageId = sq.Id, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "персијски", TranslationToLanguageId = sr.Id, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Persiska", TranslationToLanguageId = sv.Id, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiajemi", TranslationToLanguageId = sw.Id, },                     // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பர்ஸியன்", TranslationToLanguageId = ta.Id, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "పెర్షియన్", TranslationToLanguageId = te.Id, },                      // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวเปอร์เซีย", TranslationToLanguageId = th.Id, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Farsça", TranslationToLanguageId = tr.Id, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Перська", TranslationToLanguageId = uk.Id, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "فارسی", TranslationToLanguageId = ur.Id, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Ba Tư", TranslationToLanguageId = vi.Id, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פּערסיש", TranslationToLanguageId = yi.Id, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "波斯语", TranslationToLanguageId = zh.Id, },                      // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fa.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (!fi.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Finnish", TranslationToLanguageId = en.Id, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "finlandés", TranslationToLanguageId = es.Id, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Finnisch", TranslationToLanguageId = de.Id, },                   // #3  Language = "German"
//                    new CreateLanguageName { Text = "فنلندية", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Finse", TranslationToLanguageId = af.Id, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "fin", TranslationToLanguageId = az.Id, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Фінская", TranslationToLanguageId = be.Id, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "фински", TranslationToLanguageId = bg.Id, },                     // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ফিনল্যাণ্ডের ভাষা", TranslationToLanguageId = bn.Id, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Finès", TranslationToLanguageId = ca.Id, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "finský", TranslationToLanguageId = cs.Id, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Ffinneg", TranslationToLanguageId = cy.Id, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "finnish", TranslationToLanguageId = da.Id, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "soome", TranslationToLanguageId = et.Id, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Finlandiako", TranslationToLanguageId = eu.Id, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "فنلاندی", TranslationToLanguageId = fa.Id, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "suomalainen", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "finlandaise", TranslationToLanguageId = fr.Id, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Fionlainnis", TranslationToLanguageId = ga.Id, },                // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Finés", TranslationToLanguageId = gl.Id, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "દેશની ભાષા", TranslationToLanguageId = gu.Id, },                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פינית", TranslationToLanguageId = he.Id, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "फ़िन जातीय", TranslationToLanguageId = hi.Id, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "finski", TranslationToLanguageId = hr.Id, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "finnish", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "finn", TranslationToLanguageId = hu.Id, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ֆիննական", TranslationToLanguageId = hy.Id, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Finlandia", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Finnska", TranslationToLanguageId = isLanguage.Id, },            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "finlandese", TranslationToLanguageId = it.Id, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "フィンランド", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ფინეთის", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಫಿನ್ನಿಶ್", TranslationToLanguageId = kn.Id, },                       // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "핀란드의", TranslationToLanguageId = ko.Id, },                    // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Finnish", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "suomių", TranslationToLanguageId = lt.Id, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Somijas", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "финскиот", TranslationToLanguageId = mk.Id, },                   // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Finland", TranslationToLanguageId = ms.Id, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Finlandiż", TranslationToLanguageId = mt.Id, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Fins", TranslationToLanguageId = nl.Id, },                       // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "finske", TranslationToLanguageId = no.Id, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "fiński", TranslationToLanguageId = pl.Id, },                     // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "finlandês", TranslationToLanguageId = pt.Id, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "finlandeză", TranslationToLanguageId = ro.Id, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "финский", TranslationToLanguageId = ru.Id, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Fínsky", TranslationToLanguageId = sk.Id, },                     // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "finski", TranslationToLanguageId = sl.Id, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "finlandisht", TranslationToLanguageId = sq.Id, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "фински", TranslationToLanguageId = sr.Id, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "finska", TranslationToLanguageId = sv.Id, },                     // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kifini", TranslationToLanguageId = sw.Id, },                     // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஃபின்னிஷ்", TranslationToLanguageId = ta.Id, },                // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఫిన్నిష్", TranslationToLanguageId = te.Id, },                      // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ฟินแลนด์", TranslationToLanguageId = th.Id, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Fince", TranslationToLanguageId = tr.Id, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Фінська", TranslationToLanguageId = uk.Id, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "فننش", TranslationToLanguageId = ur.Id, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Phần Lan", TranslationToLanguageId = vi.Id, },                   // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פֿיניש", TranslationToLanguageId = yi.Id, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "芬兰", TranslationToLanguageId = zh.Id, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fi.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!fr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "French", TranslationToLanguageId = en.Id, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "francés", TranslationToLanguageId = es.Id, },                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Französisch", TranslationToLanguageId = de.Id, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "فرنسي", TranslationToLanguageId = ar.Id, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Frans", TranslationToLanguageId = af.Id, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Fransız dili", TranslationToLanguageId = az.Id, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "французская", TranslationToLanguageId = be.Id, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "френски", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ফরাসি", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "francès", TranslationToLanguageId = ca.Id, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "francouzština", TranslationToLanguageId = cs.Id, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Ffrangeg", TranslationToLanguageId = cy.Id, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "fransk", TranslationToLanguageId = da.Id, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "prantsuse", TranslationToLanguageId = et.Id, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Frantziako", TranslationToLanguageId = eu.Id, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "فرانسوی", TranslationToLanguageId = fa.Id, },                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Ranskan", TranslationToLanguageId = fi.Id, },                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "française", TranslationToLanguageId = fr.Id, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Fraincis", TranslationToLanguageId = ga.Id, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Francés", TranslationToLanguageId = gl.Id, },                   // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ફ્રાંસની ભાષા", TranslationToLanguageId = gu.Id, },                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "צרפתי", TranslationToLanguageId = he.Id, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "फ्रांसीसी", TranslationToLanguageId = hi.Id, },                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "francuski", TranslationToLanguageId = hr.Id, },                 // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "franse", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "francia", TranslationToLanguageId = hu.Id, },                   // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ֆրանսերեն", TranslationToLanguageId = hy.Id, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Prancis", TranslationToLanguageId = id.Id, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Franska", TranslationToLanguageId = isLanguage.Id, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "francese", TranslationToLanguageId = it.Id, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "フランス", TranslationToLanguageId = ja.Id, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "საფრანგეთის", TranslationToLanguageId = ka.Id, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಫ್ರೆಂಚ್", TranslationToLanguageId = kn.Id, },                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "프랑스의", TranslationToLanguageId = ko.Id, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "French", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "prancūzų", TranslationToLanguageId = lt.Id, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "franču", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "францускиот", TranslationToLanguageId = mk.Id, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Perancis", TranslationToLanguageId = ms.Id, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Franċiż", TranslationToLanguageId = mt.Id, },                   // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Frans", TranslationToLanguageId = nl.Id, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "fransk", TranslationToLanguageId = no.Id, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "francuski", TranslationToLanguageId = pl.Id, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "francês", TranslationToLanguageId = pt.Id, },                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "franceză", TranslationToLanguageId = ro.Id, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "французский", TranslationToLanguageId = ru.Id, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "francúzština", TranslationToLanguageId = sk.Id, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "francoski", TranslationToLanguageId = sl.Id, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "frëngjisht", TranslationToLanguageId = sq.Id, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Французи", TranslationToLanguageId = sr.Id, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "franska", TranslationToLanguageId = sv.Id, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kifaransa", TranslationToLanguageId = sw.Id, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பிரஞ்சு", TranslationToLanguageId = ta.Id, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ప్రాన్సుదేశభాష", TranslationToLanguageId = te.Id, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฝรั่งเศส", TranslationToLanguageId = th.Id, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Fransız", TranslationToLanguageId = tr.Id, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "французький", TranslationToLanguageId = uk.Id, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "فرانسیسی", TranslationToLanguageId = ur.Id, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Pháp", TranslationToLanguageId = vi.Id, },                      // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פראנצויזיש", TranslationToLanguageId = yi.Id, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "法国", TranslationToLanguageId = zh.Id, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fr.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ga.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Irish", TranslationToLanguageId = en.Id, },                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "irlandés", TranslationToLanguageId = es.Id, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Irisch", TranslationToLanguageId = de.Id, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأيرلندية", TranslationToLanguageId = ar.Id, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Ierse", TranslationToLanguageId = af.Id, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "irland", TranslationToLanguageId = az.Id, },                 // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Ірландскі", TranslationToLanguageId = be.Id, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "ирландски", TranslationToLanguageId = bg.Id, },              // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আয়াল্যাণ্ড সংক্রান্ত", TranslationToLanguageId = bn.Id, },           // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "irlandès", TranslationToLanguageId = ca.Id, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "irský", TranslationToLanguageId = cs.Id, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Gwyddelig", TranslationToLanguageId = cy.Id, },              // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "irsk", TranslationToLanguageId = da.Id, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "iiri", TranslationToLanguageId = et.Id, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Gaelera", TranslationToLanguageId = eu.Id, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ایرلندی", TranslationToLanguageId = fa.Id, },               // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "irlantilainen", TranslationToLanguageId = fi.Id, },          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "irlandaise", TranslationToLanguageId = fr.Id, },             // #18 Language = "French"
//                    new CreateLanguageName { Text = "na hÉireann", TranslationToLanguageId = ga.Id, },            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "irlandés", TranslationToLanguageId = gl.Id, },               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આયર્લેન્ડનું", TranslationToLanguageId = gu.Id, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אירית", TranslationToLanguageId = he.Id, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आयरिश", TranslationToLanguageId = hi.Id, },                 // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "irski", TranslationToLanguageId = hr.Id, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Ilandè", TranslationToLanguageId = ht.Id, },                 // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "ír", TranslationToLanguageId = hu.Id, },                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իռլանդական", TranslationToLanguageId = hy.Id, },            // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Irlandia", TranslationToLanguageId = id.Id, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "írska", TranslationToLanguageId = isLanguage.Id, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "irlandese", TranslationToLanguageId = it.Id, },              // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アイリッシュ", TranslationToLanguageId = ja.Id, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ირლანდიის", TranslationToLanguageId = ka.Id, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕೋಪ", TranslationToLanguageId = kn.Id, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아일랜드", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hibernica", TranslationToLanguageId = la.Id, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "airių", TranslationToLanguageId = lt.Id, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Īrijas", TranslationToLanguageId = lv.Id, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Ирска", TranslationToLanguageId = mk.Id, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Ireland", TranslationToLanguageId = ms.Id, },                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Irlandiż", TranslationToLanguageId = mt.Id, },               // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Iers", TranslationToLanguageId = nl.Id, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "irsk", TranslationToLanguageId = no.Id, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "irlandzki", TranslationToLanguageId = pl.Id, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "irlandês", TranslationToLanguageId = pt.Id, },               // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "irlandez", TranslationToLanguageId = ro.Id, },               // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "ирландский", TranslationToLanguageId = ru.Id, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "írsky", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "irski", TranslationToLanguageId = sl.Id, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "irlandez", TranslationToLanguageId = sq.Id, },               // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Ирци", TranslationToLanguageId = sr.Id, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "irländska", TranslationToLanguageId = sv.Id, },              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Ireland", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஐயர்லாந்தை சார்ந்த", TranslationToLanguageId = ta.Id, },  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఐరిష్", TranslationToLanguageId = te.Id, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ไอริช", TranslationToLanguageId = th.Id, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İrlandalı", TranslationToLanguageId = tr.Id, },              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Ірландський", TranslationToLanguageId = uk.Id, },            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آئيرش", TranslationToLanguageId = ur.Id, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ailen", TranslationToLanguageId = vi.Id, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "איריש", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "爱尔兰", TranslationToLanguageId = zh.Id, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ga.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!gl.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = en.Id, },             // #1  Language = "English"
//                    new CreateLanguageName { Text = "gallego", TranslationToLanguageId = es.Id, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "galizischen", TranslationToLanguageId = de.Id, },           // #3  Language = "German"
//                    new CreateLanguageName { Text = "الجاليكية", TranslationToLanguageId = ar.Id, },            // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Galicies", TranslationToLanguageId = af.Id, },              // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Qalisian", TranslationToLanguageId = az.Id, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Галіцка", TranslationToLanguageId = be.Id, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "галисийски", TranslationToLanguageId = bg.Id, },            // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "গ্যালিশিয়", TranslationToLanguageId = bn.Id, },                // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Gallego", TranslationToLanguageId = ca.Id, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = cs.Id, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = cy.Id, },              // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "galiciske", TranslationToLanguageId = da.Id, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = et.Id, },              // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Galiziako", TranslationToLanguageId = eu.Id, },             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "گالیسیایی", TranslationToLanguageId = fa.Id, },            // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = fi.Id, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Galice", TranslationToLanguageId = fr.Id, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Gailísis", TranslationToLanguageId = ga.Id, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Galego", TranslationToLanguageId = gl.Id, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ગેલિશિયન", TranslationToLanguageId = gu.Id, },              // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גליציאנית", TranslationToLanguageId = he.Id, },            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "गैलिशियन्", TranslationToLanguageId = hi.Id, },              // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Galicijski", TranslationToLanguageId = hr.Id, },            // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Galisyen", TranslationToLanguageId = ht.Id, },              // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = hu.Id, },              // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "գալիսերեն", TranslationToLanguageId = hy.Id, },             // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Galicia", TranslationToLanguageId = id.Id, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "galisíska", TranslationToLanguageId = isLanguage.Id, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "galiziano", TranslationToLanguageId = it.Id, },             // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ガリシア語", TranslationToLanguageId = ja.Id, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "გალიციური", TranslationToLanguageId = ka.Id, },            // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಗ್ಯಾಲಿಶಿಯನ್", TranslationToLanguageId = kn.Id, },             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "갈리시아어", TranslationToLanguageId = ko.Id, },             // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Gallaeca", TranslationToLanguageId = la.Id, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Galicijos", TranslationToLanguageId = lt.Id, },             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "galisiešu", TranslationToLanguageId = lv.Id, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Галиција", TranslationToLanguageId = mk.Id, },              // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Galicia", TranslationToLanguageId = ms.Id, },        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Galicia", TranslationToLanguageId = mt.Id, },               // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "galician", TranslationToLanguageId = nl.Id, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "galiciske", TranslationToLanguageId = no.Id, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "galicyjskiej", TranslationToLanguageId = pl.Id, },          // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "galego", TranslationToLanguageId = pt.Id, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "galiciană", TranslationToLanguageId = ro.Id, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "Галицко", TranslationToLanguageId = ru.Id, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sk.Id, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sl.Id, },              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sq.Id, },              // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "галицијски", TranslationToLanguageId = sr.Id, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "galiciska", TranslationToLanguageId = sv.Id, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sw.Id, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "காலிசியன்", TranslationToLanguageId = ta.Id, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "గెలీసియన్", TranslationToLanguageId = te.Id, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "กาลิเซีย", TranslationToLanguageId = th.Id, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Galicia'ya ait", TranslationToLanguageId = tr.Id, },        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Галицько", TranslationToLanguageId = uk.Id, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "گاليشيائی", TranslationToLanguageId = ur.Id, },            // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Galicia", TranslationToLanguageId = vi.Id, },         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "גאליציאנער", TranslationToLanguageId = yi.Id, },           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "加利西亚", TranslationToLanguageId = zh.Id, },               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gl.Id;
//                    _createLanguageName.Handle(n);
//                });



//            if (!gu.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = en.Id, },            // #1  Language = "English"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = es.Id, },             // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = de.Id, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "الغوجاراتية", TranslationToLanguageId = ar.Id, },        // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = af.Id, },             // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = az.Id, },             // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Гуяраці", TranslationToLanguageId = be.Id, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "гуджарати", TranslationToLanguageId = bg.Id, },            // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "গুজরাটি", TranslationToLanguageId = bn.Id, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = ca.Id, },             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = cs.Id, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Gwjarati", TranslationToLanguageId = cy.Id, },             // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = da.Id, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "gudžarati", TranslationToLanguageId = et.Id, },            // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = eu.Id, },             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "گجراتی", TranslationToLanguageId = fa.Id, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "gudzarati", TranslationToLanguageId = fi.Id, },            // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Goudjrati", TranslationToLanguageId = fr.Id, },            // #18 Language = "French"
//                    new CreateLanguageName { Text = "Gúisearáitis", TranslationToLanguageId = ga.Id, },         // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Guzerate", TranslationToLanguageId = gl.Id, },             // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ગુજરાતી", TranslationToLanguageId = gu.Id, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גוג'ראטית", TranslationToLanguageId = he.Id, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "गुजराती", TranslationToLanguageId = hi.Id, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = hr.Id, },             // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = ht.Id, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "gudzsaráti", TranslationToLanguageId = hu.Id, },           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "գուջարատերեն", TranslationToLanguageId = hy.Id, },        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Gujarat", TranslationToLanguageId = id.Id, },              // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "gújaratí", TranslationToLanguageId = isLanguage.Id, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = it.Id, },             // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "グジャラート語", TranslationToLanguageId = ja.Id, },        // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "გუჯარატი", TranslationToLanguageId = ka.Id, },            // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಗುಜರಾತಿ", TranslationToLanguageId = kn.Id, },               // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "구자라트어", TranslationToLanguageId = ko.Id, },            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Guiaratica", TranslationToLanguageId = la.Id, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "gudžarati", TranslationToLanguageId = lt.Id, },            // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "gudžaratu", TranslationToLanguageId = lv.Id, },            // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Гуџарати", TranslationToLanguageId = mk.Id, },             // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = ms.Id, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Guġarati", TranslationToLanguageId = mt.Id, },             // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = nl.Id, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = no.Id, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "gudżarati", TranslationToLanguageId = pl.Id, },            // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "guzerate", TranslationToLanguageId = pt.Id, },             // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = ro.Id, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "гуджарати", TranslationToLanguageId = ru.Id, },            // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = sk.Id, },             // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "gudžaratščina", TranslationToLanguageId = sl.Id, },        // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "guxharati", TranslationToLanguageId = sq.Id, },            // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "гујарати", TranslationToLanguageId = sr.Id, },             // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = sv.Id, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kigujarati", TranslationToLanguageId = sw.Id, },           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "குஜராத்தி", TranslationToLanguageId = ta.Id, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "గుజరాతీ", TranslationToLanguageId = te.Id, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "คุชราต", TranslationToLanguageId = th.Id, },                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = tr.Id, },             // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "гуджараті", TranslationToLanguageId = uk.Id, },            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "گجراتی", TranslationToLanguageId = ur.Id, },              // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = vi.Id, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "גודזשאַראַטי", TranslationToLanguageId = yi.Id, },          // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "古吉拉特语", TranslationToLanguageId = zh.Id, },            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gu.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!he.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = en.Id, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "hebreo", TranslationToLanguageId = es.Id, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Hebräisch", TranslationToLanguageId = de.Id, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "العبرية", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Hebreeus", TranslationToLanguageId = af.Id, },                   // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İvrit", TranslationToLanguageId = az.Id, },                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "іўрыт", TranslationToLanguageId = be.Id, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "иврит", TranslationToLanguageId = bg.Id, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইহুদি", TranslationToLanguageId = bn.Id, },                        // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "hebreu", TranslationToLanguageId = ca.Id, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "hebrejský", TranslationToLanguageId = cs.Id, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Hebraeg", TranslationToLanguageId = cy.Id, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = da.Id, },                     // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "heebrea", TranslationToLanguageId = et.Id, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Hebrear", TranslationToLanguageId = eu.Id, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان عبری", TranslationToLanguageId = fa.Id, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "heprealainen", TranslationToLanguageId = fi.Id, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "l'hébreu", TranslationToLanguageId = fr.Id, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Eabhrais", TranslationToLanguageId = ga.Id, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Hebreo", TranslationToLanguageId = gl.Id, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "યહુદી", TranslationToLanguageId = gu.Id, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "עברית", TranslationToLanguageId = he.Id, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "यहूदी", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Hebrejski", TranslationToLanguageId = hr.Id, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "lang ebre", TranslationToLanguageId = ht.Id, },                  // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "héber", TranslationToLanguageId = hu.Id, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "եբրայեցի", TranslationToLanguageId = hy.Id, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Ibrani", TranslationToLanguageId = id.Id, },                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "hebreska", TranslationToLanguageId = isLanguage.Id, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ebraico", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ヘブライ", TranslationToLanguageId = ja.Id, },                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ებრაული", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹೀಬ್ರೂ", TranslationToLanguageId = kn.Id, },                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "히브리어", TranslationToLanguageId = ko.Id, },                    // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = la.Id, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "hebrajų", TranslationToLanguageId = lt.Id, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "ebreju", TranslationToLanguageId = lv.Id, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "хебрејски јазик", TranslationToLanguageId = mk.Id, },            // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Ibrani", TranslationToLanguageId = ms.Id, },                     // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ebrajk", TranslationToLanguageId = mt.Id, },                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Hebreeuws", TranslationToLanguageId = nl.Id, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "hebraisk", TranslationToLanguageId = no.Id, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "hebrajski", TranslationToLanguageId = pl.Id, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "hebraico", TranslationToLanguageId = pt.Id, },                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "evreiesc", TranslationToLanguageId = ro.Id, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "иврит", TranslationToLanguageId = ru.Id, },                      // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "hebrejský", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = sl.Id, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Hebraisht", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јеврејски", TranslationToLanguageId = sr.Id, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "hebreiska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiyahudi", TranslationToLanguageId = sw.Id, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "யூதர்", TranslationToLanguageId = ta.Id, },                      // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "యూదుల భాష", TranslationToLanguageId = te.Id, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฮิบรู", TranslationToLanguageId = th.Id, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İbranice", TranslationToLanguageId = tr.Id, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Іврит", TranslationToLanguageId = uk.Id, },                      // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "عبرانی", TranslationToLanguageId = ur.Id, },                    // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Do Thái,", TranslationToLanguageId = vi.Id, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "העברעיש", TranslationToLanguageId = yi.Id, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "希伯来文", TranslationToLanguageId = zh.Id, },                    // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = he.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!hi.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = en.Id, },                                        // #1  Language = "English"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = es.Id, },                                         // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = de.Id, },                                         // #3  Language = "German"
//                    new CreateLanguageName { Text = "هندي", TranslationToLanguageId = ar.Id, },                                         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = af.Id, },                                         // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = az.Id, },                                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "ня", TranslationToLanguageId = be.Id, },                                            // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = bg.Id, },                                         // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "না", TranslationToLanguageId = bn.Id, },                                            // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = ca.Id, },                                         // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "hindština", TranslationToLanguageId = cs.Id, },                                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = cy.Id, },                                         // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = da.Id, },                                         // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = et.Id, },                                         // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "ez", TranslationToLanguageId = eu.Id, },                                            // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "هندی", TranslationToLanguageId = fa.Id, },                                         // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = fi.Id, },                                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = fr.Id, },                                         // #18 Language = "French"
//                    new CreateLanguageName { Text = "Hiondúis", TranslationToLanguageId = ga.Id, },                                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = gl.Id, },                                         // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "નથી", TranslationToLanguageId = gu.Id, },                                           // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הינדי", TranslationToLanguageId = he.Id, },                                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "हिंदी", TranslationToLanguageId = hi.Id, },                                           // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Hindski", TranslationToLanguageId = hr.Id, },                                       // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = ht.Id, },                                         // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = hu.Id, },                                         // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = hy.Id, },                                         // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = id.Id, },                                         // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = isLanguage.Id, },                                 // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = it.Id, },                                         // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ヒンディー語", TranslationToLanguageId = ja.Id, },                                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჰინდი", TranslationToLanguageId = ka.Id, },                                         // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹಿಂದಿ", TranslationToLanguageId = kn.Id, },                                          // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "힌디어", TranslationToLanguageId = ko.Id, },                                        // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = la.Id, },                                         // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = lt.Id, },                                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = lv.Id, },                                         // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = mk.Id, },                                         // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = ms.Id, },                                         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ħindi", TranslationToLanguageId = mt.Id, },                                         // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = nl.Id, },                                         // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = no.Id, },                                         // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = pl.Id, },                                         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = pt.Id, },                                         // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = ro.Id, },                                         // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = ru.Id, },                                         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "hindčina", TranslationToLanguageId = sk.Id, },                                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = sl.Id, },                                         // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = sq.Id, },                                         // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = sr.Id, },                                         // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = sv.Id, },                                         // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = sw.Id, },                                         // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இந்தியாவில் பரவலாக பேசப்படும் மொழி", TranslationToLanguageId = ta.Id, },   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "హిందీభాష", TranslationToLanguageId = te.Id, },                                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฮินดี", TranslationToLanguageId = th.Id, },                                        // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Hintçe", TranslationToLanguageId = tr.Id, },                                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Хінді", TranslationToLanguageId = uk.Id, },                                         // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہندی", TranslationToLanguageId = ur.Id, },                                         // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Hin-ddi", TranslationToLanguageId = vi.Id, },                                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "הינדיש", TranslationToLanguageId = yi.Id, },                                       // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "印地文", TranslationToLanguageId = zh.Id, },                                        // #61 Language = "Chinese"

//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hi.Id;
//                    _createLanguageName.Handle(n);
//                });



//            if (!hr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Croatian", TranslationToLanguageId = en.Id, },                     // #1  Language = "English"
//                    new CreateLanguageName { Text = "croata", TranslationToLanguageId = es.Id, },                        // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "kroatisch", TranslationToLanguageId = de.Id, },                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "الكرواتي", TranslationToLanguageId = ar.Id, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Kroaties", TranslationToLanguageId = af.Id, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Xorvat", TranslationToLanguageId = az.Id, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Харвацкая", TranslationToLanguageId = be.Id, },                     // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "хърватски", TranslationToLanguageId = bg.Id, },                     // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ক্রোয়েশিয়ান", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "croat", TranslationToLanguageId = ca.Id, },                         // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "chorvatský", TranslationToLanguageId = cs.Id, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Croateg", TranslationToLanguageId = cy.Id, },                       // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Kroatisk", TranslationToLanguageId = da.Id, },                      // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "horvaatia", TranslationToLanguageId = et.Id, },                     // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Kroazierara", TranslationToLanguageId = eu.Id, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کرواتی", TranslationToLanguageId = fa.Id, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "kroaatti", TranslationToLanguageId = fi.Id, },                      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "croate", TranslationToLanguageId = fr.Id, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Cróitis", TranslationToLanguageId = ga.Id, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Croata", TranslationToLanguageId = gl.Id, },                        // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ક્રોએશિયન", TranslationToLanguageId = gu.Id, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קרואטית", TranslationToLanguageId = he.Id, },                      // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "क्रोएशियाई", TranslationToLanguageId = hi.Id, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "hrvatski", TranslationToLanguageId = hr.Id, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "kwoasyen", TranslationToLanguageId = ht.Id, },                      // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "horvát", TranslationToLanguageId = hu.Id, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "խորվաթական", TranslationToLanguageId = hy.Id, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Kroasia", TranslationToLanguageId = id.Id, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Króatíska", TranslationToLanguageId = isLanguage.Id, },             // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "croato", TranslationToLanguageId = it.Id, },                        // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "クロアチア語", TranslationToLanguageId = ja.Id, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ხორვატული", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕ್ರೊಯೇಷಿಯಾದ", TranslationToLanguageId = kn.Id, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "크로아티아의", TranslationToLanguageId = ko.Id, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Illyrica", TranslationToLanguageId = la.Id, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Kroatijos", TranslationToLanguageId = lt.Id, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Horvātijas", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Хрватска", TranslationToLanguageId = mk.Id, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Croatia", TranslationToLanguageId = ms.Id, },                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Kroat", TranslationToLanguageId = mt.Id, },                         // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Kroatisch", TranslationToLanguageId = nl.Id, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "kroatisk", TranslationToLanguageId = no.Id, },                      // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "chorwacki", TranslationToLanguageId = pl.Id, },                     // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "croata", TranslationToLanguageId = pt.Id, },                        // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "croat", TranslationToLanguageId = ro.Id, },                         // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "хорватский", TranslationToLanguageId = ru.Id, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Chorvátsky", TranslationToLanguageId = sk.Id, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "hrvaški", TranslationToLanguageId = sl.Id, },                       // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "kroate", TranslationToLanguageId = sq.Id, },                        // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "хрватски", TranslationToLanguageId = sr.Id, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "kroatiska", TranslationToLanguageId = sv.Id, },                     // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kikroeshia", TranslationToLanguageId = sw.Id, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கரோஷியன்", TranslationToLanguageId = ta.Id, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "క్రొయేషియన్", TranslationToLanguageId = te.Id, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "โครเอเชีย", TranslationToLanguageId = th.Id, },                        // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Hırvat", TranslationToLanguageId = tr.Id, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Хорватська", TranslationToLanguageId = uk.Id, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کروشین", TranslationToLanguageId = ur.Id, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Croatia", TranslationToLanguageId = vi.Id, },                       // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קראָאַטיש", TranslationToLanguageId = yi.Id, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "克罗地亚", TranslationToLanguageId = zh.Id, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hr.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ht.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = en.Id, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "buenas Noticias", TranslationToLanguageId = es.Id, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Good News", TranslationToLanguageId = de.Id, },                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "أخبار سارة", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "goeie Nuus", TranslationToLanguageId = af.Id, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Xoş Xəbər", TranslationToLanguageId = az.Id, },                     // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "добрыя навіны", TranslationToLanguageId = be.Id, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "Добрата новина", TranslationToLanguageId = bg.Id, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সু - সংবাদ", TranslationToLanguageId = bn.Id, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "bones Notícies", TranslationToLanguageId = ca.Id, },                // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "dobré zprávy", TranslationToLanguageId = cs.Id, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Newyddion Da", TranslationToLanguageId = cy.Id, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "gode nyheder", TranslationToLanguageId = da.Id, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "head uudised", TranslationToLanguageId = et.Id, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Good News", TranslationToLanguageId = eu.Id, },                     // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مژده", TranslationToLanguageId = fa.Id, },                         // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "hyviä uutisia", TranslationToLanguageId = fi.Id, },                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "French", TranslationToLanguageId = fr.Id, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "dea-Scéal", TranslationToLanguageId = ga.Id, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "boa Nova", TranslationToLanguageId = gl.Id, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સારા સમાચાર", TranslationToLanguageId = gu.Id, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "חדשות טובות", TranslationToLanguageId = he.Id, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अच्छी खबर", TranslationToLanguageId = hi.Id, },                     // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Haićanski kreolski", TranslationToLanguageId = hr.Id, },            // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "kreyòl ayisyen", TranslationToLanguageId = ht.Id, },                // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "jó hír", TranslationToLanguageId = hu.Id, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Haitian կրեոլ", TranslationToLanguageId = hy.Id, },                 // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "baik Berita", TranslationToLanguageId = id.Id, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Góðar fréttir", TranslationToLanguageId = isLanguage.Id, },         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "buona Novella", TranslationToLanguageId = it.Id, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "良いニュース", TranslationToLanguageId = ja.Id, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = ka.Id, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹೈಟಿ ಕ್ರಿಯೋಲ", TranslationToLanguageId = kn.Id, },                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아이티 크리올어 크리올", TranslationToLanguageId = ko.Id, },         // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = la.Id, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Haičio kreolų", TranslationToLanguageId = lt.Id, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Haiti kreolu", TranslationToLanguageId = lv.Id, },                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Хаити креолски", TranslationToLanguageId = mk.Id, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Haiti Creole", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ħaitjan", TranslationToLanguageId = mt.Id, },                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Haïtiaans Creools", TranslationToLanguageId = nl.Id, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "haitisk kreolsk", TranslationToLanguageId = no.Id, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Haiti Creole", TranslationToLanguageId = pl.Id, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "crioulo haitiano", TranslationToLanguageId = pt.Id, },              // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "haitiană Creole", TranslationToLanguageId = ro.Id, },               // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "гаитянский креольский", TranslationToLanguageId = ru.Id, },         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Haitian kreolský", TranslationToLanguageId = sk.Id, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = sl.Id, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "haitian Creole", TranslationToLanguageId = sq.Id, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "хаићански креолски", TranslationToLanguageId = sr.Id, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Haitisk kreol", TranslationToLanguageId = sv.Id, },                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஹெய்டியன் கிரியோல்", TranslationToLanguageId = ta.Id, },       // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "హైటియన్ క్రియోల్", TranslationToLanguageId = te.Id, },                 // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ครีโอลเฮติ", TranslationToLanguageId = th.Id, },                       // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Haiti Creole", TranslationToLanguageId = tr.Id, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "гаїтянський креольський", TranslationToLanguageId = uk.Id, },       // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہیٹی Creole", TranslationToLanguageId = ur.Id, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Haiti", TranslationToLanguageId = vi.Id, },                         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "האַיטיאַן קרעאָלע", TranslationToLanguageId = yi.Id, },              // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "海地克里奥尔语", TranslationToLanguageId = zh.Id, },                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ht.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!hu.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = en.Id, },                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "húngaro", TranslationToLanguageId = es.Id, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Ungarisch", TranslationToLanguageId = de.Id, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "المجري", TranslationToLanguageId = ar.Id, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Hongaars", TranslationToLanguageId = af.Id, },                   // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "macar", TranslationToLanguageId = az.Id, },                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Венгерская", TranslationToLanguageId = be.Id, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "унгарски", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "হাঙ্গেরীয়", TranslationToLanguageId = bn.Id, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "hongarès", TranslationToLanguageId = ca.Id, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "maďarština", TranslationToLanguageId = cs.Id, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Hwngari", TranslationToLanguageId = cy.Id, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "ungarsk", TranslationToLanguageId = da.Id, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "ungari", TranslationToLanguageId = et.Id, },                     // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = eu.Id, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مجارستانی", TranslationToLanguageId = fa.Id, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "unkari", TranslationToLanguageId = fi.Id, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "hongroises", TranslationToLanguageId = fr.Id, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Ungáiris", TranslationToLanguageId = ga.Id, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Húngaro", TranslationToLanguageId = gl.Id, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "હંગેરિયન", TranslationToLanguageId = gu.Id, },                    // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הונגרי", TranslationToLanguageId = he.Id, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "हंगेरी", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "mađarski", TranslationToLanguageId = hr.Id, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Ongwa", TranslationToLanguageId = ht.Id, },                      // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "magyar", TranslationToLanguageId = hu.Id, },                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հունգարերեն", TranslationToLanguageId = hy.Id, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Hongaria", TranslationToLanguageId = id.Id, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Ungverska", TranslationToLanguageId = isLanguage.Id, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ungherese", TranslationToLanguageId = it.Id, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ハンガリー", TranslationToLanguageId = ja.Id, },                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "უნგრეთის", TranslationToLanguageId = ka.Id, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹಂಗೇರಿಯ", TranslationToLanguageId = kn.Id, },                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "헝가리어", TranslationToLanguageId = ko.Id, },                    // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hungarica", TranslationToLanguageId = la.Id, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "vengrų", TranslationToLanguageId = lt.Id, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Ungārijas", TranslationToLanguageId = lv.Id, },                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "унгарскиот", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Hungary", TranslationToLanguageId = ms.Id, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ungeriż", TranslationToLanguageId = mt.Id, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Hongaars", TranslationToLanguageId = nl.Id, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = no.Id, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "węgierski", TranslationToLanguageId = pl.Id, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "húngaro", TranslationToLanguageId = pt.Id, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba maghiară", TranslationToLanguageId = ro.Id, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "венгерский", TranslationToLanguageId = ru.Id, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "maďarčina", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "madžarski", TranslationToLanguageId = sl.Id, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "hungarez", TranslationToLanguageId = sq.Id, },                   // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "мађарски", TranslationToLanguageId = sr.Id, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Ungerska", TranslationToLanguageId = sv.Id, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = sw.Id, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஹங்கேரியன்", TranslationToLanguageId = ta.Id, },             // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "హన్గేరియన్", TranslationToLanguageId = te.Id, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฮังการี", TranslationToLanguageId = th.Id, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Macar", TranslationToLanguageId = tr.Id, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Угорський", TranslationToLanguageId = uk.Id, },                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہنگیرین", TranslationToLanguageId = ur.Id, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Hungary", TranslationToLanguageId = vi.Id, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אונגעריש", TranslationToLanguageId = yi.Id, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "匈牙利", TranslationToLanguageId = zh.Id, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hu.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!hy.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Armenian", TranslationToLanguageId = en.Id, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "armenio", TranslationToLanguageId = es.Id, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Armenisch", TranslationToLanguageId = de.Id, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأرميني", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Armeens", TranslationToLanguageId = af.Id, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "erməni", TranslationToLanguageId = az.Id, },                     // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "армянскі", TranslationToLanguageId = be.Id, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "арменски", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আর্মেনিয়ান", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "armeni", TranslationToLanguageId = ca.Id, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "arménský", TranslationToLanguageId = cs.Id, },                   // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Armenia", TranslationToLanguageId = cy.Id, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "armenske", TranslationToLanguageId = da.Id, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "armeenia", TranslationToLanguageId = et.Id, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Armenian", TranslationToLanguageId = eu.Id, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ارمنی", TranslationToLanguageId = fa.Id, },                     // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Armenian", TranslationToLanguageId = fi.Id, },                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "arméniens", TranslationToLanguageId = fr.Id, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Airméinis", TranslationToLanguageId = ga.Id, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Armenio", TranslationToLanguageId = gl.Id, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આર્મેનિયન", TranslationToLanguageId = gu.Id, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ארמני", TranslationToLanguageId = he.Id, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आर्मीनियाई", TranslationToLanguageId = hi.Id, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Armenski", TranslationToLanguageId = hr.Id, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Amenyen", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "örmény", TranslationToLanguageId = hu.Id, },                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հայերեն", TranslationToLanguageId = hy.Id, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Armenia", TranslationToLanguageId = id.Id, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "armenska", TranslationToLanguageId = isLanguage.Id, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "armeno", TranslationToLanguageId = it.Id, },                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アルメニア語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სომხეთის", TranslationToLanguageId = ka.Id, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅರ್ಮೇನಿಯನ್", TranslationToLanguageId = kn.Id, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아르메니아의", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Armenius", TranslationToLanguageId = la.Id, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "armėnų", TranslationToLanguageId = lt.Id, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "armēņu", TranslationToLanguageId = lv.Id, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "ерменскиот", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Armenia", TranslationToLanguageId = ms.Id, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Armen", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Armeens", TranslationToLanguageId = nl.Id, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "armensk", TranslationToLanguageId = no.Id, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Ormianin", TranslationToLanguageId = pl.Id, },                   // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "armênio", TranslationToLanguageId = pt.Id, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "armean", TranslationToLanguageId = ro.Id, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "армянский", TranslationToLanguageId = ru.Id, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "arménsky", TranslationToLanguageId = sk.Id, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "armenski", TranslationToLanguageId = sl.Id, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "armen", TranslationToLanguageId = sq.Id, },                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јерменски", TranslationToLanguageId = sr.Id, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "armeniska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "muarmeni", TranslationToLanguageId = sw.Id, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஆர்மீனியன்", TranslationToLanguageId = ta.Id, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "అర్మేనియా దేశస్తుడు", TranslationToLanguageId = te.Id, },            // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "อาร์เมเนีย", TranslationToLanguageId = th.Id, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Ermeni", TranslationToLanguageId = tr.Id, },                     // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "вірменський", TranslationToLanguageId = uk.Id, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آرمینیا", TranslationToLanguageId = ur.Id, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Armenia", TranslationToLanguageId = vi.Id, },              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ארמאניש", TranslationToLanguageId = yi.Id, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "亚美尼亚", TranslationToLanguageId = zh.Id, },                    // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hy.Id;
//                    _createLanguageName.Handle(n);
//                });



//            if (!id.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Indonesian", TranslationToLanguageId = en.Id, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "indonesio", TranslationToLanguageId = es.Id, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Indonesier", TranslationToLanguageId = de.Id, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأندونيسية", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Indonesies", TranslationToLanguageId = af.Id, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İndoneziya", TranslationToLanguageId = az.Id, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "інданезійская", TranslationToLanguageId = be.Id, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "индонезийски", TranslationToLanguageId = bg.Id, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইন্দোনেশীয়", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "indonesi", TranslationToLanguageId = ca.Id, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Indonésan", TranslationToLanguageId = cs.Id, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Indonesia", TranslationToLanguageId = cy.Id, },                     // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "indonesisk", TranslationToLanguageId = da.Id, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "indoneesia", TranslationToLanguageId = et.Id, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Indonesian", TranslationToLanguageId = eu.Id, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اندونزی", TranslationToLanguageId = fa.Id, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "indonesialainen", TranslationToLanguageId = fi.Id, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "indonésienne", TranslationToLanguageId = fr.Id, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Indinéisis", TranslationToLanguageId = ga.Id, },                    // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Indonesio", TranslationToLanguageId = gl.Id, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઇન્ડોનેશિયન", TranslationToLanguageId = gu.Id, },                     // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אינדונזית", TranslationToLanguageId = he.Id, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "इंडोनेशिया का या उससे संबद्ध", TranslationToLanguageId = hi.Id, },       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "indonezijski", TranslationToLanguageId = hr.Id, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Endonezyen", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "indonéz", TranslationToLanguageId = hu.Id, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ինդոնեզերեն", TranslationToLanguageId = hy.Id, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bahasa Indonesia", TranslationToLanguageId = id.Id, },              // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "indónesísku", TranslationToLanguageId = isLanguage.Id, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "indonesiano", TranslationToLanguageId = it.Id, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "インドネシア", TranslationToLanguageId = ja.Id, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ინდონეზიის", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಇಂಡೋನೀಷಿಯ ದೇಶಕ್ಕೆ ಸೇರಿದ", TranslationToLanguageId = kn.Id, },        // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "인도네시아의", TranslationToLanguageId = ko.Id, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Indonesiaca", TranslationToLanguageId = la.Id, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Indonezijos", TranslationToLanguageId = lt.Id, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Indonēzijas", TranslationToLanguageId = lv.Id, },                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "индонезиски", TranslationToLanguageId = mk.Id, },                   // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Indonesia", TranslationToLanguageId = ms.Id, },                     // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Indoneżjan", TranslationToLanguageId = mt.Id, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Indonesisch", TranslationToLanguageId = nl.Id, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "indonesisk", TranslationToLanguageId = no.Id, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "indonezyjski", TranslationToLanguageId = pl.Id, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "indonésio", TranslationToLanguageId = pt.Id, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "indoneziană", TranslationToLanguageId = ro.Id, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "индонезийский", TranslationToLanguageId = ru.Id, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Indonézan", TranslationToLanguageId = sk.Id, },                     // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "indonezijski", TranslationToLanguageId = sl.Id, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "indonezian", TranslationToLanguageId = sq.Id, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "индонезијски", TranslationToLanguageId = sr.Id, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "indonesiska", TranslationToLanguageId = sv.Id, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiindonesia", TranslationToLanguageId = sw.Id, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இந்தோனேசிய", TranslationToLanguageId = ta.Id, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఇండోనేషియా", TranslationToLanguageId = te.Id, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวอินโดนีเซีย", TranslationToLanguageId = th.Id, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Endonezyalı", TranslationToLanguageId = tr.Id, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Індонезійська", TranslationToLanguageId = uk.Id, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "انڈونیشی", TranslationToLanguageId = ur.Id, },                     // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Indonesia", TranslationToLanguageId = vi.Id, },                     // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אינדאָנעזיש", TranslationToLanguageId = yi.Id, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "印度尼西亚", TranslationToLanguageId = zh.Id, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = id.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!isLanguage.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Icelandic", TranslationToLanguageId = en.Id, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "islandés", TranslationToLanguageId = es.Id, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Isländisch", TranslationToLanguageId = de.Id, },                   // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأيسلندية", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Yslands", TranslationToLanguageId = af.Id, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "island", TranslationToLanguageId = az.Id, },                       // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Ісландская", TranslationToLanguageId = be.Id, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "исландски", TranslationToLanguageId = bg.Id, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আইস্ল্যাণ্ডের ভাষা", TranslationToLanguageId = bn.Id, },                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "islandès", TranslationToLanguageId = ca.Id, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "islandský", TranslationToLanguageId = cs.Id, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Islandeg", TranslationToLanguageId = cy.Id, },                     // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "islandsk", TranslationToLanguageId = da.Id, },                     // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "islandi", TranslationToLanguageId = et.Id, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Islandiako", TranslationToLanguageId = eu.Id, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان ایسلندی", TranslationToLanguageId = fa.Id, },               // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Islannin", TranslationToLanguageId = fi.Id, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "islandais", TranslationToLanguageId = fr.Id, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Íoslainnis", TranslationToLanguageId = ga.Id, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Islandés", TranslationToLanguageId = gl.Id, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આઇસલેન્ડિક", TranslationToLanguageId = gu.Id, },                    // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "איסלנדית", TranslationToLanguageId = he.Id, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आइसलैंड का", TranslationToLanguageId = hi.Id, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "islandski", TranslationToLanguageId = hr.Id, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "icelandic", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "izlandi", TranslationToLanguageId = hu.Id, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իսլանդերեն", TranslationToLanguageId = hy.Id, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Islandia", TranslationToLanguageId = id.Id, },                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Íslenska", TranslationToLanguageId = isLanguage.Id, },             // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "islandese", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アイスランド語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ისლანდიის", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಐಸ್ಲ್ಯಾಂಡಿಕ್", TranslationToLanguageId = kn.Id, },                     // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아이슬란드의", TranslationToLanguageId = ko.Id, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Islandica", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Islandijos", TranslationToLanguageId = lt.Id, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "islandiešu", TranslationToLanguageId = lv.Id, },                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Исланд", TranslationToLanguageId = mk.Id, },                       // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Iceland", TranslationToLanguageId = ms.Id, },                      // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Iceland", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "IJslands", TranslationToLanguageId = nl.Id, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "islandsk", TranslationToLanguageId = no.Id, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "islandzki", TranslationToLanguageId = pl.Id, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "islandês", TranslationToLanguageId = pt.Id, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "islandez", TranslationToLanguageId = ro.Id, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "исландский", TranslationToLanguageId = ru.Id, },                   // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "islandský", TranslationToLanguageId = sk.Id, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "islandski", TranslationToLanguageId = sl.Id, },                    // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "islandez", TranslationToLanguageId = sq.Id, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "исландски", TranslationToLanguageId = sr.Id, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "isländska", TranslationToLanguageId = sv.Id, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiaislandi", TranslationToLanguageId = sw.Id, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஐஸ்லாந்து", TranslationToLanguageId = ta.Id, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఐస్లాండిక్", TranslationToLanguageId = te.Id, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ไอซ์แลนด์", TranslationToLanguageId = th.Id, },                       // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İzlanda", TranslationToLanguageId = tr.Id, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Ісландська", TranslationToLanguageId = uk.Id, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آئس لینڈی", TranslationToLanguageId = ur.Id, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Iceland", TranslationToLanguageId = vi.Id, },                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "איסלענדיש", TranslationToLanguageId = yi.Id, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "冰岛", TranslationToLanguageId = zh.Id, },                         // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = isLanguage.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!it.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = en.Id, },                         // #1  Language = "English"
//                    new CreateLanguageName { Text = "italiano", TranslationToLanguageId = es.Id, },                         // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Italienisch", TranslationToLanguageId = de.Id, },                      // #3  Language = "German"
//                    new CreateLanguageName { Text = "إيطالي", TranslationToLanguageId = ar.Id, },                          // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Italiaanse", TranslationToLanguageId = af.Id, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "italyan", TranslationToLanguageId = az.Id, },                          // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Італьянская", TranslationToLanguageId = be.Id, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "италиански", TranslationToLanguageId = bg.Id, },                       // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইতালিয়", TranslationToLanguageId = bn.Id, },                            // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "italià", TranslationToLanguageId = ca.Id, },                           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Ital", TranslationToLanguageId = cs.Id, },                             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Eidaleg", TranslationToLanguageId = cy.Id, },                          // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "italiensk", TranslationToLanguageId = da.Id, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "itaalia", TranslationToLanguageId = et.Id, },                          // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = eu.Id, },                          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ایتالیایی", TranslationToLanguageId = fa.Id, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = fi.Id, },                          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "italienne", TranslationToLanguageId = fr.Id, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Iodáilis", TranslationToLanguageId = ga.Id, },                         // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Italiano", TranslationToLanguageId = gl.Id, },                         // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઇટાલીનું", TranslationToLanguageId = gu.Id, },                           // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "איטלקית", TranslationToLanguageId = he.Id, },                         // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "इतालवी", TranslationToLanguageId = hi.Id, },                           // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "talijanski", TranslationToLanguageId = hr.Id, },                       // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Italyen", TranslationToLanguageId = ht.Id, },                          // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "olasz", TranslationToLanguageId = hu.Id, },                            // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իտալերեն", TranslationToLanguageId = hy.Id, },                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Italia", TranslationToLanguageId = id.Id, },                           // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Ítalska", TranslationToLanguageId = isLanguage.Id, },                  // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "italiano", TranslationToLanguageId = it.Id, },                         // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "イタリア", TranslationToLanguageId = ja.Id, },                          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "იტალიური", TranslationToLanguageId = ka.Id, },                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಇಟಲಿಯವ", TranslationToLanguageId = kn.Id, },                          // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "이탈리아의", TranslationToLanguageId = ko.Id, },                        // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Italica", TranslationToLanguageId = la.Id, },                          // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Italijos", TranslationToLanguageId = lt.Id, },                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Itālijas", TranslationToLanguageId = lv.Id, },                         // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "италијански", TranslationToLanguageId = mk.Id, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Itali", TranslationToLanguageId = ms.Id, },                            // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Taljan", TranslationToLanguageId = mt.Id, },                           // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Italiaans", TranslationToLanguageId = nl.Id, },                        // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = no.Id, },                          // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "włoski", TranslationToLanguageId = pl.Id, },                           // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "italiano", TranslationToLanguageId = pt.Id, },                         // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "italian", TranslationToLanguageId = ro.Id, },                          // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "итальянский", TranslationToLanguageId = ru.Id, },                      // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Talian", TranslationToLanguageId = sk.Id, },                           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Italijanska", TranslationToLanguageId = sl.Id, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "italian", TranslationToLanguageId = sq.Id, },                          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "италијански", TranslationToLanguageId = sr.Id, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "italienska", TranslationToLanguageId = sv.Id, },                       // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Italia", TranslationToLanguageId = sw.Id, },                           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இத்தாலிய நாட்டை சார்ந்த", TranslationToLanguageId = ta.Id, },      // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఇటాలియన్", TranslationToLanguageId = te.Id, },                         // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "อิตาเลียน", TranslationToLanguageId = th.Id, },                           // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İtalyan", TranslationToLanguageId = tr.Id, },                          // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Італійська", TranslationToLanguageId = uk.Id, },                       // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "اطالوی", TranslationToLanguageId = ur.Id, },                          // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ý", TranslationToLanguageId = vi.Id, },                                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "איטאַליעניש", TranslationToLanguageId = yi.Id, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "意大利", TranslationToLanguageId = zh.Id, },                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = it.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (!ja.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Japanese", TranslationToLanguageId = en.Id, },                     // #1  Language = "English"
//                    new CreateLanguageName { Text = "japonés", TranslationToLanguageId = es.Id, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Japaner", TranslationToLanguageId = de.Id, },                       // #3  Language = "German"
//                    new CreateLanguageName { Text = "اليابانية", TranslationToLanguageId = ar.Id, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Japannese", TranslationToLanguageId = af.Id, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "yapon", TranslationToLanguageId = az.Id, },                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Японская", TranslationToLanguageId = be.Id, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "японски", TranslationToLanguageId = bg.Id, },                       // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "জাপানি", TranslationToLanguageId = bn.Id, },                         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "japonès", TranslationToLanguageId = ca.Id, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Japonec", TranslationToLanguageId = cs.Id, },                       // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Siapaneaidd", TranslationToLanguageId = cy.Id, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Japansk", TranslationToLanguageId = da.Id, },                       // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "jaapani", TranslationToLanguageId = et.Id, },                       // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Japoniako", TranslationToLanguageId = eu.Id, },                     // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ژاپنی", TranslationToLanguageId = fa.Id, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "japanilainen", TranslationToLanguageId = fi.Id, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "japonaise", TranslationToLanguageId = fr.Id, },                     // #18 Language = "French"
//                    new CreateLanguageName { Text = "Seapáinis", TranslationToLanguageId = ga.Id, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "xaponés", TranslationToLanguageId = gl.Id, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "જાપાનીઝ", TranslationToLanguageId = gu.Id, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "יפנית", TranslationToLanguageId = he.Id, },                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "जापानी", TranslationToLanguageId = hi.Id, },                         // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "japanski", TranslationToLanguageId = hr.Id, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Japonè", TranslationToLanguageId = ht.Id, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "japán", TranslationToLanguageId = hu.Id, },                         // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ճապոնացի", TranslationToLanguageId = hy.Id, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Jepang", TranslationToLanguageId = id.Id, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "japanska", TranslationToLanguageId = isLanguage.Id, },              // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "giapponese", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "日本", TranslationToLanguageId = ja.Id, },                          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "იაპონური", TranslationToLanguageId = ka.Id, },                     // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಜಪಾನಿನವನು", TranslationToLanguageId = kn.Id, },                     // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "일본어", TranslationToLanguageId = ko.Id, },                        // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Iaponica", TranslationToLanguageId = la.Id, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Japonijos", TranslationToLanguageId = lt.Id, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Japānas", TranslationToLanguageId = lv.Id, },                       // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "јапонски", TranslationToLanguageId = mk.Id, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Jepun", TranslationToLanguageId = ms.Id, },                         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ġappuniż", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Japans", TranslationToLanguageId = nl.Id, },                        // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Japanese", TranslationToLanguageId = no.Id, },                      // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "japoński", TranslationToLanguageId = pl.Id, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "japonês", TranslationToLanguageId = pt.Id, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "japonez", TranslationToLanguageId = ro.Id, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "японский", TranslationToLanguageId = ru.Id, },                      // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Japonec", TranslationToLanguageId = sk.Id, },                       // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "japonski", TranslationToLanguageId = sl.Id, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "japonisht", TranslationToLanguageId = sq.Id, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јапански", TranslationToLanguageId = sr.Id, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Japanska", TranslationToLanguageId = sv.Id, },                      // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kijapani", TranslationToLanguageId = sw.Id, },                      // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஜப்பனீஸ்", TranslationToLanguageId = ta.Id, },                    // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "జాపనీస్", TranslationToLanguageId = te.Id, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาญี่ปุ่น", TranslationToLanguageId = th.Id, },                       // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Japon", TranslationToLanguageId = tr.Id, },                         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Японський", TranslationToLanguageId = uk.Id, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "جاپانی", TranslationToLanguageId = ur.Id, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Nhật Bản", TranslationToLanguageId = vi.Id, },                      // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "יאַפּאַניש", TranslationToLanguageId = yi.Id, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "日本", TranslationToLanguageId = zh.Id, },                          // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ja.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ka.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Georgian", TranslationToLanguageId = en.Id, },                       // #1  Language = "English"
//                    new CreateLanguageName { Text = "georgiano", TranslationToLanguageId = es.Id, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Georgier", TranslationToLanguageId = de.Id, },                       // #3  Language = "German"
//                    new CreateLanguageName { Text = "الجورجية", TranslationToLanguageId = ar.Id, },                      // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Georgiaans", TranslationToLanguageId = af.Id, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "gürcü", TranslationToLanguageId = az.Id, },                          // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "грузінскі", TranslationToLanguageId = be.Id, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "грузински", TranslationToLanguageId = bg.Id, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "জর্জিয়াদেশীয়", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "georgià", TranslationToLanguageId = ca.Id, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "gruzínský", TranslationToLanguageId = cs.Id, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Georgaidd", TranslationToLanguageId = cy.Id, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "georgisk", TranslationToLanguageId = da.Id, },                       // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "gruusia", TranslationToLanguageId = et.Id, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Georgian", TranslationToLanguageId = eu.Id, },                       // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "گرجی", TranslationToLanguageId = fa.Id, },                          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "georgialainen", TranslationToLanguageId = fi.Id, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "géorgienne", TranslationToLanguageId = fr.Id, },                     // #18 Language = "French"
//                    new CreateLanguageName { Text = "Seoirsis", TranslationToLanguageId = ga.Id, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "xeorxiano", TranslationToLanguageId = gl.Id, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "જ્યોર્જિયન", TranslationToLanguageId = gu.Id, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גאורגיה", TranslationToLanguageId = he.Id, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "जार्जकालीन", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "gruzijski", TranslationToLanguageId = hr.Id, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "georgian", TranslationToLanguageId = ht.Id, },                       // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "grúz", TranslationToLanguageId = hu.Id, },                           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "վրացերեն", TranslationToLanguageId = hy.Id, },                      // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Georgia", TranslationToLanguageId = id.Id, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Georgian", TranslationToLanguageId = isLanguage.Id, },               // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "georgiano", TranslationToLanguageId = it.Id, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ジョージアン", TranslationToLanguageId = ja.Id, },                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "საქართველოს", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಜಾರ್ಜಿಯನ್", TranslationToLanguageId = kn.Id, },                       // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "그루지야의", TranslationToLanguageId = ko.Id, },                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Pontica", TranslationToLanguageId = la.Id, },                        // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "gruzinų", TranslationToLanguageId = lt.Id, },                        // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "gruzīnu", TranslationToLanguageId = lv.Id, },                        // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Грузија", TranslationToLanguageId = mk.Id, },                        // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Georgia", TranslationToLanguageId = ms.Id, },                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ġorġjan", TranslationToLanguageId = mt.Id, },                        // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Georgisch", TranslationToLanguageId = nl.Id, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "georgisk", TranslationToLanguageId = no.Id, },                       // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "gruziński", TranslationToLanguageId = pl.Id, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "georgiano", TranslationToLanguageId = pt.Id, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "georgian", TranslationToLanguageId = ro.Id, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "грузинский", TranslationToLanguageId = ru.Id, },                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "gruzínsky", TranslationToLanguageId = sk.Id, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "gruzijski", TranslationToLanguageId = sl.Id, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "gjeorgjian", TranslationToLanguageId = sq.Id, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "грузијски", TranslationToLanguageId = sr.Id, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "georgiska", TranslationToLanguageId = sv.Id, },                      // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kijiojia", TranslationToLanguageId = sw.Id, },                       // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஜோர்ஜிய", TranslationToLanguageId = ta.Id, },                     // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "జార్జియన్", TranslationToLanguageId = te.Id, },                         // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "จอร์เจีย", TranslationToLanguageId = th.Id, },                          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Gürcü", TranslationToLanguageId = tr.Id, },                          // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Грузинський", TranslationToLanguageId = uk.Id, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "جارجیا", TranslationToLanguageId = ur.Id, },                        // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Georgia", TranslationToLanguageId = vi.Id, },                        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "גרוזיניש", TranslationToLanguageId = yi.Id, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "格鲁吉亚", TranslationToLanguageId = zh.Id, },                        // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ka.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!kn.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = en.Id, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "Canarés", TranslationToLanguageId = es.Id, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = de.Id, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "الكانادا", TranslationToLanguageId = ar.Id, },              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = af.Id, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = az.Id, },                // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = be.Id, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = bg.Id, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "কন্নড", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = ca.Id, },                // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = cs.Id, },                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = cy.Id, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = da.Id, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = et.Id, },                // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "kannadera", TranslationToLanguageId = eu.Id, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کانادهای", TranslationToLanguageId = fa.Id, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = fr.Id, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Cannadais", TranslationToLanguageId = ga.Id, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Canara", TranslationToLanguageId = gl.Id, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કન્નડા", TranslationToLanguageId = gu.Id, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קנאדה", TranslationToLanguageId = he.Id, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "कन्नड़", TranslationToLanguageId = hi.Id, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = hr.Id, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = ht.Id, },                // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = hu.Id, },                // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "կաննադա", TranslationToLanguageId = hy.Id, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = id.Id, },                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = isLanguage.Id, },        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = it.Id, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "カンナダ語", TranslationToLanguageId = ja.Id, },              // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "კანადა", TranslationToLanguageId = ka.Id, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕನ್ನಡ", TranslationToLanguageId = kn.Id, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "칸나다어", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Kannadica", TranslationToLanguageId = la.Id, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "kanadų", TranslationToLanguageId = lt.Id, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = lv.Id, },                // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Канада", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = ms.Id, },                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = mt.Id, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = nl.Id, },                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = no.Id, },                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = pl.Id, },                // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "canará", TranslationToLanguageId = pt.Id, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = ro.Id, },                // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = ru.Id, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sk.Id, },                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = sl.Id, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sq.Id, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = sr.Id, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sv.Id, },                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கன்னடம்", TranslationToLanguageId = ta.Id, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "కన్నడ", TranslationToLanguageId = te.Id, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "กันนาดา", TranslationToLanguageId = th.Id, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = tr.Id, },                // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = uk.Id, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کناڈا", TranslationToLanguageId = ur.Id, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = vi.Id, },                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קאַנאַדאַ", TranslationToLanguageId = yi.Id, },                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "卡纳达语", TranslationToLanguageId = zh.Id, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kn.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ko.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = en.Id, },                                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "coreano", TranslationToLanguageId = es.Id, },                                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Koreanisch", TranslationToLanguageId = de.Id, },                              // #3  Language = "German"
//                    new CreateLanguageName { Text = "كوري", TranslationToLanguageId = ar.Id, },                                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Koreaanse", TranslationToLanguageId = af.Id, },                               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Koreya", TranslationToLanguageId = az.Id, },                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Карэйская", TranslationToLanguageId = be.Id, },                               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "корейски", TranslationToLanguageId = bg.Id, },                                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "কোরিয়ান", TranslationToLanguageId = bn.Id, },                                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "coreà", TranslationToLanguageId = ca.Id, },                                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "korejský", TranslationToLanguageId = cs.Id, },                                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Corea", TranslationToLanguageId = cy.Id, },                                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = da.Id, },                                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "korea", TranslationToLanguageId = et.Id, },                                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = eu.Id, },                                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کره ای", TranslationToLanguageId = fa.Id, },                                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = fi.Id, },                                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "coréenne", TranslationToLanguageId = fr.Id, },                                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Cóiréis", TranslationToLanguageId = ga.Id, },                                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Coreano", TranslationToLanguageId = gl.Id, },                                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કોરિયન", TranslationToLanguageId = gu.Id, },                                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קוריאה", TranslationToLanguageId = he.Id, },                                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "कोरियाई", TranslationToLanguageId = hi.Id, },                                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "korejski", TranslationToLanguageId = hr.Id, },                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Kore", TranslationToLanguageId = ht.Id, },                                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "koreai", TranslationToLanguageId = hu.Id, },                                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "կորեական", TranslationToLanguageId = hy.Id, },                               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Korea", TranslationToLanguageId = id.Id, },                                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Kóreumaður", TranslationToLanguageId = isLanguage.Id, },                      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "coreano", TranslationToLanguageId = it.Id, },                                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "韓国", TranslationToLanguageId = ja.Id, },                                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "კორეის", TranslationToLanguageId = ka.Id, },                                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕೊರಿಯಾ ದೇಶದವನು ಯಾ ಆ ದೇಶದ ಭಾಷೆ", TranslationToLanguageId = kn.Id, },        // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "한국의", TranslationToLanguageId = ko.Id, },                                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Lorem", TranslationToLanguageId = la.Id, },                                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "korėjiečių", TranslationToLanguageId = lt.Id, },                              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "korejiešu", TranslationToLanguageId = lv.Id, },                               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Кореја", TranslationToLanguageId = mk.Id, },                                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Korea", TranslationToLanguageId = ms.Id, },                                   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Koreana", TranslationToLanguageId = mt.Id, },                                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Koreaans", TranslationToLanguageId = nl.Id, },                                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "koreansk", TranslationToLanguageId = no.Id, },                                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "koreański", TranslationToLanguageId = pl.Id, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "coreano", TranslationToLanguageId = pt.Id, },                                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "coreean", TranslationToLanguageId = ro.Id, },                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "корейский", TranslationToLanguageId = ru.Id, },                               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "kórejský", TranslationToLanguageId = sk.Id, },                                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = sl.Id, },                                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "korean", TranslationToLanguageId = sq.Id, },                                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "корејски", TranslationToLanguageId = sr.Id, },                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "koreanska", TranslationToLanguageId = sv.Id, },                               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kikorea", TranslationToLanguageId = sw.Id, },                                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கொரிய", TranslationToLanguageId = ta.Id, },                                // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "కొరియా", TranslationToLanguageId = te.Id, },                                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกาหลี", TranslationToLanguageId = th.Id, },                                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Kore", TranslationToLanguageId = tr.Id, },                                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Корейська", TranslationToLanguageId = uk.Id, },                               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کورین", TranslationToLanguageId = ur.Id, },                                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Hàn Quốc", TranslationToLanguageId = vi.Id, },                                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קאָרעיִש", TranslationToLanguageId = yi.Id, },                                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "韩国", TranslationToLanguageId = zh.Id, },                                    // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ko.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!la.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = en.Id, },                       // #1  Language = "English"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = es.Id, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Latein", TranslationToLanguageId = de.Id, },                      // #3  Language = "German"
//                    new CreateLanguageName { Text = "لاتينية", TranslationToLanguageId = ar.Id, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Latyns-", TranslationToLanguageId = af.Id, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "latın", TranslationToLanguageId = az.Id, },                       // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "лацінскі", TranslationToLanguageId = be.Id, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "латински", TranslationToLanguageId = bg.Id, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ল্যাটিন", TranslationToLanguageId = bn.Id, },                       // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "llatí", TranslationToLanguageId = ca.Id, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "latina", TranslationToLanguageId = cs.Id, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Lladin", TranslationToLanguageId = cy.Id, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = da.Id, },                       // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "ladina", TranslationToLanguageId = et.Id, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = eu.Id, },                       // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "لاتین", TranslationToLanguageId = fa.Id, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Latinalaisen", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "latine", TranslationToLanguageId = fr.Id, },                      // #18 Language = "French"
//                    new CreateLanguageName { Text = "لاتین", TranslationToLanguageId = ga.Id, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = gl.Id, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "લેટિન", TranslationToLanguageId = gu.Id, },                        // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "לטינית", TranslationToLanguageId = he.Id, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "लातौनी", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "latinski", TranslationToLanguageId = hr.Id, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Latin lan", TranslationToLanguageId = ht.Id, },                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "latin", TranslationToLanguageId = hu.Id, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "լատիներեն", TranslationToLanguageId = hy.Id, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = id.Id, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = isLanguage.Id, },               // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = it.Id, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ラテン", TranslationToLanguageId = ja.Id, },                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ლათინური", TranslationToLanguageId = ka.Id, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಪ್ರಾಚೀನ ರೋಮನರ", TranslationToLanguageId = kn.Id, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "라틴어", TranslationToLanguageId = ko.Id, },                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "latine", TranslationToLanguageId = la.Id, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "lotynų", TranslationToLanguageId = lt.Id, },                      // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "latīņu", TranslationToLanguageId = lv.Id, },                      // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Латинска", TranslationToLanguageId = mk.Id, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = ms.Id, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "latin", TranslationToLanguageId = mt.Id, },                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Latijn", TranslationToLanguageId = nl.Id, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = no.Id, },                       // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "łacina", TranslationToLanguageId = pl.Id, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = pt.Id, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "latin", TranslationToLanguageId = ro.Id, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "латинский", TranslationToLanguageId = ru.Id, },                   // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "latinčina", TranslationToLanguageId = sk.Id, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = sl.Id, },                       // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "latinisht", TranslationToLanguageId = sq.Id, },                   // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "латински", TranslationToLanguageId = sr.Id, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "latinska", TranslationToLanguageId = sv.Id, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kilatini", TranslationToLanguageId = sw.Id, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இலத்தீன் மொழி", TranslationToLanguageId = ta.Id, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "లాటిన్", TranslationToLanguageId = te.Id, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ละติน", TranslationToLanguageId = th.Id, },                        // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = tr.Id, },                       // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Латинську", TranslationToLanguageId = uk.Id, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "لاطینی", TranslationToLanguageId = ur.Id, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = vi.Id, },                       // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "לאַטייַן", TranslationToLanguageId = yi.Id, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "拉丁美洲", TranslationToLanguageId = zh.Id, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = la.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!lt.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = en.Id, },                                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = es.Id, },                                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Litauisch", TranslationToLanguageId = de.Id, },                                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "لتواني", TranslationToLanguageId = ar.Id, },                                       // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Litaus", TranslationToLanguageId = af.Id, },                                        // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Litva", TranslationToLanguageId = az.Id, },                                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Літоўскі", TranslationToLanguageId = be.Id, },                                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "литовски", TranslationToLanguageId = bg.Id, },                                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "লিথুনিয়ান", TranslationToLanguageId = bn.Id, },                                       // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "lituà", TranslationToLanguageId = ca.Id, },                                         // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "litevský", TranslationToLanguageId = cs.Id, },                                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Lithwaneg", TranslationToLanguageId = cy.Id, },                                     // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "litauisk", TranslationToLanguageId = da.Id, },                                      // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "leedu", TranslationToLanguageId = et.Id, },                                         // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Lithuaniera", TranslationToLanguageId = eu.Id, },                                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان لیتوانی", TranslationToLanguageId = fa.Id, },                                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Liettuan", TranslationToLanguageId = fi.Id, },                                      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Lituanie", TranslationToLanguageId = fr.Id, },                                      // #18 Language = "French"
//                    new CreateLanguageName { Text = "Liotuáinis", TranslationToLanguageId = ga.Id, },                                    // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Lituano", TranslationToLanguageId = gl.Id, },                                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "લિથુનિયન", TranslationToLanguageId = gu.Id, },                                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ליטא", TranslationToLanguageId = he.Id, },                                         // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "लिथुअनिअन की भाषा", TranslationToLanguageId = hi.Id, },                             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "litvanski", TranslationToLanguageId = hr.Id, },                                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Haitian", TranslationToLanguageId = ht.Id, },                                       // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "litván", TranslationToLanguageId = hu.Id, },                                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "լիտվական", TranslationToLanguageId = hy.Id, },                                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = id.Id, },                                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = isLanguage.Id, },                            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = it.Id, },                                       // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "リトアニア語", TranslationToLanguageId = ja.Id, },                                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ლიტვის", TranslationToLanguageId = ka.Id, },                                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರಷ್ಯಾದ ಬಾಲ್ಟಿಕ್ ಗಣರಾಜ್ಯವಾದ ಲಿಥುಯೇನಿಯದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguageId = kn.Id, },   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "리투아니아의", TranslationToLanguageId = ko.Id, },                                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Lithuaniae", TranslationToLanguageId = la.Id, },                                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Lietuvos", TranslationToLanguageId = lt.Id, },                                      // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Lietuvas", TranslationToLanguageId = lv.Id, },                                      // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = mk.Id, },                                     // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Lithuania", TranslationToLanguageId = ms.Id, },                              // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Litwana", TranslationToLanguageId = mt.Id, },                                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Litouws", TranslationToLanguageId = nl.Id, },                                       // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = no.Id, },                                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "litewski", TranslationToLanguageId = pl.Id, },                                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = pt.Id, },                                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba lituaniană", TranslationToLanguageId = ro.Id, },                              // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "литовский", TranslationToLanguageId = ru.Id, },                                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "litovský", TranslationToLanguageId = sk.Id, },                                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "litovski", TranslationToLanguageId = sl.Id, },                                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "lituanisht", TranslationToLanguageId = sq.Id, },                                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = sr.Id, },                                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "litauiska", TranslationToLanguageId = sv.Id, },                                     // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kilithuania", TranslationToLanguageId = sw.Id, },                                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "லிதுயேனியன்", TranslationToLanguageId = ta.Id, },                                // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "లిథువేనియన్", TranslationToLanguageId = te.Id, },                                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกี่ยวกับประเทศลิธัวเนีย", TranslationToLanguageId = th.Id, },                               // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Litvanya", TranslationToLanguageId = tr.Id, },                                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Литовська", TranslationToLanguageId = uk.Id, },                                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "لتھواینین", TranslationToLanguageId = ur.Id, },                                    // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = vi.Id, },                                     // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ליטוויש", TranslationToLanguageId = yi.Id, },                                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "立陶宛", TranslationToLanguageId = zh.Id, },                                         // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lt.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!lv.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Latvian", TranslationToLanguageId = en.Id, },                                          // #1  Language = "English"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = es.Id, },                                          // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Litauisch", TranslationToLanguageId = de.Id, },                                        // #3  Language = "German"
//                    new CreateLanguageName { Text = "لتواني", TranslationToLanguageId = ar.Id, },                                          // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Litaus", TranslationToLanguageId = af.Id, },                                           // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Litva", TranslationToLanguageId = az.Id, },                                            // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Літоўскі", TranslationToLanguageId = be.Id, },                                         // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "литовски", TranslationToLanguageId = bg.Id, },                                         // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "লিথুনিয়ান", TranslationToLanguageId = bn.Id, },                                          // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "lituà", TranslationToLanguageId = ca.Id, },                                            // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "litevský", TranslationToLanguageId = cs.Id, },                                         // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Lithwaneg", TranslationToLanguageId = cy.Id, },                                        // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "litauisk", TranslationToLanguageId = da.Id, },                                         // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "leedu", TranslationToLanguageId = et.Id, },                                            // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Lithuaniera", TranslationToLanguageId = eu.Id, },                                      // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان لیتوانی", TranslationToLanguageId = fa.Id, },                                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Liettuan", TranslationToLanguageId = fi.Id, },                                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Lituanie", TranslationToLanguageId = fr.Id, },                                         // #18 Language = "French"
//                    new CreateLanguageName { Text = "Liotuáinis", TranslationToLanguageId = ga.Id, },                                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Lituano", TranslationToLanguageId = gl.Id, },                                          // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "લિથુનિયન", TranslationToLanguageId = gu.Id, },                                         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ליטא", TranslationToLanguageId = he.Id, },                                            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "लिथुअनिअन की भाषा", TranslationToLanguageId = hi.Id, },                                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "litvanski", TranslationToLanguageId = hr.Id, },                                        // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Haitian", TranslationToLanguageId = ht.Id, },                                          // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "litván", TranslationToLanguageId = hu.Id, },                                           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "լիտվական", TranslationToLanguageId = hy.Id, },                                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = id.Id, },                                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = isLanguage.Id, },                               // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = it.Id, },                                          // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "リトアニア語", TranslationToLanguageId = ja.Id, },                                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ლიტვის", TranslationToLanguageId = ka.Id, },                                          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರಷ್ಯಾದ ಬಾಲ್ಟಿಕ್ ಗಣರಾಜ್ಯವಾದ ಲಿಥುಯೇನಿಯದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguageId = kn.Id, },      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "리투아니아의", TranslationToLanguageId = ko.Id, },                                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Lithuaniae", TranslationToLanguageId = la.Id, },                                       // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Lietuvos", TranslationToLanguageId = lt.Id, },                                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Lietuvas", TranslationToLanguageId = lv.Id, },                                         // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = mk.Id, },                                        // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Lithuania", TranslationToLanguageId = ms.Id, },                                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Litwana", TranslationToLanguageId = mt.Id, },                                          // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Litouws", TranslationToLanguageId = nl.Id, },                                          // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = no.Id, },                                       // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "litewski", TranslationToLanguageId = pl.Id, },                                         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = pt.Id, },                                          // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba lituaniană", TranslationToLanguageId = ro.Id, },                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "литовский", TranslationToLanguageId = ru.Id, },                                        // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "litovský", TranslationToLanguageId = sk.Id, },                                         // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "litovski", TranslationToLanguageId = sl.Id, },                                         // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "lituanisht", TranslationToLanguageId = sq.Id, },                                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = sr.Id, },                                        // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "litauiska", TranslationToLanguageId = sv.Id, },                                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kilithuania", TranslationToLanguageId = sw.Id, },                                      // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "லிதுயேனியன்", TranslationToLanguageId = ta.Id, },                                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "లిథువేనియన్", TranslationToLanguageId = te.Id, },                                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกี่ยวกับประเทศลิธัวเนีย", TranslationToLanguageId = th.Id, },                                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Litvanya", TranslationToLanguageId = tr.Id, },                                         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Литовська", TranslationToLanguageId = uk.Id, },                                        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "لتھواینین", TranslationToLanguageId = ur.Id, },                                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = vi.Id, },                                        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ליטוויש", TranslationToLanguageId = yi.Id, },                                         // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "立陶宛", TranslationToLanguageId = zh.Id, },                                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lv.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!mk.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Macedonian", TranslationToLanguageId = en.Id, },                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "macedonio", TranslationToLanguageId = es.Id, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Mazedonier", TranslationToLanguageId = de.Id, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "المقدونية", TranslationToLanguageId = ar.Id, },                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Macedonies", TranslationToLanguageId = af.Id, },                 // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "makedoniyalı", TranslationToLanguageId = az.Id, },               // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "македонскай", TranslationToLanguageId = be.Id, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "македонски", TranslationToLanguageId = bg.Id, },                 // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "মাসিডনের লোক", TranslationToLanguageId = bn.Id, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "macedoni", TranslationToLanguageId = ca.Id, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "makedonský", TranslationToLanguageId = cs.Id, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Macedoneg", TranslationToLanguageId = cy.Id, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "makedonsk", TranslationToLanguageId = da.Id, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Makedoonia", TranslationToLanguageId = et.Id, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Mazedonierara", TranslationToLanguageId = eu.Id, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مقدونی", TranslationToLanguageId = fa.Id, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Makedonian", TranslationToLanguageId = fi.Id, },                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "macédoniennes", TranslationToLanguageId = fr.Id, },              // #18 Language = "French"
//                    new CreateLanguageName { Text = "Macadóinis", TranslationToLanguageId = ga.Id, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Macedonia", TranslationToLanguageId = gl.Id, },                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "મેસેડોનિયન", TranslationToLanguageId = gu.Id, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "מקדוניה", TranslationToLanguageId = he.Id, },                   // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "मेसीडोनियन", TranslationToLanguageId = hi.Id, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "makedonski", TranslationToLanguageId = hr.Id, },                 // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Masedonyen", TranslationToLanguageId = ht.Id, },                 // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "macedóniai", TranslationToLanguageId = hu.Id, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "մակեդոներեն", TranslationToLanguageId = hy.Id, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Macedonia", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "makedónska", TranslationToLanguageId = isLanguage.Id, },         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "macedone", TranslationToLanguageId = it.Id, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "マケドニア語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "მაკედონიის", TranslationToLanguageId = ka.Id, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಮೆಸಿಡೋನಿಯನ್", TranslationToLanguageId = kn.Id, },                // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "마케도니아의", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Macedonum", TranslationToLanguageId = la.Id, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Makedonijos", TranslationToLanguageId = lt.Id, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "maķedoniešu", TranslationToLanguageId = lv.Id, },                // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "македонски", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Macedonia", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Maċedonjan", TranslationToLanguageId = mt.Id, },                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Macedonisch", TranslationToLanguageId = nl.Id, },                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "makedonsk", TranslationToLanguageId = no.Id, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "macedoński", TranslationToLanguageId = pl.Id, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "macedônia", TranslationToLanguageId = pt.Id, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "macedonean", TranslationToLanguageId = ro.Id, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "македонский", TranslationToLanguageId = ru.Id, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "macedónsky", TranslationToLanguageId = sk.Id, },                 // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "makedonski", TranslationToLanguageId = sl.Id, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "maqedonas", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "македонски", TranslationToLanguageId = sr.Id, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "makedonska", TranslationToLanguageId = sv.Id, },                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kimasedonia", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மாஸிடோனியன்", TranslationToLanguageId = ta.Id, },          // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "మసడోనియన్", TranslationToLanguageId = te.Id, },                 // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "มาซิโดเนีย", TranslationToLanguageId = th.Id, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Makedonya", TranslationToLanguageId = tr.Id, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Македонський", TranslationToLanguageId = uk.Id, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "مقدونيائی", TranslationToLanguageId = ur.Id, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Macedonia", TranslationToLanguageId = vi.Id, },            // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "מאַקעדאָניש", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "马其顿", TranslationToLanguageId = zh.Id, },                      // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mk.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ms.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = en.Id, },                        // #1  Language = "English"
//                    new CreateLanguageName { Text = "malayo", TranslationToLanguageId = es.Id, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "malaiisch", TranslationToLanguageId = de.Id, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الملايو", TranslationToLanguageId = ar.Id, },                      // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Maleis", TranslationToLanguageId = af.Id, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "malay", TranslationToLanguageId = az.Id, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Малайская", TranslationToLanguageId = be.Id, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "малайски", TranslationToLanguageId = bg.Id, },                     // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "মালয়দেশীয় লোক", TranslationToLanguageId = bn.Id, },                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "malai", TranslationToLanguageId = ca.Id, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Malajská", TranslationToLanguageId = cs.Id, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = cy.Id, },                        // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = da.Id, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "malai", TranslationToLanguageId = et.Id, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Malaysiera", TranslationToLanguageId = eu.Id, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مالایا", TranslationToLanguageId = fa.Id, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "malaiji", TranslationToLanguageId = fi.Id, },                      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "malais", TranslationToLanguageId = fr.Id, },                       // #18 Language = "French"
//                    new CreateLanguageName { Text = "Malaeis", TranslationToLanguageId = ga.Id, },                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "malaio", TranslationToLanguageId = gl.Id, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "મલય", TranslationToLanguageId = gu.Id, },                         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "מלאית", TranslationToLanguageId = he.Id, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "मलायी", TranslationToLanguageId = hi.Id, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "malajski", TranslationToLanguageId = hr.Id, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "malay", TranslationToLanguageId = ht.Id, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "maláj", TranslationToLanguageId = hu.Id, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "մալայական", TranslationToLanguageId = hy.Id, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Melayu", TranslationToLanguageId = id.Id, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = isLanguage.Id, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "malese", TranslationToLanguageId = it.Id, },                       // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "マレー語", TranslationToLanguageId = ja.Id, },                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "მალაური", TranslationToLanguageId = ka.Id, },                     // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಮಲಯಾ ಭಾಷೆಯ", TranslationToLanguageId = kn.Id, },                // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "말레이 사람", TranslationToLanguageId = ko.Id, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Malaeorum", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "malajų", TranslationToLanguageId = lt.Id, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "malajiešu valoda", TranslationToLanguageId = lv.Id, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "малајски", TranslationToLanguageId = mk.Id, },                     // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Melayu", TranslationToLanguageId = ms.Id, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Malajan", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Maleis", TranslationToLanguageId = nl.Id, },                       // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = no.Id, },                        // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "malajski", TranslationToLanguageId = pl.Id, },                     // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "malaio", TranslationToLanguageId = pt.Id, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "malaezian", TranslationToLanguageId = ro.Id, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "малайский", TranslationToLanguageId = ru.Id, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "malajzijská", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = sl.Id, },                        // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "malajisht", TranslationToLanguageId = sq.Id, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "малајски", TranslationToLanguageId = sr.Id, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = sv.Id, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = sw.Id, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மலாய்", TranslationToLanguageId = ta.Id, },                      // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "మలే", TranslationToLanguageId = te.Id, },                          // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษามลายู", TranslationToLanguageId = th.Id, },                      // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = tr.Id, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "малайський", TranslationToLanguageId = uk.Id, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "مالائی", TranslationToLanguageId = ur.Id, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Mã Lai", TranslationToLanguageId = vi.Id, },                       // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "מאַלייַיש", TranslationToLanguageId = yi.Id, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "马来人", TranslationToLanguageId = zh.Id, },                        // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ms.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!mt.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = en.Id, },                      // #1  Language = "English"
//                    new CreateLanguageName { Text = "maltés", TranslationToLanguageId = es.Id, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Malteser", TranslationToLanguageId = de.Id, },                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "المالطية", TranslationToLanguageId = ar.Id, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = af.Id, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = az.Id, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "мальтыйская", TranslationToLanguageId = be.Id, },                  // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "малтийски", TranslationToLanguageId = bg.Id, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "মালটার ভাষা", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "maltès", TranslationToLanguageId = ca.Id, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "maltese", TranslationToLanguageId = cs.Id, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = cy.Id, },                        // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "maltesiske", TranslationToLanguageId = da.Id, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = et.Id, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Maltako", TranslationToLanguageId = eu.Id, },                      // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اهل مالت", TranslationToLanguageId = fa.Id, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Maltan", TranslationToLanguageId = fi.Id, },                       // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "maltais", TranslationToLanguageId = fr.Id, },                      // #18 Language = "French"
//                    new CreateLanguageName { Text = "Mháltais", TranslationToLanguageId = ga.Id, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Maltés", TranslationToLanguageId = gl.Id, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "માલ્ટી", TranslationToLanguageId = gu.Id, },                         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "מלטה", TranslationToLanguageId = he.Id, },                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "मोलतिज़", TranslationToLanguageId = hi.Id, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "malteški", TranslationToLanguageId = hr.Id, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "maltese", TranslationToLanguageId = ht.Id, },                      // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "máltai", TranslationToLanguageId = hu.Id, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "մալթացի", TranslationToLanguageId = hy.Id, },                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = id.Id, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "maltneska", TranslationToLanguageId = isLanguage.Id, },            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "maltese", TranslationToLanguageId = it.Id, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "マルタ語", TranslationToLanguageId = ja.Id, },                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "მალტური", TranslationToLanguageId = ka.Id, },                    // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಮಾಲ್ಟಾ ದ್ವೀಪದ ನಿವಾಸಿ ಯಾ ಭಾಷೆ", TranslationToLanguageId = kn.Id, },    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "몰티즈", TranslationToLanguageId = ko.Id, },                       // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Melitica", TranslationToLanguageId = la.Id, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Maltos", TranslationToLanguageId = lt.Id, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Maltas", TranslationToLanguageId = lv.Id, },                       // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "малтешки", TranslationToLanguageId = mk.Id, },                     // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = ms.Id, },                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Malti", TranslationToLanguageId = mt.Id, },                        // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Maltees", TranslationToLanguageId = nl.Id, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = no.Id, },                      // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "maltański", TranslationToLanguageId = pl.Id, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "maltês", TranslationToLanguageId = pt.Id, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "maltez", TranslationToLanguageId = ro.Id, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "мальтийский", TranslationToLanguageId = ru.Id, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = sk.Id, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "malteški", TranslationToLanguageId = sl.Id, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "maltez", TranslationToLanguageId = sq.Id, },                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "малтешки", TranslationToLanguageId = sr.Id, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "maltesiska", TranslationToLanguageId = sv.Id, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kimalta", TranslationToLanguageId = sw.Id, },                      // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மால்டிஸ்", TranslationToLanguageId = ta.Id, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "మాల్టీస్", TranslationToLanguageId = te.Id, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกี่ยวกับมอลตา", TranslationToLanguageId = th.Id, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = tr.Id, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Мальтійська", TranslationToLanguageId = uk.Id, },                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "مالٹی", TranslationToLanguageId = ur.Id, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Malta", TranslationToLanguageId = vi.Id, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "מאלטיזיש", TranslationToLanguageId = yi.Id, },                    // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "马耳他语", TranslationToLanguageId = zh.Id, },                      // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mt.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!nl.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Dutch", TranslationToLanguageId = en.Id, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "holandés", TranslationToLanguageId = es.Id, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Holländer", TranslationToLanguageId = de.Id, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "هولندي", TranslationToLanguageId = ar.Id, },                  // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Nederlandse", TranslationToLanguageId = af.Id, },              // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "holland", TranslationToLanguageId = az.Id, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Галандская", TranslationToLanguageId = be.Id, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "холандски", TranslationToLanguageId = bg.Id, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ওলন্দাজি", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "holandès", TranslationToLanguageId = ca.Id, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "holandský", TranslationToLanguageId = cs.Id, },                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Iseldireg", TranslationToLanguageId = cy.Id, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "hollandsk", TranslationToLanguageId = da.Id, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hollandi", TranslationToLanguageId = et.Id, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Nederlanderara", TranslationToLanguageId = eu.Id, },           // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "هلندی", TranslationToLanguageId = fa.Id, },                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "hollantilainen", TranslationToLanguageId = fi.Id, },           // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "hollandaise", TranslationToLanguageId = fr.Id, },              // #18 Language = "French"
//                    new CreateLanguageName { Text = "Ollainnis", TranslationToLanguageId = ga.Id, },                // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Holandés", TranslationToLanguageId = gl.Id, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "હૉલાન્ડની ડચ ભાષા", TranslationToLanguageId = gu.Id, },         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הולנדי", TranslationToLanguageId = he.Id, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "डच", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "nizozemski", TranslationToLanguageId = hr.Id, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Olandè", TranslationToLanguageId = ht.Id, },                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "holland", TranslationToLanguageId = hu.Id, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հոլանդական", TranslationToLanguageId = hy.Id, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Belanda", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Hollenska", TranslationToLanguageId = isLanguage.Id, },        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "olandese", TranslationToLanguageId = it.Id, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "オランダ", TranslationToLanguageId = ja.Id, },                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჰოლანდიელი", TranslationToLanguageId = ka.Id, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಡಚ್", TranslationToLanguageId = kn.Id, },                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "네덜란드", TranslationToLanguageId = ko.Id, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Dutch", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Olandijos", TranslationToLanguageId = lt.Id, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "holandiešu", TranslationToLanguageId = lv.Id, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Холандија", TranslationToLanguageId = mk.Id, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Belanda", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Olandiż", TranslationToLanguageId = mt.Id, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Nederlands", TranslationToLanguageId = nl.Id, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "nederlandsk", TranslationToLanguageId = no.Id, },              // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "holenderski", TranslationToLanguageId = pl.Id, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "holandês", TranslationToLanguageId = pt.Id, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "olandez", TranslationToLanguageId = ro.Id, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "голландский", TranslationToLanguageId = ru.Id, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Holandský", TranslationToLanguageId = sk.Id, },                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "nizozemski", TranslationToLanguageId = sl.Id, },               // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "holandez", TranslationToLanguageId = sq.Id, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "холандски", TranslationToLanguageId = sr.Id, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "nederländska", TranslationToLanguageId = sv.Id, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiholanzi", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "உலாந்திய", TranslationToLanguageId = ta.Id, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "డచ్", TranslationToLanguageId = te.Id, },                      // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ดัตช์", TranslationToLanguageId = th.Id, },                      // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Hollandalı", TranslationToLanguageId = tr.Id, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Голландська", TranslationToLanguageId = uk.Id, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ڈچ", TranslationToLanguageId = ur.Id, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Hà Lan", TranslationToLanguageId = vi.Id, },                   // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "האָלענדיש", TranslationToLanguageId = yi.Id, },                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "荷兰", TranslationToLanguageId = zh.Id, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nl.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!no.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Norwegian", TranslationToLanguageId = en.Id, },                     // #1  Language = "English"
//                    new CreateLanguageName { Text = "noruego", TranslationToLanguageId = es.Id, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Norwegisch", TranslationToLanguageId = de.Id, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "النرويجية", TranslationToLanguageId = ar.Id, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Noorse", TranslationToLanguageId = af.Id, },                        // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "norveçli", TranslationToLanguageId = az.Id, },                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Нарвежская", TranslationToLanguageId = be.Id, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "норвежки", TranslationToLanguageId = bg.Id, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "নরওয়েবাসী", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "noruec", TranslationToLanguageId = ca.Id, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "norský", TranslationToLanguageId = cs.Id, },                        // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Norwy", TranslationToLanguageId = cy.Id, },                         // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "norske", TranslationToLanguageId = da.Id, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "norra", TranslationToLanguageId = et.Id, },                         // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Norvegiako", TranslationToLanguageId = eu.Id, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "نروژی", TranslationToLanguageId = fa.Id, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Norja", TranslationToLanguageId = fi.Id, },                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "norvégiennes", TranslationToLanguageId = fr.Id, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Ioruais", TranslationToLanguageId = ga.Id, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Noruego", TranslationToLanguageId = gl.Id, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "નોર્વેજિયન", TranslationToLanguageId = gu.Id, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "נורווגית", TranslationToLanguageId = he.Id, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "नार्वेजियन", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "norveški", TranslationToLanguageId = hr.Id, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Nòvejyen", TranslationToLanguageId = ht.Id, },                      // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "norvég", TranslationToLanguageId = hu.Id, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "նորվեգերեն", TranslationToLanguageId = hy.Id, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Norwegia", TranslationToLanguageId = id.Id, },                      // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Norska", TranslationToLanguageId = isLanguage.Id, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "norvegese", TranslationToLanguageId = it.Id, },                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ノルウェー", TranslationToLanguageId = ja.Id, },                     // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ნორვეგიის", TranslationToLanguageId = ka.Id, },                    // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ನಾರ್ವೇ ದೇಶದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguageId = kn.Id, },         // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "노르웨이의", TranslationToLanguageId = ko.Id, },                     // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Norvegica", TranslationToLanguageId = la.Id, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Norvegijos", TranslationToLanguageId = lt.Id, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Norvēģijas", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Норвешка", TranslationToLanguageId = mk.Id, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Norway", TranslationToLanguageId = ms.Id, },                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Norveġiż", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Noors", TranslationToLanguageId = nl.Id, },                         // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "norske", TranslationToLanguageId = no.Id, },                        // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "norweski", TranslationToLanguageId = pl.Id, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "norueguês", TranslationToLanguageId = pt.Id, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "norvegian", TranslationToLanguageId = ro.Id, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "норвежский", TranslationToLanguageId = ru.Id, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Nórsky", TranslationToLanguageId = sk.Id, },                        // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "norveški", TranslationToLanguageId = sl.Id, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "norvegjez", TranslationToLanguageId = sq.Id, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "норвешки", TranslationToLanguageId = sr.Id, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "norska", TranslationToLanguageId = sv.Id, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Norway", TranslationToLanguageId = sw.Id, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "நார்வேஜியன்", TranslationToLanguageId = ta.Id, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "నార్వేజియన్", TranslationToLanguageId = te.Id, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "นอร์เวย์", TranslationToLanguageId = th.Id, },                         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Norveç", TranslationToLanguageId = tr.Id, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Норвезька", TranslationToLanguageId = uk.Id, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ناروے", TranslationToLanguageId = ur.Id, },                        // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Na Uy", TranslationToLanguageId = vi.Id, },                         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "נאָרוועגיש", TranslationToLanguageId = yi.Id, },                    // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "挪威", TranslationToLanguageId = zh.Id, },                          // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = no.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!pl.Names.Any())
//                new List<CreateLanguageName>
//                {

//                    new CreateLanguageName { Text = "Polish", TranslationToLanguageId = en.Id, },                                               // #1  Language = "English"
//                    new CreateLanguageName { Text = "polaco", TranslationToLanguageId = es.Id, },                                               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "polnisch", TranslationToLanguageId = de.Id, },                                             // #3  Language = "German"
//                    new CreateLanguageName { Text = "بولندي", TranslationToLanguageId = ar.Id, },                                              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Pools", TranslationToLanguageId = af.Id, },                                                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "sürtmək", TranslationToLanguageId = az.Id, },                                              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Польскі", TranslationToLanguageId = be.Id, },                                              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "лак", TranslationToLanguageId = bg.Id, },                                                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "পালিশ", TranslationToLanguageId = bn.Id, },                                                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "polonès", TranslationToLanguageId = ca.Id, },                                              // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "polský", TranslationToLanguageId = cs.Id, },                                               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Pwyleg", TranslationToLanguageId = cy.Id, },                                               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "polsk", TranslationToLanguageId = da.Id, },                                                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "poola", TranslationToLanguageId = et.Id, },                                                // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Polonierara", TranslationToLanguageId = eu.Id, },                                          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "لهستانی", TranslationToLanguageId = fa.Id, },                                             // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "puola", TranslationToLanguageId = fi.Id, },                                                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "polonaise", TranslationToLanguageId = fr.Id, },                                            // #18 Language = "French"
//                    new CreateLanguageName { Text = "Polainnis", TranslationToLanguageId = ga.Id, },                                            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Polaco", TranslationToLanguageId = gl.Id, },                                               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઘસીને ઘસાઈને લીસું ચકચકિત કરવું કે થવું ઓપવું કે ઓપાવવું", TranslationToLanguageId = gu.Id, },   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פולני", TranslationToLanguageId = he.Id, },                                               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "पोलिश", TranslationToLanguageId = hi.Id, },                                                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "poljski", TranslationToLanguageId = hr.Id, },                                              // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Polonè", TranslationToLanguageId = ht.Id, },                                               // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "lengyel", TranslationToLanguageId = hu.Id, },                                              // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հղկում", TranslationToLanguageId = hy.Id, },                                               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Polandia", TranslationToLanguageId = id.Id, },                                             // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "pólskur", TranslationToLanguageId = isLanguage.Id, },                                      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "polacco", TranslationToLanguageId = it.Id, },                                              // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ポーランド", TranslationToLanguageId = ja.Id, },                                            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "პოლონეთის", TranslationToLanguageId = ka.Id, },                                          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಒಪ್ಪಹಾಕು", TranslationToLanguageId = kn.Id, },                                              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "폴란드의", TranslationToLanguageId = ko.Id, },                                              // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Polonica", TranslationToLanguageId = la.Id, },                                             // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Lenkijos", TranslationToLanguageId = lt.Id, },                                             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Polijas", TranslationToLanguageId = lv.Id, },                                              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "полски", TranslationToLanguageId = mk.Id, },                                               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Poland", TranslationToLanguageId = ms.Id, },                                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Pollakk", TranslationToLanguageId = mt.Id, },                                              // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Pools", TranslationToLanguageId = nl.Id, },                                                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "polsk", TranslationToLanguageId = no.Id, },                                                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "polski", TranslationToLanguageId = pl.Id, },                                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "polonês", TranslationToLanguageId = pt.Id, },                                              // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "polonez", TranslationToLanguageId = ro.Id, },                                              // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "польский", TranslationToLanguageId = ru.Id, },                                             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Poľský", TranslationToLanguageId = sk.Id, },                                               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "poljski", TranslationToLanguageId = sl.Id, },                                              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "polonisht", TranslationToLanguageId = sq.Id, },                                            // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "пољски", TranslationToLanguageId = sr.Id, },                                               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "polska", TranslationToLanguageId = sv.Id, },                                               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kipolishi", TranslationToLanguageId = sw.Id, },                                            // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மினுக்கு", TranslationToLanguageId = ta.Id, },                                             // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "సంస్కరించు", TranslationToLanguageId = te.Id, },                                             // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ขัด", TranslationToLanguageId = th.Id, },                                                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Polonya", TranslationToLanguageId = tr.Id, },                                              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Польський", TranslationToLanguageId = uk.Id, },                                            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "پولش", TranslationToLanguageId = ur.Id, },                                                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ba Lan", TranslationToLanguageId = vi.Id, },                                               // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פּויליש", TranslationToLanguageId = yi.Id, },                                              // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "波兰", TranslationToLanguageId = zh.Id, },                                                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pl.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!pt.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Portuguese", TranslationToLanguageId = en.Id, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "portugués", TranslationToLanguageId = es.Id, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Portugiesisch", TranslationToLanguageId = de.Id, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "البرتغالية", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Portugees", TranslationToLanguageId = af.Id, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Portuqaliya", TranslationToLanguageId = az.Id, },                   // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Партугальская", TranslationToLanguageId = be.Id, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "португалски", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "পটুর্গালদেশীয়", TranslationToLanguageId = bn.Id, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "portuguès", TranslationToLanguageId = ca.Id, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "portugalština", TranslationToLanguageId = cs.Id, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Portiwgaleg", TranslationToLanguageId = cy.Id, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Portugisisk", TranslationToLanguageId = da.Id, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "portugali", TranslationToLanguageId = et.Id, },                     // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Portuguese", TranslationToLanguageId = eu.Id, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "پرتغالی", TranslationToLanguageId = fa.Id, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "portugali", TranslationToLanguageId = fi.Id, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "portugaise", TranslationToLanguageId = fr.Id, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Portaingéilis", TranslationToLanguageId = ga.Id, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Portugués", TranslationToLanguageId = gl.Id, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "પોર્ટુગીઝ ભાષા", TranslationToLanguageId = gu.Id, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פורטוגזית", TranslationToLanguageId = he.Id, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "पुर्तगाली", TranslationToLanguageId = hi.Id, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "portugalisht", TranslationToLanguageId = hr.Id, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Pòtigè", TranslationToLanguageId = ht.Id, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "portugál", TranslationToLanguageId = hu.Id, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "պորտուգալերեն", TranslationToLanguageId = hy.Id, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Portugis", TranslationToLanguageId = id.Id, },                      // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Portúgalska", TranslationToLanguageId = isLanguage.Id, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "portoghese", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ポルトガル", TranslationToLanguageId = ja.Id, },                     // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "პორტუგალიის", TranslationToLanguageId = ka.Id, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಪೋರ್ಚುಗೀಸ್", TranslationToLanguageId = kn.Id, },                     // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "포르투갈어", TranslationToLanguageId = ko.Id, },                     // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Portuguese", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "portugalų", TranslationToLanguageId = lt.Id, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "portugāļu", TranslationToLanguageId = lv.Id, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "португалски", TranslationToLanguageId = mk.Id, },                   // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Portugis", TranslationToLanguageId = ms.Id, },               // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Portugiż", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Portugees", TranslationToLanguageId = nl.Id, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Portugisisk", TranslationToLanguageId = no.Id, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "portugalski", TranslationToLanguageId = pl.Id, },                   // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "português", TranslationToLanguageId = pt.Id, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "portughez", TranslationToLanguageId = ro.Id, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "португальский", TranslationToLanguageId = ru.Id, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "portugalčina", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "portugalski", TranslationToLanguageId = sl.Id, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "portugalisht", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "португалски", TranslationToLanguageId = sr.Id, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "portugisiska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kireno", TranslationToLanguageId = sw.Id, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "போர்த்துகீசியம்", TranslationToLanguageId = ta.Id, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "పోర్చుగీసు", TranslationToLanguageId = te.Id, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาโปรตุเกส", TranslationToLanguageId = th.Id, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Portekizce", TranslationToLanguageId = tr.Id, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Португальська", TranslationToLanguageId = uk.Id, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "پرتگالی", TranslationToLanguageId = ur.Id, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Bồ Đào Nha", TranslationToLanguageId = vi.Id, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פּאָרטוגעזיש", TranslationToLanguageId = yi.Id, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "葡萄牙语", TranslationToLanguageId = zh.Id, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pt.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ro.Names.Any())
//                new List<CreateLanguageName>
//                {

//                    new CreateLanguageName { Text = "Romanian", TranslationToLanguageId = en.Id, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "rumano", TranslationToLanguageId = es.Id, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Rumänisch", TranslationToLanguageId = de.Id, },              // #3  Language = "German"
//                    new CreateLanguageName { Text = "رومانيا", TranslationToLanguageId = ar.Id, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Roemeens", TranslationToLanguageId = af.Id, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "rumın", TranslationToLanguageId = az.Id, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "румынская", TranslationToLanguageId = be.Id, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "румънски", TranslationToLanguageId = bg.Id, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "রোমানিয়ান", TranslationToLanguageId = bn.Id, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "romanès", TranslationToLanguageId = ca.Id, },                // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "rumunský", TranslationToLanguageId = cs.Id, },               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Rwmaneg", TranslationToLanguageId = cy.Id, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "rumænsk", TranslationToLanguageId = da.Id, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "rumeenia", TranslationToLanguageId = et.Id, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Errumanierara", TranslationToLanguageId = eu.Id, },          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "رومانیایی", TranslationToLanguageId = fa.Id, },             // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Romanian", TranslationToLanguageId = fi.Id, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "roumaine", TranslationToLanguageId = fr.Id, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Rómáinis", TranslationToLanguageId = ga.Id, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Romanés", TranslationToLanguageId = gl.Id, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "રોમાનિયન", TranslationToLanguageId = gu.Id, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "רומניה", TranslationToLanguageId = he.Id, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "रोमानियाई", TranslationToLanguageId = hi.Id, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Rumunjski", TranslationToLanguageId = hr.Id, },              // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "romanian", TranslationToLanguageId = ht.Id, },               // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "román", TranslationToLanguageId = hu.Id, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ռումիներեն", TranslationToLanguageId = hy.Id, },             // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Rumania", TranslationToLanguageId = id.Id, },                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "rúmensku", TranslationToLanguageId = isLanguage.Id, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "rumeno", TranslationToLanguageId = it.Id, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ルーマニア語", TranslationToLanguageId = ja.Id, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "რუმინეთის", TranslationToLanguageId = ka.Id, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರೊಮೇನಿಯನ್", TranslationToLanguageId = kn.Id, },             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "루마니아 사람", TranslationToLanguageId = ko.Id, },           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Romanian", TranslationToLanguageId = la.Id, },               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "rumunų", TranslationToLanguageId = lt.Id, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "rumāņu", TranslationToLanguageId = lv.Id, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "романскиот", TranslationToLanguageId = mk.Id, },             // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Romania", TranslationToLanguageId = ms.Id, },         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Rumen", TranslationToLanguageId = mt.Id, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Roemeense", TranslationToLanguageId = nl.Id, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "rumensk", TranslationToLanguageId = no.Id, },                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "rumuński", TranslationToLanguageId = pl.Id, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "romeno", TranslationToLanguageId = pt.Id, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "român", TranslationToLanguageId = ro.Id, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "румынский", TranslationToLanguageId = ru.Id, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Rumunský", TranslationToLanguageId = sk.Id, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "romunski", TranslationToLanguageId = sl.Id, },               // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "rumun", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "румунски", TranslationToLanguageId = sr.Id, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "rumänska", TranslationToLanguageId = sv.Id, },               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiromania", TranslationToLanguageId = sw.Id, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ரோமானியம்", TranslationToLanguageId = ta.Id, },          // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "రోమేనియన్", TranslationToLanguageId = te.Id, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "โรมาเนีย", TranslationToLanguageId = th.Id, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Romanya", TranslationToLanguageId = tr.Id, },                // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Румунська", TranslationToLanguageId = uk.Id, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "رومانیہ", TranslationToLanguageId = ur.Id, },               // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Rumani", TranslationToLanguageId = vi.Id, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "רומעניש", TranslationToLanguageId = yi.Id, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "罗马尼亚", TranslationToLanguageId = zh.Id, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ro.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ru.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Russian", TranslationToLanguageId = en.Id, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "ruso", TranslationToLanguageId = es.Id, },                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Russisch", TranslationToLanguageId = de.Id, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "الروسية", TranslationToLanguageId = ar.Id, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Russiese", TranslationToLanguageId = af.Id, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "rus", TranslationToLanguageId = az.Id, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "рускі", TranslationToLanguageId = be.Id, },                  // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "руски", TranslationToLanguageId = bg.Id, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "রাশিয়ান", TranslationToLanguageId = bn.Id, },                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "rus", TranslationToLanguageId = ca.Id, },                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Rus", TranslationToLanguageId = cs.Id, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Rwsia", TranslationToLanguageId = cy.Id, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "russisk", TranslationToLanguageId = da.Id, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "vene", TranslationToLanguageId = et.Id, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Errusierara", TranslationToLanguageId = eu.Id, },            // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "روسی", TranslationToLanguageId = fa.Id, },                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Venäjän", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "russes", TranslationToLanguageId = fr.Id, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Rúisis", TranslationToLanguageId = ga.Id, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Ruso", TranslationToLanguageId = gl.Id, },                   // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "રશિયાનું", TranslationToLanguageId = gu.Id, },                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "רוסי", TranslationToLanguageId = he.Id, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "रूसी", TranslationToLanguageId = hi.Id, },                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "ruski", TranslationToLanguageId = hr.Id, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Ris", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "orosz", TranslationToLanguageId = hu.Id, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ռուսերեն", TranslationToLanguageId = hy.Id, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Rusia", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Rússneska", TranslationToLanguageId = isLanguage.Id, },      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "russo", TranslationToLanguageId = it.Id, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ロシア語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "რუსეთის", TranslationToLanguageId = ka.Id, },               // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರಶಿಯನ್", TranslationToLanguageId = kn.Id, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "러시아의", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Russian", TranslationToLanguageId = la.Id, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Rusijos", TranslationToLanguageId = lt.Id, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "krievu", TranslationToLanguageId = lv.Id, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Руската", TranslationToLanguageId = mk.Id, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Rusia", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Russu", TranslationToLanguageId = mt.Id, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Russisch", TranslationToLanguageId = nl.Id, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "russiske", TranslationToLanguageId = no.Id, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "rosyjski", TranslationToLanguageId = pl.Id, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "russo", TranslationToLanguageId = pt.Id, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "rusesc", TranslationToLanguageId = ro.Id, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "русский", TranslationToLanguageId = ru.Id, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Rus", TranslationToLanguageId = sk.Id, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "ruski", TranslationToLanguageId = sl.Id, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "rusisht", TranslationToLanguageId = sq.Id, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "руски", TranslationToLanguageId = sr.Id, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "ryska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kirusi", TranslationToLanguageId = sw.Id, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ரஷியன்", TranslationToLanguageId = ta.Id, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "రష్యన్", TranslationToLanguageId = te.Id, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษารัสเซีย", TranslationToLanguageId = th.Id, },               // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Rus", TranslationToLanguageId = tr.Id, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Російська", TranslationToLanguageId = uk.Id, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "روسی", TranslationToLanguageId = ur.Id, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Nga", TranslationToLanguageId = vi.Id, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "רוסיש", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "俄罗斯", TranslationToLanguageId = zh.Id, },                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ru.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sk.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = en.Id, },                        // #1  Language = "English"
//                    new CreateLanguageName { Text = "eslovaco", TranslationToLanguageId = es.Id, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Slowakisch", TranslationToLanguageId = de.Id, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "السلوفاكية", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Slowaakse", TranslationToLanguageId = af.Id, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = az.Id, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Славацкая", TranslationToLanguageId = be.Id, },                     // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "словашки", TranslationToLanguageId = bg.Id, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্লোভাক", TranslationToLanguageId = bn.Id, },                         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "eslovac", TranslationToLanguageId = ca.Id, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "slovenský", TranslationToLanguageId = cs.Id, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Slofacia", TranslationToLanguageId = cy.Id, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Slovakiske", TranslationToLanguageId = da.Id, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "slovaki", TranslationToLanguageId = et.Id, },                       // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Eslovakierara", TranslationToLanguageId = eu.Id, },                 // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اسلواکی", TranslationToLanguageId = fa.Id, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Slovakian", TranslationToLanguageId = fi.Id, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "slovaques", TranslationToLanguageId = fr.Id, },                     // #18 Language = "French"
//                    new CreateLanguageName { Text = "Slóvaicis", TranslationToLanguageId = ga.Id, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Eslovaco", TranslationToLanguageId = gl.Id, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્લોવાક", TranslationToLanguageId = gu.Id, },                        // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סלובקית", TranslationToLanguageId = he.Id, },                      // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्लोवाक", TranslationToLanguageId = hi.Id, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "slovački", TranslationToLanguageId = hr.Id, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "slovak", TranslationToLanguageId = ht.Id, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "szlovák", TranslationToLanguageId = hu.Id, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սլովակ", TranslationToLanguageId = hy.Id, },                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Slovakia", TranslationToLanguageId = id.Id, },                      // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = isLanguage.Id, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "slovacco", TranslationToLanguageId = it.Id, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スロバキア語", TranslationToLanguageId = ja.Id, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სლოვაკეთის", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ಲೋವ್ಯಾಕ್ ಭಾಷೆ ಯಾ ಜನಾಂಗದವರು", TranslationToLanguageId = kn.Id, },   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "슬로바키아어", TranslationToLanguageId = ko.Id, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Moravica", TranslationToLanguageId = la.Id, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Slovakijos", TranslationToLanguageId = lt.Id, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Slovākijas", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Словачка", TranslationToLanguageId = mk.Id, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Slovakia", TranslationToLanguageId = ms.Id, },                      // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Slovakka", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Slowaaks", TranslationToLanguageId = nl.Id, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "slovakisk", TranslationToLanguageId = no.Id, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "słowacki", TranslationToLanguageId = pl.Id, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "eslovaco", TranslationToLanguageId = pt.Id, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "slovac", TranslationToLanguageId = ro.Id, },                        // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "словацкий", TranslationToLanguageId = ru.Id, },                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "slovenských", TranslationToLanguageId = sk.Id, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Slovaški", TranslationToLanguageId = sl.Id, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "sllovak", TranslationToLanguageId = sq.Id, },                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "словачки", TranslationToLanguageId = sr.Id, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "slovak", TranslationToLanguageId = sv.Id, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kislovakia", TranslationToLanguageId = sw.Id, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்லோவாக்", TranslationToLanguageId = ta.Id, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్లోవక్", TranslationToLanguageId = te.Id, },                          // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "สโลวัก", TranslationToLanguageId = th.Id, },                          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = tr.Id, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Словацька", TranslationToLanguageId = uk.Id, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سلاواکی", TranslationToLanguageId = ur.Id, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Slovak", TranslationToLanguageId = vi.Id, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סלאָוואַקיש", TranslationToLanguageId = yi.Id, },                    // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "斯洛伐克", TranslationToLanguageId = zh.Id, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sk.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sl.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Slovenian", TranslationToLanguageId = en.Id, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "esloveno", TranslationToLanguageId = es.Id, },                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Slowenisch", TranslationToLanguageId = de.Id, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "سلوفيني", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Sloweens", TranslationToLanguageId = af.Id, },                   // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Sloven", TranslationToLanguageId = az.Id, },                     // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Славенская", TranslationToLanguageId = be.Id, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "словенски", TranslationToLanguageId = bg.Id, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্লোভেনিয়ান", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "eslovè", TranslationToLanguageId = ca.Id, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "slovinský", TranslationToLanguageId = cs.Id, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Slofenia", TranslationToLanguageId = cy.Id, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "slovenske", TranslationToLanguageId = da.Id, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "sloveeni", TranslationToLanguageId = et.Id, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Eslovenierara", TranslationToLanguageId = eu.Id, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اسلوونیایی", TranslationToLanguageId = fa.Id, },                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Slovenian", TranslationToLanguageId = fi.Id, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "slovènes", TranslationToLanguageId = fr.Id, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Slóivéinis", TranslationToLanguageId = ga.Id, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Esloveno", TranslationToLanguageId = gl.Id, },                   // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્લોવેનિયન", TranslationToLanguageId = gu.Id, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סלובנית", TranslationToLanguageId = he.Id, },                   // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्लॉवेनियन", TranslationToLanguageId = hi.Id, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Slovenski", TranslationToLanguageId = hr.Id, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Sloveni", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "szlovén", TranslationToLanguageId = hu.Id, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սլովեներեն", TranslationToLanguageId = hy.Id, },                 // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Slovenia", TranslationToLanguageId = id.Id, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "slóvensku", TranslationToLanguageId = isLanguage.Id, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "sloveno", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スロベニア語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სლოვენიის", TranslationToLanguageId = ka.Id, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ಲೋವೇನಿಯಾದ ಜನರ ಯಾ ಭಾಷೆಯ", TranslationToLanguageId = kn.Id, }, // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "슬로베니아", TranslationToLanguageId = ko.Id, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Carnica", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Slovėnijos", TranslationToLanguageId = lt.Id, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "slovēņu", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Словенија", TranslationToLanguageId = mk.Id, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Slovenia", TranslationToLanguageId = ms.Id, },                   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Sloven", TranslationToLanguageId = mt.Id, },                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Sloveens", TranslationToLanguageId = nl.Id, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "slovensk", TranslationToLanguageId = no.Id, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "słoweński", TranslationToLanguageId = pl.Id, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "esloveno", TranslationToLanguageId = pt.Id, },                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "sloven", TranslationToLanguageId = ro.Id, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "словенский", TranslationToLanguageId = ru.Id, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Slovinský", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "slovenski", TranslationToLanguageId = sl.Id, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "slloven", TranslationToLanguageId = sq.Id, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "словеначки", TranslationToLanguageId = sr.Id, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "slovenska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kislovenia", TranslationToLanguageId = sw.Id, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்லோவேனியன்", TranslationToLanguageId = ta.Id, },         // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్లోవేనియన్", TranslationToLanguageId = te.Id, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาสโลเวเนีย", TranslationToLanguageId = th.Id, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Slovenya", TranslationToLanguageId = tr.Id, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Словенська", TranslationToLanguageId = uk.Id, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سلووینیا", TranslationToLanguageId = ur.Id, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Slovenia", TranslationToLanguageId = vi.Id, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סלאוועניש", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "斯洛文尼亚", TranslationToLanguageId = zh.Id, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sl.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sq.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Albanian", TranslationToLanguageId = en.Id, },          // #1  Language = "English"
//                    new CreateLanguageName { Text = "albanés", TranslationToLanguageId = es.Id, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Albanisch", TranslationToLanguageId = de.Id, },         // #3  Language = "German"
//                    new CreateLanguageName { Text = "الألبانية", TranslationToLanguageId = ar.Id, },         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Albanees", TranslationToLanguageId = af.Id, },          // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "alban", TranslationToLanguageId = az.Id, },             // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "албанская", TranslationToLanguageId = be.Id, },         // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "албански", TranslationToLanguageId = bg.Id, },          // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আলবেনিয়ান", TranslationToLanguageId = bn.Id, },         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Albanès", TranslationToLanguageId = ca.Id, },           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "albánský", TranslationToLanguageId = cs.Id, },          // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Albaneg", TranslationToLanguageId = cy.Id, },           // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "albansk", TranslationToLanguageId = da.Id, },           // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "albaania", TranslationToLanguageId = et.Id, },          // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Albanian", TranslationToLanguageId = eu.Id, },          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آلبانیایی", TranslationToLanguageId = fa.Id, },        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Albanian", TranslationToLanguageId = fi.Id, },          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "albanaises", TranslationToLanguageId = fr.Id, },        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Albáinis", TranslationToLanguageId = ga.Id, },          // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Albanés", TranslationToLanguageId = gl.Id, },           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "અલ્બેનિયન", TranslationToLanguageId = gu.Id, },         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אלבנית", TranslationToLanguageId = he.Id, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अल्बेनियन्", TranslationToLanguageId = hi.Id, },          // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "अल्बेनियन्", TranslationToLanguageId = hr.Id, },          // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Haitian", TranslationToLanguageId = ht.Id, },           // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "albán", TranslationToLanguageId = hu.Id, },             // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ալբանական", TranslationToLanguageId = hy.Id, },       // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "bahasa Albania", TranslationToLanguageId = id.Id, },    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Albanska", TranslationToLanguageId = isLanguage.Id, },  // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "albanese", TranslationToLanguageId = it.Id, },          // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アルバニア語", TranslationToLanguageId = ja.Id, },       // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ალბანური", TranslationToLanguageId = ka.Id, },         // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅಲ್ಬೇನಿಯನ್", TranslationToLanguageId = kn.Id, },         // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "알바니아", TranslationToLanguageId = ko.Id, },           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Illyrica", TranslationToLanguageId = la.Id, },          // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "albanų", TranslationToLanguageId = lt.Id, },            // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "albāņu", TranslationToLanguageId = lv.Id, },            // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Албанци", TranslationToLanguageId = mk.Id, },           // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Albania", TranslationToLanguageId = ms.Id, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Albaniż", TranslationToLanguageId = mt.Id, },           // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Albanees", TranslationToLanguageId = nl.Id, },          // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Albansk", TranslationToLanguageId = no.Id, },           // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "albański", TranslationToLanguageId = pl.Id, },          // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "albanês", TranslationToLanguageId = pt.Id, },           // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "albanez", TranslationToLanguageId = ro.Id, },           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "албанский", TranslationToLanguageId = ru.Id, },         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Albánsky", TranslationToLanguageId = sk.Id, },          // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "albanski", TranslationToLanguageId = sl.Id, },          // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "shqiptar", TranslationToLanguageId = sq.Id, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "албански", TranslationToLanguageId = sr.Id, },          // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "albanska", TranslationToLanguageId = sv.Id, },          // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kialbeni", TranslationToLanguageId = sw.Id, },          // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "அல்பானியன்", TranslationToLanguageId = ta.Id, },     // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "అల్బేనియా దేశస్థుడు", TranslationToLanguageId = te.Id, },   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวแอลเบเนีย", TranslationToLanguageId = th.Id, },         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Arnavut", TranslationToLanguageId = tr.Id, },           // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Албанська", TranslationToLanguageId = uk.Id, },         // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "البانی", TranslationToLanguageId = ur.Id, },           // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng An-ba-ni", TranslationToLanguageId = vi.Id, },    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַלבאַניש", TranslationToLanguageId = yi.Id, },          // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "阿尔巴尼亚人", TranslationToLanguageId = zh.Id, },       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sq.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = en.Id, },                               // #1  Language = "English"
//                    new CreateLanguageName { Text = "serbio", TranslationToLanguageId = es.Id, },                                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "serbisch", TranslationToLanguageId = de.Id, },                              // #3  Language = "German"
//                    new CreateLanguageName { Text = "صربي", TranslationToLanguageId = ar.Id, },                                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Serwies", TranslationToLanguageId = af.Id, },                               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "serb", TranslationToLanguageId = az.Id, },                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Сербская", TranslationToLanguageId = be.Id, },                              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "сръбски", TranslationToLanguageId = bg.Id, },                               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সার্বিয়া সংক্রান্ত", TranslationToLanguageId = bn.Id, },                           // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "serbi", TranslationToLanguageId = ca.Id, },                                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "srbský", TranslationToLanguageId = cs.Id, },                                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Serbeg", TranslationToLanguageId = cy.Id, },                                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "serbisk", TranslationToLanguageId = da.Id, },                               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "serbia", TranslationToLanguageId = et.Id, },                                // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = eu.Id, },                               // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "صرب", TranslationToLanguageId = fa.Id, },                                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = fi.Id, },                               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "serbes", TranslationToLanguageId = fr.Id, },                                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Seirbis", TranslationToLanguageId = ga.Id, },                               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = gl.Id, },                               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સર્બિયન", TranslationToLanguageId = gu.Id, },                                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הסרבי", TranslationToLanguageId = he.Id, },                                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "सर्बिया की (भाषा या निवासी)", TranslationToLanguageId = hi.Id, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "srpski", TranslationToLanguageId = hr.Id, },                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Sèb", TranslationToLanguageId = ht.Id, },                                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "szerb", TranslationToLanguageId = hu.Id, },                                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սերբերեն", TranslationToLanguageId = hy.Id, },                              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = id.Id, },                                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "serbneska", TranslationToLanguageId = isLanguage.Id, },                     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "serbo", TranslationToLanguageId = it.Id, },                                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "セルビア語", TranslationToLanguageId = ja.Id, },                             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სერბეთის", TranslationToLanguageId = ka.Id, },                             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸರ್ಬಿಯ ರಾಷ್ಟ್ರದ ಯಾ ಅದರ ಭಾಷೆಗೆ ಸಂಬಂಧಿಸಿದ", TranslationToLanguageId = kn.Id, },  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "세르비아의", TranslationToLanguageId = ko.Id, },                             // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Serbiae", TranslationToLanguageId = la.Id, },                               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Serbijos", TranslationToLanguageId = lt.Id, },                              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Serbijas", TranslationToLanguageId = lv.Id, },                              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Србија", TranslationToLanguageId = mk.Id, },                                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = ms.Id, },                                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Serb", TranslationToLanguageId = mt.Id, },                                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Servisch", TranslationToLanguageId = nl.Id, },                              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "serbisk", TranslationToLanguageId = no.Id, },                               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "serbski", TranslationToLanguageId = pl.Id, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "sérvio", TranslationToLanguageId = pt.Id, },                                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "sârb", TranslationToLanguageId = ro.Id, },                                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "сербский", TranslationToLanguageId = ru.Id, },                              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "srbský", TranslationToLanguageId = sk.Id, },                                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "srbski", TranslationToLanguageId = sl.Id, },                                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "serb", TranslationToLanguageId = sq.Id, },                                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "српски", TranslationToLanguageId = sr.Id, },                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "serbiska", TranslationToLanguageId = sv.Id, },                              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = sw.Id, },                                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "செர்பியாவை சார்ந்த", TranslationToLanguageId = ta.Id, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "సెర్బియా", TranslationToLanguageId = te.Id, },                                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เซอร์เบีย", TranslationToLanguageId = th.Id, },                                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Sırp", TranslationToLanguageId = tr.Id, },                                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Сербська", TranslationToLanguageId = uk.Id, },                              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سربیا", TranslationToLanguageId = ur.Id, },                                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = vi.Id, },                                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סערביש", TranslationToLanguageId = yi.Id, },                               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "塞尔维亚", TranslationToLanguageId = zh.Id, },                               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sr.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sv.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Swedish", TranslationToLanguageId = en.Id, },                                // #1  Language = "English"
//                    new CreateLanguageName { Text = "sueco", TranslationToLanguageId = es.Id, },                                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Schwedisch", TranslationToLanguageId = de.Id, },                             // #3  Language = "German"
//                    new CreateLanguageName { Text = "السويدية", TranslationToLanguageId = ar.Id, },                              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Sweedse", TranslationToLanguageId = af.Id, },                                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İsveç", TranslationToLanguageId = az.Id, },                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Шведская", TranslationToLanguageId = be.Id, },                               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "шведски", TranslationToLanguageId = bg.Id, },                                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সুইডেনের ভাষা", TranslationToLanguageId = bn.Id, },                            // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "suec", TranslationToLanguageId = ca.Id, },                                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "švédský", TranslationToLanguageId = cs.Id, },                                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Sweden", TranslationToLanguageId = cy.Id, },                                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "svensk", TranslationToLanguageId = da.Id, },                                 // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "rootsi", TranslationToLanguageId = et.Id, },                                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Suedierara", TranslationToLanguageId = eu.Id, },                             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "سوئد", TranslationToLanguageId = fa.Id, },                                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "ruotsi", TranslationToLanguageId = fi.Id, },                                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Suède", TranslationToLanguageId = fr.Id, },                                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Sualainnis", TranslationToLanguageId = ga.Id, },                             // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Sueco", TranslationToLanguageId = gl.Id, },                                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્વીડીશ", TranslationToLanguageId = gu.Id, },                                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "שוודית", TranslationToLanguageId = he.Id, },                                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्वीडिश", TranslationToLanguageId = hi.Id, },                                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "švedski", TranslationToLanguageId = hr.Id, },                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Syèd", TranslationToLanguageId = ht.Id, },                                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "svéd", TranslationToLanguageId = hu.Id, },                                   // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "շվեդերեն", TranslationToLanguageId = hy.Id, },                               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Swedia", TranslationToLanguageId = id.Id, },                                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Sænska", TranslationToLanguageId = isLanguage.Id, },                         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "svedese", TranslationToLanguageId = it.Id, },                                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スウェーデン", TranslationToLanguageId = ja.Id, },                            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "შვედეთის", TranslationToLanguageId = ka.Id, },                              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ವೀಡಿಷ್", TranslationToLanguageId = kn.Id, },                                  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "스웨덴의", TranslationToLanguageId = ko.Id, },                                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Suecica", TranslationToLanguageId = la.Id, },                                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Švedijos", TranslationToLanguageId = lt.Id, },                               // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Zviedru", TranslationToLanguageId = lv.Id, },                                // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Шведската", TranslationToLanguageId = mk.Id, },                              // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Sweden", TranslationToLanguageId = ms.Id, },                                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Svediż", TranslationToLanguageId = mt.Id, },                                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Zweeds", TranslationToLanguageId = nl.Id, },                                 // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Svenske", TranslationToLanguageId = no.Id, },                                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "szwedzki", TranslationToLanguageId = pl.Id, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "sueco", TranslationToLanguageId = pt.Id, },                                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "suedez", TranslationToLanguageId = ro.Id, },                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "шведский", TranslationToLanguageId = ru.Id, },                               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Švédsky", TranslationToLanguageId = sk.Id, },                                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "švedski", TranslationToLanguageId = sl.Id, },                                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "suedez", TranslationToLanguageId = sq.Id, },                                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "шведски", TranslationToLanguageId = sr.Id, },                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "svenska", TranslationToLanguageId = sv.Id, },                                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Swedish", TranslationToLanguageId = sw.Id, },                                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்வீடன் நாட்டு மொழி, மக்கள்", TranslationToLanguageId = ta.Id, },       // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్వీడిష్", TranslationToLanguageId = te.Id, },                                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "สวีเดน", TranslationToLanguageId = th.Id, },                                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İsveç", TranslationToLanguageId = tr.Id, },                                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Шведська", TranslationToLanguageId = uk.Id, },                               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سويڈش", TranslationToLanguageId = ur.Id, },                                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Thụy Điển", TranslationToLanguageId = vi.Id, },                              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "שוועדיש", TranslationToLanguageId = yi.Id, },                               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "瑞典语", TranslationToLanguageId = zh.Id, },                                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sv.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sw.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = en.Id, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = es.Id, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = de.Id, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "السواحيلية", TranslationToLanguageId = ar.Id, },           // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = af.Id, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Suahili", TranslationToLanguageId = az.Id, },               // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "суахілі", TranslationToLanguageId = be.Id, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "суахили", TranslationToLanguageId = bg.Id, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সোয়াহিলি", TranslationToLanguageId = bn.Id, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "suahili", TranslationToLanguageId = ca.Id, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "svahilština", TranslationToLanguageId = cs.Id, },           // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = cy.Id, },               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = da.Id, },               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "suahiili", TranslationToLanguageId = et.Id, },              // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = eu.Id, },               // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "سواحیلی", TranslationToLanguageId = fa.Id, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Suahili", TranslationToLanguageId = fi.Id, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = fr.Id, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Svahaílis", TranslationToLanguageId = ga.Id, },             // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "suahili", TranslationToLanguageId = gl.Id, },               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્વાહિલી", TranslationToLanguageId = gu.Id, },                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סוואהילית", TranslationToLanguageId = he.Id, },            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्वाहिली", TranslationToLanguageId = hi.Id, },                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = hr.Id, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = ht.Id, },               // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "szuahéli", TranslationToLanguageId = hu.Id, },              // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սուահիլի", TranslationToLanguageId = hy.Id, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = id.Id, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "svahílí", TranslationToLanguageId = isLanguage.Id, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = it.Id, },               // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スワヒリ語", TranslationToLanguageId = ja.Id, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სუაჰილი", TranslationToLanguageId = ka.Id, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ವಾಹಿಲಿ", TranslationToLanguageId = kn.Id, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "스와힐리어", TranslationToLanguageId = ko.Id, },             // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = la.Id, },               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = lt.Id, },               // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = lv.Id, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "свахили", TranslationToLanguageId = mk.Id, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Swahili", TranslationToLanguageId = ms.Id, },        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Swaħili", TranslationToLanguageId = mt.Id, },               // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = nl.Id, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = no.Id, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "suahili", TranslationToLanguageId = pl.Id, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "suaíli", TranslationToLanguageId = pt.Id, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = ro.Id, },               // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "суахили", TranslationToLanguageId = ru.Id, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "svahilština", TranslationToLanguageId = sk.Id, },           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = sl.Id, },               // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "suahilisht", TranslationToLanguageId = sq.Id, },            // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Свахили", TranslationToLanguageId = sr.Id, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = sv.Id, },               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = sw.Id, },               // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "சுவாஹிலி", TranslationToLanguageId = ta.Id, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్వాహిలి", TranslationToLanguageId = te.Id, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาสวาฮีลี", TranslationToLanguageId = th.Id, },              // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = tr.Id, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Суахілі", TranslationToLanguageId = uk.Id, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سواہیلی", TranslationToLanguageId = ur.Id, },              // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Swahili", TranslationToLanguageId = vi.Id, },         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סוואַהילי", TranslationToLanguageId = yi.Id, },             // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "斯瓦希里", TranslationToLanguageId = zh.Id, },               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sw.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ta.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = en.Id, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = es.Id, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Tamilisch", TranslationToLanguageId = de.Id, },              // #3  Language = "German"
//                    new CreateLanguageName { Text = "التاميل", TranslationToLanguageId = ar.Id, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = af.Id, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = az.Id, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "тамільская", TranslationToLanguageId = be.Id, },             // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "тамилски", TranslationToLanguageId = bg.Id, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "তামিল", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "tàmil", TranslationToLanguageId = ca.Id, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = cs.Id, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = cy.Id, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = da.Id, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = et.Id, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = eu.Id, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "تامیل", TranslationToLanguageId = fa.Id, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = fi.Id, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = fr.Id, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = ga.Id, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = gl.Id, },                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "તમિળ", TranslationToLanguageId = gu.Id, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "טמילית", TranslationToLanguageId = he.Id, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "तमिल", TranslationToLanguageId = hi.Id, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "tamilski", TranslationToLanguageId = hr.Id, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = ht.Id, },                  // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = hu.Id, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "թամիլերեն", TranslationToLanguageId = hy.Id, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = isLanguage.Id, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = it.Id, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "タミル語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ტამილური", TranslationToLanguageId = ka.Id, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ತಮಿಳು", TranslationToLanguageId = kn.Id, },                  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "타밀 사람", TranslationToLanguageId = ko.Id, },               // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = la.Id, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "tamilų", TranslationToLanguageId = lt.Id, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "tamilu", TranslationToLanguageId = lv.Id, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Тамилските", TranslationToLanguageId = mk.Id, },             // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = mt.Id, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = nl.Id, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = no.Id, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = pl.Id, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "tâmil", TranslationToLanguageId = pt.Id, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "tamilă", TranslationToLanguageId = ro.Id, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "тамильский", TranslationToLanguageId = ru.Id, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sl.Id, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "тамилски", TranslationToLanguageId = sr.Id, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sw.Id, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "தமிழ்", TranslationToLanguageId = ta.Id, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "తమిళ్", TranslationToLanguageId = te.Id, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ทมิฬ", TranslationToLanguageId = th.Id, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = tr.Id, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "тамільська", TranslationToLanguageId = uk.Id, },             // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "تامل", TranslationToLanguageId = ur.Id, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = vi.Id, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טאַמיל", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "泰米尔人", TranslationToLanguageId = zh.Id, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ta.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!te.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = en.Id, },           // #1  Language = "English"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = es.Id, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = de.Id, },           // #3  Language = "German"
//                    new CreateLanguageName { Text = "التيلجو", TranslationToLanguageId = ar.Id, },         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = af.Id, },           // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Teluqu", TranslationToLanguageId = az.Id, },           // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "тэлугу", TranslationToLanguageId = be.Id, },           // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = bg.Id, },           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "তেলুগু", TranslationToLanguageId = bn.Id, },             // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = ca.Id, },           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = cs.Id, },           // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = cy.Id, },           // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = da.Id, },           // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = et.Id, },           // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = eu.Id, },           // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "تلوگو", TranslationToLanguageId = fa.Id, },           // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = fi.Id, },           // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = fr.Id, },           // #18 Language = "French"
//                    new CreateLanguageName { Text = "Teileagúis", TranslationToLanguageId = ga.Id, },       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = gl.Id, },           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "તેલુગુ", TranslationToLanguageId = gu.Id, },             // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "טלוגו", TranslationToLanguageId = he.Id, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "तेलुगु", TranslationToLanguageId = hi.Id, },             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = hr.Id, },           // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = ht.Id, },           // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = hu.Id, },           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "տելուգու", TranslationToLanguageId = hy.Id, },         // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = id.Id, },           // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "telúgú", TranslationToLanguageId = isLanguage.Id, },   // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = it.Id, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "テルグ語", TranslationToLanguageId = ja.Id, },          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ტელუგუ", TranslationToLanguageId = ka.Id, },         // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ತೆಲುಗು", TranslationToLanguageId = kn.Id, },            // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "텔루구어", TranslationToLanguageId = ko.Id, },          // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = la.Id, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "telugų", TranslationToLanguageId = lt.Id, },           // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = lv.Id, },           // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = mk.Id, },           // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = ms.Id, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = mt.Id, },           // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = nl.Id, },           // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = no.Id, },           // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = pl.Id, },           // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = pt.Id, },           // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = ro.Id, },           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = ru.Id, },           // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = sk.Id, },           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sl.Id, },           // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sq.Id, },           // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = sr.Id, },           // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sv.Id, },           // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sw.Id, },           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "தெலுங்கு", TranslationToLanguageId = ta.Id, },        // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "తెలుగు", TranslationToLanguageId = te.Id, },            // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เตลูกู", TranslationToLanguageId = th.Id, },              // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = tr.Id, },           // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = uk.Id, },           // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "تيلوگو", TranslationToLanguageId = ur.Id, },          // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = vi.Id, },           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טעלוגו", TranslationToLanguageId = yi.Id, },          // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "泰卢固语", TranslationToLanguageId = zh.Id, },         // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = te.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!th.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = en.Id, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "tailandés", TranslationToLanguageId = es.Id, },             // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "thailändisch", TranslationToLanguageId = de.Id, },          // #3  Language = "German"
//                    new CreateLanguageName { Text = "التايلاندية", TranslationToLanguageId = ar.Id, },           // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = af.Id, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Tay", TranslationToLanguageId = az.Id, },                   // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Тайская", TranslationToLanguageId = be.Id, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "тайландски", TranslationToLanguageId = bg.Id, },            // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "থাই", TranslationToLanguageId = bn.Id, },                    // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "tailandès", TranslationToLanguageId = ca.Id, },             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "thajské", TranslationToLanguageId = cs.Id, },               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = cy.Id, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = da.Id, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "tai", TranslationToLanguageId = et.Id, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = eu.Id, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "تایلندی", TranslationToLanguageId = fa.Id, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "thaimaalainen", TranslationToLanguageId = fi.Id, },         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "thaïlandaise", TranslationToLanguageId = fr.Id, },          // #18 Language = "French"
//                    new CreateLanguageName { Text = "Téalainnis", TranslationToLanguageId = ga.Id, },            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Tailandés", TranslationToLanguageId = gl.Id, },             // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "થાઈ", TranslationToLanguageId = gu.Id, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "תאילנדי", TranslationToLanguageId = he.Id, },              // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "थाई", TranslationToLanguageId = hi.Id, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Tajlandski", TranslationToLanguageId = hr.Id, },            // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Tayi", TranslationToLanguageId = ht.Id, },                  // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = hu.Id, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "թայերեն", TranslationToLanguageId = hy.Id, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Thailand", TranslationToLanguageId = id.Id, },              // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = isLanguage.Id, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = it.Id, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "タイ", TranslationToLanguageId = ja.Id, },                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ტაილანდური", TranslationToLanguageId = ka.Id, },          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಥಾಯ್", TranslationToLanguageId = kn.Id, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "타이어", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = la.Id, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Tailando", TranslationToLanguageId = lt.Id, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Taizemes", TranslationToLanguageId = lv.Id, },              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Тајланд", TranslationToLanguageId = mk.Id, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = ms.Id, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Tajlandiż", TranslationToLanguageId = mt.Id, },             // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = nl.Id, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = no.Id, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "tajski", TranslationToLanguageId = pl.Id, },                // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "tailandês", TranslationToLanguageId = pt.Id, },             // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "tailandez", TranslationToLanguageId = ro.Id, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "тайский", TranslationToLanguageId = ru.Id, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Thajské", TranslationToLanguageId = sk.Id, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "tajski", TranslationToLanguageId = sl.Id, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Тајландски", TranslationToLanguageId = sr.Id, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "thailändska", TranslationToLanguageId = sv.Id, },           // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = sw.Id, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "தாய்", TranslationToLanguageId = ta.Id, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "థాయ్", TranslationToLanguageId = te.Id, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ไทย", TranslationToLanguageId = th.Id, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Tayland", TranslationToLanguageId = tr.Id, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "тайський", TranslationToLanguageId = uk.Id, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "تھائی", TranslationToLanguageId = ur.Id, },                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Thái", TranslationToLanguageId = vi.Id, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טייַלענדיש", TranslationToLanguageId = yi.Id, },            // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "泰国", TranslationToLanguageId = zh.Id, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = th.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!tr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Turkish", TranslationToLanguageId = en.Id, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "turco", TranslationToLanguageId = es.Id, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Türkisch", TranslationToLanguageId = de.Id, },              // #3  Language = "German"
//                    new CreateLanguageName { Text = "تركي", TranslationToLanguageId = ar.Id, },                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Turkse", TranslationToLanguageId = af.Id, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Türk", TranslationToLanguageId = az.Id, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Турэцкая", TranslationToLanguageId = be.Id, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "турски", TranslationToLanguageId = bg.Id, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "তুর্কী", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "turc", TranslationToLanguageId = ca.Id, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "turečtina", TranslationToLanguageId = cs.Id, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Twrcaidd", TranslationToLanguageId = cy.Id, },              // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "tyrkisk", TranslationToLanguageId = da.Id, },               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "türgi", TranslationToLanguageId = et.Id, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Turkish", TranslationToLanguageId = eu.Id, },               // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ترکی", TranslationToLanguageId = fa.Id, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "turkki", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "turque", TranslationToLanguageId = fr.Id, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Tuircis", TranslationToLanguageId = ga.Id, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Turco", TranslationToLanguageId = gl.Id, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ટર્કિશ", TranslationToLanguageId = gu.Id, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "טורקית", TranslationToLanguageId = he.Id, },               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "तुर्की का", TranslationToLanguageId = hi.Id, },                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "turski", TranslationToLanguageId = hr.Id, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Tik", TranslationToLanguageId = ht.Id, },                   // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "török", TranslationToLanguageId = hu.Id, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "թուրքական", TranslationToLanguageId = hy.Id, },            // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Turki", TranslationToLanguageId = id.Id, },                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Tyrkneska", TranslationToLanguageId = isLanguage.Id, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "turco", TranslationToLanguageId = it.Id, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "トルコ", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "თურქეთის", TranslationToLanguageId = ka.Id, },            // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಟರ್ಕಿಷ್", TranslationToLanguageId = kn.Id, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "터키어", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Turkish", TranslationToLanguageId = la.Id, },               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Turkijos", TranslationToLanguageId = lt.Id, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "turku", TranslationToLanguageId = lv.Id, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Турција", TranslationToLanguageId = mk.Id, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Turki", TranslationToLanguageId = ms.Id, },                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Tork", TranslationToLanguageId = mt.Id, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Turks", TranslationToLanguageId = nl.Id, },                 // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "tyrkisk", TranslationToLanguageId = no.Id, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "turecki", TranslationToLanguageId = pl.Id, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "turco", TranslationToLanguageId = pt.Id, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "turc", TranslationToLanguageId = ro.Id, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "турецкий", TranslationToLanguageId = ru.Id, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "turečtina", TranslationToLanguageId = sk.Id, },             // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "turečtina", TranslationToLanguageId = sl.Id, },             // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "turk", TranslationToLanguageId = sq.Id, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "турски", TranslationToLanguageId = sr.Id, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "turkiska", TranslationToLanguageId = sv.Id, },              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kituruki", TranslationToLanguageId = sw.Id, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "டர்கிஷ்", TranslationToLanguageId = ta.Id, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "టర్కిష్", TranslationToLanguageId = te.Id, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ตุรกี", TranslationToLanguageId = th.Id, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Türk", TranslationToLanguageId = tr.Id, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Турецька", TranslationToLanguageId = uk.Id, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ترکی", TranslationToLanguageId = ur.Id, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Thổ Nhĩ Kỳ", TranslationToLanguageId = vi.Id, },            // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טערקיש", TranslationToLanguageId = yi.Id, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "土耳其", TranslationToLanguageId = zh.Id, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tr.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!uk.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ukrainian", TranslationToLanguageId = en.Id, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "ucranio", TranslationToLanguageId = es.Id, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Ukrainisch", TranslationToLanguageId = de.Id, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأوكراني", TranslationToLanguageId = ar.Id, },                  // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Oekraïens", TranslationToLanguageId = af.Id, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Ukrayna", TranslationToLanguageId = az.Id, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Украінскі", TranslationToLanguageId = be.Id, },                  // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "украински", TranslationToLanguageId = bg.Id, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইউক্রেরিয়ান", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "ucraïnès", TranslationToLanguageId = ca.Id, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "ukrajinský", TranslationToLanguageId = cs.Id, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Wcreineg", TranslationToLanguageId = cy.Id, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "ukrainsk", TranslationToLanguageId = da.Id, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "ukraina", TranslationToLanguageId = et.Id, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Ukrainian", TranslationToLanguageId = eu.Id, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اوکراین", TranslationToLanguageId = fa.Id, },                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "ukrainalainen", TranslationToLanguageId = fi.Id, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "ukrainiens", TranslationToLanguageId = fr.Id, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Úcráinis", TranslationToLanguageId = ga.Id, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Ucraíno", TranslationToLanguageId = gl.Id, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "યુક્રેનિયન", TranslationToLanguageId = gu.Id, },                    // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אוקראינית", TranslationToLanguageId = he.Id, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "यूक्रेनी", TranslationToLanguageId = hi.Id, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "ukrajinski", TranslationToLanguageId = hr.Id, },                 // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "ukrainian", TranslationToLanguageId = ht.Id, },                  // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "ukrán", TranslationToLanguageId = hu.Id, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ուկրաիներեն", TranslationToLanguageId = hy.Id, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Ukraina", TranslationToLanguageId = id.Id, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "úkraínska", TranslationToLanguageId = isLanguage.Id, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ucraino", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ウクライナ語", TranslationToLanguageId = ja.Id, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "უკრაინის", TranslationToLanguageId = ka.Id, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಉಕ್ರೇನ್ ಪ್ರಾಂತ ಯಾ ಅದರ ಭಾಷೆ", TranslationToLanguageId = kn.Id, },  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "우크라이나의", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Ukrainian", TranslationToLanguageId = la.Id, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Ukrainos", TranslationToLanguageId = lt.Id, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "ukraiņu", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Украина", TranslationToLanguageId = mk.Id, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Ukraine", TranslationToLanguageId = ms.Id, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ukraina", TranslationToLanguageId = mt.Id, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Oekraïens", TranslationToLanguageId = nl.Id, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "ukrainsk", TranslationToLanguageId = no.Id, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "ukraiński", TranslationToLanguageId = pl.Id, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "ucraniano", TranslationToLanguageId = pt.Id, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "ucrainean", TranslationToLanguageId = ro.Id, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "украинский", TranslationToLanguageId = ru.Id, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Ukrajinský", TranslationToLanguageId = sk.Id, },                 // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "ukrajinski", TranslationToLanguageId = sl.Id, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "ukrainisht", TranslationToLanguageId = sq.Id, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "украјински", TranslationToLanguageId = sr.Id, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "ukrainska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiukreni", TranslationToLanguageId = sw.Id, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "உக்ரேனியன்", TranslationToLanguageId = ta.Id, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "యుక్రేయిన్", TranslationToLanguageId = te.Id, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษายูเครน", TranslationToLanguageId = th.Id, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Ukrayna", TranslationToLanguageId = tr.Id, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Український", TranslationToLanguageId = uk.Id, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "يوکرينی", TranslationToLanguageId = ur.Id, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ucraina", TranslationToLanguageId = vi.Id, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אוקרייניש", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "乌克兰", TranslationToLanguageId = zh.Id, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = uk.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ur.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = en.Id, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = es.Id, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = de.Id, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأردية", TranslationToLanguageId = ar.Id, },                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Oerdoe", TranslationToLanguageId = af.Id, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = az.Id, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Мова урду", TranslationToLanguageId = be.Id, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "урду", TranslationToLanguageId = bg.Id, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "উর্দু", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = ca.Id, },                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = cs.Id, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = cy.Id, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = da.Id, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "urdu keel", TranslationToLanguageId = et.Id, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = eu.Id, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان اردو", TranslationToLanguageId = fa.Id, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = fi.Id, },                    // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "ourdou", TranslationToLanguageId = fr.Id, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Urdais", TranslationToLanguageId = ga.Id, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "urdú", TranslationToLanguageId = gl.Id, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઉર્દુ", TranslationToLanguageId = gu.Id, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אורדו", TranslationToLanguageId = he.Id, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "उर्दू", TranslationToLanguageId = hi.Id, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = hr.Id, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = ht.Id, },                    // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = hu.Id, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ուրդու", TranslationToLanguageId = hy.Id, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = id.Id, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "úrdú", TranslationToLanguageId = isLanguage.Id, },            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = it.Id, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ウルドゥー語", TranslationToLanguageId = ja.Id, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ურდული", TranslationToLanguageId = ka.Id, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಉರ್ದು ಭಾಷೆ", TranslationToLanguageId = kn.Id, },               // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "우르 두말", TranslationToLanguageId = ko.Id, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = la.Id, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "urdų kalba", TranslationToLanguageId = lt.Id, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "урду", TranslationToLanguageId = mk.Id, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = ms.Id, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = mt.Id, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = nl.Id, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = no.Id, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = pl.Id, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = pt.Id, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = ro.Id, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "язык урду", TranslationToLanguageId = ru.Id, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = sk.Id, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = sl.Id, },                    // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "urduisht", TranslationToLanguageId = sq.Id, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "урду", TranslationToLanguageId = sr.Id, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = sv.Id, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiurdu", TranslationToLanguageId = sw.Id, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "முள்ளம் பன்றி", TranslationToLanguageId = ta.Id, },         // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఉర్దూ భాష", TranslationToLanguageId = te.Id, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาอิรดู", TranslationToLanguageId = th.Id, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Urduca", TranslationToLanguageId = tr.Id, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "мова урду", TranslationToLanguageId = uk.Id, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "اردو", TranslationToLanguageId = ur.Id, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Urdu", TranslationToLanguageId = vi.Id, },              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אורדו", TranslationToLanguageId = yi.Id, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "乌尔都语", TranslationToLanguageId = zh.Id, },                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ur.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!vi.Names.Any())
//                new List<CreateLanguageName>
//                {

//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = en.Id, },                      // #1  Language = "English"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = es.Id, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = de.Id, },                      // #3  Language = "German"
//                    new CreateLanguageName { Text = "الفيتنامية", TranslationToLanguageId = ar.Id, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Viëtnamees", TranslationToLanguageId = af.Id, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Vyetnam", TranslationToLanguageId = az.Id, },                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "в'етнамскі", TranslationToLanguageId = be.Id, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "виетнамски", TranslationToLanguageId = bg.Id, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ভিএত্নেম লোক", TranslationToLanguageId = bn.Id, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = ca.Id, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "vietnamský", TranslationToLanguageId = cs.Id, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Fietnameg", TranslationToLanguageId = cy.Id, },                       // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "vietnamesisk", TranslationToLanguageId = da.Id, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "vietnami", TranslationToLanguageId = et.Id, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = eu.Id, },                      // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ویتنامی", TranslationToLanguageId = fa.Id, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "vietnam", TranslationToLanguageId = fi.Id, },                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "vietnamienne", TranslationToLanguageId = fr.Id, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Vítneaimis", TranslationToLanguageId = ga.Id, },                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Vietnamita", TranslationToLanguageId = gl.Id, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "વિયેતનામીઝ", TranslationToLanguageId = gu.Id, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ויאטנמית", TranslationToLanguageId = he.Id, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "वियतनामी", TranslationToLanguageId = hi.Id, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "vijetnamski", TranslationToLanguageId = hr.Id, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Vyetnamyen", TranslationToLanguageId = ht.Id, },                      // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "vietnami", TranslationToLanguageId = hu.Id, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "վիետնամերեն", TranslationToLanguageId = hy.Id, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Vietnam", TranslationToLanguageId = id.Id, },                         // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "víetnamska", TranslationToLanguageId = isLanguage.Id, },              // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = it.Id, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ベトナム", TranslationToLanguageId = ja.Id, },                         // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ვიეტნამური", TranslationToLanguageId = ka.Id, },                     // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ವಿಯೆಟ್ನಾಂ ದೇಶದ ವ್ಯಕ್ತಿ ಯಾ ಪ್ರಜೆ ಯಾ ಭಾಷೆ", TranslationToLanguageId = kn.Id, }, // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "베트남 사람", TranslationToLanguageId = ko.Id, },                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Vietnamica", TranslationToLanguageId = la.Id, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "vietnamiečių", TranslationToLanguageId = lt.Id, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "vjetnamiešu", TranslationToLanguageId = lv.Id, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "виетнамски", TranslationToLanguageId = mk.Id, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Vietnam", TranslationToLanguageId = ms.Id, },                         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Vjetnamiż", TranslationToLanguageId = mt.Id, },                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = nl.Id, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "vietnamesisk", TranslationToLanguageId = no.Id, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "wietnamski", TranslationToLanguageId = pl.Id, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = pt.Id, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "vietnameză", TranslationToLanguageId = ro.Id, },                      // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "вьетнамский", TranslationToLanguageId = ru.Id, },                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "vietnamský", TranslationToLanguageId = sk.Id, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = sl.Id, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "vietnamisht", TranslationToLanguageId = sq.Id, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "вијетнамски", TranslationToLanguageId = sr.Id, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "vietnamese", TranslationToLanguageId = sv.Id, },                      // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kivietinamu", TranslationToLanguageId = sw.Id, },                     // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "வியட்னாமீஸ்", TranslationToLanguageId = ta.Id, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "వియత్నమీస్", TranslationToLanguageId = te.Id, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เวียตนาม", TranslationToLanguageId = th.Id, },                          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Vietnam", TranslationToLanguageId = tr.Id, },                         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "В'єтнамський", TranslationToLanguageId = uk.Id, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ويتنامی", TranslationToLanguageId = ur.Id, },                        // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Việt", TranslationToLanguageId = vi.Id, },                            // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "וויעטנאַמעזיש", TranslationToLanguageId = yi.Id, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "越南", TranslationToLanguageId = zh.Id, },                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = vi.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!yi.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = en.Id, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "yídish", TranslationToLanguageId = es.Id, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Jiddisch", TranslationToLanguageId = de.Id, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "اليديشية", TranslationToLanguageId = ar.Id, },              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Jiddisj", TranslationToLanguageId = af.Id, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Yidiş", TranslationToLanguageId = az.Id, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "ідыш", TranslationToLanguageId = be.Id, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "идиш", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইডীশ্", TranslationToLanguageId = bn.Id, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Jiddisch", TranslationToLanguageId = ca.Id, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = cs.Id, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Iddeweg", TranslationToLanguageId = cy.Id, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "jiddisch", TranslationToLanguageId = da.Id, },               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Jidiš", TranslationToLanguageId = et.Id, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = eu.Id, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ییدیش", TranslationToLanguageId = fa.Id, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Jiddiš", TranslationToLanguageId = fi.Id, },                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "yiddish", TranslationToLanguageId = fr.Id, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Giúdais", TranslationToLanguageId = ga.Id, },                // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = gl.Id, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "યીદ્દીશ", TranslationToLanguageId = gu.Id, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אידיש", TranslationToLanguageId = he.Id, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "यहूदी", TranslationToLanguageId = hi.Id, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = hr.Id, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = ht.Id, },                // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "jiddis", TranslationToLanguageId = hu.Id, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իդիշ", TranslationToLanguageId = hy.Id, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Yiddi", TranslationToLanguageId = id.Id, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "jiddíska", TranslationToLanguageId = isLanguage.Id, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "yiddish", TranslationToLanguageId = it.Id, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "イディッシュ語", TranslationToLanguageId = ja.Id, },          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "იდიშზე", TranslationToLanguageId = ka.Id, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಯಿಡ್ಡಿಷ್", TranslationToLanguageId = kn.Id, },                  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "이디시 말의 뜻", TranslationToLanguageId = ko.Id, },          // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = la.Id, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = lt.Id, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "jidišs", TranslationToLanguageId = lv.Id, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "јидски", TranslationToLanguageId = mk.Id, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Yiddish", TranslationToLanguageId = ms.Id, },         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Jiddix", TranslationToLanguageId = mt.Id, },                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Jiddisch", TranslationToLanguageId = nl.Id, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "jiddisch", TranslationToLanguageId = no.Id, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "jidysz", TranslationToLanguageId = pl.Id, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "ídiche", TranslationToLanguageId = pt.Id, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "idiş", TranslationToLanguageId = ro.Id, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "идиш", TranslationToLanguageId = ru.Id, },                   // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = sk.Id, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = sl.Id, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "jidish", TranslationToLanguageId = sq.Id, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јидиш", TranslationToLanguageId = sr.Id, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "jiddisch", TranslationToLanguageId = sv.Id, },               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = sw.Id, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஈத்திஷ", TranslationToLanguageId = ta.Id, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "యిడ్డిష్", TranslationToLanguageId = te.Id, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = th.Id, },                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Eskenazi dili", TranslationToLanguageId = tr.Id, },          // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Ідиш", TranslationToLanguageId = uk.Id, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "يادش", TranslationToLanguageId = ur.Id, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Yiddish", TranslationToLanguageId = vi.Id, },          // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ייִדיש", TranslationToLanguageId = yi.Id, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "意第绪语", TranslationToLanguageId = zh.Id, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = yi.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (!zh.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chinese", TranslationToLanguageId = en.Id, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "chino", TranslationToLanguageId = es.Id, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Chinese", TranslationToLanguageId = de.Id, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الصينية", TranslationToLanguageId = ar.Id, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Chinese", TranslationToLanguageId = af.Id, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "çin", TranslationToLanguageId = az.Id, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Кітайскі", TranslationToLanguageId = be.Id, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "китайски", TranslationToLanguageId = bg.Id, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "চীনা", TranslationToLanguageId = bn.Id, },                        // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "xinès", TranslationToLanguageId = ca.Id, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Číňan", TranslationToLanguageId = cs.Id, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Tseiniaidd", TranslationToLanguageId = cy.Id, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "kinesisk", TranslationToLanguageId = da.Id, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hiina", TranslationToLanguageId = et.Id, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "txinera", TranslationToLanguageId = eu.Id, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "چینی", TranslationToLanguageId = fa.Id, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "kiinalainen", TranslationToLanguageId = fi.Id, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "chinoise", TranslationToLanguageId = fr.Id, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Sínis", TranslationToLanguageId = ga.Id, },                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "chinés", TranslationToLanguageId = gl.Id, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ચિની", TranslationToLanguageId = gu.Id, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סינית", TranslationToLanguageId = he.Id, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "चीनी", TranslationToLanguageId = hi.Id, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "kineski", TranslationToLanguageId = hr.Id, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Chinwa", TranslationToLanguageId = ht.Id, },                     // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "kínai", TranslationToLanguageId = hu.Id, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "չինացի", TranslationToLanguageId = hy.Id, },                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Cina", TranslationToLanguageId = id.Id, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "kínverska", TranslationToLanguageId = isLanguage.Id, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "cinese", TranslationToLanguageId = it.Id, },                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "中国", TranslationToLanguageId = ja.Id, },                       // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჩინური", TranslationToLanguageId = ka.Id, },                    // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಚೀನೀ", TranslationToLanguageId = kn.Id, },                       // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "중국어", TranslationToLanguageId = ko.Id, },                     // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Sinica", TranslationToLanguageId = la.Id, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Kinijos", TranslationToLanguageId = lt.Id, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "ķīniešu", TranslationToLanguageId = lv.Id, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "кинески", TranslationToLanguageId = mk.Id, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Cina", TranslationToLanguageId = ms.Id, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ċiniż", TranslationToLanguageId = mt.Id, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Chinees", TranslationToLanguageId = nl.Id, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "kinesiske", TranslationToLanguageId = no.Id, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "chiński", TranslationToLanguageId = pl.Id, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "chinês", TranslationToLanguageId = pt.Id, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "chinezesc", TranslationToLanguageId = ro.Id, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "китайский", TranslationToLanguageId = ru.Id, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Číňan", TranslationToLanguageId = sk.Id, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "kitajski", TranslationToLanguageId = sl.Id, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "kinez", TranslationToLanguageId = sq.Id, },                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "кинески", TranslationToLanguageId = sr.Id, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "kinesiska", TranslationToLanguageId = sv.Id, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kichina", TranslationToLanguageId = sw.Id, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "சீன", TranslationToLanguageId = ta.Id, },                       // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "చైనీస్", TranslationToLanguageId = te.Id, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "จีน", TranslationToLanguageId = th.Id, },                         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Çin", TranslationToLanguageId = tr.Id, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Китайський", TranslationToLanguageId = uk.Id, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "چینی", TranslationToLanguageId = ur.Id, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Trung Quốc", TranslationToLanguageId = vi.Id, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "כינעזיש", TranslationToLanguageId = yi.Id, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "中国", TranslationToLanguageId = zh.Id, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = zh.Id;
//                    _createLanguageName.Handle(n);
//                });


//            //Languages with only 1 translation

//            if (aa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Afar", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = aa.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (ab.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Abkhazian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ab.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ak.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Akan", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ak.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (am.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Amharic", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = am.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (an.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Aragonese", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = an.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (asLanguage.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Assamese", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = asLanguage.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (av.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Avaric", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = av.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ay.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Aymara", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ay.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ba.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bashkir", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ba.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (bh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bihari languages", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bh.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (bi.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bislama", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bi.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (bm.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bambara", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bm.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (bo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tibetan", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bo.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (br.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Breton", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = br.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (bs.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bosnian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bs.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (ce.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chechen", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ce.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ch.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chamorro", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ch.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (co.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Corsican", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = co.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (cr.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Cree", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cr.Id;
//                    _createLanguageName.Handle(n);
//                });



//            if (cu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Church Slavic", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cu.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (cv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chuvash", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cv.Id;
//                    _createLanguageName.Handle(n);
//                });



//            if (dv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Dhivehi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = dv.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (dz.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Dzongkha", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = dz.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ee.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ewe", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ee.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (el.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Modern Greek (1453-)", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = el.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (eo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Esperanto", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = eo.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ff.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Fulah", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ff.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (fj.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Fijian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fj.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (fo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Faroese", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fo.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (fy.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Western Frisian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fy.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (gd.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Scottish Gaelic", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gd.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (gn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Guarani", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gn.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (gv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Manx", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gv.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ha.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hausa", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ha.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ho.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hiri Motu", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ho.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (hz.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Herero", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hz.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ia.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Interlingua (International Auxiliary Language Association)", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ia.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ie.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Interlingue", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ie.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ig.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Igbo", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ig.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ii.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sichuan Yi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ii.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (io.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ido", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = io.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (iu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Inuktitut", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = iu.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (jv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Javanese", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = jv.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (kg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kongo", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kg.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ki.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kikuyu", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ki.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (kj.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kuanyama", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kj.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (kk.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kazakh", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kk.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (kl.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kalaallisut", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kl.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (km.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Central Khmer", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = km.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ks.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kashmiri", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ks.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ku.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kurdish", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ku.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (kv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Komi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kv.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (kw.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Cornish", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kw.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ky.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kirghiz", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ky.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (lb.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Luxembourgish", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lb.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (lg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ganda", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lg.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (li.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Limburgan", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = li.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ln.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Lingala", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ln.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (lo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Lao", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lo.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (mg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Malagasy", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mg.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (mh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Marshallese", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mh.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (mi.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Maori", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mi.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ml.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Malayalam", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ml.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (mn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Mongolian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mn.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (mr.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Marathi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mr.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (my.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Burmese", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = my.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (na.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Nauru", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = na.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (nb.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Norwegian Bokmål", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nb.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ne.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Nepali", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ne.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ng.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ndonga", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ng.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (nn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Norwegian Nynorsk", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nn.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (nv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Navajo", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nv.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ny.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Nyanja", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ny.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (oc.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Occitan (post 1500)", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = oc.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (om.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Oromo", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = om.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (or.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Oriya", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = or.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (os.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ossetian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = os.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (pa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Panjabi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pa.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (pi.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Pali", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pi.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ps.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Pushto", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ps.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (qu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Quechua", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = qu.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (rm.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Romansh", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = rm.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (rn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Rundi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = rn.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (rw.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kinyarwanda", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = rw.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (sa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sanskrit", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sa.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (sc.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sardinian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sc.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (sd.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sindhi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sd.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (se.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Northern Sami", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = se.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (sg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sango", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sg.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (sh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Serbo-Croatian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sh.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (si.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sinhala", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = si.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (sm.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Samoan", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sm.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (sn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Shona", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sn.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (so.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Somali", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = so.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (ss.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Swati", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ss.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (st.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Southern Sotho", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = st.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (su.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sundanese", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = su.Id;
//                    _createLanguageName.Handle(n);
//                });


//            if (tg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tajik", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tg.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ti.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tigrinya", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ti.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (tk.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Turkmen", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tk.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (tl.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tagalog", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tl.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (tn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tswana", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tn.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (to.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tonga (Tonga Islands)", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = to.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ts.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tsonga", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ts.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (tt.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tatar", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tt.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (tw.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Twi", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tw.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ty.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tahitian", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ty.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ug.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Uighur", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ug.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (uz.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Uzbek", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = uz.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (ve.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Venda", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ve.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (vo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Volapük", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = vo.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (wa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Walloon", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = wa.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (wo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Wolof", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = wo.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (xh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Xhosa", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = xh.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (yo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Yoruba", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = yo.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (za.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Zhuang", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = za.Id;
//                    _createLanguageName.Handle(n);
//                });

//            if (zu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Zulu", TranslationToLanguageId = en.Id, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = zu.Id;
//                    _createLanguageName.Handle(n);
//                });

//            _unitOfWork.SaveChanges();

//            #endregion
//        }
//    }
//}