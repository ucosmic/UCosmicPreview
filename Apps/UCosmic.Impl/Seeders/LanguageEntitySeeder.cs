//using System.Collections.Generic;
//using System.Linq;
//using UCosmic.Domain.Languages;

//namespace UCosmic.Impl.Seeders2
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
//            if (_queryProcessor.Execute(new FindAllLanguagesQuery()).Any()) return;

//            #region SIL Languages

//            var en = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "en" });
//            if (en == null)
//            {
//                var enCreate = new CreateLanguage { TwoLetterIsoCode = "en", ThreeLetterIsoCode = "eng", ThreeLetterIsoBibliographicCode = "eng", };
//                _createLanguage.Handle(enCreate);
//                en = enCreate.CreatedLanguage;
//            }

//            var es = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "es" });
//            if (es == null)
//            {
//                var esCreate = new CreateLanguage { TwoLetterIsoCode = "es", ThreeLetterIsoCode = "spa", ThreeLetterIsoBibliographicCode = "spa", };
//                _createLanguage.Handle(esCreate);
//                es = esCreate.CreatedLanguage;
//            }

//            var de = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "de" });
//            if (de == null)
//            {
//                var deCreate = new CreateLanguage { TwoLetterIsoCode = "de", ThreeLetterIsoCode = "deu", ThreeLetterIsoBibliographicCode = "ger", };
//                _createLanguage.Handle(deCreate);
//                de = deCreate.CreatedLanguage;
//            }

//            var ar = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ar" });
//            if (ar == null)
//            {
//                var arCreate = new CreateLanguage { TwoLetterIsoCode = "ar", ThreeLetterIsoCode = "ara", ThreeLetterIsoBibliographicCode = "ara", };
//                _createLanguage.Handle(arCreate);
//                ar = arCreate.CreatedLanguage;
//            }

//            var aa = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "aa" });
//            if (aa == null)
//            {
//                var aaCreate = new CreateLanguage { TwoLetterIsoCode = "aa", ThreeLetterIsoCode = "aar", ThreeLetterIsoBibliographicCode = "aar", };
//                _createLanguage.Handle(aaCreate);
//                aa = aaCreate.CreatedLanguage;
//            }

//            var ab = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ab" });
//            if (ab == null)
//            {
//                var abCreate = new CreateLanguage { TwoLetterIsoCode = "ab", ThreeLetterIsoCode = "abk", ThreeLetterIsoBibliographicCode = "abk", };
//                _createLanguage.Handle(abCreate);
//                ab = abCreate.CreatedLanguage;
//            }

//            var af = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "af" });
//            if (af == null)
//            {
//                var afCreate = new CreateLanguage { TwoLetterIsoCode = "af", ThreeLetterIsoCode = "afr", ThreeLetterIsoBibliographicCode = "afr", };
//                _createLanguage.Handle(afCreate);
//                af = afCreate.CreatedLanguage;
//            }

//            var ak = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ak" });
//            if (ak == null)
//            {
//                var akCreate = new CreateLanguage { TwoLetterIsoCode = "ak", ThreeLetterIsoCode = "aka", ThreeLetterIsoBibliographicCode = "aka", };
//                _createLanguage.Handle(akCreate);
//                ak = akCreate.CreatedLanguage;
//            }

//            var am = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "am" });
//            if (am == null)
//            {
//                var amCreate = new CreateLanguage { TwoLetterIsoCode = "am", ThreeLetterIsoCode = "amh", ThreeLetterIsoBibliographicCode = "amh", };
//                _createLanguage.Handle(amCreate);
//                am = amCreate.CreatedLanguage;
//            }

//            var an = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "an" });
//            if (an == null)
//            {
//                var anCreate = new CreateLanguage { TwoLetterIsoCode = "an", ThreeLetterIsoCode = "arg", ThreeLetterIsoBibliographicCode = "arg", };
//                _createLanguage.Handle(anCreate);
//                an = anCreate.CreatedLanguage;
//            }

//            var asLanguage = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "as" });
//            if (asLanguage == null)
//            {
//                var asLanguageCreate = new CreateLanguage { TwoLetterIsoCode = "as", ThreeLetterIsoCode = "asm", ThreeLetterIsoBibliographicCode = "asm", };
//                _createLanguage.Handle(asLanguageCreate);
//                asLanguage = asLanguageCreate.CreatedLanguage;
//            }

//            var av = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "av" });
//            if (av == null)
//            {
//                var avCreate = new CreateLanguage { TwoLetterIsoCode = "av", ThreeLetterIsoCode = "ava", ThreeLetterIsoBibliographicCode = "ava", };
//                _createLanguage.Handle(avCreate);
//                av = avCreate.CreatedLanguage;
//            }

//            var ay = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ay" });
//            if (ay == null)
//            {
//                var ayCreate = new CreateLanguage { TwoLetterIsoCode = "ay", ThreeLetterIsoCode = "aym", ThreeLetterIsoBibliographicCode = "aym", };
//                _createLanguage.Handle(ayCreate);
//                ay = ayCreate.CreatedLanguage;
//            }

//            var az = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "az" });
//            if (az == null)
//            {
//                var azCreate = new CreateLanguage { TwoLetterIsoCode = "az", ThreeLetterIsoCode = "aze", ThreeLetterIsoBibliographicCode = "aze", };
//                _createLanguage.Handle(azCreate);
//                az = azCreate.CreatedLanguage;
//            }

//            var ba = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ba" });
//            if (ba == null)
//            {
//                var baCreate = new CreateLanguage { TwoLetterIsoCode = "ba", ThreeLetterIsoCode = "bak", ThreeLetterIsoBibliographicCode = "bak", };
//                _createLanguage.Handle(baCreate);
//                ba = baCreate.CreatedLanguage;
//            }

//            var be = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "be" });
//            if (be == null)
//            {
//                var beCreate = new CreateLanguage { TwoLetterIsoCode = "be", ThreeLetterIsoCode = "bel", ThreeLetterIsoBibliographicCode = "bel", };
//                _createLanguage.Handle(beCreate);
//                be = beCreate.CreatedLanguage;
//            }

//            var bg = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "bg" });
//            if (bg == null)
//            {
//                var bgCreate = new CreateLanguage { TwoLetterIsoCode = "bg", ThreeLetterIsoCode = "bul", ThreeLetterIsoBibliographicCode = "bul", };
//                _createLanguage.Handle(bgCreate);
//                bg = bgCreate.CreatedLanguage;
//            }

//            var bh = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "bh" });
//            if (bh == null)
//            {
//                var bhCreate = new CreateLanguage { TwoLetterIsoCode = "bh", ThreeLetterIsoCode = "bih", ThreeLetterIsoBibliographicCode = "bih", };
//                _createLanguage.Handle(bhCreate);
//                bh = bhCreate.CreatedLanguage;
//            }

//            var bi = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "bi" });
//            if (bi == null)
//            {
//                var biCreate = new CreateLanguage { TwoLetterIsoCode = "bi", ThreeLetterIsoCode = "bis", ThreeLetterIsoBibliographicCode = "bis", };
//                _createLanguage.Handle(biCreate);
//                bi = biCreate.CreatedLanguage;
//            }

//            var bm = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "bm" });
//            if (bm == null)
//            {
//                var bmCreate = new CreateLanguage { TwoLetterIsoCode = "bm", ThreeLetterIsoCode = "bam", ThreeLetterIsoBibliographicCode = "bam", };
//                _createLanguage.Handle(bmCreate);
//                bm = bmCreate.CreatedLanguage;
//            }

//            var bn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "bn" });
//            if (bn == null)
//            {
//                var bnCreate = new CreateLanguage { TwoLetterIsoCode = "bn", ThreeLetterIsoCode = "ben", ThreeLetterIsoBibliographicCode = "ben", };
//                _createLanguage.Handle(bnCreate);
//                bn = bnCreate.CreatedLanguage;
//            }

//            var bo = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "bo" });
//            if (bo == null)
//            {
//                var boCreate = new CreateLanguage { TwoLetterIsoCode = "bo", ThreeLetterIsoCode = "bod", ThreeLetterIsoBibliographicCode = "tib", };
//                _createLanguage.Handle(boCreate);
//                bo = boCreate.CreatedLanguage;
//            }

//            var br = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "br" });
//            if (br == null)
//            {
//                var brCreate = new CreateLanguage { TwoLetterIsoCode = "br", ThreeLetterIsoCode = "bre", ThreeLetterIsoBibliographicCode = "bre", };
//                _createLanguage.Handle(brCreate);
//                br = brCreate.CreatedLanguage;
//            }

//            var bs = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "bs" });
//            if (bs == null)
//            {
//                var bsCreate = new CreateLanguage { TwoLetterIsoCode = "bs", ThreeLetterIsoCode = "bos", ThreeLetterIsoBibliographicCode = "bos", };
//                _createLanguage.Handle(bsCreate);
//                bs = bsCreate.CreatedLanguage;
//            }

//            var ca = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ca" });
//            if (ca == null)
//            {
//                var caCreate = new CreateLanguage { TwoLetterIsoCode = "ca", ThreeLetterIsoCode = "cat", ThreeLetterIsoBibliographicCode = "cat", };
//                _createLanguage.Handle(caCreate);
//                ca = caCreate.CreatedLanguage;
//            }

//            var ce = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ce" });
//            if (ce == null)
//            {
//                var ceCreate = new CreateLanguage { TwoLetterIsoCode = "ce", ThreeLetterIsoCode = "che", ThreeLetterIsoBibliographicCode = "che", };
//                _createLanguage.Handle(ceCreate);
//                ce = ceCreate.CreatedLanguage;
//            }

//            var ch = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ch" });
//            if (ch == null)
//            {
//                var chCreate = new CreateLanguage { TwoLetterIsoCode = "ch", ThreeLetterIsoCode = "cha", ThreeLetterIsoBibliographicCode = "cha", };
//                _createLanguage.Handle(chCreate);
//                ch = chCreate.CreatedLanguage;
//            }

//            var co = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "co" });
//            if (co == null)
//            {
//                var coCreate = new CreateLanguage { TwoLetterIsoCode = "co", ThreeLetterIsoCode = "cos", ThreeLetterIsoBibliographicCode = "cos", };
//                _createLanguage.Handle(coCreate);
//                co = coCreate.CreatedLanguage;
//            }

//            var cr = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "cr" });
//            if (cr == null)
//            {
//                var crCreate = new CreateLanguage { TwoLetterIsoCode = "cr", ThreeLetterIsoCode = "cre", ThreeLetterIsoBibliographicCode = "cre", };
//                _createLanguage.Handle(crCreate);
//                cr = crCreate.CreatedLanguage;
//            }

//            var cs = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "cs" });
//            if (cs == null)
//            {
//                var csCreate = new CreateLanguage { TwoLetterIsoCode = "cs", ThreeLetterIsoCode = "ces", ThreeLetterIsoBibliographicCode = "cze", };
//                _createLanguage.Handle(csCreate);
//                cs = csCreate.CreatedLanguage;
//            }

//            var cu = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "cu" });
//            if (cu == null)
//            {
//                var cuCreate = new CreateLanguage { TwoLetterIsoCode = "cu", ThreeLetterIsoCode = "chu", ThreeLetterIsoBibliographicCode = "chu", };
//                _createLanguage.Handle(cuCreate);
//                cu = cuCreate.CreatedLanguage;
//            }

//            var cv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "cv" });
//            if (cv == null)
//            {
//                var cvCreate = new CreateLanguage { TwoLetterIsoCode = "cv", ThreeLetterIsoCode = "chv", ThreeLetterIsoBibliographicCode = "chv", };
//                _createLanguage.Handle(cvCreate);
//                cv = cvCreate.CreatedLanguage;
//            }

//            var cy = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "cy" });
//            if (cy == null)
//            {
//                var cyCreate = new CreateLanguage { TwoLetterIsoCode = "cy", ThreeLetterIsoCode = "cym", ThreeLetterIsoBibliographicCode = "wel", };
//                _createLanguage.Handle(cyCreate);
//                cy = cyCreate.CreatedLanguage;
//            }

//            var da = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "da" });
//            if (da == null)
//            {
//                var daCreate = new CreateLanguage { TwoLetterIsoCode = "da", ThreeLetterIsoCode = "dan", ThreeLetterIsoBibliographicCode = "dan", };
//                _createLanguage.Handle(daCreate);
//                da = daCreate.CreatedLanguage;
//            }

//            var dv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "dv" });
//            if (dv == null)
//            {
//                var dvCreate = new CreateLanguage { TwoLetterIsoCode = "dv", ThreeLetterIsoCode = "div", ThreeLetterIsoBibliographicCode = "div", };
//                _createLanguage.Handle(dvCreate);
//                dv = dvCreate.CreatedLanguage;
//            }

//            var dz = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "dz" });
//            if (dz == null)
//            {
//                var dzCreate = new CreateLanguage { TwoLetterIsoCode = "dz", ThreeLetterIsoCode = "dzo", ThreeLetterIsoBibliographicCode = "dzo", };
//                _createLanguage.Handle(dzCreate);
//                dz = dzCreate.CreatedLanguage;
//            }

//            var ee = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ee" });
//            if (ee == null)
//            {
//                var eeCreate = new CreateLanguage { TwoLetterIsoCode = "ee", ThreeLetterIsoCode = "ewe", ThreeLetterIsoBibliographicCode = "ewe", };
//                _createLanguage.Handle(eeCreate);
//                ee = eeCreate.CreatedLanguage;
//            }

//            var el = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "el" });
//            if (el == null)
//            {
//                var elCreate = new CreateLanguage { TwoLetterIsoCode = "el", ThreeLetterIsoCode = "ell", ThreeLetterIsoBibliographicCode = "ell", };
//                _createLanguage.Handle(elCreate);
//                el = elCreate.CreatedLanguage;
//            }

//            var eo = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "eo" });
//            if (eo == null)
//            {
//                var eoCreate = new CreateLanguage { TwoLetterIsoCode = "eo", ThreeLetterIsoCode = "epo", ThreeLetterIsoBibliographicCode = "epo", };
//                _createLanguage.Handle(eoCreate);
//                eo = eoCreate.CreatedLanguage;
//            }

//            var et = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "et" });
//            if (et == null)
//            {
//                var etCreate = new CreateLanguage { TwoLetterIsoCode = "et", ThreeLetterIsoCode = "est", ThreeLetterIsoBibliographicCode = "est", };
//                _createLanguage.Handle(etCreate);
//                et = etCreate.CreatedLanguage;
//            }

//            var eu = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "eu" });
//            if (eu == null)
//            {
//                var euCreate = new CreateLanguage { TwoLetterIsoCode = "eu", ThreeLetterIsoCode = "eus", ThreeLetterIsoBibliographicCode = "baq", };
//                _createLanguage.Handle(euCreate);
//                eu = euCreate.CreatedLanguage;
//            }

//            var fa = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "fa" });
//            if (fa == null)
//            {
//                var faCreate = new CreateLanguage { TwoLetterIsoCode = "fa", ThreeLetterIsoCode = "fas", ThreeLetterIsoBibliographicCode = "per", };
//                _createLanguage.Handle(faCreate);
//                fa = faCreate.CreatedLanguage;
//            }

//            var ff = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ff" });
//            if (ff == null)
//            {
//                var ffCreate = new CreateLanguage { TwoLetterIsoCode = "ff", ThreeLetterIsoCode = "ful", ThreeLetterIsoBibliographicCode = "ful", };
//                _createLanguage.Handle(ffCreate);
//                ff = ffCreate.CreatedLanguage;
//            }

//            var fj = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "fj" });
//            if (fj == null)
//            {
//                var fjCreate = new CreateLanguage { TwoLetterIsoCode = "fj", ThreeLetterIsoCode = "fij", ThreeLetterIsoBibliographicCode = "fij", };
//                _createLanguage.Handle(fjCreate);
//                fj = fjCreate.CreatedLanguage;
//            }

//            var fo = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "fo" });
//            if (fo == null)
//            {
//                var foCreate = new CreateLanguage { TwoLetterIsoCode = "fo", ThreeLetterIsoCode = "fao", ThreeLetterIsoBibliographicCode = "fao", };
//                _createLanguage.Handle(foCreate);
//                fo = foCreate.CreatedLanguage;
//            }

//            var fy = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "fy" });
//            if (fy == null)
//            {
//                var fyCreate = new CreateLanguage { TwoLetterIsoCode = "fy", ThreeLetterIsoCode = "fry", ThreeLetterIsoBibliographicCode = "fry", };
//                _createLanguage.Handle(fyCreate);
//                fy = fyCreate.CreatedLanguage;
//            }

//            var gd = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "gd" });
//            if (gd == null)
//            {
//                var gdCreate = new CreateLanguage { TwoLetterIsoCode = "gd", ThreeLetterIsoCode = "gla", ThreeLetterIsoBibliographicCode = "gla", };
//                _createLanguage.Handle(gdCreate);
//                gd = gdCreate.CreatedLanguage;
//            }

//            var fi = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "fi" });
//            if (fi == null)
//            {
//                var fiCreate = new CreateLanguage { TwoLetterIsoCode = "fi", ThreeLetterIsoCode = "fin", ThreeLetterIsoBibliographicCode = "fin", };
//                _createLanguage.Handle(fiCreate);
//                fi = fiCreate.CreatedLanguage;
//            }

//            var fr = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "fr" });
//            if (fr == null)
//            {
//                var frCreate = new CreateLanguage { TwoLetterIsoCode = "fr", ThreeLetterIsoCode = "fra", ThreeLetterIsoBibliographicCode = "fre", };
//                _createLanguage.Handle(frCreate);
//                fr = frCreate.CreatedLanguage;
//            }

//            var ga = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ga" });
//            if (ga == null)
//            {
//                var gaCreate = new CreateLanguage { TwoLetterIsoCode = "ga", ThreeLetterIsoCode = "gle", ThreeLetterIsoBibliographicCode = "gle", };
//                _createLanguage.Handle(gaCreate);
//                ga = gaCreate.CreatedLanguage;
//            }

//            var gl = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "gl" });
//            if (gl == null)
//            {
//                var glCreate = new CreateLanguage { TwoLetterIsoCode = "gl", ThreeLetterIsoCode = "glg", ThreeLetterIsoBibliographicCode = "glg", };
//                _createLanguage.Handle(glCreate);
//                gl = glCreate.CreatedLanguage;
//            }

//            var gn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "gn" });
//            if (gn == null)
//            {
//                var gnCreate = new CreateLanguage { TwoLetterIsoCode = "gn", ThreeLetterIsoCode = "grn", ThreeLetterIsoBibliographicCode = "grn", };
//                _createLanguage.Handle(gnCreate);
//                gn = gnCreate.CreatedLanguage;
//            }

//            var gu = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "gu" });
//            if (gu == null)
//            {
//                var guCreate = new CreateLanguage { TwoLetterIsoCode = "gu", ThreeLetterIsoCode = "guj", ThreeLetterIsoBibliographicCode = "guj", };
//                _createLanguage.Handle(guCreate);
//                gu = guCreate.CreatedLanguage;
//            }

//            var gv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "gv" });
//            if (gv == null)
//            {
//                var gvCreate = new CreateLanguage { TwoLetterIsoCode = "gv", ThreeLetterIsoCode = "glv", ThreeLetterIsoBibliographicCode = "glv", };
//                _createLanguage.Handle(gvCreate);
//                gv = gvCreate.CreatedLanguage;
//            }

//            var ha = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ha" });
//            if (ha == null)
//            {
//                var haCreate = new CreateLanguage { TwoLetterIsoCode = "ha", ThreeLetterIsoCode = "hau", ThreeLetterIsoBibliographicCode = "hau", };
//                _createLanguage.Handle(haCreate);
//                ha = haCreate.CreatedLanguage;
//            }

//            var he = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "he" });
//            if (he == null)
//            {
//                var heCreate = new CreateLanguage { TwoLetterIsoCode = "he", ThreeLetterIsoCode = "heb", ThreeLetterIsoBibliographicCode = "heb", };
//                _createLanguage.Handle(heCreate);
//                he = heCreate.CreatedLanguage;
//            }

//            var hi = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "hi" });
//            if (hi == null)
//            {
//                var hiCreate = new CreateLanguage { TwoLetterIsoCode = "hi", ThreeLetterIsoCode = "hin", ThreeLetterIsoBibliographicCode = "hin", };
//                _createLanguage.Handle(hiCreate);
//                hi = hiCreate.CreatedLanguage;
//            }

//            var ho = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ho" });
//            if (ho == null)
//            {
//                var hoCreate = new CreateLanguage { TwoLetterIsoCode = "ho", ThreeLetterIsoCode = "hmo", ThreeLetterIsoBibliographicCode = "hmo", };
//                _createLanguage.Handle(hoCreate);
//                ho = hoCreate.CreatedLanguage;
//            }

//            var hr = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "hr" });
//            if (hr == null)
//            {
//                var hrCreate = new CreateLanguage { TwoLetterIsoCode = "hr", ThreeLetterIsoCode = "hrv", ThreeLetterIsoBibliographicCode = "hrv", };
//                _createLanguage.Handle(hrCreate);
//                hr = hrCreate.CreatedLanguage;
//            }

//            var ht = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ht" });
//            if (ht == null)
//            {
//                var htCreate = new CreateLanguage { TwoLetterIsoCode = "ht", ThreeLetterIsoCode = "hat", ThreeLetterIsoBibliographicCode = "hat", };
//                _createLanguage.Handle(htCreate);
//                ht = htCreate.CreatedLanguage;
//            }

//            var hu = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "hu" });
//            if (hu == null)
//            {
//                var huCreate = new CreateLanguage { TwoLetterIsoCode = "hu", ThreeLetterIsoCode = "hun", ThreeLetterIsoBibliographicCode = "hun", };
//                _createLanguage.Handle(huCreate);
//                hu = huCreate.CreatedLanguage;
//            }

//            var hy = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "hy" });
//            if (hy == null)
//            {
//                var hyCreate = new CreateLanguage { TwoLetterIsoCode = "hy", ThreeLetterIsoCode = "hye", ThreeLetterIsoBibliographicCode = "arm", };
//                _createLanguage.Handle(hyCreate);
//                hy = hyCreate.CreatedLanguage;
//            }

//            var hz = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "hz" });
//            if (hz == null)
//            {
//                var hzCreate = new CreateLanguage { TwoLetterIsoCode = "hz", ThreeLetterIsoCode = "her", ThreeLetterIsoBibliographicCode = "her", };
//                _createLanguage.Handle(hzCreate);
//                hz = hzCreate.CreatedLanguage;
//            }

//            var ia = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ia" });
//            if (ia == null)
//            {
//                var iaCreate = new CreateLanguage { TwoLetterIsoCode = "ia", ThreeLetterIsoCode = "ina", ThreeLetterIsoBibliographicCode = "ina", };
//                _createLanguage.Handle(iaCreate);
//                ia = iaCreate.CreatedLanguage;
//            }

//            var id = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "id" });
//            if (id == null)
//            {
//                var idCreate = new CreateLanguage { TwoLetterIsoCode = "id", ThreeLetterIsoCode = "ind", ThreeLetterIsoBibliographicCode = "ind", };
//                _createLanguage.Handle(idCreate);
//                id = idCreate.CreatedLanguage;
//            }

//            var ie = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ie" });
//            if (ie == null)
//            {
//                var ieCreate = new CreateLanguage { TwoLetterIsoCode = "ie", ThreeLetterIsoCode = "ile", ThreeLetterIsoBibliographicCode = "ile", };
//                _createLanguage.Handle(ieCreate);
//                ie = ieCreate.CreatedLanguage;
//            }

//            var ig = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ig" });
//            if (ig == null)
//            {
//                var igCreate = new CreateLanguage { TwoLetterIsoCode = "ig", ThreeLetterIsoCode = "ibo", ThreeLetterIsoBibliographicCode = "ibo", };
//                _createLanguage.Handle(igCreate);
//                ig = igCreate.CreatedLanguage;
//            }

//            var ii = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ii" });
//            if (ii == null)
//            {
//                var iiCreate = new CreateLanguage { TwoLetterIsoCode = "ii", ThreeLetterIsoCode = "iii", ThreeLetterIsoBibliographicCode = "iii", };
//                _createLanguage.Handle(iiCreate);
//                ii = iiCreate.CreatedLanguage;
//            }

//            var io = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "io" });
//            if (io == null)
//            {
//                var ioCreate = new CreateLanguage { TwoLetterIsoCode = "io", ThreeLetterIsoCode = "ido", ThreeLetterIsoBibliographicCode = "ido", };
//                _createLanguage.Handle(ioCreate);
//                io = ioCreate.CreatedLanguage;
//            }

//            var isLanguage = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "is" });
//            if (isLanguage == null)
//            {
//                var isLanguageCreate = new CreateLanguage { TwoLetterIsoCode = "is", ThreeLetterIsoCode = "isl", ThreeLetterIsoBibliographicCode = "ice", };
//                _createLanguage.Handle(isLanguageCreate);
//                isLanguage = isLanguageCreate.CreatedLanguage;
//            }

//            var it = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "it" });
//            if (it == null)
//            {
//                var itCreate = new CreateLanguage { TwoLetterIsoCode = "it", ThreeLetterIsoCode = "ita", ThreeLetterIsoBibliographicCode = "ita", };
//                _createLanguage.Handle(itCreate);
//                it = itCreate.CreatedLanguage;
//            }

//            var iu = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "iu" });
//            if (iu == null)
//            {
//                var iuCreate = new CreateLanguage { TwoLetterIsoCode = "iu", ThreeLetterIsoCode = "iku", ThreeLetterIsoBibliographicCode = "iku", };
//                _createLanguage.Handle(iuCreate);
//                iu = iuCreate.CreatedLanguage;
//            }

//            var ja = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ja" });
//            if (ja == null)
//            {
//                var jaCreate = new CreateLanguage { TwoLetterIsoCode = "ja", ThreeLetterIsoCode = "jpn", ThreeLetterIsoBibliographicCode = "jpn", };
//                _createLanguage.Handle(jaCreate);
//                ja = jaCreate.CreatedLanguage;
//            }


//            var jv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "jv" });
//            if (jv == null)
//            {
//                var jvCreate = new CreateLanguage { TwoLetterIsoCode = "jv", ThreeLetterIsoCode = "jav", ThreeLetterIsoBibliographicCode = "jav", };
//                _createLanguage.Handle(jvCreate);
//                jv = jvCreate.CreatedLanguage;
//            }

//            var ka = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ka" });
//            if (ka == null)
//            {
//                var kaCreate = new CreateLanguage { TwoLetterIsoCode = "ka", ThreeLetterIsoCode = "kat", ThreeLetterIsoBibliographicCode = "geo", };
//                _createLanguage.Handle(kaCreate);
//                ka = kaCreate.CreatedLanguage;
//            }

//            var kg = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "kg" });
//            if (kg == null)
//            {
//                var kgCreate = new CreateLanguage { TwoLetterIsoCode = "kg", ThreeLetterIsoCode = "kon", ThreeLetterIsoBibliographicCode = "kon", };
//                _createLanguage.Handle(kgCreate);
//                kg = kgCreate.CreatedLanguage;
//            }

//            var ki = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ki" });
//            if (ki == null)
//            {
//                var kiCreate = new CreateLanguage { TwoLetterIsoCode = "ki", ThreeLetterIsoCode = "kik", ThreeLetterIsoBibliographicCode = "kik", };
//                _createLanguage.Handle(kiCreate);
//                ki = kiCreate.CreatedLanguage;
//            }

//            var kj = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "kj" });
//            if (kj == null)
//            {
//                var kjCreate = new CreateLanguage { TwoLetterIsoCode = "kj", ThreeLetterIsoCode = "kua", ThreeLetterIsoBibliographicCode = "kua", };
//                _createLanguage.Handle(kjCreate);
//                kj = kjCreate.CreatedLanguage;
//            }

//            var kk = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "kk" });
//            if (kk == null)
//            {
//                var kkCreate = new CreateLanguage { TwoLetterIsoCode = "kk", ThreeLetterIsoCode = "kaz", ThreeLetterIsoBibliographicCode = "kaz", };
//                _createLanguage.Handle(kkCreate);
//                kk = kkCreate.CreatedLanguage;
//            }

//            var kl = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "kl" });
//            if (kl == null)
//            {
//                var klCreate = new CreateLanguage { TwoLetterIsoCode = "kl", ThreeLetterIsoCode = "kal", ThreeLetterIsoBibliographicCode = "kal", };
//                _createLanguage.Handle(klCreate);
//                kl = klCreate.CreatedLanguage;
//            }

//            var km = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "km" });
//            if (km == null)
//            {
//                var kmCreate = new CreateLanguage { TwoLetterIsoCode = "km", ThreeLetterIsoCode = "khm", ThreeLetterIsoBibliographicCode = "khm", };
//                _createLanguage.Handle(kmCreate);
//                km = kmCreate.CreatedLanguage;
//            }

//            var kn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "kn" });
//            if (kn == null)
//            {
//                var knCreate = new CreateLanguage { TwoLetterIsoCode = "kn", ThreeLetterIsoCode = "kan", ThreeLetterIsoBibliographicCode = "kan", };
//                _createLanguage.Handle(knCreate);
//                kn = knCreate.CreatedLanguage;
//            }

//            var ko = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ko" });
//            if (ko == null)
//            {
//                var koCreate = new CreateLanguage { TwoLetterIsoCode = "ko", ThreeLetterIsoCode = "kor", ThreeLetterIsoBibliographicCode = "kor", };
//                _createLanguage.Handle(koCreate);
//                ko = koCreate.CreatedLanguage;
//            }

//            var ks = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ks" });
//            if (ks == null)
//            {
//                var ksCreate = new CreateLanguage { TwoLetterIsoCode = "ks", ThreeLetterIsoCode = "kas", ThreeLetterIsoBibliographicCode = "kas", };
//                _createLanguage.Handle(ksCreate);
//                ks = ksCreate.CreatedLanguage;
//            }

//            var ku = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ku" });
//            if (ku == null)
//            {
//                var kuCreate = new CreateLanguage { TwoLetterIsoCode = "ku", ThreeLetterIsoCode = "kur", ThreeLetterIsoBibliographicCode = "kur", };
//                _createLanguage.Handle(kuCreate);
//                ku = kuCreate.CreatedLanguage;
//            }

//            var kv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "kv" });
//            if (kv == null)
//            {
//                var kvCreate = new CreateLanguage { TwoLetterIsoCode = "kv", ThreeLetterIsoCode = "kom", ThreeLetterIsoBibliographicCode = "kom", };
//                _createLanguage.Handle(kvCreate);
//                kv = kvCreate.CreatedLanguage;
//            }

//            var kw = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "kw" });
//            if (kw == null)
//            {
//                var kwCreate = new CreateLanguage { TwoLetterIsoCode = "kw", ThreeLetterIsoCode = "cor", ThreeLetterIsoBibliographicCode = "cor", };
//                _createLanguage.Handle(kwCreate);
//                kw = kwCreate.CreatedLanguage;
//            }

//            var ky = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ky" });
//            if (ky == null)
//            {
//                var kyCreate = new CreateLanguage { TwoLetterIsoCode = "ky", ThreeLetterIsoCode = "kir", ThreeLetterIsoBibliographicCode = "kir", };
//                _createLanguage.Handle(kyCreate);
//                ky = kyCreate.CreatedLanguage;
//            }

//            var la = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "la" });
//            if (la == null)
//            {
//                var laCreate = new CreateLanguage { TwoLetterIsoCode = "la", ThreeLetterIsoCode = "lat", ThreeLetterIsoBibliographicCode = "lat", };
//                _createLanguage.Handle(laCreate);
//                la = laCreate.CreatedLanguage;
//            }

//            var lb = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "lb" });
//            if (lb == null)
//            {
//                var lbCreate = new CreateLanguage { TwoLetterIsoCode = "lb", ThreeLetterIsoCode = "ltz", ThreeLetterIsoBibliographicCode = "ltz", };
//                _createLanguage.Handle(lbCreate);
//                lb = lbCreate.CreatedLanguage;
//            }

//            var lg = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "lg" });
//            if (lg == null)
//            {
//                var lgCreate = new CreateLanguage { TwoLetterIsoCode = "lg", ThreeLetterIsoCode = "lug", ThreeLetterIsoBibliographicCode = "lug", };
//                _createLanguage.Handle(lgCreate);
//                lg = lgCreate.CreatedLanguage;
//            }

//            var li = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "li" });
//            if (li == null)
//            {
//                var liCreate = new CreateLanguage { TwoLetterIsoCode = "li", ThreeLetterIsoCode = "lim", ThreeLetterIsoBibliographicCode = "lim", };
//                _createLanguage.Handle(liCreate);
//                li = liCreate.CreatedLanguage;
//            }

//            var ln = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ln" });
//            if (ln == null)
//            {
//                var lnCreate = new CreateLanguage { TwoLetterIsoCode = "ln", ThreeLetterIsoCode = "lin", ThreeLetterIsoBibliographicCode = "lin", };
//                _createLanguage.Handle(lnCreate);
//                ln = lnCreate.CreatedLanguage;
//            }

//            var lo = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "lo" });
//            if (lo == null)
//            {
//                var loCreate = new CreateLanguage { TwoLetterIsoCode = "lo", ThreeLetterIsoCode = "lao", ThreeLetterIsoBibliographicCode = "lao", };
//                _createLanguage.Handle(loCreate);
//                lo = loCreate.CreatedLanguage;
//            }

//            var lt = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "lt" });
//            if (lt == null)
//            {
//                var ltCreate = new CreateLanguage { TwoLetterIsoCode = "lt", ThreeLetterIsoCode = "lit", ThreeLetterIsoBibliographicCode = "lit", };
//                _createLanguage.Handle(ltCreate);
//                lt = ltCreate.CreatedLanguage;
//            }

//            var lv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "lv" });
//            if (lv == null)
//            {
//                var lvCreate = new CreateLanguage { TwoLetterIsoCode = "lv", ThreeLetterIsoCode = "lav", ThreeLetterIsoBibliographicCode = "lav", };
//                _createLanguage.Handle(lvCreate);
//                lv = lvCreate.CreatedLanguage;
//            }

//            var mg = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "mg" });
//            if (mg == null)
//            {
//                var mgCreate = new CreateLanguage { TwoLetterIsoCode = "mg", ThreeLetterIsoCode = "mlg", ThreeLetterIsoBibliographicCode = "mlg", };
//                _createLanguage.Handle(mgCreate);
//                mg = mgCreate.CreatedLanguage;
//            }

//            var mh = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "mh" });
//            if (mh == null)
//            {
//                var mhCreate = new CreateLanguage { TwoLetterIsoCode = "mh", ThreeLetterIsoCode = "mah", ThreeLetterIsoBibliographicCode = "mah", };
//                _createLanguage.Handle(mhCreate);
//                mh = mhCreate.CreatedLanguage;
//            }

//            var mi = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "mi" });
//            if (mi == null)
//            {
//                var miCreate = new CreateLanguage { TwoLetterIsoCode = "mi", ThreeLetterIsoCode = "mri", ThreeLetterIsoBibliographicCode = "mao", };
//                _createLanguage.Handle(miCreate);
//                mi = miCreate.CreatedLanguage;
//            }

//            var mk = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "mk" });
//            if (mk == null)
//            {
//                var mkCreate = new CreateLanguage { TwoLetterIsoCode = "mk", ThreeLetterIsoCode = "mkd", ThreeLetterIsoBibliographicCode = "mac", };
//                _createLanguage.Handle(mkCreate);
//                mk = mkCreate.CreatedLanguage;
//            }

//            var ml = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ml" });
//            if (ml == null)
//            {
//                var mlCreate = new CreateLanguage { TwoLetterIsoCode = "ml", ThreeLetterIsoCode = "mal", ThreeLetterIsoBibliographicCode = "mal", };
//                _createLanguage.Handle(mlCreate);
//                ml = mlCreate.CreatedLanguage;
//            }

//            var mn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "mn" });
//            if (mn == null)
//            {
//                var mnCreate = new CreateLanguage { TwoLetterIsoCode = "mn", ThreeLetterIsoCode = "mon", ThreeLetterIsoBibliographicCode = "mon", };
//                _createLanguage.Handle(mnCreate);
//                mn = mnCreate.CreatedLanguage;
//            }

//            var mr = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "mr" });
//            if (mr == null)
//            {
//                var mrCreate = new CreateLanguage { TwoLetterIsoCode = "mr", ThreeLetterIsoCode = "mar", ThreeLetterIsoBibliographicCode = "mar", };
//                _createLanguage.Handle(mrCreate);
//                mr = mrCreate.CreatedLanguage;
//            }

//            var ms = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ms" });
//            if (ms == null)
//            {
//                var msCreate = new CreateLanguage { TwoLetterIsoCode = "ms", ThreeLetterIsoCode = "msa", ThreeLetterIsoBibliographicCode = "may", };
//                _createLanguage.Handle(msCreate);
//                ms = msCreate.CreatedLanguage;
//            }

//            var mt = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "mt" });
//            if (mt == null)
//            {
//                var mtCreate = new CreateLanguage { TwoLetterIsoCode = "mt", ThreeLetterIsoCode = "mlt", ThreeLetterIsoBibliographicCode = "mlt", };
//                _createLanguage.Handle(mtCreate);
//                mt = mtCreate.CreatedLanguage;
//            }

//            var my = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "my" });
//            if (my == null)
//            {
//                var myCreate = new CreateLanguage { TwoLetterIsoCode = "my", ThreeLetterIsoCode = "mya", ThreeLetterIsoBibliographicCode = "bur", };
//                _createLanguage.Handle(myCreate);
//                my = myCreate.CreatedLanguage;
//            }

//            var na = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "na" });
//            if (na == null)
//            {
//                var naCreate = new CreateLanguage { TwoLetterIsoCode = "na", ThreeLetterIsoCode = "nau", ThreeLetterIsoBibliographicCode = "nau", };
//                _createLanguage.Handle(naCreate);
//                na = naCreate.CreatedLanguage;
//            }

//            var nb = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "nb" });
//            if (nb == null)
//            {
//                var nbCreate = new CreateLanguage { TwoLetterIsoCode = "nb", ThreeLetterIsoCode = "nob", ThreeLetterIsoBibliographicCode = "nob", };
//                _createLanguage.Handle(nbCreate);
//                nb = nbCreate.CreatedLanguage;
//            }

//            var ne = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ne" });
//            if (ne == null)
//            {
//                var neCreate = new CreateLanguage { TwoLetterIsoCode = "ne", ThreeLetterIsoCode = "nep", ThreeLetterIsoBibliographicCode = "nep", };
//                _createLanguage.Handle(neCreate);
//                ne = neCreate.CreatedLanguage;
//            }

//            var ng = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ng" });
//            if (ng == null)
//            {
//                var ngCreate = new CreateLanguage { TwoLetterIsoCode = "ng", ThreeLetterIsoCode = "ndo", ThreeLetterIsoBibliographicCode = "ndo", };
//                _createLanguage.Handle(ngCreate);
//                ng = ngCreate.CreatedLanguage;
//            }

//            var nl = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "nl" });
//            if (nl == null)
//            {
//                var nlCreate = new CreateLanguage { TwoLetterIsoCode = "nl", ThreeLetterIsoCode = "nld", ThreeLetterIsoBibliographicCode = "dut", };
//                _createLanguage.Handle(nlCreate);
//                nl = nlCreate.CreatedLanguage;
//            }

//            var nn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "nn" });
//            if (nn == null)
//            {
//                var nnCreate = new CreateLanguage { TwoLetterIsoCode = "nn", ThreeLetterIsoCode = "nno", ThreeLetterIsoBibliographicCode = "nno", };
//                _createLanguage.Handle(nnCreate);
//                nn = nnCreate.CreatedLanguage;
//            }

//            var no = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "no" });
//            if (no == null)
//            {
//                var noCreate = new CreateLanguage { TwoLetterIsoCode = "no", ThreeLetterIsoCode = "nor", ThreeLetterIsoBibliographicCode = "nor", };
//                _createLanguage.Handle(noCreate);
//                no = noCreate.CreatedLanguage;
//            }

//            var nv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "nv" });
//            if (nv == null)
//            {
//                var nvCreate = new CreateLanguage { TwoLetterIsoCode = "nv", ThreeLetterIsoCode = "nav", ThreeLetterIsoBibliographicCode = "nav", };
//                _createLanguage.Handle(nvCreate);
//                nv = nvCreate.CreatedLanguage;
//            }

//            var ny = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ny" });
//            if (ny == null)
//            {
//                var nyCreate = new CreateLanguage { TwoLetterIsoCode = "ny", ThreeLetterIsoCode = "nya", ThreeLetterIsoBibliographicCode = "nya", };
//                _createLanguage.Handle(nyCreate);
//                ny = nyCreate.CreatedLanguage;
//            }

//            var oc = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "oc" });
//            if (oc == null)
//            {
//                var ocCreate = new CreateLanguage { TwoLetterIsoCode = "oc", ThreeLetterIsoCode = "oci", ThreeLetterIsoBibliographicCode = "oci", };
//                _createLanguage.Handle(ocCreate);
//                oc = ocCreate.CreatedLanguage;
//            }

//            var om = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "om" });
//            if (om == null)
//            {
//                var omCreate = new CreateLanguage { TwoLetterIsoCode = "om", ThreeLetterIsoCode = "orm", ThreeLetterIsoBibliographicCode = "orm", };
//                _createLanguage.Handle(omCreate);
//                om = omCreate.CreatedLanguage;
//            }

//            var or = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "or" });
//            if (or == null)
//            {
//                var orCreate = new CreateLanguage { TwoLetterIsoCode = "or", ThreeLetterIsoCode = "ori", ThreeLetterIsoBibliographicCode = "ori", };
//                _createLanguage.Handle(orCreate);
//                or = orCreate.CreatedLanguage;
//            }

//            var os = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "os" });
//            if (os == null)
//            {
//                var osCreate = new CreateLanguage { TwoLetterIsoCode = "os", ThreeLetterIsoCode = "oss", ThreeLetterIsoBibliographicCode = "oss", };
//                _createLanguage.Handle(osCreate);
//                os = osCreate.CreatedLanguage;
//            }

//            var pa = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "pa" });
//            if (pa == null)
//            {
//                var paCreate = new CreateLanguage { TwoLetterIsoCode = "pa", ThreeLetterIsoCode = "pan", ThreeLetterIsoBibliographicCode = "pan", };
//                _createLanguage.Handle(paCreate);
//                pa = paCreate.CreatedLanguage;
//            }

//            var pi = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "pi" });
//            if (pi == null)
//            {
//                var piCreate = new CreateLanguage { TwoLetterIsoCode = "pi", ThreeLetterIsoCode = "pli", ThreeLetterIsoBibliographicCode = "pli", };
//                _createLanguage.Handle(piCreate);
//                pi = piCreate.CreatedLanguage;
//            }

//            var pl = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "pl" });
//            if (pl == null)
//            {
//                var plCreate = new CreateLanguage { TwoLetterIsoCode = "pl", ThreeLetterIsoCode = "pol", ThreeLetterIsoBibliographicCode = "pol", };
//                _createLanguage.Handle(plCreate);
//                pl = plCreate.CreatedLanguage;
//            }

//            var ps = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ps" });
//            if (ps == null)
//            {
//                var psCreate = new CreateLanguage { TwoLetterIsoCode = "ps", ThreeLetterIsoCode = "pus", ThreeLetterIsoBibliographicCode = "pus", };
//                _createLanguage.Handle(psCreate);
//                ps = psCreate.CreatedLanguage;
//            }

//            var pt = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "pt" });
//            if (pt == null)
//            {
//                var ptCreate = new CreateLanguage { TwoLetterIsoCode = "pt", ThreeLetterIsoCode = "por", ThreeLetterIsoBibliographicCode = "por", };
//                _createLanguage.Handle(ptCreate);
//                pt = ptCreate.CreatedLanguage;
//            }

//            var qu = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "qu" });
//            if (qu == null)
//            {
//                var quCreate = new CreateLanguage { TwoLetterIsoCode = "qu", ThreeLetterIsoCode = "que", ThreeLetterIsoBibliographicCode = "que", };
//                _createLanguage.Handle(quCreate);
//                qu = quCreate.CreatedLanguage;
//            }

//            var rm = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "rm" });
//            if (rm == null)
//            {
//                var rmCreate = new CreateLanguage { TwoLetterIsoCode = "rm", ThreeLetterIsoCode = "roh", ThreeLetterIsoBibliographicCode = "roh", };
//                _createLanguage.Handle(rmCreate);
//                rm = rmCreate.CreatedLanguage;
//            }

//            var rn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "rn" });
//            if (rn == null)
//            {
//                var rnCreate = new CreateLanguage { TwoLetterIsoCode = "rn", ThreeLetterIsoCode = "run", ThreeLetterIsoBibliographicCode = "run", };
//                _createLanguage.Handle(rnCreate);
//                rn = rnCreate.CreatedLanguage;
//            }

//            var ro = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ro" });
//            if (ro == null)
//            {
//                var roCreate = new CreateLanguage { TwoLetterIsoCode = "ro", ThreeLetterIsoCode = "ron", ThreeLetterIsoBibliographicCode = "rum", };
//                _createLanguage.Handle(roCreate);
//                ro = roCreate.CreatedLanguage;
//            }

//            var ru = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ru" });
//            if (ru == null)
//            {
//                var ruCreate = new CreateLanguage { TwoLetterIsoCode = "ru", ThreeLetterIsoCode = "rus", ThreeLetterIsoBibliographicCode = "rus", };
//                _createLanguage.Handle(ruCreate);
//                ru = ruCreate.CreatedLanguage;
//            }

//            var rw = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "rw" });
//            if (rw == null)
//            {
//                var rwCreate = new CreateLanguage { TwoLetterIsoCode = "rw", ThreeLetterIsoCode = "kin", ThreeLetterIsoBibliographicCode = "kin", };
//                _createLanguage.Handle(rwCreate);
//                rw = rwCreate.CreatedLanguage;
//            }

//            var sa = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sa" });
//            if (sa == null)
//            {
//                var saCreate = new CreateLanguage { TwoLetterIsoCode = "sa", ThreeLetterIsoCode = "san", ThreeLetterIsoBibliographicCode = "san", };
//                _createLanguage.Handle(saCreate);
//                sa = saCreate.CreatedLanguage;
//            }

//            var sc = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sc" });
//            if (sc == null)
//            {
//                var scCreate = new CreateLanguage { TwoLetterIsoCode = "sc", ThreeLetterIsoCode = "srd", ThreeLetterIsoBibliographicCode = "srd", };
//                _createLanguage.Handle(scCreate);
//                sc = scCreate.CreatedLanguage;
//            }

//            var sd = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sd" });
//            if (sd == null)
//            {
//                var sdCreate = new CreateLanguage { TwoLetterIsoCode = "sd", ThreeLetterIsoCode = "snd", ThreeLetterIsoBibliographicCode = "snd", };
//                _createLanguage.Handle(sdCreate);
//                sd = sdCreate.CreatedLanguage;
//            }

//            var se = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "se" });
//            if (se == null)
//            {
//                var seCreate = new CreateLanguage { TwoLetterIsoCode = "se", ThreeLetterIsoCode = "sme", ThreeLetterIsoBibliographicCode = "sme", };
//                _createLanguage.Handle(seCreate);
//                se = seCreate.CreatedLanguage;
//            }

//            var sg = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sg" });
//            if (sg == null)
//            {
//                var sgCreate = new CreateLanguage { TwoLetterIsoCode = "sg", ThreeLetterIsoCode = "sag", ThreeLetterIsoBibliographicCode = "sag", };
//                _createLanguage.Handle(sgCreate);
//                sg = sgCreate.CreatedLanguage;
//            }

//            var sh = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sh" });
//            if (sh == null)
//            {
//                var shCreate = new CreateLanguage { TwoLetterIsoCode = "sh", ThreeLetterIsoCode = "hbs", ThreeLetterIsoBibliographicCode = "hbs", };
//                _createLanguage.Handle(shCreate);
//                sh = shCreate.CreatedLanguage;
//            }

//            var si = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "si" });
//            if (si == null)
//            {
//                var siCreate = new CreateLanguage { TwoLetterIsoCode = "si", ThreeLetterIsoCode = "sin", ThreeLetterIsoBibliographicCode = "sin", };
//                _createLanguage.Handle(siCreate);
//                si = siCreate.CreatedLanguage;
//            }

//            var sk = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sk" });
//            if (sk == null)
//            {
//                var skCreate = new CreateLanguage { TwoLetterIsoCode = "sk", ThreeLetterIsoCode = "slk", ThreeLetterIsoBibliographicCode = "slo", };
//                _createLanguage.Handle(skCreate);
//                sk = skCreate.CreatedLanguage;
//            }

//            var sl = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sl" });
//            if (sl == null)
//            {
//                var slCreate = new CreateLanguage { TwoLetterIsoCode = "sl", ThreeLetterIsoCode = "slv", ThreeLetterIsoBibliographicCode = "slv", };
//                _createLanguage.Handle(slCreate);
//                sl = slCreate.CreatedLanguage;
//            }

//            var sm = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sm" });
//            if (sm == null)
//            {
//                var smCreate = new CreateLanguage { TwoLetterIsoCode = "sm", ThreeLetterIsoCode = "smo", ThreeLetterIsoBibliographicCode = "smo", };
//                _createLanguage.Handle(smCreate);
//                sm = smCreate.CreatedLanguage;
//            }

//            var sn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sn" });
//            if (sn == null)
//            {
//                var snCreate = new CreateLanguage { TwoLetterIsoCode = "sn", ThreeLetterIsoCode = "sna", ThreeLetterIsoBibliographicCode = "sna", };
//                _createLanguage.Handle(snCreate);
//                sn = snCreate.CreatedLanguage;
//            }

//            var so = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "so" });
//            if (so == null)
//            {
//                var soCreate = new CreateLanguage { TwoLetterIsoCode = "so", ThreeLetterIsoCode = "som", ThreeLetterIsoBibliographicCode = "som", };
//                _createLanguage.Handle(soCreate);
//                so = soCreate.CreatedLanguage;
//            }

//            var sq = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sq" });
//            if (sq == null)
//            {
//                var sqCreate = new CreateLanguage { TwoLetterIsoCode = "sq", ThreeLetterIsoCode = "sqi", ThreeLetterIsoBibliographicCode = "alb", };
//                _createLanguage.Handle(sqCreate);
//                sq = sqCreate.CreatedLanguage;
//            }

//            var sr = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sr" });
//            if (sr == null)
//            {
//                var srCreate = new CreateLanguage { TwoLetterIsoCode = "sr", ThreeLetterIsoCode = "srp", ThreeLetterIsoBibliographicCode = "srp", };
//                _createLanguage.Handle(srCreate);
//                sr = srCreate.CreatedLanguage;
//            }

//            var ss = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ss" });
//            if (ss == null)
//            {
//                var ssCreate = new CreateLanguage { TwoLetterIsoCode = "ss", ThreeLetterIsoCode = "ssw", ThreeLetterIsoBibliographicCode = "ssw", };
//                _createLanguage.Handle(ssCreate);
//                ss = ssCreate.CreatedLanguage;
//            }

//            var st = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "st" });
//            if (st == null)
//            {
//                var stCreate = new CreateLanguage { TwoLetterIsoCode = "st", ThreeLetterIsoCode = "sot", ThreeLetterIsoBibliographicCode = "sot", };
//                _createLanguage.Handle(stCreate);
//                st = stCreate.CreatedLanguage;
//            }

//            var su = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "su" });
//            if (su == null)
//            {
//                var suCreate = new CreateLanguage { TwoLetterIsoCode = "su", ThreeLetterIsoCode = "sun", ThreeLetterIsoBibliographicCode = "sun", };
//                _createLanguage.Handle(suCreate);
//                su = suCreate.CreatedLanguage;
//            }

//            var sv = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sv" });
//            if (sv == null)
//            {
//                var svCreate = new CreateLanguage { TwoLetterIsoCode = "sv", ThreeLetterIsoCode = "swe", ThreeLetterIsoBibliographicCode = "swe", };
//                _createLanguage.Handle(svCreate);
//                sv = svCreate.CreatedLanguage;
//            }

//            var sw = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "sw" });
//            if (sw == null)
//            {
//                var swCreate = new CreateLanguage { TwoLetterIsoCode = "sw", ThreeLetterIsoCode = "swa", ThreeLetterIsoBibliographicCode = "swa", };
//                _createLanguage.Handle(swCreate);
//                sw = swCreate.CreatedLanguage;
//            }

//            var ta = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ta" });
//            if (ta == null)
//            {
//                var taCreate = new CreateLanguage { TwoLetterIsoCode = "ta", ThreeLetterIsoCode = "tam", ThreeLetterIsoBibliographicCode = "tam", };
//                _createLanguage.Handle(taCreate);
//                ta = taCreate.CreatedLanguage;
//            }

//            var te = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "te" });
//            if (te == null)
//            {
//                var teCreate = new CreateLanguage { TwoLetterIsoCode = "te", ThreeLetterIsoCode = "tel", ThreeLetterIsoBibliographicCode = "tel", };
//                _createLanguage.Handle(teCreate);
//                te = teCreate.CreatedLanguage;
//            }

//            var tg = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "tg" });
//            if (tg == null)
//            {
//                var tgCreate = new CreateLanguage { TwoLetterIsoCode = "tg", ThreeLetterIsoCode = "tgk", ThreeLetterIsoBibliographicCode = "tgk", };
//                _createLanguage.Handle(tgCreate);
//                tg = tgCreate.CreatedLanguage;
//            }

//            var th = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "th" });
//            if (th == null)
//            {
//                var thCreate = new CreateLanguage { TwoLetterIsoCode = "th", ThreeLetterIsoCode = "tha", ThreeLetterIsoBibliographicCode = "tha", };
//                _createLanguage.Handle(thCreate);
//                th = thCreate.CreatedLanguage;
//            }

//            var ti = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ti" });
//            if (ti == null)
//            {
//                var tiCreate = new CreateLanguage { TwoLetterIsoCode = "ti", ThreeLetterIsoCode = "tir", ThreeLetterIsoBibliographicCode = "tir", };
//                _createLanguage.Handle(tiCreate);
//                ti = tiCreate.CreatedLanguage;
//            }

//            var tk = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "tk" });
//            if (tk == null)
//            {
//                var tkCreate = new CreateLanguage { TwoLetterIsoCode = "tk", ThreeLetterIsoCode = "tuk", ThreeLetterIsoBibliographicCode = "tuk", };
//                _createLanguage.Handle(tkCreate);
//                tk = tkCreate.CreatedLanguage;
//            }

//            var tl = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "tl" });
//            if (tl == null)
//            {
//                var tlCreate = new CreateLanguage { TwoLetterIsoCode = "tl", ThreeLetterIsoCode = "tgl", ThreeLetterIsoBibliographicCode = "tgl", };
//                _createLanguage.Handle(tlCreate);
//                tl = tlCreate.CreatedLanguage;
//            }

//            var tn = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "tn" });
//            if (tn == null)
//            {
//                var tnCreate = new CreateLanguage { TwoLetterIsoCode = "tn", ThreeLetterIsoCode = "tsn", ThreeLetterIsoBibliographicCode = "tsn", };
//                _createLanguage.Handle(tnCreate);
//                tn = tnCreate.CreatedLanguage;
//            }

//            var to = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "to" });
//            if (to == null)
//            {
//                var toCreate = new CreateLanguage { TwoLetterIsoCode = "to", ThreeLetterIsoCode = "ton", ThreeLetterIsoBibliographicCode = "ton", };
//                _createLanguage.Handle(toCreate);
//                to = toCreate.CreatedLanguage;
//            }

//            var tr = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "tr" });
//            if (tr == null)
//            {
//                var trCreate = new CreateLanguage { TwoLetterIsoCode = "tr", ThreeLetterIsoCode = "tur", ThreeLetterIsoBibliographicCode = "tur", };
//                _createLanguage.Handle(trCreate);
//                tr = trCreate.CreatedLanguage;
//            }

//            var ts = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ts" });
//            if (ts == null)
//            {
//                var tsCreate = new CreateLanguage { TwoLetterIsoCode = "ts", ThreeLetterIsoCode = "tso", ThreeLetterIsoBibliographicCode = "tso", };
//                _createLanguage.Handle(tsCreate);
//                ts = tsCreate.CreatedLanguage;
//            }

//            var tt = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "tt" });
//            if (tt == null)
//            {
//                var ttCreate = new CreateLanguage { TwoLetterIsoCode = "tt", ThreeLetterIsoCode = "tat", ThreeLetterIsoBibliographicCode = "tat", };
//                _createLanguage.Handle(ttCreate);
//                tt = ttCreate.CreatedLanguage;
//            }

//            var tw = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "tw" });
//            if (tw == null)
//            {
//                var twCreate = new CreateLanguage { TwoLetterIsoCode = "tw", ThreeLetterIsoCode = "twi", ThreeLetterIsoBibliographicCode = "twi", };
//                _createLanguage.Handle(twCreate);
//                tw = twCreate.CreatedLanguage;
//            }

//            var ty = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ty" });
//            if (ty == null)
//            {
//                var tyCreate = new CreateLanguage { TwoLetterIsoCode = "ty", ThreeLetterIsoCode = "tah", ThreeLetterIsoBibliographicCode = "tah", };
//                _createLanguage.Handle(tyCreate);
//                ty = tyCreate.CreatedLanguage;
//            }

//            var ug = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ug" });
//            if (ug == null)
//            {
//                var ugCreate = new CreateLanguage { TwoLetterIsoCode = "ug", ThreeLetterIsoCode = "uig", ThreeLetterIsoBibliographicCode = "uig", };
//                _createLanguage.Handle(ugCreate);
//                ug = ugCreate.CreatedLanguage;
//            }

//            var uk = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "uk" });
//            if (uk == null)
//            {
//                var ukCreate = new CreateLanguage { TwoLetterIsoCode = "uk", ThreeLetterIsoCode = "ukr", ThreeLetterIsoBibliographicCode = "ukr", };
//                _createLanguage.Handle(ukCreate);
//                uk = ukCreate.CreatedLanguage;
//            }

//            var ur = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ur" });
//            if (ur == null)
//            {
//                var urCreate = new CreateLanguage { TwoLetterIsoCode = "ur", ThreeLetterIsoCode = "urd", ThreeLetterIsoBibliographicCode = "urd", };
//                _createLanguage.Handle(urCreate);
//                ur = urCreate.CreatedLanguage;
//            }

//            var uz = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "uz" });
//            if (uz == null)
//            {
//                var uzCreate = new CreateLanguage { TwoLetterIsoCode = "uz", ThreeLetterIsoCode = "uzb", ThreeLetterIsoBibliographicCode = "uzb", };
//                _createLanguage.Handle(uzCreate);
//                uz = uzCreate.CreatedLanguage;
//            }

//            var ve = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "ve" });
//            if (ve == null)
//            {
//                var veCreate = new CreateLanguage { TwoLetterIsoCode = "ve", ThreeLetterIsoCode = "ven", ThreeLetterIsoBibliographicCode = "ven", };
//                _createLanguage.Handle(veCreate);
//                ve = veCreate.CreatedLanguage;
//            }

//            var vi = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "vi" });
//            if (vi == null)
//            {
//                var viCreate = new CreateLanguage { TwoLetterIsoCode = "vi", ThreeLetterIsoCode = "vie", ThreeLetterIsoBibliographicCode = "vie", };
//                _createLanguage.Handle(viCreate);
//                vi = viCreate.CreatedLanguage;
//            }

//            var vo = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "vo" });
//            if (vo == null)
//            {
//                var voCreate = new CreateLanguage { TwoLetterIsoCode = "vo", ThreeLetterIsoCode = "vol", ThreeLetterIsoBibliographicCode = "vol", };
//                _createLanguage.Handle(voCreate);
//                vo = voCreate.CreatedLanguage;
//            }

//            var wa = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "wa" });
//            if (wa == null)
//            {
//                var waCreate = new CreateLanguage { TwoLetterIsoCode = "wa", ThreeLetterIsoCode = "wln", ThreeLetterIsoBibliographicCode = "wln", };
//                _createLanguage.Handle(waCreate);
//                wa = waCreate.CreatedLanguage;
//            }

//            var wo = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "wo" });
//            if (wo == null)
//            {
//                var woCreate = new CreateLanguage { TwoLetterIsoCode = "wo", ThreeLetterIsoCode = "wol", ThreeLetterIsoBibliographicCode = "wol", };
//                _createLanguage.Handle(woCreate);
//                wo = woCreate.CreatedLanguage;
//            }

//            var xh = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "xh" });
//            if (xh == null)
//            {
//                var xhCreate = new CreateLanguage { TwoLetterIsoCode = "xh", ThreeLetterIsoCode = "xho", ThreeLetterIsoBibliographicCode = "xho", };
//                _createLanguage.Handle(xhCreate);
//                xh = xhCreate.CreatedLanguage;
//            }

//            var yi = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "yi" });
//            if (yi == null)
//            {
//                var yiCreate = new CreateLanguage { TwoLetterIsoCode = "yi", ThreeLetterIsoCode = "yid", ThreeLetterIsoBibliographicCode = "yid", };
//                _createLanguage.Handle(yiCreate);
//                yi = yiCreate.CreatedLanguage;
//            }

//            var yo = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "yo" });
//            if (yo == null)
//            {
//                var yoCreate = new CreateLanguage { TwoLetterIsoCode = "yo", ThreeLetterIsoCode = "yor", ThreeLetterIsoBibliographicCode = "yor", };
//                _createLanguage.Handle(yoCreate);
//                yo = yoCreate.CreatedLanguage;
//            }

//            var za = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "za" });
//            if (za == null)
//            {
//                var zaCreate = new CreateLanguage { TwoLetterIsoCode = "za", ThreeLetterIsoCode = "zha", ThreeLetterIsoBibliographicCode = "zha", };
//                _createLanguage.Handle(zaCreate);
//                za = zaCreate.CreatedLanguage;
//            }

//            var zh = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "zh" });
//            if (zh == null)
//            {
//                var zhCreate = new CreateLanguage { TwoLetterIsoCode = "zh", ThreeLetterIsoCode = "zho", ThreeLetterIsoBibliographicCode = "chi", };
//                _createLanguage.Handle(zhCreate);
//                zh = zhCreate.CreatedLanguage;
//            }

//            var zu = _queryProcessor.Execute(new LanguageByIsoCode { IsoCode = "zu" });
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
//                    new CreateLanguageName { Text = "English", TranslationToLanguageId = en.RevisionId, },          // #1  Language = "English"
//                    new CreateLanguageName { Text = "Inglés", TranslationToLanguageId = es.RevisionId, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Englisch", TranslationToLanguageId = de.RevisionId, },         // #3  Language = "German"
//                    new CreateLanguageName { Text = "الإنجليزية", TranslationToLanguageId = ar.RevisionId, },       // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Engels", TranslationToLanguageId = af.RevisionId, },            // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İngilis dili", TranslationToLanguageId = az.RevisionId, },      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "англійская", TranslationToLanguageId = be.RevisionId, },        // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "английски език", TranslationToLanguageId = bg.RevisionId, },    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইংরেজি", TranslationToLanguageId = bn.RevisionId, },             // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "anglès", TranslationToLanguageId = ca.RevisionId, },            // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "angličtina", TranslationToLanguageId = cs.RevisionId, },        // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Saesneg", TranslationToLanguageId = cy.RevisionId, },           // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "engelsk", TranslationToLanguageId = da.RevisionId, },           // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "inglise", TranslationToLanguageId = et.RevisionId, },           // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Ingelesa", TranslationToLanguageId = eu.RevisionId, },          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "انگلیسی", TranslationToLanguageId = fa.RevisionId, },          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "englanti", TranslationToLanguageId = fi.RevisionId, },          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "anglaise", TranslationToLanguageId = fr.RevisionId, },          // #18 Language = "French"
//                    new CreateLanguageName { Text = "Béarla", TranslationToLanguageId = ga.RevisionId, },            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Inglés", TranslationToLanguageId = gl.RevisionId, },            // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઇંગલિશ", TranslationToLanguageId = gu.RevisionId, },            // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אנגלית", TranslationToLanguageId = he.RevisionId, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अंग्रेज़ी", TranslationToLanguageId = hi.RevisionId, },             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "engleski", TranslationToLanguageId = hr.RevisionId, },          // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "angle", TranslationToLanguageId = ht.RevisionId, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "angol", TranslationToLanguageId = hu.RevisionId, },             // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "անգլերեն", TranslationToLanguageId = hy.RevisionId, },          // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bahasa Inggris", TranslationToLanguageId = id.RevisionId, },    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Enska", TranslationToLanguageId = isLanguage.RevisionId, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "inglese", TranslationToLanguageId = it.RevisionId, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "英語を", TranslationToLanguageId = ja.RevisionId, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ინგლისური", TranslationToLanguageId = ka.RevisionId, },       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಇಂಗ್ಲೀಷ್", TranslationToLanguageId = kn.RevisionId, },            // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "영어", TranslationToLanguageId = ko.RevisionId, },              // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "English", TranslationToLanguageId = la.RevisionId, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "anglų", TranslationToLanguageId = lt.RevisionId, },             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "angļu", TranslationToLanguageId = lv.RevisionId, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "англиски јазик", TranslationToLanguageId = mk.RevisionId, },    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Inggeris", TranslationToLanguageId = ms.RevisionId, },   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ingliż", TranslationToLanguageId = mt.RevisionId, },            // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Engels", TranslationToLanguageId = nl.RevisionId, },            // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "engelsk", TranslationToLanguageId = no.RevisionId, },           // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "angielski", TranslationToLanguageId = pl.RevisionId, },         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "Inglês", TranslationToLanguageId = pt.RevisionId, },            // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "englez", TranslationToLanguageId = ro.RevisionId, },            // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "английский", TranslationToLanguageId = ru.RevisionId, },        // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "angličtina", TranslationToLanguageId = sk.RevisionId, },        // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "angleški", TranslationToLanguageId = sl.RevisionId, },          // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "anglisht", TranslationToLanguageId = sq.RevisionId, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "енглески", TranslationToLanguageId = sr.RevisionId, },          // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "engelska", TranslationToLanguageId = sv.RevisionId, },          // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiingereza", TranslationToLanguageId = sw.RevisionId, },        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஆங்கிலம்", TranslationToLanguageId = ta.RevisionId, },        // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఆంగ్ల", TranslationToLanguageId = te.RevisionId, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาอังกฤษ", TranslationToLanguageId = th.RevisionId, },          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İngilizce", TranslationToLanguageId = tr.RevisionId, },         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Англійська", TranslationToLanguageId = uk.RevisionId, },        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "انگریزی", TranslationToLanguageId = ur.RevisionId, },          // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Anh", TranslationToLanguageId = vi.RevisionId, },         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ענגליש", TranslationToLanguageId = yi.RevisionId, },           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "英语", TranslationToLanguageId = zh.RevisionId, },              // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = en.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!es.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = en.RevisionId, },           // #1  Language = "English"
//                    new CreateLanguageName { Text = "Español", TranslationToLanguageId = es.RevisionId, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Spanisch", TranslationToLanguageId = de.RevisionId, },          // #3  Language = "German"
//                    new CreateLanguageName { Text = "الاسبانية", TranslationToLanguageId = ar.RevisionId, },         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = af.RevisionId, },            // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "ispan", TranslationToLanguageId = az.RevisionId, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Іспанская", TranslationToLanguageId = be.RevisionId, },          // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "испански", TranslationToLanguageId = bg.RevisionId, },           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্পেনসম্পর্কিত", TranslationToLanguageId = bn.RevisionId, },         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "espanyol", TranslationToLanguageId = ca.RevisionId, },           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "španělština", TranslationToLanguageId = cs.RevisionId, },        // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Sbaeneg", TranslationToLanguageId = cy.RevisionId, },            // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "spansk", TranslationToLanguageId = da.RevisionId, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hispaania", TranslationToLanguageId = et.RevisionId, },          // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Espainiako", TranslationToLanguageId = eu.RevisionId, },         // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اسپانیایی", TranslationToLanguageId = fa.RevisionId, },         // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "espanjalainen", TranslationToLanguageId = fi.RevisionId, },      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "espagnole", TranslationToLanguageId = fr.RevisionId, },          // #18 Language = "French"
//                    new CreateLanguageName { Text = "Spáinnis", TranslationToLanguageId = ga.RevisionId, },           // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "castelán", TranslationToLanguageId = gl.RevisionId, },           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્પેનિશ", TranslationToLanguageId = gu.RevisionId, },              // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ספרדית", TranslationToLanguageId = he.RevisionId, },            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्पेनिश", TranslationToLanguageId = hi.RevisionId, },              // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Španjolski", TranslationToLanguageId = hr.RevisionId, },         // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Panyòl", TranslationToLanguageId = ht.RevisionId, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "spanyol", TranslationToLanguageId = hu.RevisionId, },            // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իսպաներեն", TranslationToLanguageId = hy.RevisionId, },         // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Spanyol", TranslationToLanguageId = id.RevisionId, },            // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Spænska", TranslationToLanguageId = isLanguage.RevisionId, },    // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "spagnolo", TranslationToLanguageId = it.RevisionId, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スペイン", TranslationToLanguageId = ja.RevisionId, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ესპანური", TranslationToLanguageId = ka.RevisionId, },           // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ಪ್ಯಾನಿಷ್", TranslationToLanguageId = kn.RevisionId, },             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "스페인의", TranslationToLanguageId = ko.RevisionId, },            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = la.RevisionId, },            // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Ispanijos", TranslationToLanguageId = lt.RevisionId, },          // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "spāņu", TranslationToLanguageId = lv.RevisionId, },              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "шпански", TranslationToLanguageId = mk.RevisionId, },            // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Sepanyol", TranslationToLanguageId = ms.RevisionId, },    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Spanjol", TranslationToLanguageId = mt.RevisionId, },            // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Spaans", TranslationToLanguageId = nl.RevisionId, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = no.RevisionId, },            // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "hiszpański", TranslationToLanguageId = pl.RevisionId, },         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "hiszpański", TranslationToLanguageId = pt.RevisionId, },         // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "spaniol", TranslationToLanguageId = ro.RevisionId, },            // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "испанский", TranslationToLanguageId = ru.RevisionId, },          // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "španielčina", TranslationToLanguageId = sk.RevisionId, },        // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "španski", TranslationToLanguageId = sl.RevisionId, },            // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "spanjisht", TranslationToLanguageId = sq.RevisionId, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "шпански", TranslationToLanguageId = sr.RevisionId, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "spanska", TranslationToLanguageId = sv.RevisionId, },            // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kihispania", TranslationToLanguageId = sw.RevisionId, },         // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்பானிஷ்", TranslationToLanguageId = ta.RevisionId, },        // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్పానిష్", TranslationToLanguageId = te.RevisionId, },              // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาสเปน", TranslationToLanguageId = th.RevisionId, },            // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İspanyolca", TranslationToLanguageId = tr.RevisionId, },         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Іспанська", TranslationToLanguageId = uk.RevisionId, },          // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہسپانوی", TranslationToLanguageId = ur.RevisionId, },           // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tây Ban Nha", TranslationToLanguageId = vi.RevisionId, },        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "Spanish", TranslationToLanguageId = yi.RevisionId, },            // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "西班牙", TranslationToLanguageId = zh.RevisionId, },             // #61 Language = "Chinese"
//                 }
//                .ForEach(n =>
//                {
//                    n.LanguageId = es.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!de.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "German", TranslationToLanguageId = en.RevisionId, },                          // #1  Language = "English"
//                    new CreateLanguageName { Text = "Alemán", TranslationToLanguageId = es.RevisionId, },                          // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Deutsch", TranslationToLanguageId = de.RevisionId, },                         // #3  Language = "German"
//                    new CreateLanguageName { Text = "الألمانية", TranslationToLanguageId = ar.RevisionId, },                       // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Duits", TranslationToLanguageId = af.RevisionId, },                            // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "alman", TranslationToLanguageId = az.RevisionId, },                             // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Нямецкі", TranslationToLanguageId = be.RevisionId, },                          // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "немски", TranslationToLanguageId = bg.RevisionId, },                           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "জার্মান", TranslationToLanguageId = bn.RevisionId, },                             // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "alemany", TranslationToLanguageId = ca.RevisionId, },                          // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Němec", TranslationToLanguageId = cs.RevisionId, },                            // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Almaeneg", TranslationToLanguageId = cy.RevisionId, },                         // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "tysk", TranslationToLanguageId = da.RevisionId, },                             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "saksa", TranslationToLanguageId = et.RevisionId, },                            // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Alemana", TranslationToLanguageId = eu.RevisionId, },                          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آلمانی", TranslationToLanguageId = fa.RevisionId, },                          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "saksa", TranslationToLanguageId = fi.RevisionId, },                            // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "allemande", TranslationToLanguageId = fr.RevisionId, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Gearmáinis", TranslationToLanguageId = ga.RevisionId, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Alemán", TranslationToLanguageId = gl.RevisionId, },                           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "જર્મન ભાષા", TranslationToLanguageId = gu.RevisionId, },                        // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גרמנית", TranslationToLanguageId = he.RevisionId, },                          // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "जर्मन", TranslationToLanguageId = hi.RevisionId, },                             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "njemački", TranslationToLanguageId = hr.RevisionId, },                         // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Alman", TranslationToLanguageId = ht.RevisionId, },                            // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "német", TranslationToLanguageId = hu.RevisionId, },                            // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "գերմաներեն", TranslationToLanguageId = hy.RevisionId, },                      // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Jerman", TranslationToLanguageId = id.RevisionId, },                           // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Þýska", TranslationToLanguageId = isLanguage.RevisionId, },                    // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "tedesco", TranslationToLanguageId = it.RevisionId, },                          // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ドイツ", TranslationToLanguageId = ja.RevisionId, },                           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "გერმანული", TranslationToLanguageId = ka.RevisionId, },                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ವಂತ", TranslationToLanguageId = kn.RevisionId, },                             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "독일의", TranslationToLanguageId = ko.RevisionId, },                           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Germanica", TranslationToLanguageId = la.RevisionId, },                        // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Vokietijos", TranslationToLanguageId = lt.RevisionId, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "vācu", TranslationToLanguageId = lv.RevisionId, },                             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "германски", TranslationToLanguageId = mk.RevisionId, },                        // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Jerman", TranslationToLanguageId = ms.RevisionId, },                           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ġermaniż", TranslationToLanguageId = mt.RevisionId, },                         // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Duits", TranslationToLanguageId = nl.RevisionId, },                            // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "tyske", TranslationToLanguageId = no.RevisionId, },                            // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "niemiecki", TranslationToLanguageId = pl.RevisionId, },                        // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "alemão", TranslationToLanguageId = pt.RevisionId, },                           // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "german", TranslationToLanguageId = ro.RevisionId, },                           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "немецкий", TranslationToLanguageId = ru.RevisionId, },                         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Nemec", TranslationToLanguageId = sk.RevisionId, },                            // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "nemška", TranslationToLanguageId = sl.RevisionId, },                           // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "gjermanisht", TranslationToLanguageId = sq.RevisionId, },                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "немачки", TranslationToLanguageId = sr.RevisionId, },                          // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "tyska", TranslationToLanguageId = sv.RevisionId, },                            // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Ujerumani", TranslationToLanguageId = sw.RevisionId, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஜெர்மனி நாட்டை சார்ந்தவர்", TranslationToLanguageId = ta.RevisionId, },    // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "సన్నిహిత", TranslationToLanguageId = te.RevisionId, },                          // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาเยอรมัน", TranslationToLanguageId = th.RevisionId, },                         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Alman", TranslationToLanguageId = tr.RevisionId, },                            // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "німецький", TranslationToLanguageId = uk.RevisionId, },                        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "جرمن", TranslationToLanguageId = ur.RevisionId, },                            // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Đức", TranslationToLanguageId = vi.RevisionId, },                              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "דייַטש", TranslationToLanguageId = yi.RevisionId, },                           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "德国", TranslationToLanguageId = zh.RevisionId, },                             // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = de.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ar.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = en.RevisionId, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "Árabe", TranslationToLanguageId = es.RevisionId, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Arabisch", TranslationToLanguageId = de.RevisionId, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "العربية", TranslationToLanguageId = ar.RevisionId, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Arabies", TranslationToLanguageId = af.RevisionId, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Ərəb", TranslationToLanguageId = az.RevisionId, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Арабская", TranslationToLanguageId = be.RevisionId, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "арабски", TranslationToLanguageId = bg.RevisionId, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আরবি", TranslationToLanguageId = bn.RevisionId, },                  // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "àrab", TranslationToLanguageId = ca.RevisionId, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "arabština", TranslationToLanguageId = cs.RevisionId, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Arabeg", TranslationToLanguageId = cy.RevisionId, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "arabic", TranslationToLanguageId = da.RevisionId, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "araabia", TranslationToLanguageId = et.RevisionId, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = eu.RevisionId, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "عربی", TranslationToLanguageId = fa.RevisionId, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Arabia", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "arabe", TranslationToLanguageId = fr.RevisionId, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Araibis", TranslationToLanguageId = ga.RevisionId, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Árabe", TranslationToLanguageId = gl.RevisionId, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "અરબી ભાષા", TranslationToLanguageId = gu.RevisionId, },            // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ערבית", TranslationToLanguageId = he.RevisionId, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अरबी भाषा", TranslationToLanguageId = hi.RevisionId, },              // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "arapski", TranslationToLanguageId = hr.RevisionId, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "arab", TranslationToLanguageId = ht.RevisionId, },                  // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "arab", TranslationToLanguageId = hu.RevisionId, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "արաբական", TranslationToLanguageId = hy.RevisionId, },            // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Arab", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "arabíska", TranslationToLanguageId = isLanguage.RevisionId, },      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "arabo", TranslationToLanguageId = it.RevisionId, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アラビア", TranslationToLanguageId = ja.RevisionId, },               // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "არაბული", TranslationToLanguageId = ka.RevisionId, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅರಬ್ಬಿ ಭಾಷೆಯ", TranslationToLanguageId = kn.RevisionId, },           // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아랍어", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = la.RevisionId, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "arabų", TranslationToLanguageId = lt.RevisionId, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "arābu", TranslationToLanguageId = lv.RevisionId, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "арапски", TranslationToLanguageId = mk.RevisionId, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Arab", TranslationToLanguageId = ms.RevisionId, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Għarbi", TranslationToLanguageId = mt.RevisionId, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Arabisch", TranslationToLanguageId = nl.RevisionId, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "arabisk", TranslationToLanguageId = no.RevisionId, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "arabski", TranslationToLanguageId = pl.RevisionId, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "árabe", TranslationToLanguageId = pt.RevisionId, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba arabă", TranslationToLanguageId = ro.RevisionId, },           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "арабский", TranslationToLanguageId = ru.RevisionId, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "arabčina", TranslationToLanguageId = sk.RevisionId, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Arabic", TranslationToLanguageId = sl.RevisionId, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "arab", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "арапски", TranslationToLanguageId = sr.RevisionId, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Arabiska", TranslationToLanguageId = sv.RevisionId, },              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiarabu", TranslationToLanguageId = sw.RevisionId, },               // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "அரபு", TranslationToLanguageId = ta.RevisionId, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "అరబిక్", TranslationToLanguageId = te.RevisionId, },                 // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาอาหรับ", TranslationToLanguageId = th.RevisionId, },              // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Arapça", TranslationToLanguageId = tr.RevisionId, },                // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Арабська", TranslationToLanguageId = uk.RevisionId, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "عربی", TranslationToLanguageId = ur.RevisionId, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Ả Rập", TranslationToLanguageId = vi.RevisionId, },           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַראַביש", TranslationToLanguageId = yi.RevisionId, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "阿拉伯语", TranslationToLanguageId = zh.RevisionId, },               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ar.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (!af.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = en.RevisionId, },                      // #1  Language = "English"
//                    new CreateLanguageName { Text = "africaans", TranslationToLanguageId = es.RevisionId, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = de.RevisionId, },                       // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأفريكانية", TranslationToLanguageId = ar.RevisionId, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = af.RevisionId, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = az.RevisionId, },                       // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "афрыкаанс", TranslationToLanguageId = be.RevisionId, },                        // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "африканс", TranslationToLanguageId = bg.RevisionId, },                         // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আফ্রিকার অন্যতম সরকারি ভাষা", TranslationToLanguageId = bn.RevisionId, },         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Africaans", TranslationToLanguageId = ca.RevisionId, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "afrikánština", TranslationToLanguageId = cs.RevisionId, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Affricaneg", TranslationToLanguageId = cy.RevisionId, },                       // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = da.RevisionId, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "afrikaani", TranslationToLanguageId = et.RevisionId, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = eu.RevisionId, },                        // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آفریکانس", TranslationToLanguageId = fa.RevisionId, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = fi.RevisionId, },                        // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = fr.RevisionId, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = ga.RevisionId, },                        // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = gl.RevisionId, },                        // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આફ્રિકી", TranslationToLanguageId = gu.RevisionId, },                            // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אפריקאנס", TranslationToLanguageId = he.RevisionId, },                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "दक्षिण अफ्रीकी अथवा केप डच", TranslationToLanguageId = hi.RevisionId, },          // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = hr.RevisionId, },                        // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Kreyòl ayisyen", TranslationToLanguageId = ht.RevisionId, },                   // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = hu.RevisionId, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "աֆրիկանս", TranslationToLanguageId = hy.RevisionId, },                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Afrikanas", TranslationToLanguageId = id.RevisionId, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = isLanguage.RevisionId, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = it.RevisionId, },                        // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アフリカーンス", TranslationToLanguageId = ja.RevisionId, },                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "აფრიკაანსი", TranslationToLanguageId = ka.RevisionId, },                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಆಫ್ರಿಕಾನ್ಸ್", TranslationToLanguageId = kn.RevisionId, },                          // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아프리카 어", TranslationToLanguageId = ko.RevisionId, },                       // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Africanica", TranslationToLanguageId = la.RevisionId, },                       // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "afrikanų", TranslationToLanguageId = lt.RevisionId, },                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "afrikandu", TranslationToLanguageId = lv.RevisionId, },                        // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "африканс", TranslationToLanguageId = mk.RevisionId, },                         // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Afrikaan", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = mt.RevisionId, },                        // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = nl.RevisionId, },                        // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = no.RevisionId, },                        // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = pl.RevisionId, },                        // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = pt.RevisionId, },                        // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "afrikaans", TranslationToLanguageId = ro.RevisionId, },                        // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "африкаанс", TranslationToLanguageId = ru.RevisionId, },                        // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Afrikánčina", TranslationToLanguageId = sk.RevisionId, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = sl.RevisionId, },                        // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = sq.RevisionId, },                        // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "африканс", TranslationToLanguageId = sr.RevisionId, },                         // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = sv.RevisionId, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiafrikana", TranslationToLanguageId = sw.RevisionId, },                       // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஆஃப்ரிகான்ஸ்", TranslationToLanguageId = ta.RevisionId, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఆఫ్రికాన్స్", TranslationToLanguageId = te.RevisionId, },                           // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "แอฟริกา", TranslationToLanguageId = th.RevisionId, },                            // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Afrikaans", TranslationToLanguageId = tr.RevisionId, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Африкаанс", TranslationToLanguageId = uk.RevisionId, },                        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ايفريکانز", TranslationToLanguageId = ur.RevisionId, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "thứ tiếng", TranslationToLanguageId = vi.RevisionId, },                        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַפֿריקאַנס", TranslationToLanguageId = yi.RevisionId, },                        // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "南非语", TranslationToLanguageId = zh.RevisionId, },                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = af.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!az.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = en.RevisionId, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "Azerbaiyán", TranslationToLanguageId = es.RevisionId, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = de.RevisionId, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأذربيجانية", TranslationToLanguageId = ar.RevisionId, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Aserbeidjans", TranslationToLanguageId = af.RevisionId, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Azərbaycan", TranslationToLanguageId = az.RevisionId, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "азербайджанскі", TranslationToLanguageId = be.RevisionId, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "азербайджански", TranslationToLanguageId = bg.RevisionId, },              // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আজেরবাইজান", TranslationToLanguageId = bn.RevisionId, },                  // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Azerbaidjan", TranslationToLanguageId = ca.RevisionId, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "ázerbájdžánský", TranslationToLanguageId = cs.RevisionId, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = cy.RevisionId, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "aserbajdsjanske", TranslationToLanguageId = da.RevisionId, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "aserbaidžaani", TranslationToLanguageId = et.RevisionId, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = eu.RevisionId, },                 // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آذربایجان", TranslationToLanguageId = fa.RevisionId, },                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Azerbaidžanin", TranslationToLanguageId = fi.RevisionId, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "azerbaïdjanais", TranslationToLanguageId = fr.RevisionId, },              // #18 Language = "French"
//                    new CreateLanguageName { Text = "Asarbaiseáinis", TranslationToLanguageId = ga.RevisionId, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Azerbaiano", TranslationToLanguageId = gl.RevisionId, },                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "અઝરબૈજાની", TranslationToLanguageId = gu.RevisionId, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אזרביג'אן", TranslationToLanguageId = he.RevisionId, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आज़रबाइजानी", TranslationToLanguageId = hi.RevisionId, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Azarbejdžanac", TranslationToLanguageId = hr.RevisionId, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Azerbaydjan", TranslationToLanguageId = ht.RevisionId, },                 // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "azerbajdzsáni", TranslationToLanguageId = hu.RevisionId, },               // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Ադրբեջանի", TranslationToLanguageId = hy.RevisionId, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Azerbaijan", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Aserbaídsjan", TranslationToLanguageId = isLanguage.RevisionId, },        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "azero", TranslationToLanguageId = it.RevisionId, },                       // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アゼルバイジャン語", TranslationToLanguageId = ja.RevisionId, },           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "აზერბაიჯანის", TranslationToLanguageId = ka.RevisionId, },               // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅಜರ್ಬೈಜಾನಿ", TranslationToLanguageId = kn.RevisionId, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아제르바 이잔", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Azeriana", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Azerbaidžano", TranslationToLanguageId = lt.RevisionId, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Azerbaidžānas", TranslationToLanguageId = lv.RevisionId, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Азербејџан", TranslationToLanguageId = mk.RevisionId, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Azerbaijan", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ażerbajġan", TranslationToLanguageId = mt.RevisionId, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Azerbeidzjaanse", TranslationToLanguageId = nl.RevisionId, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "aserbajdsjanske", TranslationToLanguageId = no.RevisionId, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "azerbejdżański", TranslationToLanguageId = pl.RevisionId, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "azerbaijano", TranslationToLanguageId = pt.RevisionId, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Azerbaidjan", TranslationToLanguageId = ro.RevisionId, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "азербайджанский", TranslationToLanguageId = ru.RevisionId, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "azerbajdžanský", TranslationToLanguageId = sk.RevisionId, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "azerbajdžanski", TranslationToLanguageId = sl.RevisionId, },              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Azerbaijani", TranslationToLanguageId = sq.RevisionId, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "азербејџански", TranslationToLanguageId = sr.RevisionId, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "azerbajdzjanska", TranslationToLanguageId = sv.RevisionId, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiazabaijani", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "அஜர்பைஜானி", TranslationToLanguageId = ta.RevisionId, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బైజాన్", TranslationToLanguageId = te.RevisionId, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวอาเซร์ไบจัน", TranslationToLanguageId = th.RevisionId, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Azeri", TranslationToLanguageId = tr.RevisionId, },                       // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "азербайджанський", TranslationToLanguageId = uk.RevisionId, },            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آذربائیجان", TranslationToLanguageId = ur.RevisionId, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Azerbaijan", TranslationToLanguageId = vi.RevisionId, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַזערבייַדזאַניש", TranslationToLanguageId = yi.RevisionId, },             // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "阿塞拜疆", TranslationToLanguageId = zh.RevisionId, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = az.RevisionId;
//                    _createLanguageName.Handle(n);
//                });



//            if (!be.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = en.RevisionId, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "bielorruso", TranslationToLanguageId = es.RevisionId, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "belarussischen", TranslationToLanguageId = de.RevisionId, },            // #3  Language = "German"
//                    new CreateLanguageName { Text = "البيلاروسية", TranslationToLanguageId = ar.RevisionId, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Wit", TranslationToLanguageId = af.RevisionId, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "AzəBelarus", TranslationToLanguageId = az.RevisionId, },                // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Беларуская", TranslationToLanguageId = be.RevisionId, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "Беларус", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "বেলারুশীয়", TranslationToLanguageId = bn.RevisionId, },                  // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "bielorús", TranslationToLanguageId = ca.RevisionId, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "běloruské", TranslationToLanguageId = cs.RevisionId, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Belarwseg", TranslationToLanguageId = cy.RevisionId, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "belarussiske", TranslationToLanguageId = da.RevisionId, },              // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Valgevene", TranslationToLanguageId = et.RevisionId, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = eu.RevisionId, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "بلاروس", TranslationToLanguageId = fa.RevisionId, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Valko-Venäjän", TranslationToLanguageId = fi.RevisionId, },             // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "biélorusses", TranslationToLanguageId = fr.RevisionId, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Bealarúisis", TranslationToLanguageId = ga.RevisionId, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = gl.RevisionId, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "બેલારુસિયન", TranslationToLanguageId = gu.RevisionId, },                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "בלארוסית", TranslationToLanguageId = he.RevisionId, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बेलारूसी", TranslationToLanguageId = hi.RevisionId, },                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "bjeloruski", TranslationToLanguageId = hr.RevisionId, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Belarisyen", TranslationToLanguageId = ht.RevisionId, },                // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "fehérorosz", TranslationToLanguageId = hu.RevisionId, },                // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բելարուսերեն", TranslationToLanguageId = hy.RevisionId, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = id.RevisionId, },                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "hvítrússneska", TranslationToLanguageId = isLanguage.RevisionId, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "bielorusso", TranslationToLanguageId = it.RevisionId, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ベラルーシ語", TranslationToLanguageId = ja.RevisionId, },               // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბელორუსის", TranslationToLanguageId = ka.RevisionId, },               // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಬೆಲರೂಸಿಯನ್", TranslationToLanguageId = kn.RevisionId, },                // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "벨라루스어", TranslationToLanguageId = ko.RevisionId, },                 // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Belarusica", TranslationToLanguageId = la.RevisionId, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Baltarusijos", TranslationToLanguageId = lt.RevisionId, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Baltkrievijas", TranslationToLanguageId = lv.RevisionId, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "белоруски", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = ms.RevisionId, },                   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = mt.RevisionId, },                   // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Wit-Russische", TranslationToLanguageId = nl.RevisionId, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = no.RevisionId, },                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Białorusi", TranslationToLanguageId = pl.RevisionId, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = pt.RevisionId, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = ro.RevisionId, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "Белорусская", TranslationToLanguageId = ru.RevisionId, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "bieloruskej", TranslationToLanguageId = sk.RevisionId, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "beloruski", TranslationToLanguageId = sl.RevisionId, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Belarusian", TranslationToLanguageId = sq.RevisionId, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "беларусиан", TranslationToLanguageId = sr.RevisionId, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "vitryska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kibelarusi", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பெலாரஷ்யன்", TranslationToLanguageId = ta.RevisionId, },            // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బెలారుషియన్", TranslationToLanguageId = te.RevisionId, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เบลารุส", TranslationToLanguageId = th.RevisionId, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Belarusça", TranslationToLanguageId = tr.RevisionId, },                 // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Білоруська", TranslationToLanguageId = uk.RevisionId, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "بیلاروسی", TranslationToLanguageId = ur.RevisionId, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Belarus", TranslationToLanguageId = vi.RevisionId, },                   // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "בעלאָרוסיש", TranslationToLanguageId = yi.RevisionId, },                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "白俄罗斯", TranslationToLanguageId = zh.RevisionId, },                   // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = be.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!bg.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = en.RevisionId, },             // #1  Language = "English"
//                    new CreateLanguageName { Text = "búlgaro", TranslationToLanguageId = es.RevisionId, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Bulgarisch", TranslationToLanguageId = de.RevisionId, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "البلغارية", TranslationToLanguageId = ar.RevisionId, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Bulgaars", TranslationToLanguageId = af.RevisionId, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Bolqar", TranslationToLanguageId = az.RevisionId, },                 // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Балгарская", TranslationToLanguageId = be.RevisionId, },             // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "български", TranslationToLanguageId = bg.RevisionId, },              // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "বুলগেরীয়", TranslationToLanguageId = bn.RevisionId, },                // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Búlgar", TranslationToLanguageId = ca.RevisionId, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "bulharský", TranslationToLanguageId = cs.RevisionId, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Bwlgareg", TranslationToLanguageId = cy.RevisionId, },               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "bulgarian", TranslationToLanguageId = da.RevisionId, },              // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "bulgaaria", TranslationToLanguageId = et.RevisionId, },              // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = eu.RevisionId, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "بلغاری", TranslationToLanguageId = fa.RevisionId, },                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = fi.RevisionId, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "bulgares", TranslationToLanguageId = fr.RevisionId, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Bulgáiris", TranslationToLanguageId = ga.RevisionId, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Búlgaro", TranslationToLanguageId = gl.RevisionId, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "બલ્ગેરિયન", TranslationToLanguageId = gu.RevisionId, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "בולגרית", TranslationToLanguageId = he.RevisionId, },               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बल्गेरियाई", TranslationToLanguageId = hi.RevisionId, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "bugarski", TranslationToLanguageId = hr.RevisionId, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "bulgarian", TranslationToLanguageId = ht.RevisionId, },              // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "bolgár", TranslationToLanguageId = hu.RevisionId, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բուլղարացի", TranslationToLanguageId = hy.RevisionId, },             // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bulgaria", TranslationToLanguageId = id.RevisionId, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Búlgaríu", TranslationToLanguageId = isLanguage.RevisionId, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "bulgaro", TranslationToLanguageId = it.RevisionId, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ブルガリア語", TranslationToLanguageId = ja.RevisionId, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბულგარეთის", TranslationToLanguageId = ka.RevisionId, },           // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅಲ್ಲಿಯ ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "불가리아 사람", TranslationToLanguageId = ko.RevisionId, },           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Bulgarica", TranslationToLanguageId = la.RevisionId, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "bulgarų", TranslationToLanguageId = lt.RevisionId, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Bulgārijas", TranslationToLanguageId = lv.RevisionId, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Бугарија", TranslationToLanguageId = mk.RevisionId, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bulgaria", TranslationToLanguageId = ms.RevisionId, },               // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Bulgaru", TranslationToLanguageId = mt.RevisionId, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "bulgarian", TranslationToLanguageId = nl.RevisionId, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "bulgarske", TranslationToLanguageId = no.RevisionId, },              // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "bułgarski", TranslationToLanguageId = pl.RevisionId, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "búlgaro", TranslationToLanguageId = pt.RevisionId, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba bulgară", TranslationToLanguageId = ro.RevisionId, },          // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "болгарский", TranslationToLanguageId = ru.RevisionId, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "bulharský", TranslationToLanguageId = sk.RevisionId, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "bolgarski", TranslationToLanguageId = sl.RevisionId, },              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "bullgare", TranslationToLanguageId = sq.RevisionId, },               // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "бугарски", TranslationToLanguageId = sr.RevisionId, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "bulgariska", TranslationToLanguageId = sv.RevisionId, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Bulgarian", TranslationToLanguageId = sw.RevisionId, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பல்கேரியன்", TranslationToLanguageId = ta.RevisionId, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బల్గేరియన్", TranslationToLanguageId = te.RevisionId, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "บัลแกเรีย", TranslationToLanguageId = th.RevisionId, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Bulgar", TranslationToLanguageId = tr.RevisionId, },                 // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Болгарська", TranslationToLanguageId = uk.RevisionId, },             // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "بلغارین", TranslationToLanguageId = ur.RevisionId, },               // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Bun-ga-ri", TranslationToLanguageId = vi.RevisionId, },              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "בולגאַריש", TranslationToLanguageId = yi.RevisionId, },              // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "保加利亚语", TranslationToLanguageId = zh.RevisionId, },              // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bg.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (!bn.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = en.RevisionId, },            // #1  Language = "English"
//                    new CreateLanguageName { Text = "bengalí", TranslationToLanguageId = es.RevisionId, },             // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = de.RevisionId, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "بنغالي", TranslationToLanguageId = ar.RevisionId, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = af.RevisionId, },             // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Benqal", TranslationToLanguageId = az.RevisionId, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "бенгальскі", TranslationToLanguageId = be.RevisionId, },          // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "бенгалски", TranslationToLanguageId = bg.RevisionId, },           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "বাঙ্গালী", TranslationToLanguageId = bn.RevisionId, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "bengalí", TranslationToLanguageId = ca.RevisionId, },             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "bengálský", TranslationToLanguageId = cs.RevisionId, },           // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = cy.RevisionId, },             // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = da.RevisionId, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = et.RevisionId, },             // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = eu.RevisionId, },             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "بنگالی", TranslationToLanguageId = fa.RevisionId, },             // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = fi.RevisionId, },             // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = fr.RevisionId, },             // #18 Language = "French"
//                    new CreateLanguageName { Text = "Beangáilis", TranslationToLanguageId = ga.RevisionId, },          // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "bengalí", TranslationToLanguageId = gl.RevisionId, },             // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "બંગાળનું", TranslationToLanguageId = gu.RevisionId, },              // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "בנגלית", TranslationToLanguageId = he.RevisionId, },             // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बंगाली", TranslationToLanguageId = hi.RevisionId, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "bengalski", TranslationToLanguageId = hr.RevisionId, },           // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = ht.RevisionId, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "bengáli", TranslationToLanguageId = hu.RevisionId, },             // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բենգալերեն", TranslationToLanguageId = hy.RevisionId, },          // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = id.RevisionId, },             // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = isLanguage.RevisionId, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "bengalese", TranslationToLanguageId = it.RevisionId, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ベンガル語", TranslationToLanguageId = ja.RevisionId, },           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბენგალური", TranslationToLanguageId = ka.RevisionId, },          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಬಂಗಾಳಿ", TranslationToLanguageId = kn.RevisionId, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "벵골 사람", TranslationToLanguageId = ko.RevisionId, },            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Bengalica", TranslationToLanguageId = la.RevisionId, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "bengalų", TranslationToLanguageId = lt.RevisionId, },             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "bengāļu", TranslationToLanguageId = lv.RevisionId, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "бенгалски", TranslationToLanguageId = mk.RevisionId, },           // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = ms.RevisionId, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = mt.RevisionId, },             // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Bengalees", TranslationToLanguageId = nl.RevisionId, },           // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = no.RevisionId, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "bengalski", TranslationToLanguageId = pl.RevisionId, },           // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = pt.RevisionId, },             // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "bengali", TranslationToLanguageId = ro.RevisionId, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "бенгальский", TranslationToLanguageId = ru.RevisionId, },         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "bengálsky", TranslationToLanguageId = sk.RevisionId, },           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = sl.RevisionId, },             // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "bengalisht", TranslationToLanguageId = sq.RevisionId, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "бенгалски", TranslationToLanguageId = sr.RevisionId, },           // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = sv.RevisionId, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kibengali", TranslationToLanguageId = sw.RevisionId, },           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பெங்காலி", TranslationToLanguageId = ta.RevisionId, },          // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బెంగాలీ", TranslationToLanguageId = te.RevisionId, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เบงกาลี", TranslationToLanguageId = th.RevisionId, },               // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Bengal", TranslationToLanguageId = tr.RevisionId, },              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "бенгальська", TranslationToLanguageId = uk.RevisionId, },         // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "بنگالی", TranslationToLanguageId = ur.RevisionId, },             // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Bengali", TranslationToLanguageId = vi.RevisionId, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "בענגאַליש", TranslationToLanguageId = yi.RevisionId, },           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "孟加拉", TranslationToLanguageId = zh.RevisionId, },              // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (!ca.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Catalan", TranslationToLanguageId = en.RevisionId, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "catalán", TranslationToLanguageId = es.RevisionId, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Katalanisch", TranslationToLanguageId = de.RevisionId, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "الكاتالونية", TranslationToLanguageId = ar.RevisionId, },              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Katalaans", TranslationToLanguageId = af.RevisionId, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = az.RevisionId, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Каталонскі", TranslationToLanguageId = be.RevisionId, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "каталонски", TranslationToLanguageId = bg.RevisionId, },                 // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "কাটালান", TranslationToLanguageId = bn.RevisionId, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "català", TranslationToLanguageId = ca.RevisionId, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "katalánština", TranslationToLanguageId = cs.RevisionId, },               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Catalaneg", TranslationToLanguageId = cy.RevisionId, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "catalansk", TranslationToLanguageId = da.RevisionId, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Katalaani", TranslationToLanguageId = et.RevisionId, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Katalana", TranslationToLanguageId = eu.RevisionId, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کاتالان", TranslationToLanguageId = fa.RevisionId, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "katalaani", TranslationToLanguageId = fi.RevisionId, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "catalane", TranslationToLanguageId = fr.RevisionId, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Catalóinis", TranslationToLanguageId = ga.RevisionId, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Catalán", TranslationToLanguageId = gl.RevisionId, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કેટાલન", TranslationToLanguageId = gu.RevisionId, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קטלאנית", TranslationToLanguageId = he.RevisionId, },                   // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "कैटलन", TranslationToLanguageId = hi.RevisionId, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Catalan", TranslationToLanguageId = hr.RevisionId, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "katalán", TranslationToLanguageId = hu.RevisionId, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "կատալոներեն", TranslationToLanguageId = hy.RevisionId, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = id.RevisionId, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "katalónska", TranslationToLanguageId = isLanguage.RevisionId, },         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "catalano", TranslationToLanguageId = it.RevisionId, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "カタロニア語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "კატალანური", TranslationToLanguageId = ka.RevisionId, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕ್ಯಾಟಲಾನ್", TranslationToLanguageId = kn.RevisionId, },                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "카탈로니아의", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Catalana", TranslationToLanguageId = la.RevisionId, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "katalonų", TranslationToLanguageId = lt.RevisionId, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Katalāņu", TranslationToLanguageId = lv.RevisionId, },                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "каталонски", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Catalan", TranslationToLanguageId = ms.RevisionId, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = mt.RevisionId, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "catalan", TranslationToLanguageId = nl.RevisionId, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "katalansk", TranslationToLanguageId = no.RevisionId, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Kataloński", TranslationToLanguageId = pl.RevisionId, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "catalão", TranslationToLanguageId = pt.RevisionId, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "catalan", TranslationToLanguageId = ro.RevisionId, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "каталонский", TranslationToLanguageId = ru.RevisionId, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Katalánsky", TranslationToLanguageId = sk.RevisionId, },                 // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Katalonski", TranslationToLanguageId = sl.RevisionId, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Katalonje", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "каталонски", TranslationToLanguageId = sr.RevisionId, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "katalanska", TranslationToLanguageId = sv.RevisionId, },                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kikatalani", TranslationToLanguageId = sw.RevisionId, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கடாலன்", TranslationToLanguageId = ta.RevisionId, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "కాటలాన్", TranslationToLanguageId = te.RevisionId, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "คาตาลัน", TranslationToLanguageId = th.RevisionId, },                      // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Katalan", TranslationToLanguageId = tr.RevisionId, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Каталонський", TranslationToLanguageId = uk.RevisionId, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کاٹالانين", TranslationToLanguageId = ur.RevisionId, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Catalan", TranslationToLanguageId = vi.RevisionId, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קאַטאַלאַניש", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "加泰罗尼亚", TranslationToLanguageId = zh.RevisionId, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ca.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!cs.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = en.RevisionId, },                                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "checo", TranslationToLanguageId = es.RevisionId, },                                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Tschechisch", TranslationToLanguageId = de.RevisionId, },                              // #3  Language = "German"
//                    new CreateLanguageName { Text = "التشيكية", TranslationToLanguageId = ar.RevisionId, },                                // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Tsjeggies", TranslationToLanguageId = af.RevisionId, },                                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Çex", TranslationToLanguageId = az.RevisionId, },                                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Чэшскі", TranslationToLanguageId = be.RevisionId, },                                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "чешки", TranslationToLanguageId = bg.RevisionId, },                                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "চেকোশ্লোভাকিয়াবাসী স্লাভজাতির একটি শাখার লোক", TranslationToLanguageId = bn.RevisionId, },    // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "txec", TranslationToLanguageId = ca.RevisionId, },                                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "český", TranslationToLanguageId = cs.RevisionId, },                                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Tsiec", TranslationToLanguageId = cy.RevisionId, },                                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "tjekkisk", TranslationToLanguageId = da.RevisionId, },                                 // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "tšehhi", TranslationToLanguageId = et.RevisionId, },                                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Txekiar", TranslationToLanguageId = eu.RevisionId, },                                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "چک", TranslationToLanguageId = fa.RevisionId, },                                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Tšekin", TranslationToLanguageId = fi.RevisionId, },                                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "tchèque", TranslationToLanguageId = fr.RevisionId, },                                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "na Seice", TranslationToLanguageId = ga.RevisionId, },                                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Checo", TranslationToLanguageId = gl.RevisionId, },                                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ચેક", TranslationToLanguageId = gu.RevisionId, },                                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "צ", TranslationToLanguageId = he.RevisionId, },                                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "चेक", TranslationToLanguageId = hi.RevisionId, },                                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "češki", TranslationToLanguageId = hr.RevisionId, },                                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "czech", TranslationToLanguageId = ht.RevisionId, },                                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "cseh", TranslationToLanguageId = hu.RevisionId, },                                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "չեխ", TranslationToLanguageId = hy.RevisionId, },                                      // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Ceko", TranslationToLanguageId = id.RevisionId, },                                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Tékkland", TranslationToLanguageId = isLanguage.RevisionId, },                         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ceco", TranslationToLanguageId = it.RevisionId, },                                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "チェコ", TranslationToLanguageId = ja.RevisionId, },                                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჩეხეთის", TranslationToLanguageId = ka.RevisionId, },                                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಜೆಕ್", TranslationToLanguageId = kn.RevisionId, },                                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "체코어", TranslationToLanguageId = ko.RevisionId, },                                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Bohemica", TranslationToLanguageId = la.RevisionId, },                                 // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Čekijos", TranslationToLanguageId = lt.RevisionId, },                                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Čehijas", TranslationToLanguageId = lv.RevisionId, },                                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Чешка", TranslationToLanguageId = mk.RevisionId, },                                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = ms.RevisionId, },                                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ċeka", TranslationToLanguageId = mt.RevisionId, },                                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Tsjechisch", TranslationToLanguageId = nl.RevisionId, },                               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "tsjekkiske", TranslationToLanguageId = no.RevisionId, },                               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = pl.RevisionId, },                                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "tcheco", TranslationToLanguageId = pt.RevisionId, },                                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "ceh", TranslationToLanguageId = ro.RevisionId, },                                      // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "чешский", TranslationToLanguageId = ru.RevisionId, },                                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Český", TranslationToLanguageId = sk.RevisionId, },                                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Češki", TranslationToLanguageId = sl.RevisionId, },                                    // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "çek", TranslationToLanguageId = sq.RevisionId, },                                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "чешки", TranslationToLanguageId = sr.RevisionId, },                                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Tjeckien", TranslationToLanguageId = sv.RevisionId, },                                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Czech", TranslationToLanguageId = sw.RevisionId, },                                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "செக்", TranslationToLanguageId = ta.RevisionId, },                                     // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "చెక్", TranslationToLanguageId = te.RevisionId, },                                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "สาธารณรัฐเช็ก", TranslationToLanguageId = th.RevisionId, },                                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Çek", TranslationToLanguageId = tr.RevisionId, },                                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "чеський", TranslationToLanguageId = uk.RevisionId, },                                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "چیک", TranslationToLanguageId = ur.RevisionId, },                                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Séc", TranslationToLanguageId = vi.RevisionId, },                                      // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טשעכיש", TranslationToLanguageId = yi.RevisionId, },                                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "捷克", TranslationToLanguageId = zh.RevisionId, },                                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cs.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (!cy.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = en.RevisionId, },                                                // #1  Language = "English"
//                    new CreateLanguageName { Text = "galés", TranslationToLanguageId = es.RevisionId, },                                                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Walisisch", TranslationToLanguageId = de.RevisionId, },                                             // #3  Language = "German"
//                    new CreateLanguageName { Text = "تهرب من دفع الرهان", TranslationToLanguageId = ar.RevisionId, },                                  // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Walliese", TranslationToLanguageId = af.RevisionId, },                                              // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Uels", TranslationToLanguageId = az.RevisionId, },                                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Валійская", TranslationToLanguageId = be.RevisionId, },                                             // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "уелски", TranslationToLanguageId = bg.RevisionId, },                                                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ত্তয়েল্স্দেশসম্বন্ধীয়", TranslationToLanguageId = bn.RevisionId, },                                          // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Gal · lès", TranslationToLanguageId = ca.RevisionId, },                                             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "velšský", TranslationToLanguageId = cs.RevisionId, },                                               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Cymraeg", TranslationToLanguageId = cy.RevisionId, },                                               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "walisisk", TranslationToLanguageId = da.RevisionId, },                                              // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "võlga maksmata jätma", TranslationToLanguageId = et.RevisionId, },                                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Galesera", TranslationToLanguageId = eu.RevisionId, },                                              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زیر قول زدن", TranslationToLanguageId = fa.RevisionId, },                                          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Walesin", TranslationToLanguageId = fi.RevisionId, },                                               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Gallois", TranslationToLanguageId = fr.RevisionId, },                                               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Breatnais", TranslationToLanguageId = ga.RevisionId, },                                             // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Galés", TranslationToLanguageId = gl.RevisionId, },                                                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કરારભંગ કરવો", TranslationToLanguageId = gu.RevisionId, },                                           // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "וולשית", TranslationToLanguageId = he.RevisionId, },                                               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "वेल्श", TranslationToLanguageId = hi.RevisionId, },                                                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "velški", TranslationToLanguageId = hr.RevisionId, },                                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "welsh", TranslationToLanguageId = ht.RevisionId, },                                                 // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "walesi", TranslationToLanguageId = hu.RevisionId, },                                                // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ուելսացի", TranslationToLanguageId = hy.RevisionId, },                                              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = id.RevisionId, },                                                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "velska", TranslationToLanguageId = isLanguage.RevisionId, },                                        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "gallese", TranslationToLanguageId = it.RevisionId, },                                               // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ウェールズ語", TranslationToLanguageId = ja.RevisionId, },                                           // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "უელსური", TranslationToLanguageId = ka.RevisionId, },                                             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ವೇಲ್ಸಿನ ಜನರು ಯಾ ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, },                                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "웨일스 사람", TranslationToLanguageId = ko.RevisionId, },                                            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = la.RevisionId, },                                                 // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Velso", TranslationToLanguageId = lt.RevisionId, },                                                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "velsiešu", TranslationToLanguageId = lv.RevisionId, },                                              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "велшкиот", TranslationToLanguageId = mk.RevisionId, },                                              // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = ms.RevisionId, },                                                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = mt.RevisionId, },                                                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "van Wales", TranslationToLanguageId = nl.RevisionId, },                                             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "walisisk", TranslationToLanguageId = no.RevisionId, },                                              // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "walijski", TranslationToLanguageId = pl.RevisionId, },                                              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "galês", TranslationToLanguageId = pt.RevisionId, },                                                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = ro.RevisionId, },                                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "валлийский", TranslationToLanguageId = ru.RevisionId, },                                            // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "waleský", TranslationToLanguageId = sk.RevisionId, },                                               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "valižanščina", TranslationToLanguageId = sl.RevisionId, },                                          // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "shkel premtimin", TranslationToLanguageId = sq.RevisionId, },                                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "велшки", TranslationToLanguageId = sr.RevisionId, },                                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "walesiska", TranslationToLanguageId = sv.RevisionId, },                                             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Welsh", TranslationToLanguageId = sw.RevisionId, },                                                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "குதிரை பந்தயத்தில் பணம் கொடுக்காது ஓடி விடு", TranslationToLanguageId = ta.RevisionId, },    // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "వెల్ష్", TranslationToLanguageId = te.RevisionId, },                                                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาชาวเวลส์", TranslationToLanguageId = th.RevisionId, },                                             // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "şartları yerine getirmemek", TranslationToLanguageId = tr.RevisionId, },                            // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Валлійська", TranslationToLanguageId = uk.RevisionId, },                                            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ويلش", TranslationToLanguageId = ur.RevisionId, },                                                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Wales", TranslationToLanguageId = vi.RevisionId, },                                           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "וועלש", TranslationToLanguageId = yi.RevisionId, },                                                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "威尔士", TranslationToLanguageId = zh.RevisionId, },                                                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cy.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!da.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Danish", TranslationToLanguageId = en.RevisionId, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "danés", TranslationToLanguageId = es.RevisionId, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Dänisch", TranslationToLanguageId = de.RevisionId, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "دانماركي", TranslationToLanguageId = ar.RevisionId, },                // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Deens", TranslationToLanguageId = af.RevisionId, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Danimarka", TranslationToLanguageId = az.RevisionId, },                // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Дацкая", TranslationToLanguageId = be.RevisionId, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "датски", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ডেনিশ", TranslationToLanguageId = bn.RevisionId, },                    // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "danès", TranslationToLanguageId = ca.RevisionId, },                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "dánský", TranslationToLanguageId = cs.RevisionId, },                   // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Daneg", TranslationToLanguageId = cy.RevisionId, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "danske", TranslationToLanguageId = da.RevisionId, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "taani", TranslationToLanguageId = et.RevisionId, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Danimarkako", TranslationToLanguageId = eu.RevisionId, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "دانمارکی", TranslationToLanguageId = fa.RevisionId, },                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "tanska", TranslationToLanguageId = fi.RevisionId, },                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "danoises", TranslationToLanguageId = fr.RevisionId, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Danmhairgis", TranslationToLanguageId = ga.RevisionId, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Dinamarqués", TranslationToLanguageId = gl.RevisionId, },              // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ડેનિશ ભાષા", TranslationToLanguageId = gu.RevisionId, },                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "דני", TranslationToLanguageId = he.RevisionId, },                      // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "डेनिश", TranslationToLanguageId = hi.RevisionId, },                     // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "danski", TranslationToLanguageId = hr.RevisionId, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Danwa", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "dán", TranslationToLanguageId = hu.RevisionId, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "դանիերեն", TranslationToLanguageId = hy.RevisionId, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Denmark", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Danska", TranslationToLanguageId = isLanguage.RevisionId, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "danese", TranslationToLanguageId = it.RevisionId, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "デンマーク", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "დანიის", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಡೆನ್ಮಾರ್ಕ್ ದೇಶದ ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, },        // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "덴마크의", TranslationToLanguageId = ko.RevisionId, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Danish", TranslationToLanguageId = la.RevisionId, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Danijos", TranslationToLanguageId = lt.RevisionId, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Dānijas", TranslationToLanguageId = lv.RevisionId, },                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "данскиот", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Denmark", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "danish", TranslationToLanguageId = mt.RevisionId, },                   // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Deens", TranslationToLanguageId = nl.RevisionId, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "danske", TranslationToLanguageId = no.RevisionId, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "duński", TranslationToLanguageId = pl.RevisionId, },                   // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "dinamarquês", TranslationToLanguageId = pt.RevisionId, },              // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "danez", TranslationToLanguageId = ro.RevisionId, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "датский", TranslationToLanguageId = ru.RevisionId, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "dánsky", TranslationToLanguageId = sk.RevisionId, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Danish", TranslationToLanguageId = sl.RevisionId, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "danez", TranslationToLanguageId = sq.RevisionId, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "дански", TranslationToLanguageId = sr.RevisionId, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "danska", TranslationToLanguageId = sv.RevisionId, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Denmark", TranslationToLanguageId = sw.RevisionId, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "டானிஷ்", TranslationToLanguageId = ta.RevisionId, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "డేనిష్", TranslationToLanguageId = te.RevisionId, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาเดนมาร์ก", TranslationToLanguageId = th.RevisionId, },                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Danimarkalı", TranslationToLanguageId = tr.RevisionId, },              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Данська", TranslationToLanguageId = uk.RevisionId, },                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ڈينش", TranslationToLanguageId = ur.RevisionId, },                    // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Đan Mạch", TranslationToLanguageId = vi.RevisionId, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "דאַניש", TranslationToLanguageId = yi.RevisionId, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "丹麦", TranslationToLanguageId = zh.RevisionId, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = da.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (!et.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Estonian", TranslationToLanguageId = en.RevisionId, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = es.RevisionId, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Estnisch", TranslationToLanguageId = de.RevisionId, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "الاستونية", TranslationToLanguageId = ar.RevisionId, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Estnies", TranslationToLanguageId = af.RevisionId, },                 // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "eston", TranslationToLanguageId = az.RevisionId, },                   // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "эстонскі", TranslationToLanguageId = be.RevisionId, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "естонски", TranslationToLanguageId = bg.RevisionId, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "এস্তোনিয়াদেশ - সংক্রান্ত", TranslationToLanguageId = bn.RevisionId, },       // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Estònia", TranslationToLanguageId = ca.RevisionId, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "estonština", TranslationToLanguageId = cs.RevisionId, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = cy.RevisionId, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "estisk", TranslationToLanguageId = da.RevisionId, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "eesti", TranslationToLanguageId = et.RevisionId, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Estonian", TranslationToLanguageId = eu.RevisionId, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان استونی", TranslationToLanguageId = fa.RevisionId, },            // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "virolainen", TranslationToLanguageId = fi.RevisionId, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "estonien", TranslationToLanguageId = fr.RevisionId, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Eastóinis", TranslationToLanguageId = ga.RevisionId, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Estoniano", TranslationToLanguageId = gl.RevisionId, },               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "એસ્ટોનિયન", TranslationToLanguageId = gu.RevisionId, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אסטוניה", TranslationToLanguageId = he.RevisionId, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "एस्तोनियावासी", TranslationToLanguageId = hi.RevisionId, },             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "estonski", TranslationToLanguageId = hr.RevisionId, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Estonyen", TranslationToLanguageId = ht.RevisionId, },                // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "észt", TranslationToLanguageId = hu.RevisionId, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Էստոնիայի", TranslationToLanguageId = hy.RevisionId, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = id.RevisionId, },                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "eistneska", TranslationToLanguageId = isLanguage.RevisionId, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "estonian", TranslationToLanguageId = it.RevisionId, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "エストニア語", TranslationToLanguageId = ja.RevisionId, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ესტონეთის", TranslationToLanguageId = ka.RevisionId, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಎಸ್ಟೋನಿಯನ್", TranslationToLanguageId = kn.RevisionId, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "에스 토니아 사람", TranslationToLanguageId = ko.RevisionId, },         // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Estonica", TranslationToLanguageId = la.RevisionId, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Estijos", TranslationToLanguageId = lt.RevisionId, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Igaunijas", TranslationToLanguageId = lv.RevisionId, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Естонија", TranslationToLanguageId = mk.RevisionId, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Estonia", TranslationToLanguageId = ms.RevisionId, },                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Estonjan", TranslationToLanguageId = mt.RevisionId, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Estlands", TranslationToLanguageId = nl.RevisionId, },                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "estisk", TranslationToLanguageId = no.RevisionId, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "estoński", TranslationToLanguageId = pl.RevisionId, },                // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "estoniano", TranslationToLanguageId = pt.RevisionId, },               // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba estonă", TranslationToLanguageId = ro.RevisionId, },            // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "эстонский", TranslationToLanguageId = ru.RevisionId, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "estónčina", TranslationToLanguageId = sk.RevisionId, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "estonski", TranslationToLanguageId = sl.RevisionId, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "estonez", TranslationToLanguageId = sq.RevisionId, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "естонски", TranslationToLanguageId = sr.RevisionId, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "estniska", TranslationToLanguageId = sv.RevisionId, },                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiestonia", TranslationToLanguageId = sw.RevisionId, },               // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "எஸ்தானியம்", TranslationToLanguageId = ta.RevisionId, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఈస్టోనియను", TranslationToLanguageId = te.RevisionId, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เอสโตเนีย", TranslationToLanguageId = th.RevisionId, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Estonyalı", TranslationToLanguageId = tr.RevisionId, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Естонська", TranslationToLanguageId = uk.RevisionId, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "اسٹونين", TranslationToLanguageId = ur.RevisionId, },                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Estonia", TranslationToLanguageId = vi.RevisionId, },           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "עסטיש", TranslationToLanguageId = yi.RevisionId, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "爱沙尼亚语", TranslationToLanguageId = zh.RevisionId, },               // #61 Language = "Chinese"

//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = et.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!eu.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = en.RevisionId, },                                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "vasco", TranslationToLanguageId = es.RevisionId, },                                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Baskische", TranslationToLanguageId = de.RevisionId, },                               // #3  Language = "German"
//                    new CreateLanguageName { Text = "الباسكي", TranslationToLanguageId = ar.RevisionId, },                                // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Baskies", TranslationToLanguageId = af.RevisionId, },                                 // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Bask", TranslationToLanguageId = az.RevisionId, },                                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "басконскі", TranslationToLanguageId = be.RevisionId, },                               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "баска", TranslationToLanguageId = bg.RevisionId, },                                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্পেন ও ফ্রান্সের পিরেনিজ পর্বতাঞ্চলের অধিবাসী", TranslationToLanguageId = bn.RevisionId, },     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Basc", TranslationToLanguageId = ca.RevisionId, },                                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = cs.RevisionId, },                                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Basgeg", TranslationToLanguageId = cy.RevisionId, },                                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Baskerlandet", TranslationToLanguageId = da.RevisionId, },                            // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "baski", TranslationToLanguageId = et.RevisionId, },                                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Euskal", TranslationToLanguageId = eu.RevisionId, },                                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "باسک", TranslationToLanguageId = fa.RevisionId, },                                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "baski", TranslationToLanguageId = fi.RevisionId, },                                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "basques", TranslationToLanguageId = fr.RevisionId, },                                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Bascais", TranslationToLanguageId = ga.RevisionId, },                                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = gl.RevisionId, },                                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ટૂંકી ઝાલરવાળી ચોળી", TranslationToLanguageId = gu.RevisionId, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הבסקים", TranslationToLanguageId = he.RevisionId, },                                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "बस्क", TranslationToLanguageId = hi.RevisionId, },                                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "baskijski", TranslationToLanguageId = hr.RevisionId, },                               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "basque", TranslationToLanguageId = ht.RevisionId, },                                  // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "baszk", TranslationToLanguageId = hu.RevisionId, },                                   // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "բասկա", TranslationToLanguageId = hy.RevisionId, },                                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = id.RevisionId, },                                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "baskneska", TranslationToLanguageId = isLanguage.RevisionId, },                       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "basco", TranslationToLanguageId = it.RevisionId, },                                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "バスク", TranslationToLanguageId = ja.RevisionId, },                                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ბასკურ", TranslationToLanguageId = ka.RevisionId, },                                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಬಾಸ್ಕ್", TranslationToLanguageId = kn.RevisionId, },                                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "바스크 사람", TranslationToLanguageId = ko.RevisionId, },                              // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Vasca", TranslationToLanguageId = la.RevisionId, },                                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Baskų", TranslationToLanguageId = lt.RevisionId, },                                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "basku", TranslationToLanguageId = lv.RevisionId, },                                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Баскија", TranslationToLanguageId = mk.RevisionId, },                                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = ms.RevisionId, },                                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Bask", TranslationToLanguageId = mt.RevisionId, },                                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "basque", TranslationToLanguageId = nl.RevisionId, },                                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = no.RevisionId, },                                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "baskijski", TranslationToLanguageId = pl.RevisionId, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "basco", TranslationToLanguageId = pt.RevisionId, },                                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "basc", TranslationToLanguageId = ro.RevisionId, },                                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "баскский", TranslationToLanguageId = ru.RevisionId, },                                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = sk.RevisionId, },                                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = sl.RevisionId, },                                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "bask", TranslationToLanguageId = sq.RevisionId, },                                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "баскијски", TranslationToLanguageId = sr.RevisionId, },                               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "baskiska", TranslationToLanguageId = sv.RevisionId, },                                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = sw.RevisionId, },                                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பஸ்க்", TranslationToLanguageId = ta.RevisionId, },                                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "బాస్క్", TranslationToLanguageId = te.RevisionId, },                                    // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวแบสค์", TranslationToLanguageId = th.RevisionId, },                                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Bask", TranslationToLanguageId = tr.RevisionId, },                                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Баскська", TranslationToLanguageId = uk.RevisionId, },                                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "باسکی", TranslationToLanguageId = ur.RevisionId, },                                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Basque", TranslationToLanguageId = vi.RevisionId, },                                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "באַסק", TranslationToLanguageId = yi.RevisionId, },                                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "巴斯克", TranslationToLanguageId = zh.RevisionId, },                                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = eu.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!fa.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = en.RevisionId, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "persa", TranslationToLanguageId = es.RevisionId, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Persisch", TranslationToLanguageId = de.RevisionId, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "اللغة الفارسية", TranslationToLanguageId = ar.RevisionId, },            // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Persiese", TranslationToLanguageId = af.RevisionId, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "fars", TranslationToLanguageId = az.RevisionId, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Фарсі", TranslationToLanguageId = be.RevisionId, },                       // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "персийски", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "পারসিক", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "persa", TranslationToLanguageId = ca.RevisionId, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "perský", TranslationToLanguageId = cs.RevisionId, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Perseg", TranslationToLanguageId = cy.RevisionId, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "perser", TranslationToLanguageId = da.RevisionId, },                      // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Pärsia", TranslationToLanguageId = et.RevisionId, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = eu.RevisionId, },                     // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "فارسی", TranslationToLanguageId = fa.RevisionId, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "persialainen", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Persique", TranslationToLanguageId = fr.RevisionId, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Peirsis", TranslationToLanguageId = ga.RevisionId, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Persa", TranslationToLanguageId = gl.RevisionId, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ફારસી", TranslationToLanguageId = gu.RevisionId, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פרסי", TranslationToLanguageId = he.RevisionId, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "फ़ारसी", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Perzijski", TranslationToLanguageId = hr.RevisionId, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Pèsik", TranslationToLanguageId = ht.RevisionId, },                       // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "perzsa", TranslationToLanguageId = hu.RevisionId, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "պարսիկ", TranslationToLanguageId = hy.RevisionId, },                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = id.RevisionId, },                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "persneska", TranslationToLanguageId = isLanguage.RevisionId, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "persiano", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ペルシア語", TranslationToLanguageId = ja.RevisionId, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სპარსეთის", TranslationToLanguageId = ka.RevisionId, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಪರ್ಷಿಯಾಕ್ಕೆ ಸಂಬಂಧಿಸಿದ", TranslationToLanguageId = kn.RevisionId, },           // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "페르시아의", TranslationToLanguageId = ko.RevisionId, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Persae", TranslationToLanguageId = la.RevisionId, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "persų", TranslationToLanguageId = lt.RevisionId, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "persiešu", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Персискиот", TranslationToLanguageId = mk.RevisionId, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Parsi", TranslationToLanguageId = ms.RevisionId, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Persjan", TranslationToLanguageId = mt.RevisionId, },                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Perzisch", TranslationToLanguageId = nl.RevisionId, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = no.RevisionId, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "perski", TranslationToLanguageId = pl.RevisionId, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "persa", TranslationToLanguageId = pt.RevisionId, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "persană", TranslationToLanguageId = ro.RevisionId, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "персидский", TranslationToLanguageId = ru.RevisionId, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "perzský", TranslationToLanguageId = sk.RevisionId, },                     // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Persian", TranslationToLanguageId = sl.RevisionId, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "persisht", TranslationToLanguageId = sq.RevisionId, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "персијски", TranslationToLanguageId = sr.RevisionId, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Persiska", TranslationToLanguageId = sv.RevisionId, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiajemi", TranslationToLanguageId = sw.RevisionId, },                     // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பர்ஸியன்", TranslationToLanguageId = ta.RevisionId, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "పెర్షియన్", TranslationToLanguageId = te.RevisionId, },                      // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวเปอร์เซีย", TranslationToLanguageId = th.RevisionId, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Farsça", TranslationToLanguageId = tr.RevisionId, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Перська", TranslationToLanguageId = uk.RevisionId, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "فارسی", TranslationToLanguageId = ur.RevisionId, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Ba Tư", TranslationToLanguageId = vi.RevisionId, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פּערסיש", TranslationToLanguageId = yi.RevisionId, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "波斯语", TranslationToLanguageId = zh.RevisionId, },                      // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fa.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (!fi.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Finnish", TranslationToLanguageId = en.RevisionId, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "finlandés", TranslationToLanguageId = es.RevisionId, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Finnisch", TranslationToLanguageId = de.RevisionId, },                   // #3  Language = "German"
//                    new CreateLanguageName { Text = "فنلندية", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Finse", TranslationToLanguageId = af.RevisionId, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "fin", TranslationToLanguageId = az.RevisionId, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Фінская", TranslationToLanguageId = be.RevisionId, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "фински", TranslationToLanguageId = bg.RevisionId, },                     // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ফিনল্যাণ্ডের ভাষা", TranslationToLanguageId = bn.RevisionId, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Finès", TranslationToLanguageId = ca.RevisionId, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "finský", TranslationToLanguageId = cs.RevisionId, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Ffinneg", TranslationToLanguageId = cy.RevisionId, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "finnish", TranslationToLanguageId = da.RevisionId, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "soome", TranslationToLanguageId = et.RevisionId, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Finlandiako", TranslationToLanguageId = eu.RevisionId, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "فنلاندی", TranslationToLanguageId = fa.RevisionId, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "suomalainen", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "finlandaise", TranslationToLanguageId = fr.RevisionId, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Fionlainnis", TranslationToLanguageId = ga.RevisionId, },                // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Finés", TranslationToLanguageId = gl.RevisionId, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "દેશની ભાષા", TranslationToLanguageId = gu.RevisionId, },                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פינית", TranslationToLanguageId = he.RevisionId, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "फ़िन जातीय", TranslationToLanguageId = hi.RevisionId, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "finski", TranslationToLanguageId = hr.RevisionId, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "finnish", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "finn", TranslationToLanguageId = hu.RevisionId, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ֆիննական", TranslationToLanguageId = hy.RevisionId, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Finlandia", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Finnska", TranslationToLanguageId = isLanguage.RevisionId, },            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "finlandese", TranslationToLanguageId = it.RevisionId, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "フィンランド", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ფინეთის", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಫಿನ್ನಿಶ್", TranslationToLanguageId = kn.RevisionId, },                       // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "핀란드의", TranslationToLanguageId = ko.RevisionId, },                    // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Finnish", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "suomių", TranslationToLanguageId = lt.RevisionId, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Somijas", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "финскиот", TranslationToLanguageId = mk.RevisionId, },                   // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Finland", TranslationToLanguageId = ms.RevisionId, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Finlandiż", TranslationToLanguageId = mt.RevisionId, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Fins", TranslationToLanguageId = nl.RevisionId, },                       // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "finske", TranslationToLanguageId = no.RevisionId, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "fiński", TranslationToLanguageId = pl.RevisionId, },                     // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "finlandês", TranslationToLanguageId = pt.RevisionId, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "finlandeză", TranslationToLanguageId = ro.RevisionId, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "финский", TranslationToLanguageId = ru.RevisionId, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Fínsky", TranslationToLanguageId = sk.RevisionId, },                     // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "finski", TranslationToLanguageId = sl.RevisionId, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "finlandisht", TranslationToLanguageId = sq.RevisionId, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "фински", TranslationToLanguageId = sr.RevisionId, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "finska", TranslationToLanguageId = sv.RevisionId, },                     // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kifini", TranslationToLanguageId = sw.RevisionId, },                     // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஃபின்னிஷ்", TranslationToLanguageId = ta.RevisionId, },                // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఫిన్నిష్", TranslationToLanguageId = te.RevisionId, },                      // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ฟินแลนด์", TranslationToLanguageId = th.RevisionId, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Fince", TranslationToLanguageId = tr.RevisionId, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Фінська", TranslationToLanguageId = uk.RevisionId, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "فننش", TranslationToLanguageId = ur.RevisionId, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Phần Lan", TranslationToLanguageId = vi.RevisionId, },                   // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פֿיניש", TranslationToLanguageId = yi.RevisionId, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "芬兰", TranslationToLanguageId = zh.RevisionId, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fi.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!fr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "French", TranslationToLanguageId = en.RevisionId, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "francés", TranslationToLanguageId = es.RevisionId, },                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Französisch", TranslationToLanguageId = de.RevisionId, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "فرنسي", TranslationToLanguageId = ar.RevisionId, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Frans", TranslationToLanguageId = af.RevisionId, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Fransız dili", TranslationToLanguageId = az.RevisionId, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "французская", TranslationToLanguageId = be.RevisionId, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "френски", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ফরাসি", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "francès", TranslationToLanguageId = ca.RevisionId, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "francouzština", TranslationToLanguageId = cs.RevisionId, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Ffrangeg", TranslationToLanguageId = cy.RevisionId, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "fransk", TranslationToLanguageId = da.RevisionId, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "prantsuse", TranslationToLanguageId = et.RevisionId, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Frantziako", TranslationToLanguageId = eu.RevisionId, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "فرانسوی", TranslationToLanguageId = fa.RevisionId, },                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Ranskan", TranslationToLanguageId = fi.RevisionId, },                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "française", TranslationToLanguageId = fr.RevisionId, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Fraincis", TranslationToLanguageId = ga.RevisionId, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Francés", TranslationToLanguageId = gl.RevisionId, },                   // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ફ્રાંસની ભાષા", TranslationToLanguageId = gu.RevisionId, },                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "צרפתי", TranslationToLanguageId = he.RevisionId, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "फ्रांसीसी", TranslationToLanguageId = hi.RevisionId, },                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "francuski", TranslationToLanguageId = hr.RevisionId, },                 // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "franse", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "francia", TranslationToLanguageId = hu.RevisionId, },                   // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ֆրանսերեն", TranslationToLanguageId = hy.RevisionId, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Prancis", TranslationToLanguageId = id.RevisionId, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Franska", TranslationToLanguageId = isLanguage.RevisionId, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "francese", TranslationToLanguageId = it.RevisionId, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "フランス", TranslationToLanguageId = ja.RevisionId, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "საფრანგეთის", TranslationToLanguageId = ka.RevisionId, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಫ್ರೆಂಚ್", TranslationToLanguageId = kn.RevisionId, },                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "프랑스의", TranslationToLanguageId = ko.RevisionId, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "French", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "prancūzų", TranslationToLanguageId = lt.RevisionId, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "franču", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "францускиот", TranslationToLanguageId = mk.RevisionId, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Perancis", TranslationToLanguageId = ms.RevisionId, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Franċiż", TranslationToLanguageId = mt.RevisionId, },                   // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Frans", TranslationToLanguageId = nl.RevisionId, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "fransk", TranslationToLanguageId = no.RevisionId, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "francuski", TranslationToLanguageId = pl.RevisionId, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "francês", TranslationToLanguageId = pt.RevisionId, },                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "franceză", TranslationToLanguageId = ro.RevisionId, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "французский", TranslationToLanguageId = ru.RevisionId, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "francúzština", TranslationToLanguageId = sk.RevisionId, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "francoski", TranslationToLanguageId = sl.RevisionId, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "frëngjisht", TranslationToLanguageId = sq.RevisionId, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Французи", TranslationToLanguageId = sr.RevisionId, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "franska", TranslationToLanguageId = sv.RevisionId, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kifaransa", TranslationToLanguageId = sw.RevisionId, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "பிரஞ்சு", TranslationToLanguageId = ta.RevisionId, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ప్రాన్సుదేశభాష", TranslationToLanguageId = te.RevisionId, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฝรั่งเศส", TranslationToLanguageId = th.RevisionId, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Fransız", TranslationToLanguageId = tr.RevisionId, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "французький", TranslationToLanguageId = uk.RevisionId, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "فرانسیسی", TranslationToLanguageId = ur.RevisionId, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Pháp", TranslationToLanguageId = vi.RevisionId, },                      // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פראנצויזיש", TranslationToLanguageId = yi.RevisionId, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "法国", TranslationToLanguageId = zh.RevisionId, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fr.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ga.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Irish", TranslationToLanguageId = en.RevisionId, },                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "irlandés", TranslationToLanguageId = es.RevisionId, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Irisch", TranslationToLanguageId = de.RevisionId, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأيرلندية", TranslationToLanguageId = ar.RevisionId, },             // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Ierse", TranslationToLanguageId = af.RevisionId, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "irland", TranslationToLanguageId = az.RevisionId, },                 // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Ірландскі", TranslationToLanguageId = be.RevisionId, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "ирландски", TranslationToLanguageId = bg.RevisionId, },              // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আয়াল্যাণ্ড সংক্রান্ত", TranslationToLanguageId = bn.RevisionId, },           // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "irlandès", TranslationToLanguageId = ca.RevisionId, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "irský", TranslationToLanguageId = cs.RevisionId, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Gwyddelig", TranslationToLanguageId = cy.RevisionId, },              // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "irsk", TranslationToLanguageId = da.RevisionId, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "iiri", TranslationToLanguageId = et.RevisionId, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Gaelera", TranslationToLanguageId = eu.RevisionId, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ایرلندی", TranslationToLanguageId = fa.RevisionId, },               // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "irlantilainen", TranslationToLanguageId = fi.RevisionId, },          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "irlandaise", TranslationToLanguageId = fr.RevisionId, },             // #18 Language = "French"
//                    new CreateLanguageName { Text = "na hÉireann", TranslationToLanguageId = ga.RevisionId, },            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "irlandés", TranslationToLanguageId = gl.RevisionId, },               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આયર્લેન્ડનું", TranslationToLanguageId = gu.RevisionId, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אירית", TranslationToLanguageId = he.RevisionId, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आयरिश", TranslationToLanguageId = hi.RevisionId, },                 // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "irski", TranslationToLanguageId = hr.RevisionId, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Ilandè", TranslationToLanguageId = ht.RevisionId, },                 // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "ír", TranslationToLanguageId = hu.RevisionId, },                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իռլանդական", TranslationToLanguageId = hy.RevisionId, },            // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Irlandia", TranslationToLanguageId = id.RevisionId, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "írska", TranslationToLanguageId = isLanguage.RevisionId, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "irlandese", TranslationToLanguageId = it.RevisionId, },              // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アイリッシュ", TranslationToLanguageId = ja.RevisionId, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ირლანდიის", TranslationToLanguageId = ka.RevisionId, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕೋಪ", TranslationToLanguageId = kn.RevisionId, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아일랜드", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hibernica", TranslationToLanguageId = la.RevisionId, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "airių", TranslationToLanguageId = lt.RevisionId, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Īrijas", TranslationToLanguageId = lv.RevisionId, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Ирска", TranslationToLanguageId = mk.RevisionId, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Ireland", TranslationToLanguageId = ms.RevisionId, },                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Irlandiż", TranslationToLanguageId = mt.RevisionId, },               // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Iers", TranslationToLanguageId = nl.RevisionId, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "irsk", TranslationToLanguageId = no.RevisionId, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "irlandzki", TranslationToLanguageId = pl.RevisionId, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "irlandês", TranslationToLanguageId = pt.RevisionId, },               // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "irlandez", TranslationToLanguageId = ro.RevisionId, },               // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "ирландский", TranslationToLanguageId = ru.RevisionId, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "írsky", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "irski", TranslationToLanguageId = sl.RevisionId, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "irlandez", TranslationToLanguageId = sq.RevisionId, },               // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Ирци", TranslationToLanguageId = sr.RevisionId, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "irländska", TranslationToLanguageId = sv.RevisionId, },              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Ireland", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஐயர்லாந்தை சார்ந்த", TranslationToLanguageId = ta.RevisionId, },  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఐరిష్", TranslationToLanguageId = te.RevisionId, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ไอริช", TranslationToLanguageId = th.RevisionId, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İrlandalı", TranslationToLanguageId = tr.RevisionId, },              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Ірландський", TranslationToLanguageId = uk.RevisionId, },            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آئيرش", TranslationToLanguageId = ur.RevisionId, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ailen", TranslationToLanguageId = vi.RevisionId, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "איריש", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "爱尔兰", TranslationToLanguageId = zh.RevisionId, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ga.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!gl.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = en.RevisionId, },             // #1  Language = "English"
//                    new CreateLanguageName { Text = "gallego", TranslationToLanguageId = es.RevisionId, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "galizischen", TranslationToLanguageId = de.RevisionId, },           // #3  Language = "German"
//                    new CreateLanguageName { Text = "الجاليكية", TranslationToLanguageId = ar.RevisionId, },            // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Galicies", TranslationToLanguageId = af.RevisionId, },              // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Qalisian", TranslationToLanguageId = az.RevisionId, },              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Галіцка", TranslationToLanguageId = be.RevisionId, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "галисийски", TranslationToLanguageId = bg.RevisionId, },            // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "গ্যালিশিয়", TranslationToLanguageId = bn.RevisionId, },                // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Gallego", TranslationToLanguageId = ca.RevisionId, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = cs.RevisionId, },              // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = cy.RevisionId, },              // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "galiciske", TranslationToLanguageId = da.RevisionId, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = et.RevisionId, },              // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Galiziako", TranslationToLanguageId = eu.RevisionId, },             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "گالیسیایی", TranslationToLanguageId = fa.RevisionId, },            // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = fi.RevisionId, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Galice", TranslationToLanguageId = fr.RevisionId, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Gailísis", TranslationToLanguageId = ga.RevisionId, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Galego", TranslationToLanguageId = gl.RevisionId, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ગેલિશિયન", TranslationToLanguageId = gu.RevisionId, },              // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גליציאנית", TranslationToLanguageId = he.RevisionId, },            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "गैलिशियन्", TranslationToLanguageId = hi.RevisionId, },              // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Galicijski", TranslationToLanguageId = hr.RevisionId, },            // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Galisyen", TranslationToLanguageId = ht.RevisionId, },              // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = hu.RevisionId, },              // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "գալիսերեն", TranslationToLanguageId = hy.RevisionId, },             // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Galicia", TranslationToLanguageId = id.RevisionId, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "galisíska", TranslationToLanguageId = isLanguage.RevisionId, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "galiziano", TranslationToLanguageId = it.RevisionId, },             // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ガリシア語", TranslationToLanguageId = ja.RevisionId, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "გალიციური", TranslationToLanguageId = ka.RevisionId, },            // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಗ್ಯಾಲಿಶಿಯನ್", TranslationToLanguageId = kn.RevisionId, },             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "갈리시아어", TranslationToLanguageId = ko.RevisionId, },             // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Gallaeca", TranslationToLanguageId = la.RevisionId, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Galicijos", TranslationToLanguageId = lt.RevisionId, },             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "galisiešu", TranslationToLanguageId = lv.RevisionId, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Галиција", TranslationToLanguageId = mk.RevisionId, },              // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Galicia", TranslationToLanguageId = ms.RevisionId, },        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Galicia", TranslationToLanguageId = mt.RevisionId, },               // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "galician", TranslationToLanguageId = nl.RevisionId, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "galiciske", TranslationToLanguageId = no.RevisionId, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "galicyjskiej", TranslationToLanguageId = pl.RevisionId, },          // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "galego", TranslationToLanguageId = pt.RevisionId, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "galiciană", TranslationToLanguageId = ro.RevisionId, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "Галицко", TranslationToLanguageId = ru.RevisionId, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sk.RevisionId, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sl.RevisionId, },              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sq.RevisionId, },              // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "галицијски", TranslationToLanguageId = sr.RevisionId, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "galiciska", TranslationToLanguageId = sv.RevisionId, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Galician", TranslationToLanguageId = sw.RevisionId, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "காலிசியன்", TranslationToLanguageId = ta.RevisionId, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "గెలీసియన్", TranslationToLanguageId = te.RevisionId, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "กาลิเซีย", TranslationToLanguageId = th.RevisionId, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Galicia'ya ait", TranslationToLanguageId = tr.RevisionId, },        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Галицько", TranslationToLanguageId = uk.RevisionId, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "گاليشيائی", TranslationToLanguageId = ur.RevisionId, },            // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Galicia", TranslationToLanguageId = vi.RevisionId, },         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "גאליציאנער", TranslationToLanguageId = yi.RevisionId, },           // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "加利西亚", TranslationToLanguageId = zh.RevisionId, },               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gl.RevisionId;
//                    _createLanguageName.Handle(n);
//                });



//            if (!gu.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = en.RevisionId, },            // #1  Language = "English"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = es.RevisionId, },             // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = de.RevisionId, },             // #3  Language = "German"
//                    new CreateLanguageName { Text = "الغوجاراتية", TranslationToLanguageId = ar.RevisionId, },        // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = af.RevisionId, },             // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = az.RevisionId, },             // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Гуяраці", TranslationToLanguageId = be.RevisionId, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "гуджарати", TranslationToLanguageId = bg.RevisionId, },            // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "গুজরাটি", TranslationToLanguageId = bn.RevisionId, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = ca.RevisionId, },             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = cs.RevisionId, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Gwjarati", TranslationToLanguageId = cy.RevisionId, },             // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = da.RevisionId, },             // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "gudžarati", TranslationToLanguageId = et.RevisionId, },            // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = eu.RevisionId, },             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "گجراتی", TranslationToLanguageId = fa.RevisionId, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "gudzarati", TranslationToLanguageId = fi.RevisionId, },            // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Goudjrati", TranslationToLanguageId = fr.RevisionId, },            // #18 Language = "French"
//                    new CreateLanguageName { Text = "Gúisearáitis", TranslationToLanguageId = ga.RevisionId, },         // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Guzerate", TranslationToLanguageId = gl.RevisionId, },             // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ગુજરાતી", TranslationToLanguageId = gu.RevisionId, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גוג'ראטית", TranslationToLanguageId = he.RevisionId, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "गुजराती", TranslationToLanguageId = hi.RevisionId, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = hr.RevisionId, },             // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = ht.RevisionId, },             // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "gudzsaráti", TranslationToLanguageId = hu.RevisionId, },           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "գուջարատերեն", TranslationToLanguageId = hy.RevisionId, },        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Gujarat", TranslationToLanguageId = id.RevisionId, },              // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "gújaratí", TranslationToLanguageId = isLanguage.RevisionId, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = it.RevisionId, },             // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "グジャラート語", TranslationToLanguageId = ja.RevisionId, },        // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "გუჯარატი", TranslationToLanguageId = ka.RevisionId, },            // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಗುಜರಾತಿ", TranslationToLanguageId = kn.RevisionId, },               // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "구자라트어", TranslationToLanguageId = ko.RevisionId, },            // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Guiaratica", TranslationToLanguageId = la.RevisionId, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "gudžarati", TranslationToLanguageId = lt.RevisionId, },            // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "gudžaratu", TranslationToLanguageId = lv.RevisionId, },            // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Гуџарати", TranslationToLanguageId = mk.RevisionId, },             // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = ms.RevisionId, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Guġarati", TranslationToLanguageId = mt.RevisionId, },             // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = nl.RevisionId, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = no.RevisionId, },             // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "gudżarati", TranslationToLanguageId = pl.RevisionId, },            // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "guzerate", TranslationToLanguageId = pt.RevisionId, },             // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "gujarati", TranslationToLanguageId = ro.RevisionId, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "гуджарати", TranslationToLanguageId = ru.RevisionId, },            // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = sk.RevisionId, },             // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "gudžaratščina", TranslationToLanguageId = sl.RevisionId, },        // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "guxharati", TranslationToLanguageId = sq.RevisionId, },            // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "гујарати", TranslationToLanguageId = sr.RevisionId, },             // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = sv.RevisionId, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kigujarati", TranslationToLanguageId = sw.RevisionId, },           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "குஜராத்தி", TranslationToLanguageId = ta.RevisionId, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "గుజరాతీ", TranslationToLanguageId = te.RevisionId, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "คุชราต", TranslationToLanguageId = th.RevisionId, },                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = tr.RevisionId, },             // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "гуджараті", TranslationToLanguageId = uk.RevisionId, },            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "گجراتی", TranslationToLanguageId = ur.RevisionId, },              // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Gujarati", TranslationToLanguageId = vi.RevisionId, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "גודזשאַראַטי", TranslationToLanguageId = yi.RevisionId, },          // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "古吉拉特语", TranslationToLanguageId = zh.RevisionId, },            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gu.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!he.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = en.RevisionId, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "hebreo", TranslationToLanguageId = es.RevisionId, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Hebräisch", TranslationToLanguageId = de.RevisionId, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "العبرية", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Hebreeus", TranslationToLanguageId = af.RevisionId, },                   // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İvrit", TranslationToLanguageId = az.RevisionId, },                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "іўрыт", TranslationToLanguageId = be.RevisionId, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "иврит", TranslationToLanguageId = bg.RevisionId, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইহুদি", TranslationToLanguageId = bn.RevisionId, },                        // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "hebreu", TranslationToLanguageId = ca.RevisionId, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "hebrejský", TranslationToLanguageId = cs.RevisionId, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Hebraeg", TranslationToLanguageId = cy.RevisionId, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = da.RevisionId, },                     // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "heebrea", TranslationToLanguageId = et.RevisionId, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Hebrear", TranslationToLanguageId = eu.RevisionId, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان عبری", TranslationToLanguageId = fa.RevisionId, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "heprealainen", TranslationToLanguageId = fi.RevisionId, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "l'hébreu", TranslationToLanguageId = fr.RevisionId, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Eabhrais", TranslationToLanguageId = ga.RevisionId, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Hebreo", TranslationToLanguageId = gl.RevisionId, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "યહુદી", TranslationToLanguageId = gu.RevisionId, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "עברית", TranslationToLanguageId = he.RevisionId, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "यहूदी", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Hebrejski", TranslationToLanguageId = hr.RevisionId, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "lang ebre", TranslationToLanguageId = ht.RevisionId, },                  // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "héber", TranslationToLanguageId = hu.RevisionId, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "եբրայեցի", TranslationToLanguageId = hy.RevisionId, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Ibrani", TranslationToLanguageId = id.RevisionId, },                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "hebreska", TranslationToLanguageId = isLanguage.RevisionId, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ebraico", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ヘブライ", TranslationToLanguageId = ja.RevisionId, },                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ებრაული", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹೀಬ್ರೂ", TranslationToLanguageId = kn.RevisionId, },                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "히브리어", TranslationToLanguageId = ko.RevisionId, },                    // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = la.RevisionId, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "hebrajų", TranslationToLanguageId = lt.RevisionId, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "ebreju", TranslationToLanguageId = lv.RevisionId, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "хебрејски јазик", TranslationToLanguageId = mk.RevisionId, },            // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Ibrani", TranslationToLanguageId = ms.RevisionId, },                     // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ebrajk", TranslationToLanguageId = mt.RevisionId, },                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Hebreeuws", TranslationToLanguageId = nl.RevisionId, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "hebraisk", TranslationToLanguageId = no.RevisionId, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "hebrajski", TranslationToLanguageId = pl.RevisionId, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "hebraico", TranslationToLanguageId = pt.RevisionId, },                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "evreiesc", TranslationToLanguageId = ro.RevisionId, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "иврит", TranslationToLanguageId = ru.RevisionId, },                      // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "hebrejský", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Hebrew", TranslationToLanguageId = sl.RevisionId, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Hebraisht", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јеврејски", TranslationToLanguageId = sr.RevisionId, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "hebreiska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiyahudi", TranslationToLanguageId = sw.RevisionId, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "யூதர்", TranslationToLanguageId = ta.RevisionId, },                      // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "యూదుల భాష", TranslationToLanguageId = te.RevisionId, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฮิบรู", TranslationToLanguageId = th.RevisionId, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İbranice", TranslationToLanguageId = tr.RevisionId, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Іврит", TranslationToLanguageId = uk.RevisionId, },                      // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "عبرانی", TranslationToLanguageId = ur.RevisionId, },                    // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Do Thái,", TranslationToLanguageId = vi.RevisionId, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "העברעיש", TranslationToLanguageId = yi.RevisionId, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "希伯来文", TranslationToLanguageId = zh.RevisionId, },                    // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = he.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!hi.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = en.RevisionId, },                                        // #1  Language = "English"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = es.RevisionId, },                                         // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = de.RevisionId, },                                         // #3  Language = "German"
//                    new CreateLanguageName { Text = "هندي", TranslationToLanguageId = ar.RevisionId, },                                         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = af.RevisionId, },                                         // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = az.RevisionId, },                                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "ня", TranslationToLanguageId = be.RevisionId, },                                            // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = bg.RevisionId, },                                         // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "না", TranslationToLanguageId = bn.RevisionId, },                                            // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = ca.RevisionId, },                                         // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "hindština", TranslationToLanguageId = cs.RevisionId, },                                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = cy.RevisionId, },                                         // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = da.RevisionId, },                                         // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = et.RevisionId, },                                         // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "ez", TranslationToLanguageId = eu.RevisionId, },                                            // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "هندی", TranslationToLanguageId = fa.RevisionId, },                                         // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = fi.RevisionId, },                                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = fr.RevisionId, },                                         // #18 Language = "French"
//                    new CreateLanguageName { Text = "Hiondúis", TranslationToLanguageId = ga.RevisionId, },                                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = gl.RevisionId, },                                         // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "નથી", TranslationToLanguageId = gu.RevisionId, },                                           // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הינדי", TranslationToLanguageId = he.RevisionId, },                                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "हिंदी", TranslationToLanguageId = hi.RevisionId, },                                           // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Hindski", TranslationToLanguageId = hr.RevisionId, },                                       // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = ht.RevisionId, },                                         // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = hu.RevisionId, },                                         // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = hy.RevisionId, },                                         // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = id.RevisionId, },                                         // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = isLanguage.RevisionId, },                                 // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = it.RevisionId, },                                         // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ヒンディー語", TranslationToLanguageId = ja.RevisionId, },                                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჰინდი", TranslationToLanguageId = ka.RevisionId, },                                         // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹಿಂದಿ", TranslationToLanguageId = kn.RevisionId, },                                          // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "힌디어", TranslationToLanguageId = ko.RevisionId, },                                        // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = la.RevisionId, },                                         // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = lt.RevisionId, },                                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = lv.RevisionId, },                                         // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = mk.RevisionId, },                                         // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = ms.RevisionId, },                                         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ħindi", TranslationToLanguageId = mt.RevisionId, },                                         // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = nl.RevisionId, },                                         // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = no.RevisionId, },                                         // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = pl.RevisionId, },                                         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = pt.RevisionId, },                                         // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = ro.RevisionId, },                                         // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = ru.RevisionId, },                                         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "hindčina", TranslationToLanguageId = sk.RevisionId, },                                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = sl.RevisionId, },                                         // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "hindi", TranslationToLanguageId = sq.RevisionId, },                                         // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "хинди", TranslationToLanguageId = sr.RevisionId, },                                         // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = sv.RevisionId, },                                         // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Hindi", TranslationToLanguageId = sw.RevisionId, },                                         // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இந்தியாவில் பரவலாக பேசப்படும் மொழி", TranslationToLanguageId = ta.RevisionId, },   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "హిందీభాష", TranslationToLanguageId = te.RevisionId, },                                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฮินดี", TranslationToLanguageId = th.RevisionId, },                                        // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Hintçe", TranslationToLanguageId = tr.RevisionId, },                                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Хінді", TranslationToLanguageId = uk.RevisionId, },                                         // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہندی", TranslationToLanguageId = ur.RevisionId, },                                         // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Hin-ddi", TranslationToLanguageId = vi.RevisionId, },                                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "הינדיש", TranslationToLanguageId = yi.RevisionId, },                                       // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "印地文", TranslationToLanguageId = zh.RevisionId, },                                        // #61 Language = "Chinese"

//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hi.RevisionId;
//                    _createLanguageName.Handle(n);
//                });



//            if (!hr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Croatian", TranslationToLanguageId = en.RevisionId, },                     // #1  Language = "English"
//                    new CreateLanguageName { Text = "croata", TranslationToLanguageId = es.RevisionId, },                        // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "kroatisch", TranslationToLanguageId = de.RevisionId, },                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "الكرواتي", TranslationToLanguageId = ar.RevisionId, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Kroaties", TranslationToLanguageId = af.RevisionId, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Xorvat", TranslationToLanguageId = az.RevisionId, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Харвацкая", TranslationToLanguageId = be.RevisionId, },                     // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "хърватски", TranslationToLanguageId = bg.RevisionId, },                     // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ক্রোয়েশিয়ান", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "croat", TranslationToLanguageId = ca.RevisionId, },                         // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "chorvatský", TranslationToLanguageId = cs.RevisionId, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Croateg", TranslationToLanguageId = cy.RevisionId, },                       // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Kroatisk", TranslationToLanguageId = da.RevisionId, },                      // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "horvaatia", TranslationToLanguageId = et.RevisionId, },                     // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Kroazierara", TranslationToLanguageId = eu.RevisionId, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کرواتی", TranslationToLanguageId = fa.RevisionId, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "kroaatti", TranslationToLanguageId = fi.RevisionId, },                      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "croate", TranslationToLanguageId = fr.RevisionId, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Cróitis", TranslationToLanguageId = ga.RevisionId, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Croata", TranslationToLanguageId = gl.RevisionId, },                        // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ક્રોએશિયન", TranslationToLanguageId = gu.RevisionId, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קרואטית", TranslationToLanguageId = he.RevisionId, },                      // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "क्रोएशियाई", TranslationToLanguageId = hi.RevisionId, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "hrvatski", TranslationToLanguageId = hr.RevisionId, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "kwoasyen", TranslationToLanguageId = ht.RevisionId, },                      // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "horvát", TranslationToLanguageId = hu.RevisionId, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "խորվաթական", TranslationToLanguageId = hy.RevisionId, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Kroasia", TranslationToLanguageId = id.RevisionId, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Króatíska", TranslationToLanguageId = isLanguage.RevisionId, },             // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "croato", TranslationToLanguageId = it.RevisionId, },                        // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "クロアチア語", TranslationToLanguageId = ja.RevisionId, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ხორვატული", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕ್ರೊಯೇಷಿಯಾದ", TranslationToLanguageId = kn.RevisionId, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "크로아티아의", TranslationToLanguageId = ko.RevisionId, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Illyrica", TranslationToLanguageId = la.RevisionId, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Kroatijos", TranslationToLanguageId = lt.RevisionId, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Horvātijas", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Хрватска", TranslationToLanguageId = mk.RevisionId, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Croatia", TranslationToLanguageId = ms.RevisionId, },                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Kroat", TranslationToLanguageId = mt.RevisionId, },                         // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Kroatisch", TranslationToLanguageId = nl.RevisionId, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "kroatisk", TranslationToLanguageId = no.RevisionId, },                      // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "chorwacki", TranslationToLanguageId = pl.RevisionId, },                     // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "croata", TranslationToLanguageId = pt.RevisionId, },                        // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "croat", TranslationToLanguageId = ro.RevisionId, },                         // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "хорватский", TranslationToLanguageId = ru.RevisionId, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Chorvátsky", TranslationToLanguageId = sk.RevisionId, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "hrvaški", TranslationToLanguageId = sl.RevisionId, },                       // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "kroate", TranslationToLanguageId = sq.RevisionId, },                        // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "хрватски", TranslationToLanguageId = sr.RevisionId, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "kroatiska", TranslationToLanguageId = sv.RevisionId, },                     // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kikroeshia", TranslationToLanguageId = sw.RevisionId, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கரோஷியன்", TranslationToLanguageId = ta.RevisionId, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "క్రొయేషియన్", TranslationToLanguageId = te.RevisionId, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "โครเอเชีย", TranslationToLanguageId = th.RevisionId, },                        // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Hırvat", TranslationToLanguageId = tr.RevisionId, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Хорватська", TranslationToLanguageId = uk.RevisionId, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کروشین", TranslationToLanguageId = ur.RevisionId, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Croatia", TranslationToLanguageId = vi.RevisionId, },                       // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קראָאַטיש", TranslationToLanguageId = yi.RevisionId, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "克罗地亚", TranslationToLanguageId = zh.RevisionId, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hr.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ht.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = en.RevisionId, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "buenas Noticias", TranslationToLanguageId = es.RevisionId, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Good News", TranslationToLanguageId = de.RevisionId, },                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "أخبار سارة", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "goeie Nuus", TranslationToLanguageId = af.RevisionId, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Xoş Xəbər", TranslationToLanguageId = az.RevisionId, },                     // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "добрыя навіны", TranslationToLanguageId = be.RevisionId, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "Добрата новина", TranslationToLanguageId = bg.RevisionId, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সু - সংবাদ", TranslationToLanguageId = bn.RevisionId, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "bones Notícies", TranslationToLanguageId = ca.RevisionId, },                // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "dobré zprávy", TranslationToLanguageId = cs.RevisionId, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Newyddion Da", TranslationToLanguageId = cy.RevisionId, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "gode nyheder", TranslationToLanguageId = da.RevisionId, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "head uudised", TranslationToLanguageId = et.RevisionId, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Good News", TranslationToLanguageId = eu.RevisionId, },                     // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مژده", TranslationToLanguageId = fa.RevisionId, },                         // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "hyviä uutisia", TranslationToLanguageId = fi.RevisionId, },                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "French", TranslationToLanguageId = fr.RevisionId, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "dea-Scéal", TranslationToLanguageId = ga.RevisionId, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "boa Nova", TranslationToLanguageId = gl.RevisionId, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સારા સમાચાર", TranslationToLanguageId = gu.RevisionId, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "חדשות טובות", TranslationToLanguageId = he.RevisionId, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अच्छी खबर", TranslationToLanguageId = hi.RevisionId, },                     // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Haićanski kreolski", TranslationToLanguageId = hr.RevisionId, },            // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "kreyòl ayisyen", TranslationToLanguageId = ht.RevisionId, },                // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "jó hír", TranslationToLanguageId = hu.RevisionId, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "Haitian կրեոլ", TranslationToLanguageId = hy.RevisionId, },                 // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "baik Berita", TranslationToLanguageId = id.RevisionId, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Góðar fréttir", TranslationToLanguageId = isLanguage.RevisionId, },         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "buona Novella", TranslationToLanguageId = it.RevisionId, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "良いニュース", TranslationToLanguageId = ja.RevisionId, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = ka.RevisionId, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹೈಟಿ ಕ್ರಿಯೋಲ", TranslationToLanguageId = kn.RevisionId, },                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아이티 크리올어 크리올", TranslationToLanguageId = ko.RevisionId, },         // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = la.RevisionId, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Haičio kreolų", TranslationToLanguageId = lt.RevisionId, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Haiti kreolu", TranslationToLanguageId = lv.RevisionId, },                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Хаити креолски", TranslationToLanguageId = mk.RevisionId, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Haiti Creole", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ħaitjan", TranslationToLanguageId = mt.RevisionId, },                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Haïtiaans Creools", TranslationToLanguageId = nl.RevisionId, },             // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "haitisk kreolsk", TranslationToLanguageId = no.RevisionId, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Haiti Creole", TranslationToLanguageId = pl.RevisionId, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "crioulo haitiano", TranslationToLanguageId = pt.RevisionId, },              // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "haitiană Creole", TranslationToLanguageId = ro.RevisionId, },               // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "гаитянский креольский", TranslationToLanguageId = ru.RevisionId, },         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Haitian kreolský", TranslationToLanguageId = sk.RevisionId, },              // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = sl.RevisionId, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "haitian Creole", TranslationToLanguageId = sq.RevisionId, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "хаићански креолски", TranslationToLanguageId = sr.RevisionId, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Haitisk kreol", TranslationToLanguageId = sv.RevisionId, },                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Haitian Creole", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஹெய்டியன் கிரியோல்", TranslationToLanguageId = ta.RevisionId, },       // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "హైటియన్ క్రియోల్", TranslationToLanguageId = te.RevisionId, },                 // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ครีโอลเฮติ", TranslationToLanguageId = th.RevisionId, },                       // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Haiti Creole", TranslationToLanguageId = tr.RevisionId, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "гаїтянський креольський", TranslationToLanguageId = uk.RevisionId, },       // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہیٹی Creole", TranslationToLanguageId = ur.RevisionId, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Haiti", TranslationToLanguageId = vi.RevisionId, },                         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "האַיטיאַן קרעאָלע", TranslationToLanguageId = yi.RevisionId, },              // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "海地克里奥尔语", TranslationToLanguageId = zh.RevisionId, },                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ht.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!hu.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = en.RevisionId, },                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "húngaro", TranslationToLanguageId = es.RevisionId, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Ungarisch", TranslationToLanguageId = de.RevisionId, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "المجري", TranslationToLanguageId = ar.RevisionId, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Hongaars", TranslationToLanguageId = af.RevisionId, },                   // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "macar", TranslationToLanguageId = az.RevisionId, },                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Венгерская", TranslationToLanguageId = be.RevisionId, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "унгарски", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "হাঙ্গেরীয়", TranslationToLanguageId = bn.RevisionId, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "hongarès", TranslationToLanguageId = ca.RevisionId, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "maďarština", TranslationToLanguageId = cs.RevisionId, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Hwngari", TranslationToLanguageId = cy.RevisionId, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "ungarsk", TranslationToLanguageId = da.RevisionId, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "ungari", TranslationToLanguageId = et.RevisionId, },                     // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = eu.RevisionId, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مجارستانی", TranslationToLanguageId = fa.RevisionId, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "unkari", TranslationToLanguageId = fi.RevisionId, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "hongroises", TranslationToLanguageId = fr.RevisionId, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Ungáiris", TranslationToLanguageId = ga.RevisionId, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Húngaro", TranslationToLanguageId = gl.RevisionId, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "હંગેરિયન", TranslationToLanguageId = gu.RevisionId, },                    // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הונגרי", TranslationToLanguageId = he.RevisionId, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "हंगेरी", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "mađarski", TranslationToLanguageId = hr.RevisionId, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Ongwa", TranslationToLanguageId = ht.RevisionId, },                      // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "magyar", TranslationToLanguageId = hu.RevisionId, },                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հունգարերեն", TranslationToLanguageId = hy.RevisionId, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Hongaria", TranslationToLanguageId = id.RevisionId, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Ungverska", TranslationToLanguageId = isLanguage.RevisionId, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ungherese", TranslationToLanguageId = it.RevisionId, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ハンガリー", TranslationToLanguageId = ja.RevisionId, },                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "უნგრეთის", TranslationToLanguageId = ka.RevisionId, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಹಂಗೇರಿಯ", TranslationToLanguageId = kn.RevisionId, },                    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "헝가리어", TranslationToLanguageId = ko.RevisionId, },                    // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Hungarica", TranslationToLanguageId = la.RevisionId, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "vengrų", TranslationToLanguageId = lt.RevisionId, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Ungārijas", TranslationToLanguageId = lv.RevisionId, },                  // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "унгарскиот", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Hungary", TranslationToLanguageId = ms.RevisionId, },             // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ungeriż", TranslationToLanguageId = mt.RevisionId, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Hongaars", TranslationToLanguageId = nl.RevisionId, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = no.RevisionId, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "węgierski", TranslationToLanguageId = pl.RevisionId, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "húngaro", TranslationToLanguageId = pt.RevisionId, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba maghiară", TranslationToLanguageId = ro.RevisionId, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "венгерский", TranslationToLanguageId = ru.RevisionId, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "maďarčina", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "madžarski", TranslationToLanguageId = sl.RevisionId, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "hungarez", TranslationToLanguageId = sq.RevisionId, },                   // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "мађарски", TranslationToLanguageId = sr.RevisionId, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Ungerska", TranslationToLanguageId = sv.RevisionId, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Hungarian", TranslationToLanguageId = sw.RevisionId, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஹங்கேரியன்", TranslationToLanguageId = ta.RevisionId, },             // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "హన్గేరియన్", TranslationToLanguageId = te.RevisionId, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาฮังการี", TranslationToLanguageId = th.RevisionId, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Macar", TranslationToLanguageId = tr.RevisionId, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Угорський", TranslationToLanguageId = uk.RevisionId, },                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ہنگیرین", TranslationToLanguageId = ur.RevisionId, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Hungary", TranslationToLanguageId = vi.RevisionId, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אונגעריש", TranslationToLanguageId = yi.RevisionId, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "匈牙利", TranslationToLanguageId = zh.RevisionId, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hu.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!hy.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Armenian", TranslationToLanguageId = en.RevisionId, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "armenio", TranslationToLanguageId = es.RevisionId, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Armenisch", TranslationToLanguageId = de.RevisionId, },                  // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأرميني", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Armeens", TranslationToLanguageId = af.RevisionId, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "erməni", TranslationToLanguageId = az.RevisionId, },                     // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "армянскі", TranslationToLanguageId = be.RevisionId, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "арменски", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আর্মেনিয়ান", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "armeni", TranslationToLanguageId = ca.RevisionId, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "arménský", TranslationToLanguageId = cs.RevisionId, },                   // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Armenia", TranslationToLanguageId = cy.RevisionId, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "armenske", TranslationToLanguageId = da.RevisionId, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "armeenia", TranslationToLanguageId = et.RevisionId, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Armenian", TranslationToLanguageId = eu.RevisionId, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ارمنی", TranslationToLanguageId = fa.RevisionId, },                     // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Armenian", TranslationToLanguageId = fi.RevisionId, },                   // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "arméniens", TranslationToLanguageId = fr.RevisionId, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Airméinis", TranslationToLanguageId = ga.RevisionId, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Armenio", TranslationToLanguageId = gl.RevisionId, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આર્મેનિયન", TranslationToLanguageId = gu.RevisionId, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ארמני", TranslationToLanguageId = he.RevisionId, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आर्मीनियाई", TranslationToLanguageId = hi.RevisionId, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Armenski", TranslationToLanguageId = hr.RevisionId, },                   // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Amenyen", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "örmény", TranslationToLanguageId = hu.RevisionId, },                     // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հայերեն", TranslationToLanguageId = hy.RevisionId, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Armenia", TranslationToLanguageId = id.RevisionId, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "armenska", TranslationToLanguageId = isLanguage.RevisionId, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "armeno", TranslationToLanguageId = it.RevisionId, },                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アルメニア語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სომხეთის", TranslationToLanguageId = ka.RevisionId, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅರ್ಮೇನಿಯನ್", TranslationToLanguageId = kn.RevisionId, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아르메니아의", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Armenius", TranslationToLanguageId = la.RevisionId, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "armėnų", TranslationToLanguageId = lt.RevisionId, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "armēņu", TranslationToLanguageId = lv.RevisionId, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "ерменскиот", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Armenia", TranslationToLanguageId = ms.RevisionId, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Armen", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Armeens", TranslationToLanguageId = nl.RevisionId, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "armensk", TranslationToLanguageId = no.RevisionId, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Ormianin", TranslationToLanguageId = pl.RevisionId, },                   // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "armênio", TranslationToLanguageId = pt.RevisionId, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "armean", TranslationToLanguageId = ro.RevisionId, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "армянский", TranslationToLanguageId = ru.RevisionId, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "arménsky", TranslationToLanguageId = sk.RevisionId, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "armenski", TranslationToLanguageId = sl.RevisionId, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "armen", TranslationToLanguageId = sq.RevisionId, },                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јерменски", TranslationToLanguageId = sr.RevisionId, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "armeniska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "muarmeni", TranslationToLanguageId = sw.RevisionId, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஆர்மீனியன்", TranslationToLanguageId = ta.RevisionId, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "అర్మేనియా దేశస్తుడు", TranslationToLanguageId = te.RevisionId, },            // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "อาร์เมเนีย", TranslationToLanguageId = th.RevisionId, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Ermeni", TranslationToLanguageId = tr.RevisionId, },                     // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "вірменський", TranslationToLanguageId = uk.RevisionId, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آرمینیا", TranslationToLanguageId = ur.RevisionId, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Armenia", TranslationToLanguageId = vi.RevisionId, },              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ארמאניש", TranslationToLanguageId = yi.RevisionId, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "亚美尼亚", TranslationToLanguageId = zh.RevisionId, },                    // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hy.RevisionId;
//                    _createLanguageName.Handle(n);
//                });



//            if (!id.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Indonesian", TranslationToLanguageId = en.RevisionId, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "indonesio", TranslationToLanguageId = es.RevisionId, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Indonesier", TranslationToLanguageId = de.RevisionId, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأندونيسية", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Indonesies", TranslationToLanguageId = af.RevisionId, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İndoneziya", TranslationToLanguageId = az.RevisionId, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "інданезійская", TranslationToLanguageId = be.RevisionId, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "индонезийски", TranslationToLanguageId = bg.RevisionId, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইন্দোনেশীয়", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "indonesi", TranslationToLanguageId = ca.RevisionId, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Indonésan", TranslationToLanguageId = cs.RevisionId, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Indonesia", TranslationToLanguageId = cy.RevisionId, },                     // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "indonesisk", TranslationToLanguageId = da.RevisionId, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "indoneesia", TranslationToLanguageId = et.RevisionId, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Indonesian", TranslationToLanguageId = eu.RevisionId, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اندونزی", TranslationToLanguageId = fa.RevisionId, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "indonesialainen", TranslationToLanguageId = fi.RevisionId, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "indonésienne", TranslationToLanguageId = fr.RevisionId, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Indinéisis", TranslationToLanguageId = ga.RevisionId, },                    // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Indonesio", TranslationToLanguageId = gl.RevisionId, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઇન્ડોનેશિયન", TranslationToLanguageId = gu.RevisionId, },                     // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אינדונזית", TranslationToLanguageId = he.RevisionId, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "इंडोनेशिया का या उससे संबद्ध", TranslationToLanguageId = hi.RevisionId, },       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "indonezijski", TranslationToLanguageId = hr.RevisionId, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Endonezyen", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creole"
//                    new CreateLanguageName { Text = "indonéz", TranslationToLanguageId = hu.RevisionId, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ինդոնեզերեն", TranslationToLanguageId = hy.RevisionId, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Bahasa Indonesia", TranslationToLanguageId = id.RevisionId, },              // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "indónesísku", TranslationToLanguageId = isLanguage.RevisionId, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "indonesiano", TranslationToLanguageId = it.RevisionId, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "インドネシア", TranslationToLanguageId = ja.RevisionId, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ინდონეზიის", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಇಂಡೋನೀಷಿಯ ದೇಶಕ್ಕೆ ಸೇರಿದ", TranslationToLanguageId = kn.RevisionId, },        // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "인도네시아의", TranslationToLanguageId = ko.RevisionId, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Indonesiaca", TranslationToLanguageId = la.RevisionId, },                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Indonezijos", TranslationToLanguageId = lt.RevisionId, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Indonēzijas", TranslationToLanguageId = lv.RevisionId, },                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "индонезиски", TranslationToLanguageId = mk.RevisionId, },                   // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Indonesia", TranslationToLanguageId = ms.RevisionId, },                     // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Indoneżjan", TranslationToLanguageId = mt.RevisionId, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Indonesisch", TranslationToLanguageId = nl.RevisionId, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "indonesisk", TranslationToLanguageId = no.RevisionId, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "indonezyjski", TranslationToLanguageId = pl.RevisionId, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "indonésio", TranslationToLanguageId = pt.RevisionId, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "indoneziană", TranslationToLanguageId = ro.RevisionId, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "индонезийский", TranslationToLanguageId = ru.RevisionId, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Indonézan", TranslationToLanguageId = sk.RevisionId, },                     // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "indonezijski", TranslationToLanguageId = sl.RevisionId, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "indonezian", TranslationToLanguageId = sq.RevisionId, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "индонезијски", TranslationToLanguageId = sr.RevisionId, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "indonesiska", TranslationToLanguageId = sv.RevisionId, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiindonesia", TranslationToLanguageId = sw.RevisionId, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இந்தோனேசிய", TranslationToLanguageId = ta.RevisionId, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఇండోనేషియా", TranslationToLanguageId = te.RevisionId, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวอินโดนีเซีย", TranslationToLanguageId = th.RevisionId, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Endonezyalı", TranslationToLanguageId = tr.RevisionId, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Індонезійська", TranslationToLanguageId = uk.RevisionId, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "انڈونیشی", TranslationToLanguageId = ur.RevisionId, },                     // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Indonesia", TranslationToLanguageId = vi.RevisionId, },                     // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אינדאָנעזיש", TranslationToLanguageId = yi.RevisionId, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "印度尼西亚", TranslationToLanguageId = zh.RevisionId, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = id.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!isLanguage.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Icelandic", TranslationToLanguageId = en.RevisionId, },                   // #1  Language = "English"
//                    new CreateLanguageName { Text = "islandés", TranslationToLanguageId = es.RevisionId, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Isländisch", TranslationToLanguageId = de.RevisionId, },                   // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأيسلندية", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Yslands", TranslationToLanguageId = af.RevisionId, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "island", TranslationToLanguageId = az.RevisionId, },                       // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Ісландская", TranslationToLanguageId = be.RevisionId, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "исландски", TranslationToLanguageId = bg.RevisionId, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আইস্ল্যাণ্ডের ভাষা", TranslationToLanguageId = bn.RevisionId, },                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "islandès", TranslationToLanguageId = ca.RevisionId, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "islandský", TranslationToLanguageId = cs.RevisionId, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Islandeg", TranslationToLanguageId = cy.RevisionId, },                     // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "islandsk", TranslationToLanguageId = da.RevisionId, },                     // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "islandi", TranslationToLanguageId = et.RevisionId, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Islandiako", TranslationToLanguageId = eu.RevisionId, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان ایسلندی", TranslationToLanguageId = fa.RevisionId, },               // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Islannin", TranslationToLanguageId = fi.RevisionId, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "islandais", TranslationToLanguageId = fr.RevisionId, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Íoslainnis", TranslationToLanguageId = ga.RevisionId, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Islandés", TranslationToLanguageId = gl.RevisionId, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "આઇસલેન્ડિક", TranslationToLanguageId = gu.RevisionId, },                    // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "איסלנדית", TranslationToLanguageId = he.RevisionId, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "आइसलैंड का", TranslationToLanguageId = hi.RevisionId, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "islandski", TranslationToLanguageId = hr.RevisionId, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "icelandic", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "izlandi", TranslationToLanguageId = hu.RevisionId, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իսլանդերեն", TranslationToLanguageId = hy.RevisionId, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Islandia", TranslationToLanguageId = id.RevisionId, },                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Íslenska", TranslationToLanguageId = isLanguage.RevisionId, },             // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "islandese", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アイスランド語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ისლანდიის", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಐಸ್ಲ್ಯಾಂಡಿಕ್", TranslationToLanguageId = kn.RevisionId, },                     // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "아이슬란드의", TranslationToLanguageId = ko.RevisionId, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Islandica", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Islandijos", TranslationToLanguageId = lt.RevisionId, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "islandiešu", TranslationToLanguageId = lv.RevisionId, },                   // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Исланд", TranslationToLanguageId = mk.RevisionId, },                       // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Iceland", TranslationToLanguageId = ms.RevisionId, },                      // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Iceland", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "IJslands", TranslationToLanguageId = nl.RevisionId, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "islandsk", TranslationToLanguageId = no.RevisionId, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "islandzki", TranslationToLanguageId = pl.RevisionId, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "islandês", TranslationToLanguageId = pt.RevisionId, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "islandez", TranslationToLanguageId = ro.RevisionId, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "исландский", TranslationToLanguageId = ru.RevisionId, },                   // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "islandský", TranslationToLanguageId = sk.RevisionId, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "islandski", TranslationToLanguageId = sl.RevisionId, },                    // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "islandez", TranslationToLanguageId = sq.RevisionId, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "исландски", TranslationToLanguageId = sr.RevisionId, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "isländska", TranslationToLanguageId = sv.RevisionId, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiaislandi", TranslationToLanguageId = sw.RevisionId, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஐஸ்லாந்து", TranslationToLanguageId = ta.RevisionId, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఐస్లాండిక్", TranslationToLanguageId = te.RevisionId, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ไอซ์แลนด์", TranslationToLanguageId = th.RevisionId, },                       // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İzlanda", TranslationToLanguageId = tr.RevisionId, },                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Ісландська", TranslationToLanguageId = uk.RevisionId, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "آئس لینڈی", TranslationToLanguageId = ur.RevisionId, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Iceland", TranslationToLanguageId = vi.RevisionId, },                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "איסלענדיש", TranslationToLanguageId = yi.RevisionId, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "冰岛", TranslationToLanguageId = zh.RevisionId, },                         // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = isLanguage.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!it.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = en.RevisionId, },                         // #1  Language = "English"
//                    new CreateLanguageName { Text = "italiano", TranslationToLanguageId = es.RevisionId, },                         // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Italienisch", TranslationToLanguageId = de.RevisionId, },                      // #3  Language = "German"
//                    new CreateLanguageName { Text = "إيطالي", TranslationToLanguageId = ar.RevisionId, },                          // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Italiaanse", TranslationToLanguageId = af.RevisionId, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "italyan", TranslationToLanguageId = az.RevisionId, },                          // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Італьянская", TranslationToLanguageId = be.RevisionId, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "италиански", TranslationToLanguageId = bg.RevisionId, },                       // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইতালিয়", TranslationToLanguageId = bn.RevisionId, },                            // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "italià", TranslationToLanguageId = ca.RevisionId, },                           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Ital", TranslationToLanguageId = cs.RevisionId, },                             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Eidaleg", TranslationToLanguageId = cy.RevisionId, },                          // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "italiensk", TranslationToLanguageId = da.RevisionId, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "itaalia", TranslationToLanguageId = et.RevisionId, },                          // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = eu.RevisionId, },                          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ایتالیایی", TranslationToLanguageId = fa.RevisionId, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = fi.RevisionId, },                          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "italienne", TranslationToLanguageId = fr.RevisionId, },                        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Iodáilis", TranslationToLanguageId = ga.RevisionId, },                         // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Italiano", TranslationToLanguageId = gl.RevisionId, },                         // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઇટાલીનું", TranslationToLanguageId = gu.RevisionId, },                           // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "איטלקית", TranslationToLanguageId = he.RevisionId, },                         // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "इतालवी", TranslationToLanguageId = hi.RevisionId, },                           // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "talijanski", TranslationToLanguageId = hr.RevisionId, },                       // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Italyen", TranslationToLanguageId = ht.RevisionId, },                          // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "olasz", TranslationToLanguageId = hu.RevisionId, },                            // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իտալերեն", TranslationToLanguageId = hy.RevisionId, },                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Italia", TranslationToLanguageId = id.RevisionId, },                           // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Ítalska", TranslationToLanguageId = isLanguage.RevisionId, },                  // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "italiano", TranslationToLanguageId = it.RevisionId, },                         // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "イタリア", TranslationToLanguageId = ja.RevisionId, },                          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "იტალიური", TranslationToLanguageId = ka.RevisionId, },                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಇಟಲಿಯವ", TranslationToLanguageId = kn.RevisionId, },                          // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "이탈리아의", TranslationToLanguageId = ko.RevisionId, },                        // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Italica", TranslationToLanguageId = la.RevisionId, },                          // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Italijos", TranslationToLanguageId = lt.RevisionId, },                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Itālijas", TranslationToLanguageId = lv.RevisionId, },                         // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "италијански", TranslationToLanguageId = mk.RevisionId, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Itali", TranslationToLanguageId = ms.RevisionId, },                            // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Taljan", TranslationToLanguageId = mt.RevisionId, },                           // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Italiaans", TranslationToLanguageId = nl.RevisionId, },                        // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Italian", TranslationToLanguageId = no.RevisionId, },                          // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "włoski", TranslationToLanguageId = pl.RevisionId, },                           // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "italiano", TranslationToLanguageId = pt.RevisionId, },                         // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "italian", TranslationToLanguageId = ro.RevisionId, },                          // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "итальянский", TranslationToLanguageId = ru.RevisionId, },                      // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Talian", TranslationToLanguageId = sk.RevisionId, },                           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Italijanska", TranslationToLanguageId = sl.RevisionId, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "italian", TranslationToLanguageId = sq.RevisionId, },                          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "италијански", TranslationToLanguageId = sr.RevisionId, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "italienska", TranslationToLanguageId = sv.RevisionId, },                       // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Italia", TranslationToLanguageId = sw.RevisionId, },                           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இத்தாலிய நாட்டை சார்ந்த", TranslationToLanguageId = ta.RevisionId, },      // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఇటాలియన్", TranslationToLanguageId = te.RevisionId, },                         // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "อิตาเลียน", TranslationToLanguageId = th.RevisionId, },                           // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İtalyan", TranslationToLanguageId = tr.RevisionId, },                          // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Італійська", TranslationToLanguageId = uk.RevisionId, },                       // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "اطالوی", TranslationToLanguageId = ur.RevisionId, },                          // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ý", TranslationToLanguageId = vi.RevisionId, },                                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "איטאַליעניש", TranslationToLanguageId = yi.RevisionId, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "意大利", TranslationToLanguageId = zh.RevisionId, },                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = it.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (!ja.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Japanese", TranslationToLanguageId = en.RevisionId, },                     // #1  Language = "English"
//                    new CreateLanguageName { Text = "japonés", TranslationToLanguageId = es.RevisionId, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Japaner", TranslationToLanguageId = de.RevisionId, },                       // #3  Language = "German"
//                    new CreateLanguageName { Text = "اليابانية", TranslationToLanguageId = ar.RevisionId, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Japannese", TranslationToLanguageId = af.RevisionId, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "yapon", TranslationToLanguageId = az.RevisionId, },                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Японская", TranslationToLanguageId = be.RevisionId, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "японски", TranslationToLanguageId = bg.RevisionId, },                       // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "জাপানি", TranslationToLanguageId = bn.RevisionId, },                         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "japonès", TranslationToLanguageId = ca.RevisionId, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Japonec", TranslationToLanguageId = cs.RevisionId, },                       // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Siapaneaidd", TranslationToLanguageId = cy.RevisionId, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Japansk", TranslationToLanguageId = da.RevisionId, },                       // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "jaapani", TranslationToLanguageId = et.RevisionId, },                       // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Japoniako", TranslationToLanguageId = eu.RevisionId, },                     // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ژاپنی", TranslationToLanguageId = fa.RevisionId, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "japanilainen", TranslationToLanguageId = fi.RevisionId, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "japonaise", TranslationToLanguageId = fr.RevisionId, },                     // #18 Language = "French"
//                    new CreateLanguageName { Text = "Seapáinis", TranslationToLanguageId = ga.RevisionId, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "xaponés", TranslationToLanguageId = gl.RevisionId, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "જાપાનીઝ", TranslationToLanguageId = gu.RevisionId, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "יפנית", TranslationToLanguageId = he.RevisionId, },                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "जापानी", TranslationToLanguageId = hi.RevisionId, },                         // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "japanski", TranslationToLanguageId = hr.RevisionId, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Japonè", TranslationToLanguageId = ht.RevisionId, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "japán", TranslationToLanguageId = hu.RevisionId, },                         // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ճապոնացի", TranslationToLanguageId = hy.RevisionId, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Jepang", TranslationToLanguageId = id.RevisionId, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "japanska", TranslationToLanguageId = isLanguage.RevisionId, },              // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "giapponese", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "日本", TranslationToLanguageId = ja.RevisionId, },                          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "იაპონური", TranslationToLanguageId = ka.RevisionId, },                     // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಜಪಾನಿನವನು", TranslationToLanguageId = kn.RevisionId, },                     // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "일본어", TranslationToLanguageId = ko.RevisionId, },                        // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Iaponica", TranslationToLanguageId = la.RevisionId, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Japonijos", TranslationToLanguageId = lt.RevisionId, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Japānas", TranslationToLanguageId = lv.RevisionId, },                       // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "јапонски", TranslationToLanguageId = mk.RevisionId, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Jepun", TranslationToLanguageId = ms.RevisionId, },                         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ġappuniż", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Japans", TranslationToLanguageId = nl.RevisionId, },                        // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Japanese", TranslationToLanguageId = no.RevisionId, },                      // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "japoński", TranslationToLanguageId = pl.RevisionId, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "japonês", TranslationToLanguageId = pt.RevisionId, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "japonez", TranslationToLanguageId = ro.RevisionId, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "японский", TranslationToLanguageId = ru.RevisionId, },                      // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Japonec", TranslationToLanguageId = sk.RevisionId, },                       // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "japonski", TranslationToLanguageId = sl.RevisionId, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "japonisht", TranslationToLanguageId = sq.RevisionId, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јапански", TranslationToLanguageId = sr.RevisionId, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Japanska", TranslationToLanguageId = sv.RevisionId, },                      // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kijapani", TranslationToLanguageId = sw.RevisionId, },                      // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஜப்பனீஸ்", TranslationToLanguageId = ta.RevisionId, },                    // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "జాపనీస్", TranslationToLanguageId = te.RevisionId, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาญี่ปุ่น", TranslationToLanguageId = th.RevisionId, },                       // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Japon", TranslationToLanguageId = tr.RevisionId, },                         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Японський", TranslationToLanguageId = uk.RevisionId, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "جاپانی", TranslationToLanguageId = ur.RevisionId, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Nhật Bản", TranslationToLanguageId = vi.RevisionId, },                      // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "יאַפּאַניש", TranslationToLanguageId = yi.RevisionId, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "日本", TranslationToLanguageId = zh.RevisionId, },                          // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ja.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ka.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Georgian", TranslationToLanguageId = en.RevisionId, },                       // #1  Language = "English"
//                    new CreateLanguageName { Text = "georgiano", TranslationToLanguageId = es.RevisionId, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Georgier", TranslationToLanguageId = de.RevisionId, },                       // #3  Language = "German"
//                    new CreateLanguageName { Text = "الجورجية", TranslationToLanguageId = ar.RevisionId, },                      // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Georgiaans", TranslationToLanguageId = af.RevisionId, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "gürcü", TranslationToLanguageId = az.RevisionId, },                          // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "грузінскі", TranslationToLanguageId = be.RevisionId, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "грузински", TranslationToLanguageId = bg.RevisionId, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "জর্জিয়াদেশীয়", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "georgià", TranslationToLanguageId = ca.RevisionId, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "gruzínský", TranslationToLanguageId = cs.RevisionId, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Georgaidd", TranslationToLanguageId = cy.RevisionId, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "georgisk", TranslationToLanguageId = da.RevisionId, },                       // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "gruusia", TranslationToLanguageId = et.RevisionId, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Georgian", TranslationToLanguageId = eu.RevisionId, },                       // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "گرجی", TranslationToLanguageId = fa.RevisionId, },                          // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "georgialainen", TranslationToLanguageId = fi.RevisionId, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "géorgienne", TranslationToLanguageId = fr.RevisionId, },                     // #18 Language = "French"
//                    new CreateLanguageName { Text = "Seoirsis", TranslationToLanguageId = ga.RevisionId, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "xeorxiano", TranslationToLanguageId = gl.RevisionId, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "જ્યોર્જિયન", TranslationToLanguageId = gu.RevisionId, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "גאורגיה", TranslationToLanguageId = he.RevisionId, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "जार्जकालीन", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "gruzijski", TranslationToLanguageId = hr.RevisionId, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "georgian", TranslationToLanguageId = ht.RevisionId, },                       // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "grúz", TranslationToLanguageId = hu.RevisionId, },                           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "վրացերեն", TranslationToLanguageId = hy.RevisionId, },                      // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Georgia", TranslationToLanguageId = id.RevisionId, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Georgian", TranslationToLanguageId = isLanguage.RevisionId, },               // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "georgiano", TranslationToLanguageId = it.RevisionId, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ジョージアン", TranslationToLanguageId = ja.RevisionId, },                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "საქართველოს", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಜಾರ್ಜಿಯನ್", TranslationToLanguageId = kn.RevisionId, },                       // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "그루지야의", TranslationToLanguageId = ko.RevisionId, },                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Pontica", TranslationToLanguageId = la.RevisionId, },                        // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "gruzinų", TranslationToLanguageId = lt.RevisionId, },                        // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "gruzīnu", TranslationToLanguageId = lv.RevisionId, },                        // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Грузија", TranslationToLanguageId = mk.RevisionId, },                        // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Georgia", TranslationToLanguageId = ms.RevisionId, },                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ġorġjan", TranslationToLanguageId = mt.RevisionId, },                        // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Georgisch", TranslationToLanguageId = nl.RevisionId, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "georgisk", TranslationToLanguageId = no.RevisionId, },                       // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "gruziński", TranslationToLanguageId = pl.RevisionId, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "georgiano", TranslationToLanguageId = pt.RevisionId, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "georgian", TranslationToLanguageId = ro.RevisionId, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "грузинский", TranslationToLanguageId = ru.RevisionId, },                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "gruzínsky", TranslationToLanguageId = sk.RevisionId, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "gruzijski", TranslationToLanguageId = sl.RevisionId, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "gjeorgjian", TranslationToLanguageId = sq.RevisionId, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "грузијски", TranslationToLanguageId = sr.RevisionId, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "georgiska", TranslationToLanguageId = sv.RevisionId, },                      // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kijiojia", TranslationToLanguageId = sw.RevisionId, },                       // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஜோர்ஜிய", TranslationToLanguageId = ta.RevisionId, },                     // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "జార్జియన్", TranslationToLanguageId = te.RevisionId, },                         // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "จอร์เจีย", TranslationToLanguageId = th.RevisionId, },                          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Gürcü", TranslationToLanguageId = tr.RevisionId, },                          // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Грузинський", TranslationToLanguageId = uk.RevisionId, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "جارجیا", TranslationToLanguageId = ur.RevisionId, },                        // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Georgia", TranslationToLanguageId = vi.RevisionId, },                        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "גרוזיניש", TranslationToLanguageId = yi.RevisionId, },                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "格鲁吉亚", TranslationToLanguageId = zh.RevisionId, },                        // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ka.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!kn.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = en.RevisionId, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "Canarés", TranslationToLanguageId = es.RevisionId, },                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = de.RevisionId, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "الكانادا", TranslationToLanguageId = ar.RevisionId, },              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = af.RevisionId, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = az.RevisionId, },                // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = be.RevisionId, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = bg.RevisionId, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "কন্নড", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = ca.RevisionId, },                // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = cs.RevisionId, },                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = cy.RevisionId, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = da.RevisionId, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = et.RevisionId, },                // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "kannadera", TranslationToLanguageId = eu.RevisionId, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کانادهای", TranslationToLanguageId = fa.RevisionId, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = fr.RevisionId, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Cannadais", TranslationToLanguageId = ga.RevisionId, },              // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Canara", TranslationToLanguageId = gl.RevisionId, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કન્નડા", TranslationToLanguageId = gu.RevisionId, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קנאדה", TranslationToLanguageId = he.RevisionId, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "कन्नड़", TranslationToLanguageId = hi.RevisionId, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = hr.RevisionId, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = ht.RevisionId, },                // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = hu.RevisionId, },                // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "կաննադա", TranslationToLanguageId = hy.RevisionId, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = id.RevisionId, },                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = isLanguage.RevisionId, },        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = it.RevisionId, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "カンナダ語", TranslationToLanguageId = ja.RevisionId, },              // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "კანადა", TranslationToLanguageId = ka.RevisionId, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕನ್ನಡ", TranslationToLanguageId = kn.RevisionId, },                   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "칸나다어", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Kannadica", TranslationToLanguageId = la.RevisionId, },              // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "kanadų", TranslationToLanguageId = lt.RevisionId, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = lv.RevisionId, },                // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Канада", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = ms.RevisionId, },                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = mt.RevisionId, },                // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = nl.RevisionId, },                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = no.RevisionId, },                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = pl.RevisionId, },                // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "canará", TranslationToLanguageId = pt.RevisionId, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = ro.RevisionId, },                // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = ru.RevisionId, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sk.RevisionId, },                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "kannada", TranslationToLanguageId = sl.RevisionId, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sq.RevisionId, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = sr.RevisionId, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sv.RevisionId, },                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கன்னடம்", TranslationToLanguageId = ta.RevisionId, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "కన్నడ", TranslationToLanguageId = te.RevisionId, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "กันนาดา", TranslationToLanguageId = th.RevisionId, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = tr.RevisionId, },                // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "каннада", TranslationToLanguageId = uk.RevisionId, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کناڈا", TranslationToLanguageId = ur.RevisionId, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Kannada", TranslationToLanguageId = vi.RevisionId, },                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קאַנאַדאַ", TranslationToLanguageId = yi.RevisionId, },                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "卡纳达语", TranslationToLanguageId = zh.RevisionId, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ko.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = en.RevisionId, },                                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "coreano", TranslationToLanguageId = es.RevisionId, },                                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Koreanisch", TranslationToLanguageId = de.RevisionId, },                              // #3  Language = "German"
//                    new CreateLanguageName { Text = "كوري", TranslationToLanguageId = ar.RevisionId, },                                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Koreaanse", TranslationToLanguageId = af.RevisionId, },                               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Koreya", TranslationToLanguageId = az.RevisionId, },                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Карэйская", TranslationToLanguageId = be.RevisionId, },                               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "корейски", TranslationToLanguageId = bg.RevisionId, },                                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "কোরিয়ান", TranslationToLanguageId = bn.RevisionId, },                                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "coreà", TranslationToLanguageId = ca.RevisionId, },                                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "korejský", TranslationToLanguageId = cs.RevisionId, },                                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Corea", TranslationToLanguageId = cy.RevisionId, },                                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = da.RevisionId, },                                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "korea", TranslationToLanguageId = et.RevisionId, },                                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = eu.RevisionId, },                                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "کره ای", TranslationToLanguageId = fa.RevisionId, },                                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = fi.RevisionId, },                                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "coréenne", TranslationToLanguageId = fr.RevisionId, },                                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Cóiréis", TranslationToLanguageId = ga.RevisionId, },                                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Coreano", TranslationToLanguageId = gl.RevisionId, },                                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "કોરિયન", TranslationToLanguageId = gu.RevisionId, },                                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "קוריאה", TranslationToLanguageId = he.RevisionId, },                                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "कोरियाई", TranslationToLanguageId = hi.RevisionId, },                                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "korejski", TranslationToLanguageId = hr.RevisionId, },                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Kore", TranslationToLanguageId = ht.RevisionId, },                                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "koreai", TranslationToLanguageId = hu.RevisionId, },                                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "կորեական", TranslationToLanguageId = hy.RevisionId, },                               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Korea", TranslationToLanguageId = id.RevisionId, },                                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Kóreumaður", TranslationToLanguageId = isLanguage.RevisionId, },                      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "coreano", TranslationToLanguageId = it.RevisionId, },                                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "韓国", TranslationToLanguageId = ja.RevisionId, },                                    // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "კორეის", TranslationToLanguageId = ka.RevisionId, },                                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಕೊರಿಯಾ ದೇಶದವನು ಯಾ ಆ ದೇಶದ ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, },        // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "한국의", TranslationToLanguageId = ko.RevisionId, },                                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Lorem", TranslationToLanguageId = la.RevisionId, },                                   // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "korėjiečių", TranslationToLanguageId = lt.RevisionId, },                              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "korejiešu", TranslationToLanguageId = lv.RevisionId, },                               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Кореја", TranslationToLanguageId = mk.RevisionId, },                                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Korea", TranslationToLanguageId = ms.RevisionId, },                                   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Koreana", TranslationToLanguageId = mt.RevisionId, },                                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Koreaans", TranslationToLanguageId = nl.RevisionId, },                                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "koreansk", TranslationToLanguageId = no.RevisionId, },                                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "koreański", TranslationToLanguageId = pl.RevisionId, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "coreano", TranslationToLanguageId = pt.RevisionId, },                                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "coreean", TranslationToLanguageId = ro.RevisionId, },                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "корейский", TranslationToLanguageId = ru.RevisionId, },                               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "kórejský", TranslationToLanguageId = sk.RevisionId, },                                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Korean", TranslationToLanguageId = sl.RevisionId, },                                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "korean", TranslationToLanguageId = sq.RevisionId, },                                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "корејски", TranslationToLanguageId = sr.RevisionId, },                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "koreanska", TranslationToLanguageId = sv.RevisionId, },                               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kikorea", TranslationToLanguageId = sw.RevisionId, },                                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "கொரிய", TranslationToLanguageId = ta.RevisionId, },                                // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "కొరియా", TranslationToLanguageId = te.RevisionId, },                                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกาหลี", TranslationToLanguageId = th.RevisionId, },                                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Kore", TranslationToLanguageId = tr.RevisionId, },                                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Корейська", TranslationToLanguageId = uk.RevisionId, },                               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "کورین", TranslationToLanguageId = ur.RevisionId, },                                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Hàn Quốc", TranslationToLanguageId = vi.RevisionId, },                                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "קאָרעיִש", TranslationToLanguageId = yi.RevisionId, },                                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "韩国", TranslationToLanguageId = zh.RevisionId, },                                    // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ko.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!la.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = en.RevisionId, },                       // #1  Language = "English"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = es.RevisionId, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Latein", TranslationToLanguageId = de.RevisionId, },                      // #3  Language = "German"
//                    new CreateLanguageName { Text = "لاتينية", TranslationToLanguageId = ar.RevisionId, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Latyns-", TranslationToLanguageId = af.RevisionId, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "latın", TranslationToLanguageId = az.RevisionId, },                       // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "лацінскі", TranslationToLanguageId = be.RevisionId, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "латински", TranslationToLanguageId = bg.RevisionId, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ল্যাটিন", TranslationToLanguageId = bn.RevisionId, },                       // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "llatí", TranslationToLanguageId = ca.RevisionId, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "latina", TranslationToLanguageId = cs.RevisionId, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Lladin", TranslationToLanguageId = cy.RevisionId, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = da.RevisionId, },                       // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "ladina", TranslationToLanguageId = et.RevisionId, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = eu.RevisionId, },                       // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "لاتین", TranslationToLanguageId = fa.RevisionId, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Latinalaisen", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "latine", TranslationToLanguageId = fr.RevisionId, },                      // #18 Language = "French"
//                    new CreateLanguageName { Text = "لاتین", TranslationToLanguageId = ga.RevisionId, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = gl.RevisionId, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "લેટિન", TranslationToLanguageId = gu.RevisionId, },                        // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "לטינית", TranslationToLanguageId = he.RevisionId, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "लातौनी", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "latinski", TranslationToLanguageId = hr.RevisionId, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Latin lan", TranslationToLanguageId = ht.RevisionId, },                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "latin", TranslationToLanguageId = hu.RevisionId, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "լատիներեն", TranslationToLanguageId = hy.RevisionId, },                  // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = id.RevisionId, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = isLanguage.RevisionId, },               // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = it.RevisionId, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ラテン", TranslationToLanguageId = ja.RevisionId, },                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ლათინური", TranslationToLanguageId = ka.RevisionId, },                  // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಪ್ರಾಚೀನ ರೋಮನರ", TranslationToLanguageId = kn.RevisionId, },              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "라틴어", TranslationToLanguageId = ko.RevisionId, },                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "latine", TranslationToLanguageId = la.RevisionId, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "lotynų", TranslationToLanguageId = lt.RevisionId, },                      // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "latīņu", TranslationToLanguageId = lv.RevisionId, },                      // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Латинска", TranslationToLanguageId = mk.RevisionId, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = ms.RevisionId, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "latin", TranslationToLanguageId = mt.RevisionId, },                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Latijn", TranslationToLanguageId = nl.RevisionId, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = no.RevisionId, },                       // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "łacina", TranslationToLanguageId = pl.RevisionId, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "latino", TranslationToLanguageId = pt.RevisionId, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "latin", TranslationToLanguageId = ro.RevisionId, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "латинский", TranslationToLanguageId = ru.RevisionId, },                   // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "latinčina", TranslationToLanguageId = sk.RevisionId, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = sl.RevisionId, },                       // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "latinisht", TranslationToLanguageId = sq.RevisionId, },                   // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "латински", TranslationToLanguageId = sr.RevisionId, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "latinska", TranslationToLanguageId = sv.RevisionId, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kilatini", TranslationToLanguageId = sw.RevisionId, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "இலத்தீன் மொழி", TranslationToLanguageId = ta.RevisionId, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "లాటిన్", TranslationToLanguageId = te.RevisionId, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ละติน", TranslationToLanguageId = th.RevisionId, },                        // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = tr.RevisionId, },                       // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Латинську", TranslationToLanguageId = uk.RevisionId, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "لاطینی", TranslationToLanguageId = ur.RevisionId, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Latin", TranslationToLanguageId = vi.RevisionId, },                       // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "לאַטייַן", TranslationToLanguageId = yi.RevisionId, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "拉丁美洲", TranslationToLanguageId = zh.RevisionId, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = la.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!lt.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = en.RevisionId, },                                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = es.RevisionId, },                                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Litauisch", TranslationToLanguageId = de.RevisionId, },                                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "لتواني", TranslationToLanguageId = ar.RevisionId, },                                       // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Litaus", TranslationToLanguageId = af.RevisionId, },                                        // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Litva", TranslationToLanguageId = az.RevisionId, },                                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Літоўскі", TranslationToLanguageId = be.RevisionId, },                                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "литовски", TranslationToLanguageId = bg.RevisionId, },                                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "লিথুনিয়ান", TranslationToLanguageId = bn.RevisionId, },                                       // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "lituà", TranslationToLanguageId = ca.RevisionId, },                                         // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "litevský", TranslationToLanguageId = cs.RevisionId, },                                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Lithwaneg", TranslationToLanguageId = cy.RevisionId, },                                     // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "litauisk", TranslationToLanguageId = da.RevisionId, },                                      // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "leedu", TranslationToLanguageId = et.RevisionId, },                                         // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Lithuaniera", TranslationToLanguageId = eu.RevisionId, },                                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان لیتوانی", TranslationToLanguageId = fa.RevisionId, },                                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Liettuan", TranslationToLanguageId = fi.RevisionId, },                                      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Lituanie", TranslationToLanguageId = fr.RevisionId, },                                      // #18 Language = "French"
//                    new CreateLanguageName { Text = "Liotuáinis", TranslationToLanguageId = ga.RevisionId, },                                    // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Lituano", TranslationToLanguageId = gl.RevisionId, },                                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "લિથુનિયન", TranslationToLanguageId = gu.RevisionId, },                                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ליטא", TranslationToLanguageId = he.RevisionId, },                                         // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "लिथुअनिअन की भाषा", TranslationToLanguageId = hi.RevisionId, },                             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "litvanski", TranslationToLanguageId = hr.RevisionId, },                                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Haitian", TranslationToLanguageId = ht.RevisionId, },                                       // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "litván", TranslationToLanguageId = hu.RevisionId, },                                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "լիտվական", TranslationToLanguageId = hy.RevisionId, },                                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = id.RevisionId, },                                     // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = isLanguage.RevisionId, },                            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = it.RevisionId, },                                       // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "リトアニア語", TranslationToLanguageId = ja.RevisionId, },                                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ლიტვის", TranslationToLanguageId = ka.RevisionId, },                                       // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರಷ್ಯಾದ ಬಾಲ್ಟಿಕ್ ಗಣರಾಜ್ಯವಾದ ಲಿಥುಯೇನಿಯದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguageId = kn.RevisionId, },   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "리투아니아의", TranslationToLanguageId = ko.RevisionId, },                                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Lithuaniae", TranslationToLanguageId = la.RevisionId, },                                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Lietuvos", TranslationToLanguageId = lt.RevisionId, },                                      // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Lietuvas", TranslationToLanguageId = lv.RevisionId, },                                      // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = mk.RevisionId, },                                     // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Lithuania", TranslationToLanguageId = ms.RevisionId, },                              // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Litwana", TranslationToLanguageId = mt.RevisionId, },                                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Litouws", TranslationToLanguageId = nl.RevisionId, },                                       // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = no.RevisionId, },                                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "litewski", TranslationToLanguageId = pl.RevisionId, },                                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = pt.RevisionId, },                                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba lituaniană", TranslationToLanguageId = ro.RevisionId, },                              // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "литовский", TranslationToLanguageId = ru.RevisionId, },                                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "litovský", TranslationToLanguageId = sk.RevisionId, },                                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "litovski", TranslationToLanguageId = sl.RevisionId, },                                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "lituanisht", TranslationToLanguageId = sq.RevisionId, },                                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = sr.RevisionId, },                                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "litauiska", TranslationToLanguageId = sv.RevisionId, },                                     // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kilithuania", TranslationToLanguageId = sw.RevisionId, },                                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "லிதுயேனியன்", TranslationToLanguageId = ta.RevisionId, },                                // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "లిథువేనియన్", TranslationToLanguageId = te.RevisionId, },                                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกี่ยวกับประเทศลิธัวเนีย", TranslationToLanguageId = th.RevisionId, },                               // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Litvanya", TranslationToLanguageId = tr.RevisionId, },                                      // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Литовська", TranslationToLanguageId = uk.RevisionId, },                                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "لتھواینین", TranslationToLanguageId = ur.RevisionId, },                                    // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = vi.RevisionId, },                                     // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ליטוויש", TranslationToLanguageId = yi.RevisionId, },                                      // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "立陶宛", TranslationToLanguageId = zh.RevisionId, },                                         // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lt.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!lv.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Latvian", TranslationToLanguageId = en.RevisionId, },                                          // #1  Language = "English"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = es.RevisionId, },                                          // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Litauisch", TranslationToLanguageId = de.RevisionId, },                                        // #3  Language = "German"
//                    new CreateLanguageName { Text = "لتواني", TranslationToLanguageId = ar.RevisionId, },                                          // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Litaus", TranslationToLanguageId = af.RevisionId, },                                           // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Litva", TranslationToLanguageId = az.RevisionId, },                                            // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Літоўскі", TranslationToLanguageId = be.RevisionId, },                                         // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "литовски", TranslationToLanguageId = bg.RevisionId, },                                         // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "লিথুনিয়ান", TranslationToLanguageId = bn.RevisionId, },                                          // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "lituà", TranslationToLanguageId = ca.RevisionId, },                                            // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "litevský", TranslationToLanguageId = cs.RevisionId, },                                         // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Lithwaneg", TranslationToLanguageId = cy.RevisionId, },                                        // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "litauisk", TranslationToLanguageId = da.RevisionId, },                                         // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "leedu", TranslationToLanguageId = et.RevisionId, },                                            // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Lithuaniera", TranslationToLanguageId = eu.RevisionId, },                                      // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان لیتوانی", TranslationToLanguageId = fa.RevisionId, },                                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Liettuan", TranslationToLanguageId = fi.RevisionId, },                                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Lituanie", TranslationToLanguageId = fr.RevisionId, },                                         // #18 Language = "French"
//                    new CreateLanguageName { Text = "Liotuáinis", TranslationToLanguageId = ga.RevisionId, },                                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Lituano", TranslationToLanguageId = gl.RevisionId, },                                          // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "લિથુનિયન", TranslationToLanguageId = gu.RevisionId, },                                         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ליטא", TranslationToLanguageId = he.RevisionId, },                                            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "लिथुअनिअन की भाषा", TranslationToLanguageId = hi.RevisionId, },                                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "litvanski", TranslationToLanguageId = hr.RevisionId, },                                        // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Haitian", TranslationToLanguageId = ht.RevisionId, },                                          // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "litván", TranslationToLanguageId = hu.RevisionId, },                                           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "լիտվական", TranslationToLanguageId = hy.RevisionId, },                                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = id.RevisionId, },                                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = isLanguage.RevisionId, },                               // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = it.RevisionId, },                                          // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "リトアニア語", TranslationToLanguageId = ja.RevisionId, },                                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ლიტვის", TranslationToLanguageId = ka.RevisionId, },                                          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರಷ್ಯಾದ ಬಾಲ್ಟಿಕ್ ಗಣರಾಜ್ಯವಾದ ಲಿಥುಯೇನಿಯದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguageId = kn.RevisionId, },      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "리투아니아의", TranslationToLanguageId = ko.RevisionId, },                                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Lithuaniae", TranslationToLanguageId = la.RevisionId, },                                       // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Lietuvos", TranslationToLanguageId = lt.RevisionId, },                                         // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Lietuvas", TranslationToLanguageId = lv.RevisionId, },                                         // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = mk.RevisionId, },                                        // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Lithuania", TranslationToLanguageId = ms.RevisionId, },                                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Litwana", TranslationToLanguageId = mt.RevisionId, },                                          // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Litouws", TranslationToLanguageId = nl.RevisionId, },                                          // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Lithuanian", TranslationToLanguageId = no.RevisionId, },                                       // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "litewski", TranslationToLanguageId = pl.RevisionId, },                                         // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "lituano", TranslationToLanguageId = pt.RevisionId, },                                          // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "limba lituaniană", TranslationToLanguageId = ro.RevisionId, },                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "литовский", TranslationToLanguageId = ru.RevisionId, },                                        // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "litovský", TranslationToLanguageId = sk.RevisionId, },                                         // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "litovski", TranslationToLanguageId = sl.RevisionId, },                                         // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "lituanisht", TranslationToLanguageId = sq.RevisionId, },                                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "литвански", TranslationToLanguageId = sr.RevisionId, },                                        // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "litauiska", TranslationToLanguageId = sv.RevisionId, },                                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kilithuania", TranslationToLanguageId = sw.RevisionId, },                                      // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "லிதுயேனியன்", TranslationToLanguageId = ta.RevisionId, },                                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "లిథువేనియన్", TranslationToLanguageId = te.RevisionId, },                                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกี่ยวกับประเทศลิธัวเนีย", TranslationToLanguageId = th.RevisionId, },                                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Litvanya", TranslationToLanguageId = tr.RevisionId, },                                         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Литовська", TranslationToLanguageId = uk.RevisionId, },                                        // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "لتھواینین", TranslationToLanguageId = ur.RevisionId, },                                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Lithuania", TranslationToLanguageId = vi.RevisionId, },                                        // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ליטוויש", TranslationToLanguageId = yi.RevisionId, },                                         // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "立陶宛", TranslationToLanguageId = zh.RevisionId, },                                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!mk.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Macedonian", TranslationToLanguageId = en.RevisionId, },                 // #1  Language = "English"
//                    new CreateLanguageName { Text = "macedonio", TranslationToLanguageId = es.RevisionId, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Mazedonier", TranslationToLanguageId = de.RevisionId, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "المقدونية", TranslationToLanguageId = ar.RevisionId, },                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Macedonies", TranslationToLanguageId = af.RevisionId, },                 // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "makedoniyalı", TranslationToLanguageId = az.RevisionId, },               // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "македонскай", TranslationToLanguageId = be.RevisionId, },                // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "македонски", TranslationToLanguageId = bg.RevisionId, },                 // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "মাসিডনের লোক", TranslationToLanguageId = bn.RevisionId, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "macedoni", TranslationToLanguageId = ca.RevisionId, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "makedonský", TranslationToLanguageId = cs.RevisionId, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Macedoneg", TranslationToLanguageId = cy.RevisionId, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "makedonsk", TranslationToLanguageId = da.RevisionId, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Makedoonia", TranslationToLanguageId = et.RevisionId, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Mazedonierara", TranslationToLanguageId = eu.RevisionId, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مقدونی", TranslationToLanguageId = fa.RevisionId, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Makedonian", TranslationToLanguageId = fi.RevisionId, },                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "macédoniennes", TranslationToLanguageId = fr.RevisionId, },              // #18 Language = "French"
//                    new CreateLanguageName { Text = "Macadóinis", TranslationToLanguageId = ga.RevisionId, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Macedonia", TranslationToLanguageId = gl.RevisionId, },                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "મેસેડોનિયન", TranslationToLanguageId = gu.RevisionId, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "מקדוניה", TranslationToLanguageId = he.RevisionId, },                   // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "मेसीडोनियन", TranslationToLanguageId = hi.RevisionId, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "makedonski", TranslationToLanguageId = hr.RevisionId, },                 // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Masedonyen", TranslationToLanguageId = ht.RevisionId, },                 // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "macedóniai", TranslationToLanguageId = hu.RevisionId, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "մակեդոներեն", TranslationToLanguageId = hy.RevisionId, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Macedonia", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "makedónska", TranslationToLanguageId = isLanguage.RevisionId, },         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "macedone", TranslationToLanguageId = it.RevisionId, },                   // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "マケドニア語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "მაკედონიის", TranslationToLanguageId = ka.RevisionId, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಮೆಸಿಡೋನಿಯನ್", TranslationToLanguageId = kn.RevisionId, },                // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "마케도니아의", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Macedonum", TranslationToLanguageId = la.RevisionId, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Makedonijos", TranslationToLanguageId = lt.RevisionId, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "maķedoniešu", TranslationToLanguageId = lv.RevisionId, },                // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "македонски", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Macedonia", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Maċedonjan", TranslationToLanguageId = mt.RevisionId, },                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Macedonisch", TranslationToLanguageId = nl.RevisionId, },                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "makedonsk", TranslationToLanguageId = no.RevisionId, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "macedoński", TranslationToLanguageId = pl.RevisionId, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "macedônia", TranslationToLanguageId = pt.RevisionId, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "macedonean", TranslationToLanguageId = ro.RevisionId, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "македонский", TranslationToLanguageId = ru.RevisionId, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "macedónsky", TranslationToLanguageId = sk.RevisionId, },                 // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "makedonski", TranslationToLanguageId = sl.RevisionId, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "maqedonas", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "македонски", TranslationToLanguageId = sr.RevisionId, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "makedonska", TranslationToLanguageId = sv.RevisionId, },                 // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kimasedonia", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மாஸிடோனியன்", TranslationToLanguageId = ta.RevisionId, },          // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "మసడోనియన్", TranslationToLanguageId = te.RevisionId, },                 // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "มาซิโดเนีย", TranslationToLanguageId = th.RevisionId, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Makedonya", TranslationToLanguageId = tr.RevisionId, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Македонський", TranslationToLanguageId = uk.RevisionId, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "مقدونيائی", TranslationToLanguageId = ur.RevisionId, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Macedonia", TranslationToLanguageId = vi.RevisionId, },            // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "מאַקעדאָניש", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "马其顿", TranslationToLanguageId = zh.RevisionId, },                      // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mk.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ms.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = en.RevisionId, },                        // #1  Language = "English"
//                    new CreateLanguageName { Text = "malayo", TranslationToLanguageId = es.RevisionId, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "malaiisch", TranslationToLanguageId = de.RevisionId, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الملايو", TranslationToLanguageId = ar.RevisionId, },                      // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Maleis", TranslationToLanguageId = af.RevisionId, },                       // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "malay", TranslationToLanguageId = az.RevisionId, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Малайская", TranslationToLanguageId = be.RevisionId, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "малайски", TranslationToLanguageId = bg.RevisionId, },                     // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "মালয়দেশীয় লোক", TranslationToLanguageId = bn.RevisionId, },                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "malai", TranslationToLanguageId = ca.RevisionId, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Malajská", TranslationToLanguageId = cs.RevisionId, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = cy.RevisionId, },                        // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = da.RevisionId, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "malai", TranslationToLanguageId = et.RevisionId, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Malaysiera", TranslationToLanguageId = eu.RevisionId, },                   // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "مالایا", TranslationToLanguageId = fa.RevisionId, },                       // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "malaiji", TranslationToLanguageId = fi.RevisionId, },                      // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "malais", TranslationToLanguageId = fr.RevisionId, },                       // #18 Language = "French"
//                    new CreateLanguageName { Text = "Malaeis", TranslationToLanguageId = ga.RevisionId, },                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "malaio", TranslationToLanguageId = gl.RevisionId, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "મલય", TranslationToLanguageId = gu.RevisionId, },                         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "מלאית", TranslationToLanguageId = he.RevisionId, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "मलायी", TranslationToLanguageId = hi.RevisionId, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "malajski", TranslationToLanguageId = hr.RevisionId, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "malay", TranslationToLanguageId = ht.RevisionId, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "maláj", TranslationToLanguageId = hu.RevisionId, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "մալայական", TranslationToLanguageId = hy.RevisionId, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Melayu", TranslationToLanguageId = id.RevisionId, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = isLanguage.RevisionId, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "malese", TranslationToLanguageId = it.RevisionId, },                       // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "マレー語", TranslationToLanguageId = ja.RevisionId, },                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "მალაური", TranslationToLanguageId = ka.RevisionId, },                     // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಮಲಯಾ ಭಾಷೆಯ", TranslationToLanguageId = kn.RevisionId, },                // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "말레이 사람", TranslationToLanguageId = ko.RevisionId, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Malaeorum", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "malajų", TranslationToLanguageId = lt.RevisionId, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "malajiešu valoda", TranslationToLanguageId = lv.RevisionId, },             // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "малајски", TranslationToLanguageId = mk.RevisionId, },                     // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Melayu", TranslationToLanguageId = ms.RevisionId, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Malajan", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Maleis", TranslationToLanguageId = nl.RevisionId, },                       // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = no.RevisionId, },                        // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "malajski", TranslationToLanguageId = pl.RevisionId, },                     // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "malaio", TranslationToLanguageId = pt.RevisionId, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "malaezian", TranslationToLanguageId = ro.RevisionId, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "малайский", TranslationToLanguageId = ru.RevisionId, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "malajzijská", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = sl.RevisionId, },                        // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "malajisht", TranslationToLanguageId = sq.RevisionId, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "малајски", TranslationToLanguageId = sr.RevisionId, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = sv.RevisionId, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = sw.RevisionId, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மலாய்", TranslationToLanguageId = ta.RevisionId, },                      // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "మలే", TranslationToLanguageId = te.RevisionId, },                          // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษามลายู", TranslationToLanguageId = th.RevisionId, },                      // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Malay", TranslationToLanguageId = tr.RevisionId, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "малайський", TranslationToLanguageId = uk.RevisionId, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "مالائی", TranslationToLanguageId = ur.RevisionId, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Mã Lai", TranslationToLanguageId = vi.RevisionId, },                       // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "מאַלייַיש", TranslationToLanguageId = yi.RevisionId, },                     // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "马来人", TranslationToLanguageId = zh.RevisionId, },                        // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ms.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!mt.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = en.RevisionId, },                      // #1  Language = "English"
//                    new CreateLanguageName { Text = "maltés", TranslationToLanguageId = es.RevisionId, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Malteser", TranslationToLanguageId = de.RevisionId, },                     // #3  Language = "German"
//                    new CreateLanguageName { Text = "المالطية", TranslationToLanguageId = ar.RevisionId, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = af.RevisionId, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = az.RevisionId, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "мальтыйская", TranslationToLanguageId = be.RevisionId, },                  // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "малтийски", TranslationToLanguageId = bg.RevisionId, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "মালটার ভাষা", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "maltès", TranslationToLanguageId = ca.RevisionId, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "maltese", TranslationToLanguageId = cs.RevisionId, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = cy.RevisionId, },                        // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "maltesiske", TranslationToLanguageId = da.RevisionId, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = et.RevisionId, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Maltako", TranslationToLanguageId = eu.RevisionId, },                      // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اهل مالت", TranslationToLanguageId = fa.RevisionId, },                    // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Maltan", TranslationToLanguageId = fi.RevisionId, },                       // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "maltais", TranslationToLanguageId = fr.RevisionId, },                      // #18 Language = "French"
//                    new CreateLanguageName { Text = "Mháltais", TranslationToLanguageId = ga.RevisionId, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Maltés", TranslationToLanguageId = gl.RevisionId, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "માલ્ટી", TranslationToLanguageId = gu.RevisionId, },                         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "מלטה", TranslationToLanguageId = he.RevisionId, },                        // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "मोलतिज़", TranslationToLanguageId = hi.RevisionId, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "malteški", TranslationToLanguageId = hr.RevisionId, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "maltese", TranslationToLanguageId = ht.RevisionId, },                      // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "máltai", TranslationToLanguageId = hu.RevisionId, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "մալթացի", TranslationToLanguageId = hy.RevisionId, },                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = id.RevisionId, },                        // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "maltneska", TranslationToLanguageId = isLanguage.RevisionId, },            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "maltese", TranslationToLanguageId = it.RevisionId, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "マルタ語", TranslationToLanguageId = ja.RevisionId, },                      // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "მალტური", TranslationToLanguageId = ka.RevisionId, },                    // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಮಾಲ್ಟಾ ದ್ವೀಪದ ನಿವಾಸಿ ಯಾ ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, },    // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "몰티즈", TranslationToLanguageId = ko.RevisionId, },                       // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Melitica", TranslationToLanguageId = la.RevisionId, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Maltos", TranslationToLanguageId = lt.RevisionId, },                       // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Maltas", TranslationToLanguageId = lv.RevisionId, },                       // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "малтешки", TranslationToLanguageId = mk.RevisionId, },                     // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = ms.RevisionId, },                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Malti", TranslationToLanguageId = mt.RevisionId, },                        // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Maltees", TranslationToLanguageId = nl.RevisionId, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = no.RevisionId, },                      // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "maltański", TranslationToLanguageId = pl.RevisionId, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "maltês", TranslationToLanguageId = pt.RevisionId, },                       // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "maltez", TranslationToLanguageId = ro.RevisionId, },                       // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "мальтийский", TranslationToLanguageId = ru.RevisionId, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Maltese", TranslationToLanguageId = sk.RevisionId, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "malteški", TranslationToLanguageId = sl.RevisionId, },                     // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "maltez", TranslationToLanguageId = sq.RevisionId, },                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "малтешки", TranslationToLanguageId = sr.RevisionId, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "maltesiska", TranslationToLanguageId = sv.RevisionId, },                   // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kimalta", TranslationToLanguageId = sw.RevisionId, },                      // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மால்டிஸ்", TranslationToLanguageId = ta.RevisionId, },                   // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "మాల్టీస్", TranslationToLanguageId = te.RevisionId, },                        // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เกี่ยวกับมอลตา", TranslationToLanguageId = th.RevisionId, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Malta", TranslationToLanguageId = tr.RevisionId, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Мальтійська", TranslationToLanguageId = uk.RevisionId, },                  // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "مالٹی", TranslationToLanguageId = ur.RevisionId, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Malta", TranslationToLanguageId = vi.RevisionId, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "מאלטיזיש", TranslationToLanguageId = yi.RevisionId, },                    // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "马耳他语", TranslationToLanguageId = zh.RevisionId, },                      // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mt.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!nl.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Dutch", TranslationToLanguageId = en.RevisionId, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "holandés", TranslationToLanguageId = es.RevisionId, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Holländer", TranslationToLanguageId = de.RevisionId, },                // #3  Language = "German"
//                    new CreateLanguageName { Text = "هولندي", TranslationToLanguageId = ar.RevisionId, },                  // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Nederlandse", TranslationToLanguageId = af.RevisionId, },              // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "holland", TranslationToLanguageId = az.RevisionId, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Галандская", TranslationToLanguageId = be.RevisionId, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "холандски", TranslationToLanguageId = bg.RevisionId, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ওলন্দাজি", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "holandès", TranslationToLanguageId = ca.RevisionId, },                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "holandský", TranslationToLanguageId = cs.RevisionId, },                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Iseldireg", TranslationToLanguageId = cy.RevisionId, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "hollandsk", TranslationToLanguageId = da.RevisionId, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hollandi", TranslationToLanguageId = et.RevisionId, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Nederlanderara", TranslationToLanguageId = eu.RevisionId, },           // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "هلندی", TranslationToLanguageId = fa.RevisionId, },                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "hollantilainen", TranslationToLanguageId = fi.RevisionId, },           // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "hollandaise", TranslationToLanguageId = fr.RevisionId, },              // #18 Language = "French"
//                    new CreateLanguageName { Text = "Ollainnis", TranslationToLanguageId = ga.RevisionId, },                // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Holandés", TranslationToLanguageId = gl.RevisionId, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "હૉલાન્ડની ડચ ભાષા", TranslationToLanguageId = gu.RevisionId, },         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הולנדי", TranslationToLanguageId = he.RevisionId, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "डच", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "nizozemski", TranslationToLanguageId = hr.RevisionId, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Olandè", TranslationToLanguageId = ht.RevisionId, },                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "holland", TranslationToLanguageId = hu.RevisionId, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հոլանդական", TranslationToLanguageId = hy.RevisionId, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Belanda", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Hollenska", TranslationToLanguageId = isLanguage.RevisionId, },        // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "olandese", TranslationToLanguageId = it.RevisionId, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "オランダ", TranslationToLanguageId = ja.RevisionId, },                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჰოლანდიელი", TranslationToLanguageId = ka.RevisionId, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಡಚ್", TranslationToLanguageId = kn.RevisionId, },                      // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "네덜란드", TranslationToLanguageId = ko.RevisionId, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Dutch", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Olandijos", TranslationToLanguageId = lt.RevisionId, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "holandiešu", TranslationToLanguageId = lv.RevisionId, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Холандија", TranslationToLanguageId = mk.RevisionId, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Belanda", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Olandiż", TranslationToLanguageId = mt.RevisionId, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Nederlands", TranslationToLanguageId = nl.RevisionId, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "nederlandsk", TranslationToLanguageId = no.RevisionId, },              // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "holenderski", TranslationToLanguageId = pl.RevisionId, },              // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "holandês", TranslationToLanguageId = pt.RevisionId, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "olandez", TranslationToLanguageId = ro.RevisionId, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "голландский", TranslationToLanguageId = ru.RevisionId, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Holandský", TranslationToLanguageId = sk.RevisionId, },                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "nizozemski", TranslationToLanguageId = sl.RevisionId, },               // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "holandez", TranslationToLanguageId = sq.RevisionId, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "холандски", TranslationToLanguageId = sr.RevisionId, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "nederländska", TranslationToLanguageId = sv.RevisionId, },             // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiholanzi", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "உலாந்திய", TranslationToLanguageId = ta.RevisionId, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "డచ్", TranslationToLanguageId = te.RevisionId, },                      // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ดัตช์", TranslationToLanguageId = th.RevisionId, },                      // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Hollandalı", TranslationToLanguageId = tr.RevisionId, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Голландська", TranslationToLanguageId = uk.RevisionId, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ڈچ", TranslationToLanguageId = ur.RevisionId, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Hà Lan", TranslationToLanguageId = vi.RevisionId, },                   // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "האָלענדיש", TranslationToLanguageId = yi.RevisionId, },                // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "荷兰", TranslationToLanguageId = zh.RevisionId, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nl.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!no.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Norwegian", TranslationToLanguageId = en.RevisionId, },                     // #1  Language = "English"
//                    new CreateLanguageName { Text = "noruego", TranslationToLanguageId = es.RevisionId, },                       // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Norwegisch", TranslationToLanguageId = de.RevisionId, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "النرويجية", TranslationToLanguageId = ar.RevisionId, },                    // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Noorse", TranslationToLanguageId = af.RevisionId, },                        // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "norveçli", TranslationToLanguageId = az.RevisionId, },                      // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Нарвежская", TranslationToLanguageId = be.RevisionId, },                    // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "норвежки", TranslationToLanguageId = bg.RevisionId, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "নরওয়েবাসী", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "noruec", TranslationToLanguageId = ca.RevisionId, },                        // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "norský", TranslationToLanguageId = cs.RevisionId, },                        // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Norwy", TranslationToLanguageId = cy.RevisionId, },                         // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "norske", TranslationToLanguageId = da.RevisionId, },                        // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "norra", TranslationToLanguageId = et.RevisionId, },                         // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Norvegiako", TranslationToLanguageId = eu.RevisionId, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "نروژی", TranslationToLanguageId = fa.RevisionId, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Norja", TranslationToLanguageId = fi.RevisionId, },                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "norvégiennes", TranslationToLanguageId = fr.RevisionId, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Ioruais", TranslationToLanguageId = ga.RevisionId, },                       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Noruego", TranslationToLanguageId = gl.RevisionId, },                       // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "નોર્વેજિયન", TranslationToLanguageId = gu.RevisionId, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "נורווגית", TranslationToLanguageId = he.RevisionId, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "नार्वेजियन", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "norveški", TranslationToLanguageId = hr.RevisionId, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Nòvejyen", TranslationToLanguageId = ht.RevisionId, },                      // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "norvég", TranslationToLanguageId = hu.RevisionId, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "նորվեգերեն", TranslationToLanguageId = hy.RevisionId, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Norwegia", TranslationToLanguageId = id.RevisionId, },                      // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Norska", TranslationToLanguageId = isLanguage.RevisionId, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "norvegese", TranslationToLanguageId = it.RevisionId, },                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ノルウェー", TranslationToLanguageId = ja.RevisionId, },                     // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ნორვეგიის", TranslationToLanguageId = ka.RevisionId, },                    // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ನಾರ್ವೇ ದೇಶದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguageId = kn.RevisionId, },         // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "노르웨이의", TranslationToLanguageId = ko.RevisionId, },                     // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Norvegica", TranslationToLanguageId = la.RevisionId, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Norvegijos", TranslationToLanguageId = lt.RevisionId, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Norvēģijas", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Норвешка", TranslationToLanguageId = mk.RevisionId, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Norway", TranslationToLanguageId = ms.RevisionId, },                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Norveġiż", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Noors", TranslationToLanguageId = nl.RevisionId, },                         // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "norske", TranslationToLanguageId = no.RevisionId, },                        // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "norweski", TranslationToLanguageId = pl.RevisionId, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "norueguês", TranslationToLanguageId = pt.RevisionId, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "norvegian", TranslationToLanguageId = ro.RevisionId, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "норвежский", TranslationToLanguageId = ru.RevisionId, },                    // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Nórsky", TranslationToLanguageId = sk.RevisionId, },                        // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "norveški", TranslationToLanguageId = sl.RevisionId, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "norvegjez", TranslationToLanguageId = sq.RevisionId, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "норвешки", TranslationToLanguageId = sr.RevisionId, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "norska", TranslationToLanguageId = sv.RevisionId, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Norway", TranslationToLanguageId = sw.RevisionId, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "நார்வேஜியன்", TranslationToLanguageId = ta.RevisionId, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "నార్వేజియన్", TranslationToLanguageId = te.RevisionId, },                     // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "นอร์เวย์", TranslationToLanguageId = th.RevisionId, },                         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Norveç", TranslationToLanguageId = tr.RevisionId, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Норвезька", TranslationToLanguageId = uk.RevisionId, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ناروے", TranslationToLanguageId = ur.RevisionId, },                        // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Na Uy", TranslationToLanguageId = vi.RevisionId, },                         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "נאָרוועגיש", TranslationToLanguageId = yi.RevisionId, },                    // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "挪威", TranslationToLanguageId = zh.RevisionId, },                          // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = no.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!pl.Names.Any())
//                new List<CreateLanguageName>
//                {

//                    new CreateLanguageName { Text = "Polish", TranslationToLanguageId = en.RevisionId, },                                               // #1  Language = "English"
//                    new CreateLanguageName { Text = "polaco", TranslationToLanguageId = es.RevisionId, },                                               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "polnisch", TranslationToLanguageId = de.RevisionId, },                                             // #3  Language = "German"
//                    new CreateLanguageName { Text = "بولندي", TranslationToLanguageId = ar.RevisionId, },                                              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Pools", TranslationToLanguageId = af.RevisionId, },                                                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "sürtmək", TranslationToLanguageId = az.RevisionId, },                                              // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Польскі", TranslationToLanguageId = be.RevisionId, },                                              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "лак", TranslationToLanguageId = bg.RevisionId, },                                                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "পালিশ", TranslationToLanguageId = bn.RevisionId, },                                                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "polonès", TranslationToLanguageId = ca.RevisionId, },                                              // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "polský", TranslationToLanguageId = cs.RevisionId, },                                               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Pwyleg", TranslationToLanguageId = cy.RevisionId, },                                               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "polsk", TranslationToLanguageId = da.RevisionId, },                                                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "poola", TranslationToLanguageId = et.RevisionId, },                                                // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Polonierara", TranslationToLanguageId = eu.RevisionId, },                                          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "لهستانی", TranslationToLanguageId = fa.RevisionId, },                                             // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "puola", TranslationToLanguageId = fi.RevisionId, },                                                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "polonaise", TranslationToLanguageId = fr.RevisionId, },                                            // #18 Language = "French"
//                    new CreateLanguageName { Text = "Polainnis", TranslationToLanguageId = ga.RevisionId, },                                            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Polaco", TranslationToLanguageId = gl.RevisionId, },                                               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઘસીને ઘસાઈને લીસું ચકચકિત કરવું કે થવું ઓપવું કે ઓપાવવું", TranslationToLanguageId = gu.RevisionId, },   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פולני", TranslationToLanguageId = he.RevisionId, },                                               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "पोलिश", TranslationToLanguageId = hi.RevisionId, },                                                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "poljski", TranslationToLanguageId = hr.RevisionId, },                                              // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Polonè", TranslationToLanguageId = ht.RevisionId, },                                               // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "lengyel", TranslationToLanguageId = hu.RevisionId, },                                              // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "հղկում", TranslationToLanguageId = hy.RevisionId, },                                               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Polandia", TranslationToLanguageId = id.RevisionId, },                                             // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "pólskur", TranslationToLanguageId = isLanguage.RevisionId, },                                      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "polacco", TranslationToLanguageId = it.RevisionId, },                                              // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ポーランド", TranslationToLanguageId = ja.RevisionId, },                                            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "პოლონეთის", TranslationToLanguageId = ka.RevisionId, },                                          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಒಪ್ಪಹಾಕು", TranslationToLanguageId = kn.RevisionId, },                                              // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "폴란드의", TranslationToLanguageId = ko.RevisionId, },                                              // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Polonica", TranslationToLanguageId = la.RevisionId, },                                             // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Lenkijos", TranslationToLanguageId = lt.RevisionId, },                                             // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Polijas", TranslationToLanguageId = lv.RevisionId, },                                              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "полски", TranslationToLanguageId = mk.RevisionId, },                                               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Poland", TranslationToLanguageId = ms.RevisionId, },                                        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Pollakk", TranslationToLanguageId = mt.RevisionId, },                                              // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Pools", TranslationToLanguageId = nl.RevisionId, },                                                // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "polsk", TranslationToLanguageId = no.RevisionId, },                                                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "polski", TranslationToLanguageId = pl.RevisionId, },                                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "polonês", TranslationToLanguageId = pt.RevisionId, },                                              // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "polonez", TranslationToLanguageId = ro.RevisionId, },                                              // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "польский", TranslationToLanguageId = ru.RevisionId, },                                             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Poľský", TranslationToLanguageId = sk.RevisionId, },                                               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "poljski", TranslationToLanguageId = sl.RevisionId, },                                              // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "polonisht", TranslationToLanguageId = sq.RevisionId, },                                            // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "пољски", TranslationToLanguageId = sr.RevisionId, },                                               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "polska", TranslationToLanguageId = sv.RevisionId, },                                               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kipolishi", TranslationToLanguageId = sw.RevisionId, },                                            // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "மினுக்கு", TranslationToLanguageId = ta.RevisionId, },                                             // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "సంస్కరించు", TranslationToLanguageId = te.RevisionId, },                                             // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ขัด", TranslationToLanguageId = th.RevisionId, },                                                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Polonya", TranslationToLanguageId = tr.RevisionId, },                                              // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Польський", TranslationToLanguageId = uk.RevisionId, },                                            // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "پولش", TranslationToLanguageId = ur.RevisionId, },                                                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ba Lan", TranslationToLanguageId = vi.RevisionId, },                                               // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פּויליש", TranslationToLanguageId = yi.RevisionId, },                                              // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "波兰", TranslationToLanguageId = zh.RevisionId, },                                                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pl.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!pt.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Portuguese", TranslationToLanguageId = en.RevisionId, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "portugués", TranslationToLanguageId = es.RevisionId, },                     // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Portugiesisch", TranslationToLanguageId = de.RevisionId, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "البرتغالية", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Portugees", TranslationToLanguageId = af.RevisionId, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Portuqaliya", TranslationToLanguageId = az.RevisionId, },                   // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Партугальская", TranslationToLanguageId = be.RevisionId, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "португалски", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "পটুর্গালদেশীয়", TranslationToLanguageId = bn.RevisionId, },                     // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "portuguès", TranslationToLanguageId = ca.RevisionId, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "portugalština", TranslationToLanguageId = cs.RevisionId, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Portiwgaleg", TranslationToLanguageId = cy.RevisionId, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Portugisisk", TranslationToLanguageId = da.RevisionId, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "portugali", TranslationToLanguageId = et.RevisionId, },                     // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Portuguese", TranslationToLanguageId = eu.RevisionId, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "پرتغالی", TranslationToLanguageId = fa.RevisionId, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "portugali", TranslationToLanguageId = fi.RevisionId, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "portugaise", TranslationToLanguageId = fr.RevisionId, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Portaingéilis", TranslationToLanguageId = ga.RevisionId, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Portugués", TranslationToLanguageId = gl.RevisionId, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "પોર્ટુગીઝ ભાષા", TranslationToLanguageId = gu.RevisionId, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "פורטוגזית", TranslationToLanguageId = he.RevisionId, },                    // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "पुर्तगाली", TranslationToLanguageId = hi.RevisionId, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "portugalisht", TranslationToLanguageId = hr.RevisionId, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Pòtigè", TranslationToLanguageId = ht.RevisionId, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "portugál", TranslationToLanguageId = hu.RevisionId, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "պորտուգալերեն", TranslationToLanguageId = hy.RevisionId, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Portugis", TranslationToLanguageId = id.RevisionId, },                      // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Portúgalska", TranslationToLanguageId = isLanguage.RevisionId, },           // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "portoghese", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ポルトガル", TranslationToLanguageId = ja.RevisionId, },                     // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "პორტუგალიის", TranslationToLanguageId = ka.RevisionId, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಪೋರ್ಚುಗೀಸ್", TranslationToLanguageId = kn.RevisionId, },                     // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "포르투갈어", TranslationToLanguageId = ko.RevisionId, },                     // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Portuguese", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "portugalų", TranslationToLanguageId = lt.RevisionId, },                     // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "portugāļu", TranslationToLanguageId = lv.RevisionId, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "португалски", TranslationToLanguageId = mk.RevisionId, },                   // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Portugis", TranslationToLanguageId = ms.RevisionId, },               // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Portugiż", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Portugees", TranslationToLanguageId = nl.RevisionId, },                     // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Portugisisk", TranslationToLanguageId = no.RevisionId, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "portugalski", TranslationToLanguageId = pl.RevisionId, },                   // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "português", TranslationToLanguageId = pt.RevisionId, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "portughez", TranslationToLanguageId = ro.RevisionId, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "португальский", TranslationToLanguageId = ru.RevisionId, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "portugalčina", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "portugalski", TranslationToLanguageId = sl.RevisionId, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "portugalisht", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "португалски", TranslationToLanguageId = sr.RevisionId, },                   // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "portugisiska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kireno", TranslationToLanguageId = sw.RevisionId, },                        // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "போர்த்துகீசியம்", TranslationToLanguageId = ta.RevisionId, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "పోర్చుగీసు", TranslationToLanguageId = te.RevisionId, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาโปรตุเกส", TranslationToLanguageId = th.RevisionId, },                     // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Portekizce", TranslationToLanguageId = tr.RevisionId, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Португальська", TranslationToLanguageId = uk.RevisionId, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "پرتگالی", TranslationToLanguageId = ur.RevisionId, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Bồ Đào Nha", TranslationToLanguageId = vi.RevisionId, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "פּאָרטוגעזיש", TranslationToLanguageId = yi.RevisionId, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "葡萄牙语", TranslationToLanguageId = zh.RevisionId, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pt.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ro.Names.Any())
//                new List<CreateLanguageName>
//                {

//                    new CreateLanguageName { Text = "Romanian", TranslationToLanguageId = en.RevisionId, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "rumano", TranslationToLanguageId = es.RevisionId, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Rumänisch", TranslationToLanguageId = de.RevisionId, },              // #3  Language = "German"
//                    new CreateLanguageName { Text = "رومانيا", TranslationToLanguageId = ar.RevisionId, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Roemeens", TranslationToLanguageId = af.RevisionId, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "rumın", TranslationToLanguageId = az.RevisionId, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "румынская", TranslationToLanguageId = be.RevisionId, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "румънски", TranslationToLanguageId = bg.RevisionId, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "রোমানিয়ান", TranslationToLanguageId = bn.RevisionId, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "romanès", TranslationToLanguageId = ca.RevisionId, },                // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "rumunský", TranslationToLanguageId = cs.RevisionId, },               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Rwmaneg", TranslationToLanguageId = cy.RevisionId, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "rumænsk", TranslationToLanguageId = da.RevisionId, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "rumeenia", TranslationToLanguageId = et.RevisionId, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Errumanierara", TranslationToLanguageId = eu.RevisionId, },          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "رومانیایی", TranslationToLanguageId = fa.RevisionId, },             // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Romanian", TranslationToLanguageId = fi.RevisionId, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "roumaine", TranslationToLanguageId = fr.RevisionId, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Rómáinis", TranslationToLanguageId = ga.RevisionId, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Romanés", TranslationToLanguageId = gl.RevisionId, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "રોમાનિયન", TranslationToLanguageId = gu.RevisionId, },               // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "רומניה", TranslationToLanguageId = he.RevisionId, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "रोमानियाई", TranslationToLanguageId = hi.RevisionId, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Rumunjski", TranslationToLanguageId = hr.RevisionId, },              // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "romanian", TranslationToLanguageId = ht.RevisionId, },               // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "román", TranslationToLanguageId = hu.RevisionId, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ռումիներեն", TranslationToLanguageId = hy.RevisionId, },             // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Rumania", TranslationToLanguageId = id.RevisionId, },                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "rúmensku", TranslationToLanguageId = isLanguage.RevisionId, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "rumeno", TranslationToLanguageId = it.RevisionId, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ルーマニア語", TranslationToLanguageId = ja.RevisionId, },            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "რუმინეთის", TranslationToLanguageId = ka.RevisionId, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರೊಮೇನಿಯನ್", TranslationToLanguageId = kn.RevisionId, },             // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "루마니아 사람", TranslationToLanguageId = ko.RevisionId, },           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Romanian", TranslationToLanguageId = la.RevisionId, },               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "rumunų", TranslationToLanguageId = lt.RevisionId, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "rumāņu", TranslationToLanguageId = lv.RevisionId, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "романскиот", TranslationToLanguageId = mk.RevisionId, },             // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Romania", TranslationToLanguageId = ms.RevisionId, },         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Rumen", TranslationToLanguageId = mt.RevisionId, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Roemeense", TranslationToLanguageId = nl.RevisionId, },              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "rumensk", TranslationToLanguageId = no.RevisionId, },                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "rumuński", TranslationToLanguageId = pl.RevisionId, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "romeno", TranslationToLanguageId = pt.RevisionId, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "român", TranslationToLanguageId = ro.RevisionId, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "румынский", TranslationToLanguageId = ru.RevisionId, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Rumunský", TranslationToLanguageId = sk.RevisionId, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "romunski", TranslationToLanguageId = sl.RevisionId, },               // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "rumun", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "румунски", TranslationToLanguageId = sr.RevisionId, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "rumänska", TranslationToLanguageId = sv.RevisionId, },               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiromania", TranslationToLanguageId = sw.RevisionId, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ரோமானியம்", TranslationToLanguageId = ta.RevisionId, },          // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "రోమేనియన్", TranslationToLanguageId = te.RevisionId, },               // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "โรมาเนีย", TranslationToLanguageId = th.RevisionId, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Romanya", TranslationToLanguageId = tr.RevisionId, },                // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Румунська", TranslationToLanguageId = uk.RevisionId, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "رومانیہ", TranslationToLanguageId = ur.RevisionId, },               // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Rumani", TranslationToLanguageId = vi.RevisionId, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "רומעניש", TranslationToLanguageId = yi.RevisionId, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "罗马尼亚", TranslationToLanguageId = zh.RevisionId, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ro.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ru.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Russian", TranslationToLanguageId = en.RevisionId, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "ruso", TranslationToLanguageId = es.RevisionId, },                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Russisch", TranslationToLanguageId = de.RevisionId, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "الروسية", TranslationToLanguageId = ar.RevisionId, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Russiese", TranslationToLanguageId = af.RevisionId, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "rus", TranslationToLanguageId = az.RevisionId, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "рускі", TranslationToLanguageId = be.RevisionId, },                  // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "руски", TranslationToLanguageId = bg.RevisionId, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "রাশিয়ান", TranslationToLanguageId = bn.RevisionId, },                 // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "rus", TranslationToLanguageId = ca.RevisionId, },                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Rus", TranslationToLanguageId = cs.RevisionId, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Rwsia", TranslationToLanguageId = cy.RevisionId, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "russisk", TranslationToLanguageId = da.RevisionId, },                // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "vene", TranslationToLanguageId = et.RevisionId, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Errusierara", TranslationToLanguageId = eu.RevisionId, },            // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "روسی", TranslationToLanguageId = fa.RevisionId, },                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Venäjän", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "russes", TranslationToLanguageId = fr.RevisionId, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Rúisis", TranslationToLanguageId = ga.RevisionId, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Ruso", TranslationToLanguageId = gl.RevisionId, },                   // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "રશિયાનું", TranslationToLanguageId = gu.RevisionId, },                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "רוסי", TranslationToLanguageId = he.RevisionId, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "रूसी", TranslationToLanguageId = hi.RevisionId, },                    // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "ruski", TranslationToLanguageId = hr.RevisionId, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Ris", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "orosz", TranslationToLanguageId = hu.RevisionId, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ռուսերեն", TranslationToLanguageId = hy.RevisionId, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Rusia", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Rússneska", TranslationToLanguageId = isLanguage.RevisionId, },      // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "russo", TranslationToLanguageId = it.RevisionId, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ロシア語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "რუსეთის", TranslationToLanguageId = ka.RevisionId, },               // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ರಶಿಯನ್", TranslationToLanguageId = kn.RevisionId, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "러시아의", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Russian", TranslationToLanguageId = la.RevisionId, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Rusijos", TranslationToLanguageId = lt.RevisionId, },                // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "krievu", TranslationToLanguageId = lv.RevisionId, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Руската", TranslationToLanguageId = mk.RevisionId, },                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Rusia", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Russu", TranslationToLanguageId = mt.RevisionId, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Russisch", TranslationToLanguageId = nl.RevisionId, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "russiske", TranslationToLanguageId = no.RevisionId, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "rosyjski", TranslationToLanguageId = pl.RevisionId, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "russo", TranslationToLanguageId = pt.RevisionId, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "rusesc", TranslationToLanguageId = ro.RevisionId, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "русский", TranslationToLanguageId = ru.RevisionId, },                // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Rus", TranslationToLanguageId = sk.RevisionId, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "ruski", TranslationToLanguageId = sl.RevisionId, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "rusisht", TranslationToLanguageId = sq.RevisionId, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "руски", TranslationToLanguageId = sr.RevisionId, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "ryska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kirusi", TranslationToLanguageId = sw.RevisionId, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ரஷியன்", TranslationToLanguageId = ta.RevisionId, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "రష్యన్", TranslationToLanguageId = te.RevisionId, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษารัสเซีย", TranslationToLanguageId = th.RevisionId, },               // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Rus", TranslationToLanguageId = tr.RevisionId, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Російська", TranslationToLanguageId = uk.RevisionId, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "روسی", TranslationToLanguageId = ur.RevisionId, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Nga", TranslationToLanguageId = vi.RevisionId, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "רוסיש", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "俄罗斯", TranslationToLanguageId = zh.RevisionId, },                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ru.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sk.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = en.RevisionId, },                        // #1  Language = "English"
//                    new CreateLanguageName { Text = "eslovaco", TranslationToLanguageId = es.RevisionId, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Slowakisch", TranslationToLanguageId = de.RevisionId, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "السلوفاكية", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Slowaakse", TranslationToLanguageId = af.RevisionId, },                     // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = az.RevisionId, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Славацкая", TranslationToLanguageId = be.RevisionId, },                     // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "словашки", TranslationToLanguageId = bg.RevisionId, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্লোভাক", TranslationToLanguageId = bn.RevisionId, },                         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "eslovac", TranslationToLanguageId = ca.RevisionId, },                       // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "slovenský", TranslationToLanguageId = cs.RevisionId, },                     // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Slofacia", TranslationToLanguageId = cy.RevisionId, },                      // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Slovakiske", TranslationToLanguageId = da.RevisionId, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "slovaki", TranslationToLanguageId = et.RevisionId, },                       // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Eslovakierara", TranslationToLanguageId = eu.RevisionId, },                 // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اسلواکی", TranslationToLanguageId = fa.RevisionId, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Slovakian", TranslationToLanguageId = fi.RevisionId, },                     // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "slovaques", TranslationToLanguageId = fr.RevisionId, },                     // #18 Language = "French"
//                    new CreateLanguageName { Text = "Slóvaicis", TranslationToLanguageId = ga.RevisionId, },                     // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Eslovaco", TranslationToLanguageId = gl.RevisionId, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્લોવાક", TranslationToLanguageId = gu.RevisionId, },                        // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סלובקית", TranslationToLanguageId = he.RevisionId, },                      // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्लोवाक", TranslationToLanguageId = hi.RevisionId, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "slovački", TranslationToLanguageId = hr.RevisionId, },                      // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "slovak", TranslationToLanguageId = ht.RevisionId, },                        // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "szlovák", TranslationToLanguageId = hu.RevisionId, },                       // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սլովակ", TranslationToLanguageId = hy.RevisionId, },                        // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Slovakia", TranslationToLanguageId = id.RevisionId, },                      // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = isLanguage.RevisionId, },                // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "slovacco", TranslationToLanguageId = it.RevisionId, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スロバキア語", TranslationToLanguageId = ja.RevisionId, },                   // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სლოვაკეთის", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ಲೋವ್ಯಾಕ್ ಭಾಷೆ ಯಾ ಜನಾಂಗದವರು", TranslationToLanguageId = kn.RevisionId, },   // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "슬로바키아어", TranslationToLanguageId = ko.RevisionId, },                   // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Moravica", TranslationToLanguageId = la.RevisionId, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Slovakijos", TranslationToLanguageId = lt.RevisionId, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Slovākijas", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Словачка", TranslationToLanguageId = mk.RevisionId, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Slovakia", TranslationToLanguageId = ms.RevisionId, },                      // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Slovakka", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Slowaaks", TranslationToLanguageId = nl.RevisionId, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "slovakisk", TranslationToLanguageId = no.RevisionId, },                     // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "słowacki", TranslationToLanguageId = pl.RevisionId, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "eslovaco", TranslationToLanguageId = pt.RevisionId, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "slovac", TranslationToLanguageId = ro.RevisionId, },                        // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "словацкий", TranslationToLanguageId = ru.RevisionId, },                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "slovenských", TranslationToLanguageId = sk.RevisionId, },                   // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Slovaški", TranslationToLanguageId = sl.RevisionId, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "sllovak", TranslationToLanguageId = sq.RevisionId, },                       // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "словачки", TranslationToLanguageId = sr.RevisionId, },                      // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "slovak", TranslationToLanguageId = sv.RevisionId, },                        // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kislovakia", TranslationToLanguageId = sw.RevisionId, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்லோவாக்", TranslationToLanguageId = ta.RevisionId, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్లోవక్", TranslationToLanguageId = te.RevisionId, },                          // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "สโลวัก", TranslationToLanguageId = th.RevisionId, },                          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Slovak", TranslationToLanguageId = tr.RevisionId, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Словацька", TranslationToLanguageId = uk.RevisionId, },                     // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سلاواکی", TranslationToLanguageId = ur.RevisionId, },                       // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Slovak", TranslationToLanguageId = vi.RevisionId, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סלאָוואַקיש", TranslationToLanguageId = yi.RevisionId, },                    // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "斯洛伐克", TranslationToLanguageId = zh.RevisionId, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sk.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sl.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Slovenian", TranslationToLanguageId = en.RevisionId, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "esloveno", TranslationToLanguageId = es.RevisionId, },                   // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Slowenisch", TranslationToLanguageId = de.RevisionId, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "سلوفيني", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Sloweens", TranslationToLanguageId = af.RevisionId, },                   // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Sloven", TranslationToLanguageId = az.RevisionId, },                     // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Славенская", TranslationToLanguageId = be.RevisionId, },                 // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "словенски", TranslationToLanguageId = bg.RevisionId, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "স্লোভেনিয়ান", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "eslovè", TranslationToLanguageId = ca.RevisionId, },                     // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "slovinský", TranslationToLanguageId = cs.RevisionId, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Slofenia", TranslationToLanguageId = cy.RevisionId, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "slovenske", TranslationToLanguageId = da.RevisionId, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "sloveeni", TranslationToLanguageId = et.RevisionId, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Eslovenierara", TranslationToLanguageId = eu.RevisionId, },              // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اسلوونیایی", TranslationToLanguageId = fa.RevisionId, },                // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Slovenian", TranslationToLanguageId = fi.RevisionId, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "slovènes", TranslationToLanguageId = fr.RevisionId, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Slóivéinis", TranslationToLanguageId = ga.RevisionId, },                 // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Esloveno", TranslationToLanguageId = gl.RevisionId, },                   // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્લોવેનિયન", TranslationToLanguageId = gu.RevisionId, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סלובנית", TranslationToLanguageId = he.RevisionId, },                   // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्लॉवेनियन", TranslationToLanguageId = hi.RevisionId, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Slovenski", TranslationToLanguageId = hr.RevisionId, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Sloveni", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "szlovén", TranslationToLanguageId = hu.RevisionId, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սլովեներեն", TranslationToLanguageId = hy.RevisionId, },                 // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Slovenia", TranslationToLanguageId = id.RevisionId, },                   // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "slóvensku", TranslationToLanguageId = isLanguage.RevisionId, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "sloveno", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スロベニア語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სლოვენიის", TranslationToLanguageId = ka.RevisionId, },                 // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ಲೋವೇನಿಯಾದ ಜನರ ಯಾ ಭಾಷೆಯ", TranslationToLanguageId = kn.RevisionId, }, // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "슬로베니아", TranslationToLanguageId = ko.RevisionId, },                  // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Carnica", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Slovėnijos", TranslationToLanguageId = lt.RevisionId, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "slovēņu", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Словенија", TranslationToLanguageId = mk.RevisionId, },                  // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Slovenia", TranslationToLanguageId = ms.RevisionId, },                   // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Sloven", TranslationToLanguageId = mt.RevisionId, },                     // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Sloveens", TranslationToLanguageId = nl.RevisionId, },                   // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "slovensk", TranslationToLanguageId = no.RevisionId, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "słoweński", TranslationToLanguageId = pl.RevisionId, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "esloveno", TranslationToLanguageId = pt.RevisionId, },                   // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "sloven", TranslationToLanguageId = ro.RevisionId, },                     // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "словенский", TranslationToLanguageId = ru.RevisionId, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Slovinský", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "slovenski", TranslationToLanguageId = sl.RevisionId, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "slloven", TranslationToLanguageId = sq.RevisionId, },                    // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "словеначки", TranslationToLanguageId = sr.RevisionId, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "slovenska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kislovenia", TranslationToLanguageId = sw.RevisionId, },                 // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்லோவேனியன்", TranslationToLanguageId = ta.RevisionId, },         // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్లోవేనియన్", TranslationToLanguageId = te.RevisionId, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาสโลเวเนีย", TranslationToLanguageId = th.RevisionId, },                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Slovenya", TranslationToLanguageId = tr.RevisionId, },                   // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Словенська", TranslationToLanguageId = uk.RevisionId, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سلووینیا", TranslationToLanguageId = ur.RevisionId, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Slovenia", TranslationToLanguageId = vi.RevisionId, },             // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סלאוועניש", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "斯洛文尼亚", TranslationToLanguageId = zh.RevisionId, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sl.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sq.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Albanian", TranslationToLanguageId = en.RevisionId, },          // #1  Language = "English"
//                    new CreateLanguageName { Text = "albanés", TranslationToLanguageId = es.RevisionId, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Albanisch", TranslationToLanguageId = de.RevisionId, },         // #3  Language = "German"
//                    new CreateLanguageName { Text = "الألبانية", TranslationToLanguageId = ar.RevisionId, },         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Albanees", TranslationToLanguageId = af.RevisionId, },          // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "alban", TranslationToLanguageId = az.RevisionId, },             // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "албанская", TranslationToLanguageId = be.RevisionId, },         // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "албански", TranslationToLanguageId = bg.RevisionId, },          // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "আলবেনিয়ান", TranslationToLanguageId = bn.RevisionId, },         // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Albanès", TranslationToLanguageId = ca.RevisionId, },           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "albánský", TranslationToLanguageId = cs.RevisionId, },          // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Albaneg", TranslationToLanguageId = cy.RevisionId, },           // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "albansk", TranslationToLanguageId = da.RevisionId, },           // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "albaania", TranslationToLanguageId = et.RevisionId, },          // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Albanian", TranslationToLanguageId = eu.RevisionId, },          // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "آلبانیایی", TranslationToLanguageId = fa.RevisionId, },        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Albanian", TranslationToLanguageId = fi.RevisionId, },          // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "albanaises", TranslationToLanguageId = fr.RevisionId, },        // #18 Language = "French"
//                    new CreateLanguageName { Text = "Albáinis", TranslationToLanguageId = ga.RevisionId, },          // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Albanés", TranslationToLanguageId = gl.RevisionId, },           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "અલ્બેનિયન", TranslationToLanguageId = gu.RevisionId, },         // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אלבנית", TranslationToLanguageId = he.RevisionId, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "अल्बेनियन्", TranslationToLanguageId = hi.RevisionId, },          // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "अल्बेनियन्", TranslationToLanguageId = hr.RevisionId, },          // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Haitian", TranslationToLanguageId = ht.RevisionId, },           // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "albán", TranslationToLanguageId = hu.RevisionId, },             // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ալբանական", TranslationToLanguageId = hy.RevisionId, },       // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "bahasa Albania", TranslationToLanguageId = id.RevisionId, },    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Albanska", TranslationToLanguageId = isLanguage.RevisionId, },  // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "albanese", TranslationToLanguageId = it.RevisionId, },          // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "アルバニア語", TranslationToLanguageId = ja.RevisionId, },       // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ალბანური", TranslationToLanguageId = ka.RevisionId, },         // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಅಲ್ಬೇನಿಯನ್", TranslationToLanguageId = kn.RevisionId, },         // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "알바니아", TranslationToLanguageId = ko.RevisionId, },           // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Illyrica", TranslationToLanguageId = la.RevisionId, },          // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "albanų", TranslationToLanguageId = lt.RevisionId, },            // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "albāņu", TranslationToLanguageId = lv.RevisionId, },            // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Албанци", TranslationToLanguageId = mk.RevisionId, },           // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Albania", TranslationToLanguageId = ms.RevisionId, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Albaniż", TranslationToLanguageId = mt.RevisionId, },           // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Albanees", TranslationToLanguageId = nl.RevisionId, },          // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Albansk", TranslationToLanguageId = no.RevisionId, },           // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "albański", TranslationToLanguageId = pl.RevisionId, },          // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "albanês", TranslationToLanguageId = pt.RevisionId, },           // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "albanez", TranslationToLanguageId = ro.RevisionId, },           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "албанский", TranslationToLanguageId = ru.RevisionId, },         // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Albánsky", TranslationToLanguageId = sk.RevisionId, },          // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "albanski", TranslationToLanguageId = sl.RevisionId, },          // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "shqiptar", TranslationToLanguageId = sq.RevisionId, },          // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "албански", TranslationToLanguageId = sr.RevisionId, },          // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "albanska", TranslationToLanguageId = sv.RevisionId, },          // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kialbeni", TranslationToLanguageId = sw.RevisionId, },          // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "அல்பானியன்", TranslationToLanguageId = ta.RevisionId, },     // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "అల్బేనియా దేశస్థుడు", TranslationToLanguageId = te.RevisionId, },   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ชาวแอลเบเนีย", TranslationToLanguageId = th.RevisionId, },         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Arnavut", TranslationToLanguageId = tr.RevisionId, },           // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Албанська", TranslationToLanguageId = uk.RevisionId, },         // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "البانی", TranslationToLanguageId = ur.RevisionId, },           // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng An-ba-ni", TranslationToLanguageId = vi.RevisionId, },    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אַלבאַניש", TranslationToLanguageId = yi.RevisionId, },          // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "阿尔巴尼亚人", TranslationToLanguageId = zh.RevisionId, },       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sq.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = en.RevisionId, },                               // #1  Language = "English"
//                    new CreateLanguageName { Text = "serbio", TranslationToLanguageId = es.RevisionId, },                                // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "serbisch", TranslationToLanguageId = de.RevisionId, },                              // #3  Language = "German"
//                    new CreateLanguageName { Text = "صربي", TranslationToLanguageId = ar.RevisionId, },                                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Serwies", TranslationToLanguageId = af.RevisionId, },                               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "serb", TranslationToLanguageId = az.RevisionId, },                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Сербская", TranslationToLanguageId = be.RevisionId, },                              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "сръбски", TranslationToLanguageId = bg.RevisionId, },                               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সার্বিয়া সংক্রান্ত", TranslationToLanguageId = bn.RevisionId, },                           // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "serbi", TranslationToLanguageId = ca.RevisionId, },                                 // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "srbský", TranslationToLanguageId = cs.RevisionId, },                                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Serbeg", TranslationToLanguageId = cy.RevisionId, },                                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "serbisk", TranslationToLanguageId = da.RevisionId, },                               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "serbia", TranslationToLanguageId = et.RevisionId, },                                // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = eu.RevisionId, },                               // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "صرب", TranslationToLanguageId = fa.RevisionId, },                                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = fi.RevisionId, },                               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "serbes", TranslationToLanguageId = fr.RevisionId, },                                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Seirbis", TranslationToLanguageId = ga.RevisionId, },                               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Serbian", TranslationToLanguageId = gl.RevisionId, },                               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સર્બિયન", TranslationToLanguageId = gu.RevisionId, },                                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "הסרבי", TranslationToLanguageId = he.RevisionId, },                                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "सर्बिया की (भाषा या निवासी)", TranslationToLanguageId = hi.RevisionId, },               // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "srpski", TranslationToLanguageId = hr.RevisionId, },                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Sèb", TranslationToLanguageId = ht.RevisionId, },                                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "szerb", TranslationToLanguageId = hu.RevisionId, },                                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սերբերեն", TranslationToLanguageId = hy.RevisionId, },                              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = id.RevisionId, },                                // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "serbneska", TranslationToLanguageId = isLanguage.RevisionId, },                     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "serbo", TranslationToLanguageId = it.RevisionId, },                                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "セルビア語", TranslationToLanguageId = ja.RevisionId, },                             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სერბეთის", TranslationToLanguageId = ka.RevisionId, },                             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸರ್ಬಿಯ ರಾಷ್ಟ್ರದ ಯಾ ಅದರ ಭಾಷೆಗೆ ಸಂಬಂಧಿಸಿದ", TranslationToLanguageId = kn.RevisionId, },  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "세르비아의", TranslationToLanguageId = ko.RevisionId, },                             // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Serbiae", TranslationToLanguageId = la.RevisionId, },                               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Serbijos", TranslationToLanguageId = lt.RevisionId, },                              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Serbijas", TranslationToLanguageId = lv.RevisionId, },                              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Србија", TranslationToLanguageId = mk.RevisionId, },                                // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = ms.RevisionId, },                                // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Serb", TranslationToLanguageId = mt.RevisionId, },                                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Servisch", TranslationToLanguageId = nl.RevisionId, },                              // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "serbisk", TranslationToLanguageId = no.RevisionId, },                               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "serbski", TranslationToLanguageId = pl.RevisionId, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "sérvio", TranslationToLanguageId = pt.RevisionId, },                                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "sârb", TranslationToLanguageId = ro.RevisionId, },                                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "сербский", TranslationToLanguageId = ru.RevisionId, },                              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "srbský", TranslationToLanguageId = sk.RevisionId, },                                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "srbski", TranslationToLanguageId = sl.RevisionId, },                                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "serb", TranslationToLanguageId = sq.RevisionId, },                                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "српски", TranslationToLanguageId = sr.RevisionId, },                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "serbiska", TranslationToLanguageId = sv.RevisionId, },                              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = sw.RevisionId, },                                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "செர்பியாவை சார்ந்த", TranslationToLanguageId = ta.RevisionId, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "సెర్బియా", TranslationToLanguageId = te.RevisionId, },                                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เซอร์เบีย", TranslationToLanguageId = th.RevisionId, },                                 // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Sırp", TranslationToLanguageId = tr.RevisionId, },                                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Сербська", TranslationToLanguageId = uk.RevisionId, },                              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سربیا", TranslationToLanguageId = ur.RevisionId, },                                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Serbia", TranslationToLanguageId = vi.RevisionId, },                                // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סערביש", TranslationToLanguageId = yi.RevisionId, },                               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "塞尔维亚", TranslationToLanguageId = zh.RevisionId, },                               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sr.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sv.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Swedish", TranslationToLanguageId = en.RevisionId, },                                // #1  Language = "English"
//                    new CreateLanguageName { Text = "sueco", TranslationToLanguageId = es.RevisionId, },                                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Schwedisch", TranslationToLanguageId = de.RevisionId, },                             // #3  Language = "German"
//                    new CreateLanguageName { Text = "السويدية", TranslationToLanguageId = ar.RevisionId, },                              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Sweedse", TranslationToLanguageId = af.RevisionId, },                                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "İsveç", TranslationToLanguageId = az.RevisionId, },                                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Шведская", TranslationToLanguageId = be.RevisionId, },                               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "шведски", TranslationToLanguageId = bg.RevisionId, },                                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সুইডেনের ভাষা", TranslationToLanguageId = bn.RevisionId, },                            // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "suec", TranslationToLanguageId = ca.RevisionId, },                                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "švédský", TranslationToLanguageId = cs.RevisionId, },                                // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Sweden", TranslationToLanguageId = cy.RevisionId, },                                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "svensk", TranslationToLanguageId = da.RevisionId, },                                 // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "rootsi", TranslationToLanguageId = et.RevisionId, },                                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Suedierara", TranslationToLanguageId = eu.RevisionId, },                             // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "سوئد", TranslationToLanguageId = fa.RevisionId, },                                  // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "ruotsi", TranslationToLanguageId = fi.RevisionId, },                                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Suède", TranslationToLanguageId = fr.RevisionId, },                                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Sualainnis", TranslationToLanguageId = ga.RevisionId, },                             // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Sueco", TranslationToLanguageId = gl.RevisionId, },                                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્વીડીશ", TranslationToLanguageId = gu.RevisionId, },                                 // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "שוודית", TranslationToLanguageId = he.RevisionId, },                                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्वीडिश", TranslationToLanguageId = hi.RevisionId, },                                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "švedski", TranslationToLanguageId = hr.RevisionId, },                                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Syèd", TranslationToLanguageId = ht.RevisionId, },                                   // #25 Language = "Haitian Creol
//                    new CreateLanguageName { Text = "svéd", TranslationToLanguageId = hu.RevisionId, },                                   // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "շվեդերեն", TranslationToLanguageId = hy.RevisionId, },                               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Swedia", TranslationToLanguageId = id.RevisionId, },                                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Sænska", TranslationToLanguageId = isLanguage.RevisionId, },                         // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "svedese", TranslationToLanguageId = it.RevisionId, },                                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スウェーデン", TranslationToLanguageId = ja.RevisionId, },                            // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "შვედეთის", TranslationToLanguageId = ka.RevisionId, },                              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ವೀಡಿಷ್", TranslationToLanguageId = kn.RevisionId, },                                  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "스웨덴의", TranslationToLanguageId = ko.RevisionId, },                                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Suecica", TranslationToLanguageId = la.RevisionId, },                                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Švedijos", TranslationToLanguageId = lt.RevisionId, },                               // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Zviedru", TranslationToLanguageId = lv.RevisionId, },                                // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Шведската", TranslationToLanguageId = mk.RevisionId, },                              // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Sweden", TranslationToLanguageId = ms.RevisionId, },                                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Svediż", TranslationToLanguageId = mt.RevisionId, },                                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Zweeds", TranslationToLanguageId = nl.RevisionId, },                                 // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Svenske", TranslationToLanguageId = no.RevisionId, },                                // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "szwedzki", TranslationToLanguageId = pl.RevisionId, },                               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "sueco", TranslationToLanguageId = pt.RevisionId, },                                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "suedez", TranslationToLanguageId = ro.RevisionId, },                                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "шведский", TranslationToLanguageId = ru.RevisionId, },                               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Švédsky", TranslationToLanguageId = sk.RevisionId, },                                // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "švedski", TranslationToLanguageId = sl.RevisionId, },                                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "suedez", TranslationToLanguageId = sq.RevisionId, },                                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "шведски", TranslationToLanguageId = sr.RevisionId, },                                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "svenska", TranslationToLanguageId = sv.RevisionId, },                                // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Swedish", TranslationToLanguageId = sw.RevisionId, },                                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஸ்வீடன் நாட்டு மொழி, மக்கள்", TranslationToLanguageId = ta.RevisionId, },       // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్వీడిష్", TranslationToLanguageId = te.RevisionId, },                                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "สวีเดน", TranslationToLanguageId = th.RevisionId, },                                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "İsveç", TranslationToLanguageId = tr.RevisionId, },                                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Шведська", TranslationToLanguageId = uk.RevisionId, },                               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سويڈش", TranslationToLanguageId = ur.RevisionId, },                                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Thụy Điển", TranslationToLanguageId = vi.RevisionId, },                              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "שוועדיש", TranslationToLanguageId = yi.RevisionId, },                               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "瑞典语", TranslationToLanguageId = zh.RevisionId, },                                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!sw.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = en.RevisionId, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = es.RevisionId, },               // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = de.RevisionId, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "السواحيلية", TranslationToLanguageId = ar.RevisionId, },           // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = af.RevisionId, },               // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Suahili", TranslationToLanguageId = az.RevisionId, },               // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "суахілі", TranslationToLanguageId = be.RevisionId, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "суахили", TranslationToLanguageId = bg.RevisionId, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "সোয়াহিলি", TranslationToLanguageId = bn.RevisionId, },               // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "suahili", TranslationToLanguageId = ca.RevisionId, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "svahilština", TranslationToLanguageId = cs.RevisionId, },           // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = cy.RevisionId, },               // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = da.RevisionId, },               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "suahiili", TranslationToLanguageId = et.RevisionId, },              // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = eu.RevisionId, },               // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "سواحیلی", TranslationToLanguageId = fa.RevisionId, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Suahili", TranslationToLanguageId = fi.RevisionId, },               // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = fr.RevisionId, },               // #18 Language = "French"
//                    new CreateLanguageName { Text = "Svahaílis", TranslationToLanguageId = ga.RevisionId, },             // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "suahili", TranslationToLanguageId = gl.RevisionId, },               // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "સ્વાહિલી", TranslationToLanguageId = gu.RevisionId, },                // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סוואהילית", TranslationToLanguageId = he.RevisionId, },            // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "स्वाहिली", TranslationToLanguageId = hi.RevisionId, },                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = hr.RevisionId, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = ht.RevisionId, },               // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "szuahéli", TranslationToLanguageId = hu.RevisionId, },              // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "սուահիլի", TranslationToLanguageId = hy.RevisionId, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = id.RevisionId, },               // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "svahílí", TranslationToLanguageId = isLanguage.RevisionId, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = it.RevisionId, },               // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "スワヒリ語", TranslationToLanguageId = ja.RevisionId, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "სუაჰილი", TranslationToLanguageId = ka.RevisionId, },              // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಸ್ವಾಹಿಲಿ", TranslationToLanguageId = kn.RevisionId, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "스와힐리어", TranslationToLanguageId = ko.RevisionId, },             // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = la.RevisionId, },               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = lt.RevisionId, },               // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = lv.RevisionId, },               // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "свахили", TranslationToLanguageId = mk.RevisionId, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Swahili", TranslationToLanguageId = ms.RevisionId, },        // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Swaħili", TranslationToLanguageId = mt.RevisionId, },               // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = nl.RevisionId, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = no.RevisionId, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "suahili", TranslationToLanguageId = pl.RevisionId, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "suaíli", TranslationToLanguageId = pt.RevisionId, },                // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = ro.RevisionId, },               // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "суахили", TranslationToLanguageId = ru.RevisionId, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "svahilština", TranslationToLanguageId = sk.RevisionId, },           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "svahili", TranslationToLanguageId = sl.RevisionId, },               // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "suahilisht", TranslationToLanguageId = sq.RevisionId, },            // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Свахили", TranslationToLanguageId = sr.RevisionId, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "swahili", TranslationToLanguageId = sv.RevisionId, },               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = sw.RevisionId, },               // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "சுவாஹிலி", TranslationToLanguageId = ta.RevisionId, },           // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "స్వాహిలి", TranslationToLanguageId = te.RevisionId, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาสวาฮีลี", TranslationToLanguageId = th.RevisionId, },              // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Swahili", TranslationToLanguageId = tr.RevisionId, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Суахілі", TranslationToLanguageId = uk.RevisionId, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "سواہیلی", TranslationToLanguageId = ur.RevisionId, },              // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tiếng Swahili", TranslationToLanguageId = vi.RevisionId, },         // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "סוואַהילי", TranslationToLanguageId = yi.RevisionId, },             // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "斯瓦希里", TranslationToLanguageId = zh.RevisionId, },               // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sw.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ta.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = en.RevisionId, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = es.RevisionId, },                  // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Tamilisch", TranslationToLanguageId = de.RevisionId, },              // #3  Language = "German"
//                    new CreateLanguageName { Text = "التاميل", TranslationToLanguageId = ar.RevisionId, },               // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = af.RevisionId, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = az.RevisionId, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "тамільская", TranslationToLanguageId = be.RevisionId, },             // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "тамилски", TranslationToLanguageId = bg.RevisionId, },               // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "তামিল", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "tàmil", TranslationToLanguageId = ca.RevisionId, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = cs.RevisionId, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = cy.RevisionId, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = da.RevisionId, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = et.RevisionId, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = eu.RevisionId, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "تامیل", TranslationToLanguageId = fa.RevisionId, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = fi.RevisionId, },                  // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = fr.RevisionId, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = ga.RevisionId, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = gl.RevisionId, },                  // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "તમિળ", TranslationToLanguageId = gu.RevisionId, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "טמילית", TranslationToLanguageId = he.RevisionId, },                // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "तमिल", TranslationToLanguageId = hi.RevisionId, },                  // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "tamilski", TranslationToLanguageId = hr.RevisionId, },               // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = ht.RevisionId, },                  // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = hu.RevisionId, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "թամիլերեն", TranslationToLanguageId = hy.RevisionId, },              // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = isLanguage.RevisionId, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = it.RevisionId, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "タミル語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ტამილური", TranslationToLanguageId = ka.RevisionId, },             // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ತಮಿಳು", TranslationToLanguageId = kn.RevisionId, },                  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "타밀 사람", TranslationToLanguageId = ko.RevisionId, },               // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = la.RevisionId, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "tamilų", TranslationToLanguageId = lt.RevisionId, },                 // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "tamilu", TranslationToLanguageId = lv.RevisionId, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Тамилските", TranslationToLanguageId = mk.RevisionId, },             // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = mt.RevisionId, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = nl.RevisionId, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = no.RevisionId, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = pl.RevisionId, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "tâmil", TranslationToLanguageId = pt.RevisionId, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "tamilă", TranslationToLanguageId = ro.RevisionId, },                 // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "тамильский", TranslationToLanguageId = ru.RevisionId, },             // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sl.RevisionId, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "tamil", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "тамилски", TranslationToLanguageId = sr.RevisionId, },               // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = sw.RevisionId, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "தமிழ்", TranslationToLanguageId = ta.RevisionId, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "తమిళ్", TranslationToLanguageId = te.RevisionId, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ทมิฬ", TranslationToLanguageId = th.RevisionId, },                    // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = tr.RevisionId, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "тамільська", TranslationToLanguageId = uk.RevisionId, },             // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "تامل", TranslationToLanguageId = ur.RevisionId, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Tamil", TranslationToLanguageId = vi.RevisionId, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טאַמיל", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "泰米尔人", TranslationToLanguageId = zh.RevisionId, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ta.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!te.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = en.RevisionId, },           // #1  Language = "English"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = es.RevisionId, },           // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = de.RevisionId, },           // #3  Language = "German"
//                    new CreateLanguageName { Text = "التيلجو", TranslationToLanguageId = ar.RevisionId, },         // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = af.RevisionId, },           // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Teluqu", TranslationToLanguageId = az.RevisionId, },           // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "тэлугу", TranslationToLanguageId = be.RevisionId, },           // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = bg.RevisionId, },           // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "তেলুগু", TranslationToLanguageId = bn.RevisionId, },             // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = ca.RevisionId, },           // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = cs.RevisionId, },           // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = cy.RevisionId, },           // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = da.RevisionId, },           // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = et.RevisionId, },           // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = eu.RevisionId, },           // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "تلوگو", TranslationToLanguageId = fa.RevisionId, },           // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = fi.RevisionId, },           // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = fr.RevisionId, },           // #18 Language = "French"
//                    new CreateLanguageName { Text = "Teileagúis", TranslationToLanguageId = ga.RevisionId, },       // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = gl.RevisionId, },           // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "તેલુગુ", TranslationToLanguageId = gu.RevisionId, },             // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "טלוגו", TranslationToLanguageId = he.RevisionId, },           // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "तेलुगु", TranslationToLanguageId = hi.RevisionId, },             // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = hr.RevisionId, },           // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = ht.RevisionId, },           // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = hu.RevisionId, },           // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "տելուգու", TranslationToLanguageId = hy.RevisionId, },         // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = id.RevisionId, },           // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "telúgú", TranslationToLanguageId = isLanguage.RevisionId, },   // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = it.RevisionId, },           // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "テルグ語", TranslationToLanguageId = ja.RevisionId, },          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ტელუგუ", TranslationToLanguageId = ka.RevisionId, },         // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ತೆಲುಗು", TranslationToLanguageId = kn.RevisionId, },            // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "텔루구어", TranslationToLanguageId = ko.RevisionId, },          // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = la.RevisionId, },           // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "telugų", TranslationToLanguageId = lt.RevisionId, },           // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = lv.RevisionId, },           // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = mk.RevisionId, },           // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = ms.RevisionId, },           // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = mt.RevisionId, },           // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = nl.RevisionId, },           // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = no.RevisionId, },           // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = pl.RevisionId, },           // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = pt.RevisionId, },           // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = ro.RevisionId, },           // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = ru.RevisionId, },           // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "telugu", TranslationToLanguageId = sk.RevisionId, },           // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sl.RevisionId, },           // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sq.RevisionId, },           // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = sr.RevisionId, },           // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sv.RevisionId, },           // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = sw.RevisionId, },           // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "தெலுங்கு", TranslationToLanguageId = ta.RevisionId, },        // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "తెలుగు", TranslationToLanguageId = te.RevisionId, },            // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เตลูกู", TranslationToLanguageId = th.RevisionId, },              // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = tr.RevisionId, },           // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "телугу", TranslationToLanguageId = uk.RevisionId, },           // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "تيلوگو", TranslationToLanguageId = ur.RevisionId, },          // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Telugu", TranslationToLanguageId = vi.RevisionId, },           // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טעלוגו", TranslationToLanguageId = yi.RevisionId, },          // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "泰卢固语", TranslationToLanguageId = zh.RevisionId, },         // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = te.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!th.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = en.RevisionId, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "tailandés", TranslationToLanguageId = es.RevisionId, },             // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "thailändisch", TranslationToLanguageId = de.RevisionId, },          // #3  Language = "German"
//                    new CreateLanguageName { Text = "التايلاندية", TranslationToLanguageId = ar.RevisionId, },           // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = af.RevisionId, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Tay", TranslationToLanguageId = az.RevisionId, },                   // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Тайская", TranslationToLanguageId = be.RevisionId, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "тайландски", TranslationToLanguageId = bg.RevisionId, },            // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "থাই", TranslationToLanguageId = bn.RevisionId, },                    // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "tailandès", TranslationToLanguageId = ca.RevisionId, },             // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "thajské", TranslationToLanguageId = cs.RevisionId, },               // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = cy.RevisionId, },                  // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = da.RevisionId, },                  // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "tai", TranslationToLanguageId = et.RevisionId, },                   // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = eu.RevisionId, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "تایلندی", TranslationToLanguageId = fa.RevisionId, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "thaimaalainen", TranslationToLanguageId = fi.RevisionId, },         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "thaïlandaise", TranslationToLanguageId = fr.RevisionId, },          // #18 Language = "French"
//                    new CreateLanguageName { Text = "Téalainnis", TranslationToLanguageId = ga.RevisionId, },            // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Tailandés", TranslationToLanguageId = gl.RevisionId, },             // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "થાઈ", TranslationToLanguageId = gu.RevisionId, },                   // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "תאילנדי", TranslationToLanguageId = he.RevisionId, },              // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "थाई", TranslationToLanguageId = hi.RevisionId, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "Tajlandski", TranslationToLanguageId = hr.RevisionId, },            // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Tayi", TranslationToLanguageId = ht.RevisionId, },                  // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = hu.RevisionId, },                  // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "թայերեն", TranslationToLanguageId = hy.RevisionId, },               // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Thailand", TranslationToLanguageId = id.RevisionId, },              // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = isLanguage.RevisionId, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = it.RevisionId, },                  // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "タイ", TranslationToLanguageId = ja.RevisionId, },                  // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ტაილანდური", TranslationToLanguageId = ka.RevisionId, },          // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಥಾಯ್", TranslationToLanguageId = kn.RevisionId, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "타이어", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = la.RevisionId, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Tailando", TranslationToLanguageId = lt.RevisionId, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "Taizemes", TranslationToLanguageId = lv.RevisionId, },              // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Тајланд", TranslationToLanguageId = mk.RevisionId, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = ms.RevisionId, },                  // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Tajlandiż", TranslationToLanguageId = mt.RevisionId, },             // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = nl.RevisionId, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = no.RevisionId, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "tajski", TranslationToLanguageId = pl.RevisionId, },                // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "tailandês", TranslationToLanguageId = pt.RevisionId, },             // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "tailandez", TranslationToLanguageId = ro.RevisionId, },             // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "тайский", TranslationToLanguageId = ru.RevisionId, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Thajské", TranslationToLanguageId = sk.RevisionId, },               // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "tajski", TranslationToLanguageId = sl.RevisionId, },                // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "Тајландски", TranslationToLanguageId = sr.RevisionId, },            // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "thailändska", TranslationToLanguageId = sv.RevisionId, },           // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Thai", TranslationToLanguageId = sw.RevisionId, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "தாய்", TranslationToLanguageId = ta.RevisionId, },                 // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "థాయ్", TranslationToLanguageId = te.RevisionId, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ไทย", TranslationToLanguageId = th.RevisionId, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Tayland", TranslationToLanguageId = tr.RevisionId, },               // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "тайський", TranslationToLanguageId = uk.RevisionId, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "تھائی", TranslationToLanguageId = ur.RevisionId, },                // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Thái", TranslationToLanguageId = vi.RevisionId, },                  // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טייַלענדיש", TranslationToLanguageId = yi.RevisionId, },            // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "泰国", TranslationToLanguageId = zh.RevisionId, },                  // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = th.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!tr.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Turkish", TranslationToLanguageId = en.RevisionId, },               // #1  Language = "English"
//                    new CreateLanguageName { Text = "turco", TranslationToLanguageId = es.RevisionId, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Türkisch", TranslationToLanguageId = de.RevisionId, },              // #3  Language = "German"
//                    new CreateLanguageName { Text = "تركي", TranslationToLanguageId = ar.RevisionId, },                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Turkse", TranslationToLanguageId = af.RevisionId, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Türk", TranslationToLanguageId = az.RevisionId, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Турэцкая", TranslationToLanguageId = be.RevisionId, },              // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "турски", TranslationToLanguageId = bg.RevisionId, },                // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "তুর্কী", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "turc", TranslationToLanguageId = ca.RevisionId, },                  // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "turečtina", TranslationToLanguageId = cs.RevisionId, },             // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Twrcaidd", TranslationToLanguageId = cy.RevisionId, },              // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "tyrkisk", TranslationToLanguageId = da.RevisionId, },               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "türgi", TranslationToLanguageId = et.RevisionId, },                 // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Turkish", TranslationToLanguageId = eu.RevisionId, },               // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ترکی", TranslationToLanguageId = fa.RevisionId, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "turkki", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "turque", TranslationToLanguageId = fr.RevisionId, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Tuircis", TranslationToLanguageId = ga.RevisionId, },               // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Turco", TranslationToLanguageId = gl.RevisionId, },                 // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ટર્કિશ", TranslationToLanguageId = gu.RevisionId, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "טורקית", TranslationToLanguageId = he.RevisionId, },               // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "तुर्की का", TranslationToLanguageId = hi.RevisionId, },                // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "turski", TranslationToLanguageId = hr.RevisionId, },                // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Tik", TranslationToLanguageId = ht.RevisionId, },                   // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "török", TranslationToLanguageId = hu.RevisionId, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "թուրքական", TranslationToLanguageId = hy.RevisionId, },            // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Turki", TranslationToLanguageId = id.RevisionId, },                 // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "Tyrkneska", TranslationToLanguageId = isLanguage.RevisionId, },     // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "turco", TranslationToLanguageId = it.RevisionId, },                 // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "トルコ", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "თურქეთის", TranslationToLanguageId = ka.RevisionId, },            // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಟರ್ಕಿಷ್", TranslationToLanguageId = kn.RevisionId, },                 // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "터키어", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Turkish", TranslationToLanguageId = la.RevisionId, },               // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Turkijos", TranslationToLanguageId = lt.RevisionId, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "turku", TranslationToLanguageId = lv.RevisionId, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Турција", TranslationToLanguageId = mk.RevisionId, },               // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Turki", TranslationToLanguageId = ms.RevisionId, },                 // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Tork", TranslationToLanguageId = mt.RevisionId, },                  // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Turks", TranslationToLanguageId = nl.RevisionId, },                 // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "tyrkisk", TranslationToLanguageId = no.RevisionId, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "turecki", TranslationToLanguageId = pl.RevisionId, },               // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "turco", TranslationToLanguageId = pt.RevisionId, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "turc", TranslationToLanguageId = ro.RevisionId, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "турецкий", TranslationToLanguageId = ru.RevisionId, },              // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "turečtina", TranslationToLanguageId = sk.RevisionId, },             // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "turečtina", TranslationToLanguageId = sl.RevisionId, },             // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "turk", TranslationToLanguageId = sq.RevisionId, },                  // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "турски", TranslationToLanguageId = sr.RevisionId, },                // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "turkiska", TranslationToLanguageId = sv.RevisionId, },              // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kituruki", TranslationToLanguageId = sw.RevisionId, },              // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "டர்கிஷ்", TranslationToLanguageId = ta.RevisionId, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "టర్కిష్", TranslationToLanguageId = te.RevisionId, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ตุรกี", TranslationToLanguageId = th.RevisionId, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Türk", TranslationToLanguageId = tr.RevisionId, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Турецька", TranslationToLanguageId = uk.RevisionId, },              // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ترکی", TranslationToLanguageId = ur.RevisionId, },                 // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Thổ Nhĩ Kỳ", TranslationToLanguageId = vi.RevisionId, },            // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "טערקיש", TranslationToLanguageId = yi.RevisionId, },               // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "土耳其", TranslationToLanguageId = zh.RevisionId, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tr.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!uk.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ukrainian", TranslationToLanguageId = en.RevisionId, },                  // #1  Language = "English"
//                    new CreateLanguageName { Text = "ucranio", TranslationToLanguageId = es.RevisionId, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Ukrainisch", TranslationToLanguageId = de.RevisionId, },                 // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأوكراني", TranslationToLanguageId = ar.RevisionId, },                  // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Oekraïens", TranslationToLanguageId = af.RevisionId, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Ukrayna", TranslationToLanguageId = az.RevisionId, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Украінскі", TranslationToLanguageId = be.RevisionId, },                  // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "украински", TranslationToLanguageId = bg.RevisionId, },                  // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইউক্রেরিয়ান", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "ucraïnès", TranslationToLanguageId = ca.RevisionId, },                   // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "ukrajinský", TranslationToLanguageId = cs.RevisionId, },                 // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Wcreineg", TranslationToLanguageId = cy.RevisionId, },                   // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "ukrainsk", TranslationToLanguageId = da.RevisionId, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "ukraina", TranslationToLanguageId = et.RevisionId, },                    // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Ukrainian", TranslationToLanguageId = eu.RevisionId, },                  // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "اوکراین", TranslationToLanguageId = fa.RevisionId, },                   // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "ukrainalainen", TranslationToLanguageId = fi.RevisionId, },              // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "ukrainiens", TranslationToLanguageId = fr.RevisionId, },                 // #18 Language = "French"
//                    new CreateLanguageName { Text = "Úcráinis", TranslationToLanguageId = ga.RevisionId, },                   // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Ucraíno", TranslationToLanguageId = gl.RevisionId, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "યુક્રેનિયન", TranslationToLanguageId = gu.RevisionId, },                    // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אוקראינית", TranslationToLanguageId = he.RevisionId, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "यूक्रेनी", TranslationToLanguageId = hi.RevisionId, },                       // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "ukrajinski", TranslationToLanguageId = hr.RevisionId, },                 // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "ukrainian", TranslationToLanguageId = ht.RevisionId, },                  // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "ukrán", TranslationToLanguageId = hu.RevisionId, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ուկրաիներեն", TranslationToLanguageId = hy.RevisionId, },                // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Ukraina", TranslationToLanguageId = id.RevisionId, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "úkraínska", TranslationToLanguageId = isLanguage.RevisionId, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "ucraino", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ウクライナ語", TranslationToLanguageId = ja.RevisionId, },                // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "უკრაინის", TranslationToLanguageId = ka.RevisionId, },                   // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಉಕ್ರೇನ್ ಪ್ರಾಂತ ಯಾ ಅದರ ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, },  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "우크라이나의", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Ukrainian", TranslationToLanguageId = la.RevisionId, },                  // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Ukrainos", TranslationToLanguageId = lt.RevisionId, },                   // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "ukraiņu", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "Украина", TranslationToLanguageId = mk.RevisionId, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Ukraine", TranslationToLanguageId = ms.RevisionId, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ukraina", TranslationToLanguageId = mt.RevisionId, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Oekraïens", TranslationToLanguageId = nl.RevisionId, },                  // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "ukrainsk", TranslationToLanguageId = no.RevisionId, },                   // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "ukraiński", TranslationToLanguageId = pl.RevisionId, },                  // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "ucraniano", TranslationToLanguageId = pt.RevisionId, },                  // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "ucrainean", TranslationToLanguageId = ro.RevisionId, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "украинский", TranslationToLanguageId = ru.RevisionId, },                 // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Ukrajinský", TranslationToLanguageId = sk.RevisionId, },                 // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "ukrajinski", TranslationToLanguageId = sl.RevisionId, },                 // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "ukrainisht", TranslationToLanguageId = sq.RevisionId, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "украјински", TranslationToLanguageId = sr.RevisionId, },                 // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "ukrainska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiukreni", TranslationToLanguageId = sw.RevisionId, },                   // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "உக்ரேனியன்", TranslationToLanguageId = ta.RevisionId, },              // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "యుక్రేయిన్", TranslationToLanguageId = te.RevisionId, },                   // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษายูเครน", TranslationToLanguageId = th.RevisionId, },                   // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Ukrayna", TranslationToLanguageId = tr.RevisionId, },                    // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Український", TranslationToLanguageId = uk.RevisionId, },                // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "يوکرينی", TranslationToLanguageId = ur.RevisionId, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Ucraina", TranslationToLanguageId = vi.RevisionId, },                    // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אוקרייניש", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "乌克兰", TranslationToLanguageId = zh.RevisionId, },                     // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = uk.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!ur.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = en.RevisionId, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = es.RevisionId, },                    // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = de.RevisionId, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الأردية", TranslationToLanguageId = ar.RevisionId, },                 // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Oerdoe", TranslationToLanguageId = af.RevisionId, },                  // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = az.RevisionId, },                    // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Мова урду", TranslationToLanguageId = be.RevisionId, },               // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "урду", TranslationToLanguageId = bg.RevisionId, },                    // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "উর্দু", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = ca.RevisionId, },                    // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = cs.RevisionId, },                    // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = cy.RevisionId, },                    // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = da.RevisionId, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "urdu keel", TranslationToLanguageId = et.RevisionId, },               // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = eu.RevisionId, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "زبان اردو", TranslationToLanguageId = fa.RevisionId, },              // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = fi.RevisionId, },                    // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "ourdou", TranslationToLanguageId = fr.RevisionId, },                  // #18 Language = "French"
//                    new CreateLanguageName { Text = "Urdais", TranslationToLanguageId = ga.RevisionId, },                  // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "urdú", TranslationToLanguageId = gl.RevisionId, },                    // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ઉર્દુ", TranslationToLanguageId = gu.RevisionId, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אורדו", TranslationToLanguageId = he.RevisionId, },                  // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "उर्दू", TranslationToLanguageId = hi.RevisionId, },                      // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = hr.RevisionId, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = ht.RevisionId, },                    // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = hu.RevisionId, },                    // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "ուրդու", TranslationToLanguageId = hy.RevisionId, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = id.RevisionId, },                    // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "úrdú", TranslationToLanguageId = isLanguage.RevisionId, },            // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = it.RevisionId, },                    // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ウルドゥー語", TranslationToLanguageId = ja.RevisionId, },             // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ურდული", TranslationToLanguageId = ka.RevisionId, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಉರ್ದು ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, },               // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "우르 두말", TranslationToLanguageId = ko.RevisionId, },                // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = la.RevisionId, },                    // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "urdų kalba", TranslationToLanguageId = lt.RevisionId, },              // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "урду", TranslationToLanguageId = mk.RevisionId, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = ms.RevisionId, },                    // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = mt.RevisionId, },                    // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = nl.RevisionId, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = no.RevisionId, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = pl.RevisionId, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = pt.RevisionId, },                    // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = ro.RevisionId, },                    // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "язык урду", TranslationToLanguageId = ru.RevisionId, },               // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = sk.RevisionId, },                    // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "urdu", TranslationToLanguageId = sl.RevisionId, },                    // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "urduisht", TranslationToLanguageId = sq.RevisionId, },                // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "урду", TranslationToLanguageId = sr.RevisionId, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "Urdu", TranslationToLanguageId = sv.RevisionId, },                    // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kiurdu", TranslationToLanguageId = sw.RevisionId, },                  // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "முள்ளம் பன்றி", TranslationToLanguageId = ta.RevisionId, },         // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "ఉర్దూ భాష", TranslationToLanguageId = te.RevisionId, },                // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "ภาษาอิรดู", TranslationToLanguageId = th.RevisionId, },                  // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Urduca", TranslationToLanguageId = tr.RevisionId, },                  // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "мова урду", TranslationToLanguageId = uk.RevisionId, },               // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "اردو", TranslationToLanguageId = ur.RevisionId, },                   // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Urdu", TranslationToLanguageId = vi.RevisionId, },              // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "אורדו", TranslationToLanguageId = yi.RevisionId, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "乌尔都语", TranslationToLanguageId = zh.RevisionId, },                 // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ur.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!vi.Names.Any())
//                new List<CreateLanguageName>
//                {

//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = en.RevisionId, },                      // #1  Language = "English"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = es.RevisionId, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = de.RevisionId, },                      // #3  Language = "German"
//                    new CreateLanguageName { Text = "الفيتنامية", TranslationToLanguageId = ar.RevisionId, },                     // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Viëtnamees", TranslationToLanguageId = af.RevisionId, },                      // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Vyetnam", TranslationToLanguageId = az.RevisionId, },                         // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "в'етнамскі", TranslationToLanguageId = be.RevisionId, },                      // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "виетнамски", TranslationToLanguageId = bg.RevisionId, },                      // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ভিএত্নেম লোক", TranslationToLanguageId = bn.RevisionId, },                      // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = ca.RevisionId, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "vietnamský", TranslationToLanguageId = cs.RevisionId, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Fietnameg", TranslationToLanguageId = cy.RevisionId, },                       // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "vietnamesisk", TranslationToLanguageId = da.RevisionId, },                    // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "vietnami", TranslationToLanguageId = et.RevisionId, },                        // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = eu.RevisionId, },                      // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ویتنامی", TranslationToLanguageId = fa.RevisionId, },                        // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "vietnam", TranslationToLanguageId = fi.RevisionId, },                         // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "vietnamienne", TranslationToLanguageId = fr.RevisionId, },                    // #18 Language = "French"
//                    new CreateLanguageName { Text = "Vítneaimis", TranslationToLanguageId = ga.RevisionId, },                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Vietnamita", TranslationToLanguageId = gl.RevisionId, },                      // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "વિયેતનામીઝ", TranslationToLanguageId = gu.RevisionId, },                      // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "ויאטנמית", TranslationToLanguageId = he.RevisionId, },                       // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "वियतनामी", TranslationToLanguageId = hi.RevisionId, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "vijetnamski", TranslationToLanguageId = hr.RevisionId, },                     // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Vyetnamyen", TranslationToLanguageId = ht.RevisionId, },                      // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "vietnami", TranslationToLanguageId = hu.RevisionId, },                        // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "վիետնամերեն", TranslationToLanguageId = hy.RevisionId, },                    // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Vietnam", TranslationToLanguageId = id.RevisionId, },                         // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "víetnamska", TranslationToLanguageId = isLanguage.RevisionId, },              // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = it.RevisionId, },                      // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "ベトナム", TranslationToLanguageId = ja.RevisionId, },                         // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ვიეტნამური", TranslationToLanguageId = ka.RevisionId, },                     // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ವಿಯೆಟ್ನಾಂ ದೇಶದ ವ್ಯಕ್ತಿ ಯಾ ಪ್ರಜೆ ಯಾ ಭಾಷೆ", TranslationToLanguageId = kn.RevisionId, }, // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "베트남 사람", TranslationToLanguageId = ko.RevisionId, },                      // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Vietnamica", TranslationToLanguageId = la.RevisionId, },                      // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "vietnamiečių", TranslationToLanguageId = lt.RevisionId, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "vjetnamiešu", TranslationToLanguageId = lv.RevisionId, },                     // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "виетнамски", TranslationToLanguageId = mk.RevisionId, },                      // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Vietnam", TranslationToLanguageId = ms.RevisionId, },                         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Vjetnamiż", TranslationToLanguageId = mt.RevisionId, },                       // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = nl.RevisionId, },                      // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "vietnamesisk", TranslationToLanguageId = no.RevisionId, },                    // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "wietnamski", TranslationToLanguageId = pl.RevisionId, },                      // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "vietnamita", TranslationToLanguageId = pt.RevisionId, },                      // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "vietnameză", TranslationToLanguageId = ro.RevisionId, },                      // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "вьетнамский", TranslationToLanguageId = ru.RevisionId, },                     // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "vietnamský", TranslationToLanguageId = sk.RevisionId, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "Vietnamese", TranslationToLanguageId = sl.RevisionId, },                      // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "vietnamisht", TranslationToLanguageId = sq.RevisionId, },                     // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "вијетнамски", TranslationToLanguageId = sr.RevisionId, },                     // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "vietnamese", TranslationToLanguageId = sv.RevisionId, },                      // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kivietinamu", TranslationToLanguageId = sw.RevisionId, },                     // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "வியட்னாமீஸ்", TranslationToLanguageId = ta.RevisionId, },                  // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "వియత్నమీస్", TranslationToLanguageId = te.RevisionId, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "เวียตนาม", TranslationToLanguageId = th.RevisionId, },                          // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Vietnam", TranslationToLanguageId = tr.RevisionId, },                         // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "В'єтнамський", TranslationToLanguageId = uk.RevisionId, },                    // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "ويتنامی", TranslationToLanguageId = ur.RevisionId, },                        // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Việt", TranslationToLanguageId = vi.RevisionId, },                            // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "וויעטנאַמעזיש", TranslationToLanguageId = yi.RevisionId, },                  // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "越南", TranslationToLanguageId = zh.RevisionId, },                            // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = vi.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!yi.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = en.RevisionId, },                // #1  Language = "English"
//                    new CreateLanguageName { Text = "yídish", TranslationToLanguageId = es.RevisionId, },                 // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Jiddisch", TranslationToLanguageId = de.RevisionId, },               // #3  Language = "German"
//                    new CreateLanguageName { Text = "اليديشية", TranslationToLanguageId = ar.RevisionId, },              // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Jiddisj", TranslationToLanguageId = af.RevisionId, },                // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "Yidiş", TranslationToLanguageId = az.RevisionId, },                  // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "ідыш", TranslationToLanguageId = be.RevisionId, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "идиш", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "ইডীশ্", TranslationToLanguageId = bn.RevisionId, },                   // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "Jiddisch", TranslationToLanguageId = ca.RevisionId, },               // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = cs.RevisionId, },                  // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Iddeweg", TranslationToLanguageId = cy.RevisionId, },                // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "jiddisch", TranslationToLanguageId = da.RevisionId, },               // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "Jidiš", TranslationToLanguageId = et.RevisionId, },                  // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = eu.RevisionId, },                // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "ییدیش", TranslationToLanguageId = fa.RevisionId, },                 // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "Jiddiš", TranslationToLanguageId = fi.RevisionId, },                 // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "yiddish", TranslationToLanguageId = fr.RevisionId, },                // #18 Language = "French"
//                    new CreateLanguageName { Text = "Giúdais", TranslationToLanguageId = ga.RevisionId, },                // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = gl.RevisionId, },                // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "યીદ્દીશ", TranslationToLanguageId = gu.RevisionId, },                  // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "אידיש", TranslationToLanguageId = he.RevisionId, },                 // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "यहूदी", TranslationToLanguageId = hi.RevisionId, },                   // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = hr.RevisionId, },                  // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = ht.RevisionId, },                // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "jiddis", TranslationToLanguageId = hu.RevisionId, },                 // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "իդիշ", TranslationToLanguageId = hy.RevisionId, },                   // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Yiddi", TranslationToLanguageId = id.RevisionId, },                  // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "jiddíska", TranslationToLanguageId = isLanguage.RevisionId, },       // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "yiddish", TranslationToLanguageId = it.RevisionId, },                // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "イディッシュ語", TranslationToLanguageId = ja.RevisionId, },          // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "იდიშზე", TranslationToLanguageId = ka.RevisionId, },                // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಯಿಡ್ಡಿಷ್", TranslationToLanguageId = kn.RevisionId, },                  // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "이디시 말의 뜻", TranslationToLanguageId = ko.RevisionId, },          // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = la.RevisionId, },                // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = lt.RevisionId, },                  // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "jidišs", TranslationToLanguageId = lv.RevisionId, },                 // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "јидски", TranslationToLanguageId = mk.RevisionId, },                 // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Bahasa Yiddish", TranslationToLanguageId = ms.RevisionId, },         // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Jiddix", TranslationToLanguageId = mt.RevisionId, },                 // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Jiddisch", TranslationToLanguageId = nl.RevisionId, },               // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "jiddisch", TranslationToLanguageId = no.RevisionId, },               // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "jidysz", TranslationToLanguageId = pl.RevisionId, },                 // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "ídiche", TranslationToLanguageId = pt.RevisionId, },                 // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "idiş", TranslationToLanguageId = ro.RevisionId, },                   // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "идиш", TranslationToLanguageId = ru.RevisionId, },                   // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = sk.RevisionId, },                  // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "jidiš", TranslationToLanguageId = sl.RevisionId, },                  // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "jidish", TranslationToLanguageId = sq.RevisionId, },                 // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "јидиш", TranslationToLanguageId = sr.RevisionId, },                  // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "jiddisch", TranslationToLanguageId = sv.RevisionId, },               // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = sw.RevisionId, },                // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "ஈத்திஷ", TranslationToLanguageId = ta.RevisionId, },               // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "యిడ్డిష్", TranslationToLanguageId = te.RevisionId, },                  // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "Yiddish", TranslationToLanguageId = th.RevisionId, },                // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Eskenazi dili", TranslationToLanguageId = tr.RevisionId, },          // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Ідиш", TranslationToLanguageId = uk.RevisionId, },                   // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "يادش", TranslationToLanguageId = ur.RevisionId, },                  // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "tiếng Yiddish", TranslationToLanguageId = vi.RevisionId, },          // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "ייִדיש", TranslationToLanguageId = yi.RevisionId, },                 // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "意第绪语", TranslationToLanguageId = zh.RevisionId, },                // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = yi.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (!zh.Names.Any())
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chinese", TranslationToLanguageId = en.RevisionId, },                    // #1  Language = "English"
//                    new CreateLanguageName { Text = "chino", TranslationToLanguageId = es.RevisionId, },                      // #2  Language = "Spanish"
//                    new CreateLanguageName { Text = "Chinese", TranslationToLanguageId = de.RevisionId, },                    // #3  Language = "German"
//                    new CreateLanguageName { Text = "الصينية", TranslationToLanguageId = ar.RevisionId, },                   // #4  Language = "Arabic"
//                    new CreateLanguageName { Text = "Chinese", TranslationToLanguageId = af.RevisionId, },                    // #5  Language = "Afrikaans"
//                    new CreateLanguageName { Text = "çin", TranslationToLanguageId = az.RevisionId, },                        // #6  Language = "Azerbaijani"
//                    new CreateLanguageName { Text = "Кітайскі", TranslationToLanguageId = be.RevisionId, },                   // #7  Language = "Belarusian"
//                    new CreateLanguageName { Text = "китайски", TranslationToLanguageId = bg.RevisionId, },                   // #8  Language = "Bulgarian"
//                    new CreateLanguageName { Text = "চীনা", TranslationToLanguageId = bn.RevisionId, },                        // #9  Language = "Bengali"
//                    new CreateLanguageName { Text = "xinès", TranslationToLanguageId = ca.RevisionId, },                      // #10 Language = "Catalan"
//                    new CreateLanguageName { Text = "Číňan", TranslationToLanguageId = cs.RevisionId, },                      // #11 Language = "Czech"
//                    new CreateLanguageName { Text = "Tseiniaidd", TranslationToLanguageId = cy.RevisionId, },                 // #12 Language = "Welsh"
//                    new CreateLanguageName { Text = "kinesisk", TranslationToLanguageId = da.RevisionId, },                   // #13 Language = "Danish"
//                    new CreateLanguageName { Text = "hiina", TranslationToLanguageId = et.RevisionId, },                      // #14 Language = "Estonian"
//                    new CreateLanguageName { Text = "txinera", TranslationToLanguageId = eu.RevisionId, },                    // #15 Language = "Basque"
//                    new CreateLanguageName { Text = "چینی", TranslationToLanguageId = fa.RevisionId, },                      // #16 Language = "Persian"
//                    new CreateLanguageName { Text = "kiinalainen", TranslationToLanguageId = fi.RevisionId, },                // #17 Language = "Finnish"
//                    new CreateLanguageName { Text = "chinoise", TranslationToLanguageId = fr.RevisionId, },                   // #18 Language = "French"
//                    new CreateLanguageName { Text = "Sínis", TranslationToLanguageId = ga.RevisionId, },                      // #19 Language = "Irish"
//                    new CreateLanguageName { Text = "chinés", TranslationToLanguageId = gl.RevisionId, },                     // #20 Language = "Galician"
//                    new CreateLanguageName { Text = "ચિની", TranslationToLanguageId = gu.RevisionId, },                       // #21 Language = "Gujarati"
//                    new CreateLanguageName { Text = "סינית", TranslationToLanguageId = he.RevisionId, },                     // #22 Language = "Hebrew"
//                    new CreateLanguageName { Text = "चीनी", TranslationToLanguageId = hi.RevisionId, },                        // #23 Language = "Hindi"
//                    new CreateLanguageName { Text = "kineski", TranslationToLanguageId = hr.RevisionId, },                    // #24 Language = "Croatian"
//                    new CreateLanguageName { Text = "Chinwa", TranslationToLanguageId = ht.RevisionId, },                     // #25 Language = "Haitian Creo
//                    new CreateLanguageName { Text = "kínai", TranslationToLanguageId = hu.RevisionId, },                      // #26 Language = "Hungarian"
//                    new CreateLanguageName { Text = "չինացի", TranslationToLanguageId = hy.RevisionId, },                     // #27 Language = "Armenian"
//                    new CreateLanguageName { Text = "Cina", TranslationToLanguageId = id.RevisionId, },                       // #28 Language = "Indonesian"
//                    new CreateLanguageName { Text = "kínverska", TranslationToLanguageId = isLanguage.RevisionId, },          // #29 Language = "Icelandic"
//                    new CreateLanguageName { Text = "cinese", TranslationToLanguageId = it.RevisionId, },                     // #30 Language = "Italian"
//                    new CreateLanguageName { Text = "中国", TranslationToLanguageId = ja.RevisionId, },                       // #31 Language = "Japanese"
//                    new CreateLanguageName { Text = "ჩინური", TranslationToLanguageId = ka.RevisionId, },                    // #32 Language = "Georgian"
//                    new CreateLanguageName { Text = "ಚೀನೀ", TranslationToLanguageId = kn.RevisionId, },                       // #33 Language = "Kannada"
//                    new CreateLanguageName { Text = "중국어", TranslationToLanguageId = ko.RevisionId, },                     // #34 Language = "Korean"
//                    new CreateLanguageName { Text = "Sinica", TranslationToLanguageId = la.RevisionId, },                     // #35 Language = "Latin"
//                    new CreateLanguageName { Text = "Kinijos", TranslationToLanguageId = lt.RevisionId, },                    // #36 Language = "Lithuanian"
//                    new CreateLanguageName { Text = "ķīniešu", TranslationToLanguageId = lv.RevisionId, },                    // #37 Language = "Latvian"
//                    new CreateLanguageName { Text = "кинески", TranslationToLanguageId = mk.RevisionId, },                    // #38 Language = "Macedonian"
//                    new CreateLanguageName { Text = "Cina", TranslationToLanguageId = ms.RevisionId, },                       // #39 Language = "Malay"
//                    new CreateLanguageName { Text = "Ċiniż", TranslationToLanguageId = mt.RevisionId, },                      // #40 Language = "Maltese"
//                    new CreateLanguageName { Text = "Chinees", TranslationToLanguageId = nl.RevisionId, },                    // #41 Language = "Dutch"
//                    new CreateLanguageName { Text = "kinesiske", TranslationToLanguageId = no.RevisionId, },                  // #42 Language = "Norwegian"
//                    new CreateLanguageName { Text = "chiński", TranslationToLanguageId = pl.RevisionId, },                    // #43 Language = "Polish"
//                    new CreateLanguageName { Text = "chinês", TranslationToLanguageId = pt.RevisionId, },                     // #44 Language = "Portuguese"
//                    new CreateLanguageName { Text = "chinezesc", TranslationToLanguageId = ro.RevisionId, },                  // #45 Language = "Romanian"
//                    new CreateLanguageName { Text = "китайский", TranslationToLanguageId = ru.RevisionId, },                  // #46 Language = "Russian"
//                    new CreateLanguageName { Text = "Číňan", TranslationToLanguageId = sk.RevisionId, },                      // #47 Language = "Slovak"
//                    new CreateLanguageName { Text = "kitajski", TranslationToLanguageId = sl.RevisionId, },                   // #48 Language = "Slovenian"
//                    new CreateLanguageName { Text = "kinez", TranslationToLanguageId = sq.RevisionId, },                      // #49 Language = "Albanian"
//                    new CreateLanguageName { Text = "кинески", TranslationToLanguageId = sr.RevisionId, },                    // #50 Language = "Serbian"
//                    new CreateLanguageName { Text = "kinesiska", TranslationToLanguageId = sv.RevisionId, },                  // #51 Language = "Swedish"
//                    new CreateLanguageName { Text = "Kichina", TranslationToLanguageId = sw.RevisionId, },                    // #52 Language = "Swahili"
//                    new CreateLanguageName { Text = "சீன", TranslationToLanguageId = ta.RevisionId, },                       // #53 Language = "Tamil"
//                    new CreateLanguageName { Text = "చైనీస్", TranslationToLanguageId = te.RevisionId, },                       // #54 Language = "Telugu"
//                    new CreateLanguageName { Text = "จีน", TranslationToLanguageId = th.RevisionId, },                         // #55 Language = "Thai"
//                    new CreateLanguageName { Text = "Çin", TranslationToLanguageId = tr.RevisionId, },                        // #56 Language = "Turkish"
//                    new CreateLanguageName { Text = "Китайський", TranslationToLanguageId = uk.RevisionId, },                 // #57 Language = "Ukrainian"
//                    new CreateLanguageName { Text = "چینی", TranslationToLanguageId = ur.RevisionId, },                      // #58 Language = "Urdu"
//                    new CreateLanguageName { Text = "Trung Quốc", TranslationToLanguageId = vi.RevisionId, },                 // #59 Language = "Vietnamese"
//                    new CreateLanguageName { Text = "כינעזיש", TranslationToLanguageId = yi.RevisionId, },                   // #60 Language = "Yiddish"
//                    new CreateLanguageName { Text = "中国", TranslationToLanguageId = zh.RevisionId, },                       // #61 Language = "Chinese"
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = zh.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            //Languages with only 1 translation

//            if (aa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Afar", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = aa.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (ab.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Abkhazian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ab.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ak.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Akan", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ak.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (am.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Amharic", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = am.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (an.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Aragonese", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = an.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (asLanguage.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Assamese", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = asLanguage.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (av.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Avaric", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = av.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ay.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Aymara", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ay.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ba.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bashkir", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ba.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (bh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bihari languages", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bh.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (bi.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bislama", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bi.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (bm.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bambara", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bm.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (bo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tibetan", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bo.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (br.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Breton", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = br.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (bs.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Bosnian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = bs.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (ce.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chechen", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ce.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ch.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chamorro", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ch.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (co.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Corsican", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = co.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (cr.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Cree", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cr.RevisionId;
//                    _createLanguageName.Handle(n);
//                });



//            if (cu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Church Slavic", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cu.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (cv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Chuvash", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = cv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });



//            if (dv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Dhivehi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = dv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (dz.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Dzongkha", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = dz.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ee.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ewe", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ee.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (el.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Modern Greek (1453-)", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = el.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (eo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Esperanto", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = eo.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ff.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Fulah", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ff.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (fj.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Fijian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fj.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (fo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Faroese", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fo.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (fy.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Western Frisian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = fy.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (gd.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Scottish Gaelic", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gd.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (gn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Guarani", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (gv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Manx", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = gv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ha.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hausa", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ha.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ho.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Hiri Motu", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ho.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (hz.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Herero", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = hz.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ia.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Interlingua (International Auxiliary Language Association)", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ia.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ie.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Interlingue", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ie.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ig.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Igbo", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ig.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ii.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sichuan Yi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ii.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (io.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ido", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = io.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (iu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Inuktitut", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = iu.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (jv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Javanese", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = jv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (kg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kongo", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kg.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ki.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kikuyu", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ki.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (kj.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kuanyama", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kj.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (kk.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kazakh", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kk.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (kl.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kalaallisut", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kl.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (km.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Central Khmer", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = km.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ks.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kashmiri", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ks.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ku.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kurdish", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ku.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (kv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Komi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (kw.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Cornish", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = kw.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ky.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kirghiz", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ky.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (lb.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Luxembourgish", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lb.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (lg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ganda", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lg.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (li.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Limburgan", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = li.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ln.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Lingala", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ln.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (lo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Lao", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = lo.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (mg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Malagasy", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mg.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (mh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Marshallese", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mh.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (mi.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Maori", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mi.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ml.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Malayalam", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ml.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (mn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Mongolian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (mr.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Marathi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = mr.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (my.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Burmese", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = my.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (na.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Nauru", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = na.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (nb.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Norwegian Bokmål", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nb.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ne.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Nepali", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ne.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ng.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ndonga", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ng.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (nn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Norwegian Nynorsk", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (nv.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Navajo", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = nv.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ny.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Nyanja", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ny.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (oc.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Occitan (post 1500)", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = oc.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (om.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Oromo", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = om.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (or.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Oriya", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = or.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (os.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Ossetian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = os.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (pa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Panjabi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pa.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (pi.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Pali", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = pi.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ps.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Pushto", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ps.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (qu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Quechua", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = qu.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (rm.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Romansh", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = rm.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (rn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Rundi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = rn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (rw.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Kinyarwanda", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = rw.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (sa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sanskrit", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sa.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (sc.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sardinian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sc.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (sd.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sindhi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sd.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (se.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Northern Sami", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = se.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (sg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sango", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sg.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (sh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Serbo-Croatian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sh.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (si.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sinhala", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = si.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (sm.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Samoan", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sm.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (sn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Shona", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = sn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (so.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Somali", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = so.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (ss.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Swati", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ss.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (st.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Southern Sotho", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = st.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (su.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Sundanese", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = su.RevisionId;
//                    _createLanguageName.Handle(n);
//                });


//            if (tg.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tajik", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tg.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ti.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tigrinya", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ti.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (tk.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Turkmen", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tk.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (tl.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tagalog", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tl.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (tn.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tswana", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tn.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (to.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tonga (Tonga Islands)", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = to.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ts.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tsonga", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ts.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (tt.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tatar", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tt.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (tw.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Twi", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = tw.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ty.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Tahitian", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ty.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ug.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Uighur", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ug.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (uz.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Uzbek", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = uz.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (ve.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Venda", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = ve.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (vo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Volapük", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = vo.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (wa.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Walloon", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = wa.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (wo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Wolof", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = wo.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (xh.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Xhosa", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = xh.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (yo.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Yoruba", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = yo.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (za.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Zhuang", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = za.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            if (zu.Names.Count < 1)
//                new List<CreateLanguageName>
//                {
//                    new CreateLanguageName { Text = "Zulu", TranslationToLanguageId = en.RevisionId, },
//                }
//                .ForEach(n =>
//                {
//                    n.LanguageId = zu.RevisionId;
//                    _createLanguageName.Handle(n);
//                });

//            _unitOfWork.SaveChanges();

//            #endregion
//        }
//    }
//}