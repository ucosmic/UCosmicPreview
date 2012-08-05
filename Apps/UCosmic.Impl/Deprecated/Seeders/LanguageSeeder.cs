//using System.Collections.Generic;
//using System.Linq;
//using UCosmic.Impl.Orm;
//using UCosmic.Domain.Languages;

//namespace UCosmic.Impl.Seeders
//{
//    public class LanguageSeeder : ISeedDb
//    {
//        public void Seed(UCosmicContext context)
//        {
//            new LanguageSqlDbSeeder().Seed(context);
//        }
//    }

//    public class LanguageSqlDbSeeder : NonContentFileSqlDbSeeder
//    {
//        protected override IEnumerable<string> SqlScripts
//        {
//            get
//            {
//                return new[] { "LanguagesData.sql" };
//            }
//        }

//        public override void Seed(UCosmicContext context)
//        {
//            if (!context.Set<Language>().Any())
//                base.Seed(context);
//        }

//    }

//    //// languages currently are imported from a sql script

//    //public class LanguageEntityDbSeeder : UCosmicDbSeeder
//    //{
//    //    public override void Seed(UCosmicContext context)
//    //    {
//    //        Context = context;

//    //        #region SIL Languages

//    //        var en = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("en", StringComparison.InvariantCultureIgnoreCase));
//    //        if (en == null)
//    //        {
//    //            en = new Language { TwoLetterIsoCode = "en", ThreeLetterIsoCode = "eng", ThreeLetterIsoBibliographicCode = "eng", };
//    //            Context.Languages.Add(en);
//    //        }

//    //        var es = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("es", StringComparison.InvariantCultureIgnoreCase));
//    //        if (es == null)
//    //        {
//    //            es = new Language { TwoLetterIsoCode = "es", ThreeLetterIsoCode = "spa", ThreeLetterIsoBibliographicCode = "spa", };
//    //            Context.Languages.Add(es);
//    //        }

//    //        var de = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("de", StringComparison.InvariantCultureIgnoreCase));
//    //        if (de == null)
//    //        {
//    //            de = new Language { TwoLetterIsoCode = "de", ThreeLetterIsoCode = "deu", ThreeLetterIsoBibliographicCode = "ger", };
//    //            Context.Languages.Add(de);
//    //        }

//    //        var ar = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ar", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ar == null)
//    //        {
//    //            ar = new Language { TwoLetterIsoCode = "ar", ThreeLetterIsoCode = "ara", ThreeLetterIsoBibliographicCode = "ara", };
//    //            Context.Languages.Add(ar);
//    //        }

//    //        var aa = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("aa", StringComparison.InvariantCultureIgnoreCase));
//    //        if (aa == null)
//    //        {
//    //            aa = new Language { TwoLetterIsoCode = "aa", ThreeLetterIsoCode = "aar", ThreeLetterIsoBibliographicCode = "aar", };
//    //            Context.Languages.Add(aa);
//    //        }

//    //        var ab = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ab", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ab == null)
//    //        {
//    //            ab = new Language { TwoLetterIsoCode = "ab", ThreeLetterIsoCode = "abk", ThreeLetterIsoBibliographicCode = "abk", };
//    //            Context.Languages.Add(ab);
//    //        }

//    //        var af = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("af", StringComparison.InvariantCultureIgnoreCase));
//    //        if (af == null)
//    //        {
//    //            af = new Language { TwoLetterIsoCode = "af", ThreeLetterIsoCode = "afr", ThreeLetterIsoBibliographicCode = "afr", };
//    //            Context.Languages.Add(af);
//    //        }

//    //        var ak = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ak", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ak == null)
//    //        {
//    //            ak = new Language { TwoLetterIsoCode = "ak", ThreeLetterIsoCode = "aka", ThreeLetterIsoBibliographicCode = "aka", };
//    //            Context.Languages.Add(ak);
//    //        }

//    //        var am = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("am", StringComparison.InvariantCultureIgnoreCase));
//    //        if (am == null)
//    //        {
//    //            am = new Language { TwoLetterIsoCode = "am", ThreeLetterIsoCode = "amh", ThreeLetterIsoBibliographicCode = "amh", };
//    //            Context.Languages.Add(am);
//    //        }

//    //        var an = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("an", StringComparison.InvariantCultureIgnoreCase));
//    //        if (an == null)
//    //        {
//    //            an = new Language { TwoLetterIsoCode = "an", ThreeLetterIsoCode = "arg", ThreeLetterIsoBibliographicCode = "arg", };
//    //            Context.Languages.Add(an);
//    //        }

//    //        var asLanguage = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("as", StringComparison.InvariantCultureIgnoreCase));
//    //        if (asLanguage == null)
//    //        {
//    //            asLanguage = new Language { TwoLetterIsoCode = "as", ThreeLetterIsoCode = "asm", ThreeLetterIsoBibliographicCode = "asm", };
//    //            Context.Languages.Add(asLanguage);
//    //        }

//    //        var av = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("av", StringComparison.InvariantCultureIgnoreCase));
//    //        if (av == null)
//    //        {
//    //            av = new Language { TwoLetterIsoCode = "av", ThreeLetterIsoCode = "ava", ThreeLetterIsoBibliographicCode = "ava", };
//    //            Context.Languages.Add(av);
//    //        }

//    //        var ay = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ay", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ay == null)
//    //        {
//    //            ay = new Language { TwoLetterIsoCode = "ay", ThreeLetterIsoCode = "aym", ThreeLetterIsoBibliographicCode = "aym", };
//    //            Context.Languages.Add(ay);
//    //        }

//    //        var az = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("az", StringComparison.InvariantCultureIgnoreCase));
//    //        if (az == null)
//    //        {
//    //            az = new Language { TwoLetterIsoCode = "az", ThreeLetterIsoCode = "aze", ThreeLetterIsoBibliographicCode = "aze", };
//    //            Context.Languages.Add(az);
//    //        }

//    //        var ba = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ba", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ba == null)
//    //        {
//    //            ba = new Language { TwoLetterIsoCode = "ba", ThreeLetterIsoCode = "bak", ThreeLetterIsoBibliographicCode = "bak", };
//    //            Context.Languages.Add(ba);
//    //        }

//    //        var be = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("be", StringComparison.InvariantCultureIgnoreCase));
//    //        if (be == null)
//    //        {
//    //            be = new Language { TwoLetterIsoCode = "be", ThreeLetterIsoCode = "bel", ThreeLetterIsoBibliographicCode = "bel", };
//    //            Context.Languages.Add(be);
//    //        }

//    //        var bg = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("bg", StringComparison.InvariantCultureIgnoreCase));
//    //        if (bg == null)
//    //        {
//    //            bg = new Language { TwoLetterIsoCode = "bg", ThreeLetterIsoCode = "bul", ThreeLetterIsoBibliographicCode = "bul", };
//    //            Context.Languages.Add(bg);
//    //        }

//    //        var bh = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("bh", StringComparison.InvariantCultureIgnoreCase));
//    //        if (bh == null)
//    //        {
//    //            bh = new Language { TwoLetterIsoCode = "bh", ThreeLetterIsoCode = "bih", ThreeLetterIsoBibliographicCode = "bih", };
//    //            Context.Languages.Add(bh);
//    //        }

//    //        var bi = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("bi", StringComparison.InvariantCultureIgnoreCase));
//    //        if (bi == null)
//    //        {
//    //            bi = new Language { TwoLetterIsoCode = "bi", ThreeLetterIsoCode = "bis", ThreeLetterIsoBibliographicCode = "bis", };
//    //            Context.Languages.Add(bi);
//    //        }

//    //        var bm = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("bm", StringComparison.InvariantCultureIgnoreCase));
//    //        if (bm == null)
//    //        {
//    //            bm = new Language { TwoLetterIsoCode = "bm", ThreeLetterIsoCode = "bam", ThreeLetterIsoBibliographicCode = "bam", };
//    //            Context.Languages.Add(bm);
//    //        }

//    //        var bn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("bn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (bn == null)
//    //        {
//    //            bn = new Language { TwoLetterIsoCode = "bn", ThreeLetterIsoCode = "ben", ThreeLetterIsoBibliographicCode = "ben", };
//    //            Context.Languages.Add(bn);
//    //        }

//    //        var bo = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("bo", StringComparison.InvariantCultureIgnoreCase));
//    //        if (bo == null)
//    //        {
//    //            bo = new Language { TwoLetterIsoCode = "bo", ThreeLetterIsoCode = "bod", ThreeLetterIsoBibliographicCode = "tib", };
//    //            Context.Languages.Add(bo);
//    //        }

//    //        var br = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("br", StringComparison.InvariantCultureIgnoreCase));
//    //        if (br == null)
//    //        {
//    //            br = new Language { TwoLetterIsoCode = "br", ThreeLetterIsoCode = "bre", ThreeLetterIsoBibliographicCode = "bre", };
//    //            Context.Languages.Add(br);
//    //        }

//    //        var bs = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("bs", StringComparison.InvariantCultureIgnoreCase));
//    //        if (bs == null)
//    //        {
//    //            bs = new Language { TwoLetterIsoCode = "bs", ThreeLetterIsoCode = "bos", ThreeLetterIsoBibliographicCode = "bos", };
//    //            Context.Languages.Add(bs);
//    //        }

//    //        var ca = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ca", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ca == null)
//    //        {
//    //            ca = new Language { TwoLetterIsoCode = "ca", ThreeLetterIsoCode = "cat", ThreeLetterIsoBibliographicCode = "cat", };
//    //            Context.Languages.Add(ca);
//    //        }

//    //        var ce = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ce", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ce == null)
//    //        {
//    //            ce = new Language { TwoLetterIsoCode = "ce", ThreeLetterIsoCode = "che", ThreeLetterIsoBibliographicCode = "che", };
//    //            Context.Languages.Add(ce);
//    //        }

//    //        var ch = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ch", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ch == null)
//    //        {
//    //            ch = new Language { TwoLetterIsoCode = "ch", ThreeLetterIsoCode = "cha", ThreeLetterIsoBibliographicCode = "cha", };
//    //            Context.Languages.Add(ch);
//    //        }

//    //        var co = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("co", StringComparison.InvariantCultureIgnoreCase));
//    //        if (co == null)
//    //        {
//    //            co = new Language { TwoLetterIsoCode = "co", ThreeLetterIsoCode = "cos", ThreeLetterIsoBibliographicCode = "cos", };
//    //            Context.Languages.Add(co);
//    //        }

//    //        var cr = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("cr", StringComparison.InvariantCultureIgnoreCase));
//    //        if (cr == null)
//    //        {
//    //            cr = new Language { TwoLetterIsoCode = "cr", ThreeLetterIsoCode = "cre", ThreeLetterIsoBibliographicCode = "cre", };
//    //            Context.Languages.Add(cr);
//    //        }

//    //        var cs = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("cs", StringComparison.InvariantCultureIgnoreCase));
//    //        if (cs == null)
//    //        {
//    //            cs = new Language { TwoLetterIsoCode = "cs", ThreeLetterIsoCode = "ces", ThreeLetterIsoBibliographicCode = "cze", };
//    //            Context.Languages.Add(cs);
//    //        }

//    //        var cu = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("cu", StringComparison.InvariantCultureIgnoreCase));
//    //        if (cu == null)
//    //        {
//    //            cu = new Language { TwoLetterIsoCode = "cu", ThreeLetterIsoCode = "chu", ThreeLetterIsoBibliographicCode = "chu", };
//    //            Context.Languages.Add(cu);
//    //        }

//    //        var cv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("cv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (cv == null)
//    //        {
//    //            cv = new Language { TwoLetterIsoCode = "cv", ThreeLetterIsoCode = "chv", ThreeLetterIsoBibliographicCode = "chv", };
//    //            Context.Languages.Add(cv);
//    //        }

//    //        var cy = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("cy", StringComparison.InvariantCultureIgnoreCase));
//    //        if (cy == null)
//    //        {
//    //            cy = new Language { TwoLetterIsoCode = "cy", ThreeLetterIsoCode = "cym", ThreeLetterIsoBibliographicCode = "wel", };
//    //            Context.Languages.Add(cy);
//    //        }

//    //        var da = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("da", StringComparison.InvariantCultureIgnoreCase));
//    //        if (da == null)
//    //        {
//    //            da = new Language { TwoLetterIsoCode = "da", ThreeLetterIsoCode = "dan", ThreeLetterIsoBibliographicCode = "dan", };
//    //            Context.Languages.Add(da);
//    //        }

//    //        var dv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("dv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (dv == null)
//    //        {
//    //            dv = new Language { TwoLetterIsoCode = "dv", ThreeLetterIsoCode = "div", ThreeLetterIsoBibliographicCode = "div", };
//    //            Context.Languages.Add(dv);
//    //        }

//    //        var dz = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("dz", StringComparison.InvariantCultureIgnoreCase));
//    //        if (dz == null)
//    //        {
//    //            dz = new Language { TwoLetterIsoCode = "dz", ThreeLetterIsoCode = "dzo", ThreeLetterIsoBibliographicCode = "dzo", };
//    //            Context.Languages.Add(dz);
//    //        }

//    //        var ee = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ee", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ee == null)
//    //        {
//    //            ee = new Language { TwoLetterIsoCode = "ee", ThreeLetterIsoCode = "ewe", ThreeLetterIsoBibliographicCode = "ewe", };
//    //            Context.Languages.Add(ee);
//    //        }

//    //        var el = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("el", StringComparison.InvariantCultureIgnoreCase));
//    //        if (el == null)
//    //        {
//    //            el = new Language { TwoLetterIsoCode = "el", ThreeLetterIsoCode = "ell", ThreeLetterIsoBibliographicCode = "ell", };
//    //            Context.Languages.Add(el);
//    //        }

//    //        var eo = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("eo", StringComparison.InvariantCultureIgnoreCase));
//    //        if (eo == null)
//    //        {
//    //            eo = new Language { TwoLetterIsoCode = "eo", ThreeLetterIsoCode = "epo", ThreeLetterIsoBibliographicCode = "epo", };
//    //            Context.Languages.Add(eo);
//    //        }

//    //        var et = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("et", StringComparison.InvariantCultureIgnoreCase));
//    //        if (et == null)
//    //        {
//    //            et = new Language { TwoLetterIsoCode = "et", ThreeLetterIsoCode = "est", ThreeLetterIsoBibliographicCode = "est", };
//    //            Context.Languages.Add(et);
//    //        }

//    //        var eu = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("eu", StringComparison.InvariantCultureIgnoreCase));
//    //        if (eu == null)
//    //        {
//    //            eu = new Language { TwoLetterIsoCode = "eu", ThreeLetterIsoCode = "eus", ThreeLetterIsoBibliographicCode = "baq", };
//    //            Context.Languages.Add(eu);
//    //        }

//    //        var fa = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("fa", StringComparison.InvariantCultureIgnoreCase));
//    //        if (fa == null)
//    //        {
//    //            fa = new Language { TwoLetterIsoCode = "fa", ThreeLetterIsoCode = "fas", ThreeLetterIsoBibliographicCode = "per", };
//    //            Context.Languages.Add(fa);
//    //        }

//    //        var ff = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ff", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ff == null)
//    //        {
//    //            ff = new Language { TwoLetterIsoCode = "ff", ThreeLetterIsoCode = "ful", ThreeLetterIsoBibliographicCode = "ful", };
//    //            Context.Languages.Add(ff);
//    //        }

//    //        var fj = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("fj", StringComparison.InvariantCultureIgnoreCase));
//    //        if (fj == null)
//    //        {
//    //            fj = new Language { TwoLetterIsoCode = "fj", ThreeLetterIsoCode = "fij", ThreeLetterIsoBibliographicCode = "fij", };
//    //            Context.Languages.Add(fj);
//    //        }

//    //        var fo = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("fo", StringComparison.InvariantCultureIgnoreCase));
//    //        if (fo == null)
//    //        {
//    //            fo = new Language { TwoLetterIsoCode = "fo", ThreeLetterIsoCode = "fao", ThreeLetterIsoBibliographicCode = "fao", };
//    //            Context.Languages.Add(fo);
//    //        }

//    //        var fy = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("fy", StringComparison.InvariantCultureIgnoreCase));
//    //        if (fy == null)
//    //        {
//    //            fy = new Language { TwoLetterIsoCode = "fy", ThreeLetterIsoCode = "fry", ThreeLetterIsoBibliographicCode = "fry", };
//    //            Context.Languages.Add(fy);
//    //        }

//    //        var gd = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("gd", StringComparison.InvariantCultureIgnoreCase));
//    //        if (gd == null)
//    //        {
//    //            gd = new Language { TwoLetterIsoCode = "gd", ThreeLetterIsoCode = "gla", ThreeLetterIsoBibliographicCode = "gla", };
//    //            Context.Languages.Add(gd);
//    //        }

//    //        var fi = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("fi", StringComparison.InvariantCultureIgnoreCase));
//    //        if (fi == null)
//    //        {
//    //            fi = new Language { TwoLetterIsoCode = "fi", ThreeLetterIsoCode = "fin", ThreeLetterIsoBibliographicCode = "fin", };
//    //            Context.Languages.Add(fi);
//    //        }

//    //        var fr = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("fr", StringComparison.InvariantCultureIgnoreCase));
//    //        if (fr == null)
//    //        {
//    //            fr = new Language { TwoLetterIsoCode = "fr", ThreeLetterIsoCode = "fra", ThreeLetterIsoBibliographicCode = "fre", };
//    //            Context.Languages.Add(fr);
//    //        }

//    //        var ga = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ga", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ga == null)
//    //        {
//    //            ga = new Language { TwoLetterIsoCode = "ga", ThreeLetterIsoCode = "gle", ThreeLetterIsoBibliographicCode = "gle", };
//    //            Context.Languages.Add(ga);
//    //        }

//    //        var gl = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("gl", StringComparison.InvariantCultureIgnoreCase));
//    //        if (gl == null)
//    //        {
//    //            gl = new Language { TwoLetterIsoCode = "gl", ThreeLetterIsoCode = "glg", ThreeLetterIsoBibliographicCode = "glg", };
//    //            Context.Languages.Add(gl);
//    //        }

//    //        var gn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("gn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (gn == null)
//    //        {
//    //            gn = new Language { TwoLetterIsoCode = "gn", ThreeLetterIsoCode = "grn", ThreeLetterIsoBibliographicCode = "grn", };
//    //            Context.Languages.Add(gn);
//    //        }

//    //        var gu = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("gu", StringComparison.InvariantCultureIgnoreCase));
//    //        if (gu == null)
//    //        {
//    //            gu = new Language { TwoLetterIsoCode = "gu", ThreeLetterIsoCode = "guj", ThreeLetterIsoBibliographicCode = "guj", };
//    //            Context.Languages.Add(gu);
//    //        }

//    //        var gv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("gv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (gv == null)
//    //        {
//    //            gv = new Language { TwoLetterIsoCode = "gv", ThreeLetterIsoCode = "glv", ThreeLetterIsoBibliographicCode = "glv", };
//    //            Context.Languages.Add(gv);
//    //        }

//    //        var ha = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ha", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ha == null)
//    //        {
//    //            ha = new Language { TwoLetterIsoCode = "ha", ThreeLetterIsoCode = "hau", ThreeLetterIsoBibliographicCode = "hau", };
//    //            Context.Languages.Add(ha);
//    //        }

//    //        var he = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("he", StringComparison.InvariantCultureIgnoreCase));
//    //        if (he == null)
//    //        {
//    //            he = new Language { TwoLetterIsoCode = "he", ThreeLetterIsoCode = "heb", ThreeLetterIsoBibliographicCode = "heb", };
//    //            Context.Languages.Add(he);
//    //        }

//    //        var hi = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("hi", StringComparison.InvariantCultureIgnoreCase));
//    //        if (hi == null)
//    //        {
//    //            hi = new Language { TwoLetterIsoCode = "hi", ThreeLetterIsoCode = "hin", ThreeLetterIsoBibliographicCode = "hin", };
//    //            Context.Languages.Add(hi);
//    //        }

//    //        var ho = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ho", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ho == null)
//    //        {
//    //            ho = new Language { TwoLetterIsoCode = "ho", ThreeLetterIsoCode = "hmo", ThreeLetterIsoBibliographicCode = "hmo", };
//    //            Context.Languages.Add(ho);
//    //        }

//    //        var hr = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("hr", StringComparison.InvariantCultureIgnoreCase));
//    //        if (hr == null)
//    //        {
//    //            hr = new Language { TwoLetterIsoCode = "hr", ThreeLetterIsoCode = "hrv", ThreeLetterIsoBibliographicCode = "hrv", };
//    //            Context.Languages.Add(hr);
//    //        }

//    //        var ht = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ht", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ht == null)
//    //        {
//    //            ht = new Language { TwoLetterIsoCode = "ht", ThreeLetterIsoCode = "hat", ThreeLetterIsoBibliographicCode = "hat", };
//    //            Context.Languages.Add(ht);
//    //        }

//    //        var hu = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("hu", StringComparison.InvariantCultureIgnoreCase));
//    //        if (hu == null)
//    //        {
//    //            hu = new Language { TwoLetterIsoCode = "hu", ThreeLetterIsoCode = "hun", ThreeLetterIsoBibliographicCode = "hun", };
//    //            Context.Languages.Add(hu);
//    //        }

//    //        var hy = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("hy", StringComparison.InvariantCultureIgnoreCase));
//    //        if (hy == null)
//    //        {
//    //            hy = new Language { TwoLetterIsoCode = "hy", ThreeLetterIsoCode = "hye", ThreeLetterIsoBibliographicCode = "arm", };
//    //            Context.Languages.Add(hy);
//    //        }

//    //        var hz = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("hz", StringComparison.InvariantCultureIgnoreCase));
//    //        if (hz == null)
//    //        {
//    //            hz = new Language { TwoLetterIsoCode = "hz", ThreeLetterIsoCode = "her", ThreeLetterIsoBibliographicCode = "her", };
//    //            Context.Languages.Add(hz);
//    //        }

//    //        var ia = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ia", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ia == null)
//    //        {
//    //            ia = new Language { TwoLetterIsoCode = "ia", ThreeLetterIsoCode = "ina", ThreeLetterIsoBibliographicCode = "ina", };
//    //            Context.Languages.Add(ia);
//    //        }

//    //        var id = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("id", StringComparison.InvariantCultureIgnoreCase));
//    //        if (id == null)
//    //        {
//    //            id = new Language { TwoLetterIsoCode = "id", ThreeLetterIsoCode = "ind", ThreeLetterIsoBibliographicCode = "ind", };
//    //            Context.Languages.Add(id);
//    //        }

//    //        var ie = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ie", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ie == null)
//    //        {
//    //            ie = new Language { TwoLetterIsoCode = "ie", ThreeLetterIsoCode = "ile", ThreeLetterIsoBibliographicCode = "ile", };
//    //            Context.Languages.Add(ie);
//    //        }

//    //        var ig = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ig", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ig == null)
//    //        {
//    //            ig = new Language { TwoLetterIsoCode = "ig", ThreeLetterIsoCode = "ibo", ThreeLetterIsoBibliographicCode = "ibo", };
//    //            Context.Languages.Add(ig);
//    //        }

//    //        var ii = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ii", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ii == null)
//    //        {
//    //            ii = new Language { TwoLetterIsoCode = "ii", ThreeLetterIsoCode = "iii", ThreeLetterIsoBibliographicCode = "iii", };
//    //            Context.Languages.Add(ii);
//    //        }

//    //        var io = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("io", StringComparison.InvariantCultureIgnoreCase));
//    //        if (io == null)
//    //        {
//    //            io = new Language { TwoLetterIsoCode = "io", ThreeLetterIsoCode = "ido", ThreeLetterIsoBibliographicCode = "ido", };
//    //            Context.Languages.Add(io);
//    //        }

//    //        var isLanguage = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("is", StringComparison.InvariantCultureIgnoreCase));
//    //        if (isLanguage == null)
//    //        {
//    //            isLanguage = new Language { TwoLetterIsoCode = "is", ThreeLetterIsoCode = "isl", ThreeLetterIsoBibliographicCode = "ice", };
//    //            Context.Languages.Add(isLanguage);
//    //        }

//    //        var it = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("it", StringComparison.InvariantCultureIgnoreCase));
//    //        if (it == null)
//    //        {
//    //            it = new Language { TwoLetterIsoCode = "it", ThreeLetterIsoCode = "ita", ThreeLetterIsoBibliographicCode = "ita", };
//    //            Context.Languages.Add(it);
//    //        }

//    //        var iu = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("iu", StringComparison.InvariantCultureIgnoreCase));
//    //        if (iu == null)
//    //        {
//    //            iu = new Language { TwoLetterIsoCode = "iu", ThreeLetterIsoCode = "iku", ThreeLetterIsoBibliographicCode = "iku", };
//    //            Context.Languages.Add(iu);
//    //        }

//    //        var ja = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ja", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ja == null)
//    //        {
//    //            ja = new Language { TwoLetterIsoCode = "ja", ThreeLetterIsoCode = "jpn", ThreeLetterIsoBibliographicCode = "jpn", };
//    //            Context.Languages.Add(ja);
//    //        }


//    //        var jv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("jv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (jv == null)
//    //        {
//    //            jv = new Language { TwoLetterIsoCode = "jv", ThreeLetterIsoCode = "jav", ThreeLetterIsoBibliographicCode = "jav", };
//    //            Context.Languages.Add(jv);
//    //        }

//    //        var ka = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ka", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ka == null)
//    //        {
//    //            ka = new Language { TwoLetterIsoCode = "ka", ThreeLetterIsoCode = "kat", ThreeLetterIsoBibliographicCode = "geo", };
//    //            Context.Languages.Add(ka);
//    //        }

//    //        var kg = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("kg", StringComparison.InvariantCultureIgnoreCase));
//    //        if (kg == null)
//    //        {
//    //            kg = new Language { TwoLetterIsoCode = "kg", ThreeLetterIsoCode = "kon", ThreeLetterIsoBibliographicCode = "kon", };
//    //            Context.Languages.Add(kg);
//    //        }

//    //        var ki = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ki", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ki == null)
//    //        {
//    //            ki = new Language { TwoLetterIsoCode = "ki", ThreeLetterIsoCode = "kik", ThreeLetterIsoBibliographicCode = "kik", };
//    //            Context.Languages.Add(ki);
//    //        }

//    //        var kj = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("kj", StringComparison.InvariantCultureIgnoreCase));
//    //        if (kj == null)
//    //        {
//    //            kj = new Language { TwoLetterIsoCode = "kj", ThreeLetterIsoCode = "kua", ThreeLetterIsoBibliographicCode = "kua", };
//    //            Context.Languages.Add(kj);
//    //        }

//    //        var kk = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("kk", StringComparison.InvariantCultureIgnoreCase));
//    //        if (kk == null)
//    //        {
//    //            kk = new Language { TwoLetterIsoCode = "kk", ThreeLetterIsoCode = "kaz", ThreeLetterIsoBibliographicCode = "kaz", };
//    //            Context.Languages.Add(kk);
//    //        }

//    //        var kl = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("kl", StringComparison.InvariantCultureIgnoreCase));
//    //        if (kl == null)
//    //        {
//    //            kl = new Language { TwoLetterIsoCode = "kl", ThreeLetterIsoCode = "kal", ThreeLetterIsoBibliographicCode = "kal", };
//    //            Context.Languages.Add(kl);
//    //        }

//    //        var km = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("km", StringComparison.InvariantCultureIgnoreCase));
//    //        if (km == null)
//    //        {
//    //            km = new Language { TwoLetterIsoCode = "km", ThreeLetterIsoCode = "khm", ThreeLetterIsoBibliographicCode = "khm", };
//    //            Context.Languages.Add(km);
//    //        }

//    //        var kn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("kn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (kn == null)
//    //        {
//    //            kn = new Language { TwoLetterIsoCode = "kn", ThreeLetterIsoCode = "kan", ThreeLetterIsoBibliographicCode = "kan", };
//    //            Context.Languages.Add(kn);
//    //        }

//    //        var ko = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ko", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ko == null)
//    //        {
//    //            ko = new Language { TwoLetterIsoCode = "ko", ThreeLetterIsoCode = "kor", ThreeLetterIsoBibliographicCode = "kor", };
//    //            Context.Languages.Add(ko);
//    //        }

//    //        var ks = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ks", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ks == null)
//    //        {
//    //            ks = new Language { TwoLetterIsoCode = "ks", ThreeLetterIsoCode = "kas", ThreeLetterIsoBibliographicCode = "kas", };
//    //            Context.Languages.Add(ks);
//    //        }

//    //        var ku = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ku", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ku == null)
//    //        {
//    //            ku = new Language { TwoLetterIsoCode = "ku", ThreeLetterIsoCode = "kur", ThreeLetterIsoBibliographicCode = "kur", };
//    //            Context.Languages.Add(ku);
//    //        }

//    //        var kv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("kv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (kv == null)
//    //        {
//    //            kv = new Language { TwoLetterIsoCode = "kv", ThreeLetterIsoCode = "kom", ThreeLetterIsoBibliographicCode = "kom", };
//    //            Context.Languages.Add(kv);
//    //        }

//    //        var kw = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("kw", StringComparison.InvariantCultureIgnoreCase));
//    //        if (kw == null)
//    //        {
//    //            kw = new Language { TwoLetterIsoCode = "kw", ThreeLetterIsoCode = "cor", ThreeLetterIsoBibliographicCode = "cor", };
//    //            Context.Languages.Add(kw);
//    //        }

//    //        var ky = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ky", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ky == null)
//    //        {
//    //            ky = new Language { TwoLetterIsoCode = "ky", ThreeLetterIsoCode = "kir", ThreeLetterIsoBibliographicCode = "kir", };
//    //            Context.Languages.Add(ky);
//    //        }

//    //        var la = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("la", StringComparison.InvariantCultureIgnoreCase));
//    //        if (la == null)
//    //        {
//    //            la = new Language { TwoLetterIsoCode = "la", ThreeLetterIsoCode = "lat", ThreeLetterIsoBibliographicCode = "lat", };
//    //            Context.Languages.Add(la);
//    //        }

//    //        var lb = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("lb", StringComparison.InvariantCultureIgnoreCase));
//    //        if (lb == null)
//    //        {
//    //            lb = new Language { TwoLetterIsoCode = "lb", ThreeLetterIsoCode = "ltz", ThreeLetterIsoBibliographicCode = "ltz", };
//    //            Context.Languages.Add(lb);
//    //        }

//    //        var lg = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("lg", StringComparison.InvariantCultureIgnoreCase));
//    //        if (lg == null)
//    //        {
//    //            lg = new Language { TwoLetterIsoCode = "lg", ThreeLetterIsoCode = "lug", ThreeLetterIsoBibliographicCode = "lug", };
//    //            Context.Languages.Add(lg);
//    //        }

//    //        var li = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("li", StringComparison.InvariantCultureIgnoreCase));
//    //        if (li == null)
//    //        {
//    //            li = new Language { TwoLetterIsoCode = "li", ThreeLetterIsoCode = "lim", ThreeLetterIsoBibliographicCode = "lim", };
//    //            Context.Languages.Add(li);
//    //        }

//    //        var ln = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ln", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ln == null)
//    //        {
//    //            ln = new Language { TwoLetterIsoCode = "ln", ThreeLetterIsoCode = "lin", ThreeLetterIsoBibliographicCode = "lin", };
//    //            Context.Languages.Add(ln);
//    //        }

//    //        var lo = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("lo", StringComparison.InvariantCultureIgnoreCase));
//    //        if (lo == null)
//    //        {
//    //            lo = new Language { TwoLetterIsoCode = "lo", ThreeLetterIsoCode = "lao", ThreeLetterIsoBibliographicCode = "lao", };
//    //            Context.Languages.Add(lo);
//    //        }

//    //        var lt = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("lt", StringComparison.InvariantCultureIgnoreCase));
//    //        if (lt == null)
//    //        {
//    //            lt = new Language { TwoLetterIsoCode = "lt", ThreeLetterIsoCode = "lit", ThreeLetterIsoBibliographicCode = "lit", };
//    //            Context.Languages.Add(lt);
//    //        }

//    //        var lv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("lv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (lv == null)
//    //        {
//    //            lv = new Language { TwoLetterIsoCode = "lv", ThreeLetterIsoCode = "lav", ThreeLetterIsoBibliographicCode = "lav", };
//    //            Context.Languages.Add(lv);
//    //        }

//    //        var mg = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("mg", StringComparison.InvariantCultureIgnoreCase));
//    //        if (mg == null)
//    //        {
//    //            mg = new Language { TwoLetterIsoCode = "mg", ThreeLetterIsoCode = "mlg", ThreeLetterIsoBibliographicCode = "mlg", };
//    //            Context.Languages.Add(mg);
//    //        }

//    //        var mh = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("mh", StringComparison.InvariantCultureIgnoreCase));
//    //        if (mh == null)
//    //        {
//    //            mh = new Language { TwoLetterIsoCode = "mh", ThreeLetterIsoCode = "mah", ThreeLetterIsoBibliographicCode = "mah", };
//    //            Context.Languages.Add(mh);
//    //        }

//    //        var mi = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("mi", StringComparison.InvariantCultureIgnoreCase));
//    //        if (mi == null)
//    //        {
//    //            mi = new Language { TwoLetterIsoCode = "mi", ThreeLetterIsoCode = "mri", ThreeLetterIsoBibliographicCode = "mao", };
//    //            Context.Languages.Add(mi);
//    //        }

//    //        var mk = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("mk", StringComparison.InvariantCultureIgnoreCase));
//    //        if (mk == null)
//    //        {
//    //            mk = new Language { TwoLetterIsoCode = "mk", ThreeLetterIsoCode = "mkd", ThreeLetterIsoBibliographicCode = "mac", };
//    //            Context.Languages.Add(mk);
//    //        }

//    //        var ml = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ml", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ml == null)
//    //        {
//    //            ml = new Language { TwoLetterIsoCode = "ml", ThreeLetterIsoCode = "mal", ThreeLetterIsoBibliographicCode = "mal", };
//    //            Context.Languages.Add(ml);
//    //        }

//    //        var mn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("mn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (mn == null)
//    //        {
//    //            mn = new Language { TwoLetterIsoCode = "mn", ThreeLetterIsoCode = "mon", ThreeLetterIsoBibliographicCode = "mon", };
//    //            Context.Languages.Add(mn);
//    //        }

//    //        var mr = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("mr", StringComparison.InvariantCultureIgnoreCase));
//    //        if (mr == null)
//    //        {
//    //            mr = new Language { TwoLetterIsoCode = "mr", ThreeLetterIsoCode = "mar", ThreeLetterIsoBibliographicCode = "mar", };
//    //            Context.Languages.Add(mr);
//    //        }

//    //        var ms = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ms", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ms == null)
//    //        {
//    //            ms = new Language { TwoLetterIsoCode = "ms", ThreeLetterIsoCode = "msa", ThreeLetterIsoBibliographicCode = "may", };
//    //            Context.Languages.Add(ms);
//    //        }

//    //        var mt = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("mt", StringComparison.InvariantCultureIgnoreCase));
//    //        if (mt == null)
//    //        {
//    //            mt = new Language { TwoLetterIsoCode = "mt", ThreeLetterIsoCode = "mlt", ThreeLetterIsoBibliographicCode = "mlt", };
//    //            Context.Languages.Add(mt);
//    //        }

//    //        var my = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("my", StringComparison.InvariantCultureIgnoreCase));
//    //        if (my == null)
//    //        {
//    //            my = new Language { TwoLetterIsoCode = "my", ThreeLetterIsoCode = "mya", ThreeLetterIsoBibliographicCode = "bur", };
//    //            Context.Languages.Add(my);
//    //        }

//    //        var na = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("na", StringComparison.InvariantCultureIgnoreCase));
//    //        if (na == null)
//    //        {
//    //            na = new Language { TwoLetterIsoCode = "na", ThreeLetterIsoCode = "nau", ThreeLetterIsoBibliographicCode = "nau", };
//    //            Context.Languages.Add(na);
//    //        }

//    //        var nb = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("nb", StringComparison.InvariantCultureIgnoreCase));
//    //        if (nb == null)
//    //        {
//    //            nb = new Language { TwoLetterIsoCode = "nb", ThreeLetterIsoCode = "nob", ThreeLetterIsoBibliographicCode = "nob", };
//    //            Context.Languages.Add(nb);
//    //        }

//    //        var ne = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ne", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ne == null)
//    //        {
//    //            ne = new Language { TwoLetterIsoCode = "ne", ThreeLetterIsoCode = "nep", ThreeLetterIsoBibliographicCode = "nep", };
//    //            Context.Languages.Add(ne);
//    //        }

//    //        var ng = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ng", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ng == null)
//    //        {
//    //            ng = new Language { TwoLetterIsoCode = "ng", ThreeLetterIsoCode = "ndo", ThreeLetterIsoBibliographicCode = "ndo", };
//    //            Context.Languages.Add(ng);
//    //        }

//    //        var nl = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("nl", StringComparison.InvariantCultureIgnoreCase));
//    //        if (nl == null)
//    //        {
//    //            nl = new Language { TwoLetterIsoCode = "nl", ThreeLetterIsoCode = "nld", ThreeLetterIsoBibliographicCode = "dut", };
//    //            Context.Languages.Add(nl);
//    //        }

//    //        var nn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("nn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (nn == null)
//    //        {
//    //            nn = new Language { TwoLetterIsoCode = "nn", ThreeLetterIsoCode = "nno", ThreeLetterIsoBibliographicCode = "nno", };
//    //            Context.Languages.Add(nn);
//    //        }

//    //        var no = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("no", StringComparison.InvariantCultureIgnoreCase));
//    //        if (no == null)
//    //        {
//    //            no = new Language { TwoLetterIsoCode = "no", ThreeLetterIsoCode = "nor", ThreeLetterIsoBibliographicCode = "nor", };
//    //            Context.Languages.Add(no);
//    //        }

//    //        var nv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("nv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (nv == null)
//    //        {
//    //            nv = new Language { TwoLetterIsoCode = "nv", ThreeLetterIsoCode = "nav", ThreeLetterIsoBibliographicCode = "nav", };
//    //            Context.Languages.Add(nv);
//    //        }

//    //        var ny = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ny", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ny == null)
//    //        {
//    //            ny = new Language { TwoLetterIsoCode = "ny", ThreeLetterIsoCode = "nya", ThreeLetterIsoBibliographicCode = "nya", };
//    //            Context.Languages.Add(ny);
//    //        }

//    //        var oc = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("oc", StringComparison.InvariantCultureIgnoreCase));
//    //        if (oc == null)
//    //        {
//    //            oc = new Language { TwoLetterIsoCode = "oc", ThreeLetterIsoCode = "oci", ThreeLetterIsoBibliographicCode = "oci", };
//    //            Context.Languages.Add(oc);
//    //        }

//    //        var om = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("om", StringComparison.InvariantCultureIgnoreCase));
//    //        if (om == null)
//    //        {
//    //            om = new Language { TwoLetterIsoCode = "om", ThreeLetterIsoCode = "orm", ThreeLetterIsoBibliographicCode = "orm", };
//    //            Context.Languages.Add(om);
//    //        }

//    //        var or = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("or", StringComparison.InvariantCultureIgnoreCase));
//    //        if (or == null)
//    //        {
//    //            or = new Language { TwoLetterIsoCode = "or", ThreeLetterIsoCode = "ori", ThreeLetterIsoBibliographicCode = "ori", };
//    //            Context.Languages.Add(or);
//    //        }

//    //        var os = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("os", StringComparison.InvariantCultureIgnoreCase));
//    //        if (os == null)
//    //        {
//    //            os = new Language { TwoLetterIsoCode = "os", ThreeLetterIsoCode = "oss", ThreeLetterIsoBibliographicCode = "oss", };
//    //            Context.Languages.Add(os);
//    //        }

//    //        var pa = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("pa", StringComparison.InvariantCultureIgnoreCase));
//    //        if (pa == null)
//    //        {
//    //            pa = new Language { TwoLetterIsoCode = "pa", ThreeLetterIsoCode = "pan", ThreeLetterIsoBibliographicCode = "pan", };
//    //            Context.Languages.Add(pa);
//    //        }

//    //        var pi = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("pi", StringComparison.InvariantCultureIgnoreCase));
//    //        if (pi == null)
//    //        {
//    //            pi = new Language { TwoLetterIsoCode = "pi", ThreeLetterIsoCode = "pli", ThreeLetterIsoBibliographicCode = "pli", };
//    //            Context.Languages.Add(pi);
//    //        }

//    //        var pl = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("pl", StringComparison.InvariantCultureIgnoreCase));
//    //        if (pl == null)
//    //        {
//    //            pl = new Language { TwoLetterIsoCode = "pl", ThreeLetterIsoCode = "pol", ThreeLetterIsoBibliographicCode = "pol", };
//    //            Context.Languages.Add(pl);
//    //        }

//    //        var ps = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ps", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ps == null)
//    //        {
//    //            ps = new Language { TwoLetterIsoCode = "ps", ThreeLetterIsoCode = "pus", ThreeLetterIsoBibliographicCode = "pus", };
//    //            Context.Languages.Add(ps);
//    //        }

//    //        var pt = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("pt", StringComparison.InvariantCultureIgnoreCase));
//    //        if (pt == null)
//    //        {
//    //            pt = new Language { TwoLetterIsoCode = "pt", ThreeLetterIsoCode = "por", ThreeLetterIsoBibliographicCode = "por", };
//    //            Context.Languages.Add(pt);
//    //        }

//    //        var qu = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("qu", StringComparison.InvariantCultureIgnoreCase));
//    //        if (qu == null)
//    //        {
//    //            qu = new Language { TwoLetterIsoCode = "qu", ThreeLetterIsoCode = "que", ThreeLetterIsoBibliographicCode = "que", };
//    //            Context.Languages.Add(qu);
//    //        }

//    //        var rm = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("rm", StringComparison.InvariantCultureIgnoreCase));
//    //        if (rm == null)
//    //        {
//    //            rm = new Language { TwoLetterIsoCode = "rm", ThreeLetterIsoCode = "roh", ThreeLetterIsoBibliographicCode = "roh", };
//    //            Context.Languages.Add(rm);
//    //        }

//    //        var rn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("rn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (rn == null)
//    //        {
//    //            rn = new Language { TwoLetterIsoCode = "rn", ThreeLetterIsoCode = "run", ThreeLetterIsoBibliographicCode = "run", };
//    //            Context.Languages.Add(rn);
//    //        }

//    //        var ro = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ro", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ro == null)
//    //        {
//    //            ro = new Language { TwoLetterIsoCode = "ro", ThreeLetterIsoCode = "ron", ThreeLetterIsoBibliographicCode = "rum", };
//    //            Context.Languages.Add(ro);
//    //        }

//    //        var ru = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ru", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ru == null)
//    //        {
//    //            ru = new Language { TwoLetterIsoCode = "ru", ThreeLetterIsoCode = "rus", ThreeLetterIsoBibliographicCode = "rus", };
//    //            Context.Languages.Add(ru);
//    //        }

//    //        var rw = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("rw", StringComparison.InvariantCultureIgnoreCase));
//    //        if (rw == null)
//    //        {
//    //            rw = new Language { TwoLetterIsoCode = "rw", ThreeLetterIsoCode = "kin", ThreeLetterIsoBibliographicCode = "kin", };
//    //            Context.Languages.Add(rw);
//    //        }

//    //        var sa = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sa", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sa == null)
//    //        {
//    //            sa = new Language { TwoLetterIsoCode = "sa", ThreeLetterIsoCode = "san", ThreeLetterIsoBibliographicCode = "san", };
//    //            Context.Languages.Add(sa);
//    //        }

//    //        var sc = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sc", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sc == null)
//    //        {
//    //            sc = new Language { TwoLetterIsoCode = "sc", ThreeLetterIsoCode = "srd", ThreeLetterIsoBibliographicCode = "srd", };
//    //            Context.Languages.Add(sc);
//    //        }

//    //        var sd = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sd", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sd == null)
//    //        {
//    //            sd = new Language { TwoLetterIsoCode = "sd", ThreeLetterIsoCode = "snd", ThreeLetterIsoBibliographicCode = "snd", };
//    //            Context.Languages.Add(sd);
//    //        }

//    //        var se = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("se", StringComparison.InvariantCultureIgnoreCase));
//    //        if (se == null)
//    //        {
//    //            se = new Language { TwoLetterIsoCode = "se", ThreeLetterIsoCode = "sme", ThreeLetterIsoBibliographicCode = "sme", };
//    //            Context.Languages.Add(se);
//    //        }

//    //        var sg = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sg", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sg == null)
//    //        {
//    //            sg = new Language { TwoLetterIsoCode = "sg", ThreeLetterIsoCode = "sag", ThreeLetterIsoBibliographicCode = "sag", };
//    //            Context.Languages.Add(sg);
//    //        }

//    //        var sh = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sh", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sh == null)
//    //        {
//    //            sh = new Language { TwoLetterIsoCode = "sh", ThreeLetterIsoCode = "hbs", ThreeLetterIsoBibliographicCode = "hbs", };
//    //            Context.Languages.Add(sh);
//    //        }

//    //        var si = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("si", StringComparison.InvariantCultureIgnoreCase));
//    //        if (si == null)
//    //        {
//    //            si = new Language { TwoLetterIsoCode = "si", ThreeLetterIsoCode = "sin", ThreeLetterIsoBibliographicCode = "sin", };
//    //            Context.Languages.Add(si);
//    //        }

//    //        var sk = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sk", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sk == null)
//    //        {
//    //            sk = new Language { TwoLetterIsoCode = "sk", ThreeLetterIsoCode = "slk", ThreeLetterIsoBibliographicCode = "slo", };
//    //            Context.Languages.Add(sk);
//    //        }

//    //        var sl = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sl", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sl == null)
//    //        {
//    //            sl = new Language { TwoLetterIsoCode = "sl", ThreeLetterIsoCode = "slv", ThreeLetterIsoBibliographicCode = "slv", };
//    //            Context.Languages.Add(sl);
//    //        }

//    //        var sm = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sm", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sm == null)
//    //        {
//    //            sm = new Language { TwoLetterIsoCode = "sm", ThreeLetterIsoCode = "smo", ThreeLetterIsoBibliographicCode = "smo", };
//    //            Context.Languages.Add(sm);
//    //        }

//    //        var sn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sn == null)
//    //        {
//    //            sn = new Language { TwoLetterIsoCode = "sn", ThreeLetterIsoCode = "sna", ThreeLetterIsoBibliographicCode = "sna", };
//    //            Context.Languages.Add(sn);
//    //        }

//    //        var so = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("so", StringComparison.InvariantCultureIgnoreCase));
//    //        if (so == null)
//    //        {
//    //            so = new Language { TwoLetterIsoCode = "so", ThreeLetterIsoCode = "som", ThreeLetterIsoBibliographicCode = "som", };
//    //            Context.Languages.Add(so);
//    //        }

//    //        var sq = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sq", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sq == null)
//    //        {
//    //            sq = new Language { TwoLetterIsoCode = "sq", ThreeLetterIsoCode = "sqi", ThreeLetterIsoBibliographicCode = "alb", };
//    //            Context.Languages.Add(sq);
//    //        }

//    //        var sr = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sr", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sr == null)
//    //        {
//    //            sr = new Language { TwoLetterIsoCode = "sr", ThreeLetterIsoCode = "srp", ThreeLetterIsoBibliographicCode = "srp", };
//    //            Context.Languages.Add(sr);
//    //        }

//    //        var ss = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ss", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ss == null)
//    //        {
//    //            ss = new Language { TwoLetterIsoCode = "ss", ThreeLetterIsoCode = "ssw", ThreeLetterIsoBibliographicCode = "ssw", };
//    //            Context.Languages.Add(ss);
//    //        }

//    //        var st = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("st", StringComparison.InvariantCultureIgnoreCase));
//    //        if (st == null)
//    //        {
//    //            st = new Language { TwoLetterIsoCode = "st", ThreeLetterIsoCode = "sot", ThreeLetterIsoBibliographicCode = "sot", };
//    //            Context.Languages.Add(st);
//    //        }

//    //        var su = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("su", StringComparison.InvariantCultureIgnoreCase));
//    //        if (su == null)
//    //        {
//    //            su = new Language { TwoLetterIsoCode = "su", ThreeLetterIsoCode = "sun", ThreeLetterIsoBibliographicCode = "sun", };
//    //            Context.Languages.Add(su);
//    //        }

//    //        var sv = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sv", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sv == null)
//    //        {
//    //            sv = new Language { TwoLetterIsoCode = "sv", ThreeLetterIsoCode = "swe", ThreeLetterIsoBibliographicCode = "swe", };
//    //            Context.Languages.Add(sv);
//    //        }

//    //        var sw = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("sw", StringComparison.InvariantCultureIgnoreCase));
//    //        if (sw == null)
//    //        {
//    //            sw = new Language { TwoLetterIsoCode = "sw", ThreeLetterIsoCode = "swa", ThreeLetterIsoBibliographicCode = "swa", };
//    //            Context.Languages.Add(sw);
//    //        }

//    //        var ta = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ta", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ta == null)
//    //        {
//    //            ta = new Language { TwoLetterIsoCode = "ta", ThreeLetterIsoCode = "tam", ThreeLetterIsoBibliographicCode = "tam", };
//    //            Context.Languages.Add(ta);
//    //        }

//    //        var te = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("te", StringComparison.InvariantCultureIgnoreCase));
//    //        if (te == null)
//    //        {
//    //            te = new Language { TwoLetterIsoCode = "te", ThreeLetterIsoCode = "tel", ThreeLetterIsoBibliographicCode = "tel", };
//    //            Context.Languages.Add(te);
//    //        }

//    //        var tg = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("tg", StringComparison.InvariantCultureIgnoreCase));
//    //        if (tg == null)
//    //        {
//    //            tg = new Language { TwoLetterIsoCode = "tg", ThreeLetterIsoCode = "tgk", ThreeLetterIsoBibliographicCode = "tgk", };
//    //            Context.Languages.Add(tg);
//    //        }

//    //        var th = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("th", StringComparison.InvariantCultureIgnoreCase));
//    //        if (th == null)
//    //        {
//    //            th = new Language { TwoLetterIsoCode = "th", ThreeLetterIsoCode = "tha", ThreeLetterIsoBibliographicCode = "tha", };
//    //            Context.Languages.Add(th);
//    //        }

//    //        var ti = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ti", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ti == null)
//    //        {
//    //            ti = new Language { TwoLetterIsoCode = "ti", ThreeLetterIsoCode = "tir", ThreeLetterIsoBibliographicCode = "tir", };
//    //            Context.Languages.Add(ti);
//    //        }

//    //        var tk = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("tk", StringComparison.InvariantCultureIgnoreCase));
//    //        if (tk == null)
//    //        {
//    //            tk = new Language { TwoLetterIsoCode = "tk", ThreeLetterIsoCode = "tuk", ThreeLetterIsoBibliographicCode = "tuk", };
//    //            Context.Languages.Add(tk);
//    //        }

//    //        var tl = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("tl", StringComparison.InvariantCultureIgnoreCase));
//    //        if (tl == null)
//    //        {
//    //            tl = new Language { TwoLetterIsoCode = "tl", ThreeLetterIsoCode = "tgl", ThreeLetterIsoBibliographicCode = "tgl", };
//    //            Context.Languages.Add(tl);
//    //        }

//    //        var tn = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("tn", StringComparison.InvariantCultureIgnoreCase));
//    //        if (tn == null)
//    //        {
//    //            tn = new Language { TwoLetterIsoCode = "tn", ThreeLetterIsoCode = "tsn", ThreeLetterIsoBibliographicCode = "tsn", };
//    //            Context.Languages.Add(tn);
//    //        }

//    //        var to = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("to", StringComparison.InvariantCultureIgnoreCase));
//    //        if (to == null)
//    //        {
//    //            to = new Language { TwoLetterIsoCode = "to", ThreeLetterIsoCode = "ton", ThreeLetterIsoBibliographicCode = "ton", };
//    //            Context.Languages.Add(to);
//    //        }

//    //        var tr = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("tr", StringComparison.InvariantCultureIgnoreCase));
//    //        if (tr == null)
//    //        {
//    //            tr = new Language { TwoLetterIsoCode = "tr", ThreeLetterIsoCode = "tur", ThreeLetterIsoBibliographicCode = "tur", };
//    //            Context.Languages.Add(tr);
//    //        }

//    //        var ts = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ts", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ts == null)
//    //        {
//    //            ts = new Language { TwoLetterIsoCode = "ts", ThreeLetterIsoCode = "tso", ThreeLetterIsoBibliographicCode = "tso", };
//    //            Context.Languages.Add(ts);
//    //        }

//    //        var tt = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("tt", StringComparison.InvariantCultureIgnoreCase));
//    //        if (tt == null)
//    //        {
//    //            tt = new Language { TwoLetterIsoCode = "tt", ThreeLetterIsoCode = "tat", ThreeLetterIsoBibliographicCode = "tat", };
//    //            Context.Languages.Add(tt);
//    //        }

//    //        var tw = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("tw", StringComparison.InvariantCultureIgnoreCase));
//    //        if (tw == null)
//    //        {
//    //            tw = new Language { TwoLetterIsoCode = "tw", ThreeLetterIsoCode = "twi", ThreeLetterIsoBibliographicCode = "twi", };
//    //            Context.Languages.Add(tw);
//    //        }

//    //        var ty = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ty", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ty == null)
//    //        {
//    //            ty = new Language { TwoLetterIsoCode = "ty", ThreeLetterIsoCode = "tah", ThreeLetterIsoBibliographicCode = "tah", };
//    //            Context.Languages.Add(ty);
//    //        }

//    //        var ug = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ug", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ug == null)
//    //        {
//    //            ug = new Language { TwoLetterIsoCode = "ug", ThreeLetterIsoCode = "uig", ThreeLetterIsoBibliographicCode = "uig", };
//    //            Context.Languages.Add(ug);
//    //        }

//    //        var uk = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("uk", StringComparison.InvariantCultureIgnoreCase));
//    //        if (uk == null)
//    //        {
//    //            uk = new Language { TwoLetterIsoCode = "uk", ThreeLetterIsoCode = "ukr", ThreeLetterIsoBibliographicCode = "ukr", };
//    //            Context.Languages.Add(uk);
//    //        }

//    //        var ur = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ur", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ur == null)
//    //        {
//    //            ur = new Language { TwoLetterIsoCode = "ur", ThreeLetterIsoCode = "urd", ThreeLetterIsoBibliographicCode = "urd", };
//    //            Context.Languages.Add(ur);
//    //        }

//    //        var uz = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("uz", StringComparison.InvariantCultureIgnoreCase));
//    //        if (uz == null)
//    //        {
//    //            uz = new Language { TwoLetterIsoCode = "uz", ThreeLetterIsoCode = "uzb", ThreeLetterIsoBibliographicCode = "uzb", };
//    //            Context.Languages.Add(uz);
//    //        }

//    //        var ve = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("ve", StringComparison.InvariantCultureIgnoreCase));
//    //        if (ve == null)
//    //        {
//    //            ve = new Language { TwoLetterIsoCode = "ve", ThreeLetterIsoCode = "ven", ThreeLetterIsoBibliographicCode = "ven", };
//    //            Context.Languages.Add(ve);
//    //        }

//    //        var vi = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("vi", StringComparison.InvariantCultureIgnoreCase));
//    //        if (vi == null)
//    //        {
//    //            vi = new Language { TwoLetterIsoCode = "vi", ThreeLetterIsoCode = "vie", ThreeLetterIsoBibliographicCode = "vie", };
//    //            Context.Languages.Add(vi);
//    //        }

//    //        var vo = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("vo", StringComparison.InvariantCultureIgnoreCase));
//    //        if (vo == null)
//    //        {
//    //            vo = new Language { TwoLetterIsoCode = "vo", ThreeLetterIsoCode = "vol", ThreeLetterIsoBibliographicCode = "vol", };
//    //            Context.Languages.Add(vo);
//    //        }

//    //        var wa = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("wa", StringComparison.InvariantCultureIgnoreCase));
//    //        if (wa == null)
//    //        {
//    //            wa = new Language { TwoLetterIsoCode = "wa", ThreeLetterIsoCode = "wln", ThreeLetterIsoBibliographicCode = "wln", };
//    //            Context.Languages.Add(wa);
//    //        }

//    //        var wo = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("wo", StringComparison.InvariantCultureIgnoreCase));
//    //        if (wo == null)
//    //        {
//    //            wo = new Language { TwoLetterIsoCode = "wo", ThreeLetterIsoCode = "wol", ThreeLetterIsoBibliographicCode = "wol", };
//    //            Context.Languages.Add(wo);
//    //        }

//    //        var xh = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("xh", StringComparison.InvariantCultureIgnoreCase));
//    //        if (xh == null)
//    //        {
//    //            xh = new Language { TwoLetterIsoCode = "xh", ThreeLetterIsoCode = "xho", ThreeLetterIsoBibliographicCode = "xho", };
//    //            Context.Languages.Add(xh);
//    //        }

//    //        var yi = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("yi", StringComparison.InvariantCultureIgnoreCase));
//    //        if (yi == null)
//    //        {
//    //            yi = new Language { TwoLetterIsoCode = "yi", ThreeLetterIsoCode = "yid", ThreeLetterIsoBibliographicCode = "yid", };
//    //            Context.Languages.Add(yi);
//    //        }

//    //        var yo = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("yo", StringComparison.InvariantCultureIgnoreCase));
//    //        if (yo == null)
//    //        {
//    //            yo = new Language { TwoLetterIsoCode = "yo", ThreeLetterIsoCode = "yor", ThreeLetterIsoBibliographicCode = "yor", };
//    //            Context.Languages.Add(yo);
//    //        }

//    //        var za = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("za", StringComparison.InvariantCultureIgnoreCase));
//    //        if (za == null)
//    //        {
//    //            za = new Language { TwoLetterIsoCode = "za", ThreeLetterIsoCode = "zha", ThreeLetterIsoBibliographicCode = "zha", };
//    //            Context.Languages.Add(za);
//    //        }

//    //        var zh = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("zh", StringComparison.InvariantCultureIgnoreCase));
//    //        if (zh == null)
//    //        {
//    //            zh = new Language { TwoLetterIsoCode = "zh", ThreeLetterIsoCode = "zho", ThreeLetterIsoBibliographicCode = "chi", };
//    //            Context.Languages.Add(zh);
//    //        }

//    //        var zu = Context.Languages.SingleOrDefault(l => l.TwoLetterIsoCode.Equals("zu", StringComparison.InvariantCultureIgnoreCase));
//    //        if (zu == null)
//    //        {
//    //            zu = new Language { TwoLetterIsoCode = "zu", ThreeLetterIsoCode = "zul", ThreeLetterIsoBibliographicCode = "zul", };
//    //            Context.Languages.Add(zu);
//    //        }

//    //        Context.SaveChanges();
//    //        Context.Languages.ToList().ForEach(l => l.Names = new List<LanguageName>());

//    //        #endregion
//    //        #region Language Names

//    //        //Languages with 61 translations

//    //        if (en.Names.Count < 61)
//    //            en.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "English", TranslationToLanguage = en, },          // #1  Language = "English"
//    //                new LanguageName{ Text = "Inglés", TranslationToLanguage = es, },           // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Englisch", TranslationToLanguage = de, },         // #3  Language = "German"
//    //                new LanguageName{ Text = "الإنجليزية", TranslationToLanguage = ar, },       // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Engels", TranslationToLanguage = af,},            // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "İngilis dili", TranslationToLanguage = az,},      // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "англійская", TranslationToLanguage = be,},        // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "английски език", TranslationToLanguage = bg,},    // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ইংরেজি", TranslationToLanguage = bn,},             // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "anglès", TranslationToLanguage = ca,},            // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "angličtina", TranslationToLanguage = cs,},        // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Saesneg", TranslationToLanguage = cy,},           // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "engelsk", TranslationToLanguage = da,},           // #13 Language = "Danish"
//    //                new LanguageName{ Text = "inglise", TranslationToLanguage = et,},           // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Ingelesa", TranslationToLanguage = eu,},          // #15 Language = "Basque"
//    //                new LanguageName{ Text = "انگلیسی", TranslationToLanguage = fa,},          // #16 Language = "Persian"
//    //                new LanguageName{ Text = "englanti", TranslationToLanguage = fi,},          // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "anglaise", TranslationToLanguage = fr,},          // #18 Language = "French"
//    //                new LanguageName{ Text = "Béarla", TranslationToLanguage = ga,},            // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Inglés", TranslationToLanguage = gl,},            // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ઇંગલિશ", TranslationToLanguage = gu,},            // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "אנגלית", TranslationToLanguage = he,},           // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "अंग्रेज़ी", TranslationToLanguage = hi,},             // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "engleski", TranslationToLanguage = hr,},          // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "angle", TranslationToLanguage = ht,},             // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "angol", TranslationToLanguage = hu,},             // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "անգլերեն", TranslationToLanguage = hy,},          // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Bahasa Inggris", TranslationToLanguage = id,},    // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Enska", TranslationToLanguage = isLanguage,},     // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "inglese", TranslationToLanguage = it,},           // #30 Language = "Italian"
//    //                new LanguageName{ Text = "英語を", TranslationToLanguage = ja,},            // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ინგლისური", TranslationToLanguage = ka,},       // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಇಂಗ್ಲೀಷ್", TranslationToLanguage = kn,},            // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "영어", TranslationToLanguage = ko,},              // #34 Language = "Korean"
//    //                new LanguageName{ Text = "English", TranslationToLanguage = la,},           // #35 Language = "Latin"
//    //                new LanguageName{ Text = "anglų", TranslationToLanguage = lt,},             // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "angļu", TranslationToLanguage = lv,},             // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "англиски јазик", TranslationToLanguage = mk,},    // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Inggeris", TranslationToLanguage = ms,},   // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ingliż", TranslationToLanguage = mt,},            // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Engels", TranslationToLanguage = nl,},            // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "engelsk", TranslationToLanguage = no,},           // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "angielski", TranslationToLanguage = pl,},         // #43 Language = "Polish"
//    //                new LanguageName{ Text = "Inglês", TranslationToLanguage = pt,},            // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "englez", TranslationToLanguage = ro,},            // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "английский", TranslationToLanguage = ru,},        // #46 Language = "Russian"
//    //                new LanguageName{ Text = "angličtina", TranslationToLanguage = sk,},        // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "angleški", TranslationToLanguage = sl,},          // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "anglisht", TranslationToLanguage = sq,},          // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "енглески", TranslationToLanguage = sr,},          // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "engelska", TranslationToLanguage = sv,},          // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiingereza", TranslationToLanguage = sw,},        // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஆங்கிலம்", TranslationToLanguage = ta,},        // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ఆంగ్ల", TranslationToLanguage = te,},               // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาอังกฤษ", TranslationToLanguage = th,},          // #55 Language = "Thai"
//    //                new LanguageName{ Text = "İngilizce", TranslationToLanguage = tr,},         // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Англійська", TranslationToLanguage = uk,},        // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "انگریزی", TranslationToLanguage = ur,},          // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "tiếng Anh", TranslationToLanguage = vi,},         // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "ענגליש", TranslationToLanguage = yi,},           // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "英语", TranslationToLanguage = zh,},              // #61 Language = "Chinese"
//    //            };

//    //        if (es.Names.Count < 61)
//    //            es.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Spanish", TranslationToLanguage = en, },           // #1  Language = "English"
//    //                new LanguageName{ Text = "Español", TranslationToLanguage = es, },           // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Spanisch", TranslationToLanguage = de, },          // #3  Language = "German"
//    //                new LanguageName{ Text = "الاسبانية", TranslationToLanguage = ar, },         // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Spanish", TranslationToLanguage = af,},            // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "ispan", TranslationToLanguage = az,},              // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Іспанская", TranslationToLanguage = be,},          // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "испански", TranslationToLanguage = bg,},           // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "স্পেনসম্পর্কিত", TranslationToLanguage = bn,},         // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "espanyol", TranslationToLanguage = ca,},           // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "španělština", TranslationToLanguage = cs,},        // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Sbaeneg", TranslationToLanguage = cy,},            // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "spansk", TranslationToLanguage = da,},             // #13 Language = "Danish"
//    //                new LanguageName{ Text = "hispaania", TranslationToLanguage = et,},          // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Espainiako", TranslationToLanguage = eu,},         // #15 Language = "Basque"
//    //                new LanguageName{ Text = "اسپانیایی", TranslationToLanguage = fa,},         // #16 Language = "Persian"
//    //                new LanguageName{ Text = "espanjalainen", TranslationToLanguage = fi,},      // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "espagnole", TranslationToLanguage = fr,},          // #18 Language = "French"
//    //                new LanguageName{ Text = "Spáinnis", TranslationToLanguage = ga,},           // #19 Language = "Irish"
//    //                new LanguageName{ Text = "castelán", TranslationToLanguage = gl,},           // #20 Language = "Galician"
//    //                new LanguageName{ Text = "સ્પેનિશ", TranslationToLanguage = gu,},              // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "ספרדית", TranslationToLanguage = he,},            // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "स्पेनिश", TranslationToLanguage = hi,},              // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Španjolski", TranslationToLanguage = hr,},         // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Panyòl", TranslationToLanguage = ht,},             // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "spanyol", TranslationToLanguage = hu,},            // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "իսպաներեն", TranslationToLanguage = hy,},         // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Spanyol", TranslationToLanguage = id,},            // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Spænska", TranslationToLanguage = isLanguage,},    // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "spagnolo", TranslationToLanguage = it,},           // #30 Language = "Italian"
//    //                new LanguageName{ Text = "スペイン", TranslationToLanguage = ja,},            // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ესპანური", TranslationToLanguage = ka,},           // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಸ್ಪ್ಯಾನಿಷ್", TranslationToLanguage = kn,},             // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "스페인의", TranslationToLanguage = ko,},            // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Spanish", TranslationToLanguage = la,},            // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Ispanijos", TranslationToLanguage = lt,},          // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "spāņu", TranslationToLanguage = lv,},              // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "шпански", TranslationToLanguage = mk,},            // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Sepanyol", TranslationToLanguage = ms,},    // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Spanjol", TranslationToLanguage = mt,},            // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Spaans", TranslationToLanguage = nl,},             // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Spanish", TranslationToLanguage = no,},            // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "hiszpański", TranslationToLanguage = pl,},         // #43 Language = "Polish"
//    //                new LanguageName{ Text = "hiszpański", TranslationToLanguage = pt,},         // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "spaniol", TranslationToLanguage = ro,},            // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "испанский", TranslationToLanguage = ru,},          // #46 Language = "Russian"
//    //                new LanguageName{ Text = "španielčina", TranslationToLanguage = sk,},        // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "španski", TranslationToLanguage = sl,},            // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "spanjisht", TranslationToLanguage = sq,},          // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "шпански", TranslationToLanguage = sr,},            // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "spanska", TranslationToLanguage = sv,},            // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kihispania", TranslationToLanguage = sw,},         // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஸ்பானிஷ்", TranslationToLanguage = ta,},        // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "స్పానిష్", TranslationToLanguage = te,},              // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาสเปน", TranslationToLanguage = th,},            // #55 Language = "Thai"
//    //                new LanguageName{ Text = "İspanyolca", TranslationToLanguage = tr,},         // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Іспанська", TranslationToLanguage = uk,},          // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "ہسپانوی", TranslationToLanguage = ur,},           // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Tây Ban Nha", TranslationToLanguage = vi,},        // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "Spanish", TranslationToLanguage = yi,},            // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "西班牙", TranslationToLanguage = zh,},             // #61 Language = "Chinese"
//    //             };

//    //        if (de.Names.Count < 61)
//    //            de.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "German", TranslationToLanguage = en, },                          // #1  Language = "English"
//    //                new LanguageName{ Text = "Alemán", TranslationToLanguage = es, },                          // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Deutsch", TranslationToLanguage = de, },                         // #3  Language = "German"
//    //                new LanguageName{ Text = "الألمانية", TranslationToLanguage = ar, },                       // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Duits", TranslationToLanguage = af,},                            // #5  Language = "Afrikaans"
//    //                new LanguageName{Text = "alman", TranslationToLanguage = az,},                             // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Нямецкі", TranslationToLanguage = be,},                          // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "немски", TranslationToLanguage = bg,},                           // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "জার্মান", TranslationToLanguage = bn,},                             // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "alemany", TranslationToLanguage = ca,},                          // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "Němec", TranslationToLanguage = cs,},                            // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Almaeneg", TranslationToLanguage = cy,},                         // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "tysk", TranslationToLanguage = da,},                             // #13 Language = "Danish"
//    //                new LanguageName{ Text = "saksa", TranslationToLanguage = et,},                            // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Alemana", TranslationToLanguage = eu,},                          // #15 Language = "Basque"
//    //                new LanguageName{ Text = "آلمانی", TranslationToLanguage = fa,},                          // #16 Language = "Persian"
//    //                new LanguageName{ Text = "saksa", TranslationToLanguage = fi,},                            // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "allemande", TranslationToLanguage = fr,},                        // #18 Language = "French"
//    //                new LanguageName{ Text = "Gearmáinis", TranslationToLanguage = ga,},                       // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Alemán", TranslationToLanguage = gl,},                           // #20 Language = "Galician"
//    //                new LanguageName{ Text = "જર્મન ભાષા", TranslationToLanguage = gu,},                        // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "גרמנית", TranslationToLanguage = he,},                          // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "जर्मन", TranslationToLanguage = hi,},                             // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "njemački", TranslationToLanguage = hr,},                         // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Alman", TranslationToLanguage = ht,},                            // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "német", TranslationToLanguage = hu,},                            // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "գերմաներեն", TranslationToLanguage = hy,},                      // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Jerman", TranslationToLanguage = id,},                           // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Þýska", TranslationToLanguage = isLanguage,},                    // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "tedesco", TranslationToLanguage = it,},                          // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ドイツ", TranslationToLanguage = ja,},                           // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "გერმანული", TranslationToLanguage = ka,},                       // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಸ್ವಂತ", TranslationToLanguage = kn,},                             // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "독일의", TranslationToLanguage = ko,},                           // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Germanica", TranslationToLanguage = la,},                        // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Vokietijos", TranslationToLanguage = lt,},                       // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "vācu", TranslationToLanguage = lv,},                             // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "германски", TranslationToLanguage = mk,},                        // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Jerman", TranslationToLanguage = ms,},                           // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ġermaniż", TranslationToLanguage = mt,},                         // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Duits", TranslationToLanguage = nl,},                            // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "tyske", TranslationToLanguage = no,},                            // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "niemiecki", TranslationToLanguage = pl,},                        // #43 Language = "Polish"
//    //                new LanguageName{ Text = "alemão", TranslationToLanguage = pt,},                           // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "german", TranslationToLanguage = ro,},                           // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "немецкий", TranslationToLanguage = ru,},                         // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Nemec", TranslationToLanguage = sk,},                            // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "nemška", TranslationToLanguage = sl,},                           // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "gjermanisht", TranslationToLanguage = sq,},                      // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "немачки", TranslationToLanguage = sr,},                          // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "tyska", TranslationToLanguage = sv,},                            // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Ujerumani", TranslationToLanguage = sw,},                        // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஜெர்மனி நாட்டை சார்ந்தவர்", TranslationToLanguage = ta,},    // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "సన్నిహిత", TranslationToLanguage = te,},                          // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาเยอรมัน", TranslationToLanguage = th,},                         // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Alman", TranslationToLanguage = tr,},                            // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "німецький", TranslationToLanguage = uk,},                        // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "جرمن", TranslationToLanguage = ur,},                            // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Đức", TranslationToLanguage = vi,},                              // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "דייַטש", TranslationToLanguage = yi,},                           // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "德国", TranslationToLanguage = zh,},                             // #61 Language = "Chinese"
//    //            };

//    //        if (ar.Names.Count < 61)
//    //            ar.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Arabic", TranslationToLanguage = en, },               // #1  Language = "English"
//    //                new LanguageName{ Text = "Árabe", TranslationToLanguage = es, },                // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Arabisch", TranslationToLanguage = de, },             // #3  Language = "German"
//    //                new LanguageName{ Text = "العربية", TranslationToLanguage = ar, },             // #4  Language = "Arabic"
//    //                new LanguageName{ Text ="Arabies", TranslationToLanguage = af,},                // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Ərəb", TranslationToLanguage = az,},                  // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Арабская", TranslationToLanguage = be,},              // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "арабски", TranslationToLanguage = bg,},               // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "আরবি", TranslationToLanguage = bn,},                  // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "àrab", TranslationToLanguage = ca,},                  // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "arabština", TranslationToLanguage = cs,},             // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Arabeg", TranslationToLanguage = cy,},                // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "arabic", TranslationToLanguage = da,},                // #13 Language = "Danish"
//    //                new LanguageName{ Text = "araabia", TranslationToLanguage = et,},               // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Arabic", TranslationToLanguage = eu,},                // #15 Language = "Basque"
//    //                new LanguageName{ Text = "عربی", TranslationToLanguage = fa,},                 // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Arabia", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "arabe", TranslationToLanguage = fr,},                 // #18 Language = "French"
//    //                new LanguageName{ Text = "Araibis", TranslationToLanguage = ga,},               // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Árabe", TranslationToLanguage = gl,},                 // #20 Language = "Galician"
//    //                new LanguageName{ Text = "અરબી ભાષા", TranslationToLanguage = gu,},            // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "ערבית", TranslationToLanguage = he,},                // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "अरबी भाषा", TranslationToLanguage = hi,},              // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "arapski", TranslationToLanguage = hr,},               // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "arab", TranslationToLanguage = ht,},                  // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "arab", TranslationToLanguage = hu,},                  // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "արաբական", TranslationToLanguage = hy,},            // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Arab", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "arabíska", TranslationToLanguage = isLanguage,},      // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "arabo", TranslationToLanguage = it,},                 // #30 Language = "Italian"
//    //                new LanguageName{ Text = "アラビア", TranslationToLanguage = ja,},               // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "არაბული", TranslationToLanguage = ka,},              // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಅರಬ್ಬಿ ಭಾಷೆಯ", TranslationToLanguage = kn,},           // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "아랍어", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Arabic", TranslationToLanguage = la,},                // #35 Language = "Latin"
//    //                new LanguageName{ Text = "arabų", TranslationToLanguage = lt,},                 // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "arābu", TranslationToLanguage = lv,},                 // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "арапски", TranslationToLanguage = mk,},               // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Arab", TranslationToLanguage = ms,},           // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Għarbi", TranslationToLanguage = mt,},                // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Arabisch", TranslationToLanguage = nl,},              // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "arabisk", TranslationToLanguage = no,},               // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "arabski", TranslationToLanguage = pl,},               // #43 Language = "Polish"
//    //                new LanguageName{ Text = "árabe", TranslationToLanguage = pt,},                 // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "limba arabă", TranslationToLanguage = ro,},           // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "арабский", TranslationToLanguage = ru,},              // #46 Language = "Russian"
//    //                new LanguageName{ Text = "arabčina", TranslationToLanguage = sk,},              // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Arabic", TranslationToLanguage = sl,},                // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "arab", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "арапски", TranslationToLanguage = sr,},               // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Arabiska", TranslationToLanguage = sv,},              // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiarabu", TranslationToLanguage = sw,},               // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "அரபு", TranslationToLanguage = ta,},                 // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "అరబిక్", TranslationToLanguage = te,},                 // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาอาหรับ", TranslationToLanguage = th,},              // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Arapça", TranslationToLanguage = tr,},                // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Арабська", TranslationToLanguage = uk,},              // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "عربی", TranslationToLanguage = ur,},                 // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "tiếng Ả Rập", TranslationToLanguage = vi,},           // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "אַראַביש", TranslationToLanguage = yi,},               // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "阿拉伯语", TranslationToLanguage = zh,},               // #61 Language = "Chinese"
//    //            };


//    //        if (af.Names.Count < 61)
//    //            af.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Afrikaans", TranslationToLanguage = en, },                      // #1  Language = "English"
//    //                new LanguageName{ Text = "africaans", TranslationToLanguage = es,},                       // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Afrikaans", TranslationToLanguage = de,},                       // #3  Language = "German"
//    //                new LanguageName{ Text = "الأفريكانية", TranslationToLanguage = ar,},                     // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Afrikaans", TranslationToLanguage = af,},                       // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Afrikaans", TranslationToLanguage = az,},                       // #6  Language = "Azerbaijani"
//    //                new LanguageName{Text = "афрыкаанс", TranslationToLanguage = be,},                        // #7  Language = "Belarusian"
//    //                new LanguageName{Text = "африканс", TranslationToLanguage = bg,},                         // #8  Language = "Bulgarian"
//    //                new LanguageName{Text = "আফ্রিকার অন্যতম সরকারি ভাষা", TranslationToLanguage = bn,},         // #9  Language = "Bengali"
//    //                new LanguageName{Text = "Africaans", TranslationToLanguage = ca,},                        // #10 Language = "Catalan"
//    //                new LanguageName{Text = "afrikánština", TranslationToLanguage = cs,},                     // #11 Language = "Czech"
//    //                new LanguageName{Text = "Affricaneg", TranslationToLanguage = cy,},                       // #12 Language = "Welsh"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = da,},                        // #13 Language = "Danish"
//    //                new LanguageName{Text = "afrikaani", TranslationToLanguage = et,},                        // #14 Language = "Estonian"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = eu,},                        // #15 Language = "Basque"
//    //                new LanguageName{Text = "آفریکانس", TranslationToLanguage = fa,},                        // #16 Language = "Persian"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = fi,},                        // #17 Language = "Finnish"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = fr,},                        // #18 Language = "French"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = ga,},                        // #19 Language = "Irish"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = gl,},                        // #20 Language = "Galician"
//    //                new LanguageName{Text = "આફ્રિકી", TranslationToLanguage = gu,},                            // #21 Language = "Gujarati"
//    //                new LanguageName{Text = "אפריקאנס", TranslationToLanguage = he,},                        // #22 Language = "Hebrew"
//    //                new LanguageName{Text = "दक्षिण अफ्रीकी अथवा केप डच", TranslationToLanguage = hi,},          // #23 Language = "Hindi"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = hr,},                        // #24 Language = "Croatian"
//    //                new LanguageName{Text = "Kreyòl ayisyen", TranslationToLanguage = ht,},                   // #25 Language = "Haitian Creole"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = hu,},                        // #26 Language = "Hungarian"
//    //                new LanguageName{Text = "աֆրիկանս", TranslationToLanguage = hy,},                        // #27 Language = "Armenian"
//    //                new LanguageName{Text = "Afrikanas", TranslationToLanguage = id,},                        // #28 Language = "Indonesian"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = isLanguage,},                // #29 Language = "Icelandic"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = it,},                        // #30 Language = "Italian"
//    //                new LanguageName{Text = "アフリカーンス", TranslationToLanguage = ja,},                    // #31 Language = "Japanese"
//    //                new LanguageName{Text = "აფრიკაანსი", TranslationToLanguage = ka,},                       // #32 Language = "Georgian"
//    //                new LanguageName{Text = "ಆಫ್ರಿಕಾನ್ಸ್", TranslationToLanguage = kn,},                          // #33 Language = "Kannada"
//    //                new LanguageName{Text = "아프리카 어", TranslationToLanguage = ko,},                       // #34 Language = "Korean"
//    //                new LanguageName{Text = "Africanica", TranslationToLanguage = la,},                       // #35 Language = "Latin"
//    //                new LanguageName{Text = "afrikanų", TranslationToLanguage = lt,},                         // #36 Language = "Lithuanian"
//    //                new LanguageName{Text = "afrikandu", TranslationToLanguage = lv,},                        // #37 Language = "Latvian"
//    //                new LanguageName{Text = "африканс", TranslationToLanguage = mk,},                         // #38 Language = "Macedonian"
//    //                new LanguageName{Text = "Bahasa Afrikaan", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = mt,},                        // #40 Language = "Maltese"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = nl,},                        // #41 Language = "Dutch"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = no,},                        // #42 Language = "Norwegian"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = pl,},                        // #43 Language = "Polish"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = pt,},                        // #44 Language = "Portuguese"
//    //                new LanguageName{Text = "afrikaans", TranslationToLanguage = ro,},                        // #45 Language = "Romanian"
//    //                new LanguageName{Text = "африкаанс", TranslationToLanguage = ru,},                        // #46 Language = "Russian"
//    //                new LanguageName{Text = "Afrikánčina", TranslationToLanguage = sk,},                      // #47 Language = "Slovak"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = sl,},                        // #48 Language = "Slovenian"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = sq,},                        // #49 Language = "Albanian"
//    //                new LanguageName{Text = "африканс", TranslationToLanguage = sr,},                         // #50 Language = "Serbian"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = sv,},                        // #51 Language = "Swedish"
//    //                new LanguageName{Text = "Kiafrikana", TranslationToLanguage = sw,},                       // #52 Language = "Swahili"
//    //                new LanguageName{Text = "ஆஃப்ரிகான்ஸ்", TranslationToLanguage = ta,},                   // #53 Language = "Tamil"
//    //                new LanguageName{Text = "ఆఫ్రికాన్స్", TranslationToLanguage = te,},                           // #54 Language = "Telugu"
//    //                new LanguageName{Text = "แอฟริกา", TranslationToLanguage = th,},                            // #55 Language = "Thai"
//    //                new LanguageName{Text = "Afrikaans", TranslationToLanguage = tr,},                        // #56 Language = "Turkish"
//    //                new LanguageName{Text = "Африкаанс", TranslationToLanguage = uk,},                        // #57 Language = "Ukrainian"
//    //                new LanguageName{Text = "ايفريکانز", TranslationToLanguage = ur,},                       // #58 Language = "Urdu"
//    //                new LanguageName{Text = "thứ tiếng", TranslationToLanguage = vi,},                        // #59 Language = "Vietnamese"
//    //                new LanguageName{Text = "אַפֿריקאַנס", TranslationToLanguage = yi,},                        // #60 Language = "Yiddish"
//    //                new LanguageName{Text = "南非语", TranslationToLanguage = zh,},                            // #61 Language = "Chinese"
//    //            };

//    //        if (az.Names.Count < 61)
//    //            az.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Azerbaijani", TranslationToLanguage = en, },                // #1  Language = "English"
//    //                new LanguageName{ Text = "Azerbaiyán", TranslationToLanguage = es,},                  // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Azerbaijani", TranslationToLanguage = de,},                 // #3  Language = "German"
//    //                new LanguageName{ Text = "الأذربيجانية", TranslationToLanguage = ar,},               // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Aserbeidjans", TranslationToLanguage = af,},                // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Azərbaycan", TranslationToLanguage = az,},                  // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "азербайджанскі", TranslationToLanguage = be,},              // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "азербайджански", TranslationToLanguage = bg,},              // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "আজেরবাইজান", TranslationToLanguage = bn,},                  // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Azerbaidjan", TranslationToLanguage = ca,},                 // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "ázerbájdžánský", TranslationToLanguage = cs,},              // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Azerbaijani", TranslationToLanguage = cy,},                 // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "aserbajdsjanske", TranslationToLanguage = da,},             // #13 Language = "Danish"
//    //                new LanguageName{ Text = "aserbaidžaani", TranslationToLanguage = et,},               // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Azerbaijani", TranslationToLanguage = eu,},                 // #15 Language = "Basque"
//    //                new LanguageName{ Text = "آذربایجان", TranslationToLanguage = fa,},                  // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Azerbaidžanin", TranslationToLanguage = fi,},               // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "azerbaïdjanais", TranslationToLanguage = fr,},              // #18 Language = "French"
//    //                new LanguageName{ Text = "Asarbaiseáinis", TranslationToLanguage = ga,},              // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Azerbaiano", TranslationToLanguage = gl,},                  // #20 Language = "Galician"
//    //                new LanguageName{ Text = "અઝરબૈજાની", TranslationToLanguage = gu,},                   // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "אזרביג'אן", TranslationToLanguage = he,},                  // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "आज़रबाइजानी", TranslationToLanguage = hi,},                  // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Azarbejdžanac", TranslationToLanguage = hr,},               // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Azerbaydjan", TranslationToLanguage = ht,},                 // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "azerbajdzsáni", TranslationToLanguage = hu,},               // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "Ադրբեջանի", TranslationToLanguage = hy,},                  // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Azerbaijan", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Aserbaídsjan", TranslationToLanguage = isLanguage,},        // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "azero", TranslationToLanguage = it,},                       // #30 Language = "Italian"
//    //                new LanguageName{ Text = "アゼルバイジャン語", TranslationToLanguage = ja,},           // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "აზერბაიჯანის", TranslationToLanguage = ka,},               // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಅಜರ್ಬೈಜಾನಿ", TranslationToLanguage = kn,},                   // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "아제르바 이잔", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Azeriana", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Azerbaidžano", TranslationToLanguage = lt,},                // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Azerbaidžānas", TranslationToLanguage = lv,},               // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Азербејџан", TranslationToLanguage = mk,},                  // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Azerbaijan", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ażerbajġan", TranslationToLanguage = mt,},                  // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Azerbeidzjaanse", TranslationToLanguage = nl,},             // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "aserbajdsjanske", TranslationToLanguage = no,},             // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "azerbejdżański", TranslationToLanguage = pl,},              // #43 Language = "Polish"
//    //                new LanguageName{ Text = "azerbaijano", TranslationToLanguage = pt,},                 // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "Azerbaidjan", TranslationToLanguage = ro,},                 // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "азербайджанский", TranslationToLanguage = ru,},             // #46 Language = "Russian"
//    //                new LanguageName{ Text = "azerbajdžanský", TranslationToLanguage = sk,},              // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "azerbajdžanski", TranslationToLanguage = sl,},              // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "Azerbaijani", TranslationToLanguage = sq,},                 // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "азербејџански", TranslationToLanguage = sr,},               // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "azerbajdzjanska", TranslationToLanguage = sv,},             // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiazabaijani", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "அஜர்பைஜானி", TranslationToLanguage = ta,},              // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "బైజాన్", TranslationToLanguage = te,},                        // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ชาวอาเซร์ไบจัน", TranslationToLanguage = th,},                  // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Azeri", TranslationToLanguage = tr,},                       // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "азербайджанський", TranslationToLanguage = uk,},            // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "آذربائیجان", TranslationToLanguage = ur,},                 // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Azerbaijan", TranslationToLanguage = vi,},                  // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "אַזערבייַדזאַניש", TranslationToLanguage = yi,},             // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "阿塞拜疆", TranslationToLanguage = zh,},                     // #61 Language = "Chinese"
//    //            };



//    //        if (be.Names.Count < 61)
//    //            be.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Belarusian", TranslationToLanguage = en, },               // #1  Language = "English"
//    //                new LanguageName{ Text = "bielorruso", TranslationToLanguage = es,},                // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "belarussischen", TranslationToLanguage = de,},            // #3  Language = "German"
//    //                new LanguageName{ Text = "البيلاروسية", TranslationToLanguage = ar,},               // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Wit", TranslationToLanguage = af,},                       // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "AzəBelarus", TranslationToLanguage = az,},                // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Беларуская", TranslationToLanguage = be,},                // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "Беларус", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "বেলারুশীয়", TranslationToLanguage = bn,},                  // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "bielorús", TranslationToLanguage = ca,},                  // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "běloruské", TranslationToLanguage = cs,},                 // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Belarwseg", TranslationToLanguage = cy,},                 // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "belarussiske", TranslationToLanguage = da,},              // #13 Language = "Danish"
//    //                new LanguageName{ Text = "Valgevene", TranslationToLanguage = et,},                 // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Belarusian", TranslationToLanguage = eu,},                // #15 Language = "Basque"
//    //                new LanguageName{ Text = "بلاروس", TranslationToLanguage = fa,},                    // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Valko-Venäjän", TranslationToLanguage = fi,},             // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "biélorusses", TranslationToLanguage = fr,},               // #18 Language = "French"
//    //                new LanguageName{ Text = "Bealarúisis", TranslationToLanguage = ga,},               // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Belarusian", TranslationToLanguage = gl,},                // #20 Language = "Galician"
//    //                new LanguageName{ Text = "બેલારુસિયન", TranslationToLanguage = gu,},                 // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "בלארוסית", TranslationToLanguage = he,},                 // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "बेलारूसी", TranslationToLanguage = hi,},                    // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "bjeloruski", TranslationToLanguage = hr,},                // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Belarisyen", TranslationToLanguage = ht,},                // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "fehérorosz", TranslationToLanguage = hu,},                // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "բելարուսերեն", TranslationToLanguage = hy,},              // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Belarusian", TranslationToLanguage = id,},                // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "hvítrússneska", TranslationToLanguage = isLanguage,},     // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "bielorusso", TranslationToLanguage = it,},                // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ベラルーシ語", TranslationToLanguage = ja,},               // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ბელორუსის", TranslationToLanguage = ka,},               // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಬೆಲರೂಸಿಯನ್", TranslationToLanguage = kn,},                // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "벨라루스어", TranslationToLanguage = ko,},                 // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Belarusica", TranslationToLanguage = la,},                // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Baltarusijos", TranslationToLanguage = lt,},              // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Baltkrievijas", TranslationToLanguage = lv,},             // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "белоруски", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Belarus", TranslationToLanguage = ms,},                   // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Belarus", TranslationToLanguage = mt,},                   // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Wit-Russische", TranslationToLanguage = nl,},             // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Belarusian", TranslationToLanguage = no,},                // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "Białorusi", TranslationToLanguage = pl,},                 // #43 Language = "Polish"
//    //                new LanguageName{ Text = "Belarusian", TranslationToLanguage = pt,},                // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "Belarus", TranslationToLanguage = ro,},                   // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "Белорусская", TranslationToLanguage = ru,},               // #46 Language = "Russian"
//    //                new LanguageName{ Text = "bieloruskej", TranslationToLanguage = sk,},               // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "beloruski", TranslationToLanguage = sl,},                 // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "Belarusian", TranslationToLanguage = sq,},                // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "беларусиан", TranslationToLanguage = sr,},                // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "vitryska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kibelarusi", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "பெலாரஷ்யன்", TranslationToLanguage = ta,},            // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "బెలారుషియన్", TranslationToLanguage = te,},                // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "เบลารุส", TranslationToLanguage = th,},                     // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Belarusça", TranslationToLanguage = tr,},                 // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Білоруська", TranslationToLanguage = uk,},                // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "بیلاروسی", TranslationToLanguage = ur,},                  // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Belarus", TranslationToLanguage = vi,},                   // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "בעלאָרוסיש", TranslationToLanguage = yi,},                // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "白俄罗斯", TranslationToLanguage = zh,},                   // #61 Language = "Chinese"
//    //            };

//    //        if (bg.Names.Count < 61)
//    //            bg.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Bulgarian", TranslationToLanguage = en, },             // #1  Language = "English"
//    //                new LanguageName{ Text = "búlgaro", TranslationToLanguage = es,},                // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Bulgarisch", TranslationToLanguage = de,},             // #3  Language = "German"
//    //                new LanguageName{ Text = "البلغارية", TranslationToLanguage = ar,},             // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Bulgaars", TranslationToLanguage = af,},               // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Bolqar", TranslationToLanguage = az,},                 // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Балгарская", TranslationToLanguage = be,},             // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "български", TranslationToLanguage = bg,},              // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "বুলগেরীয়", TranslationToLanguage = bn,},                // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Búlgar", TranslationToLanguage = ca,},                 // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "bulharský", TranslationToLanguage = cs,},              // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Bwlgareg", TranslationToLanguage = cy,},               // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "bulgarian", TranslationToLanguage = da,},              // #13 Language = "Danish"
//    //                new LanguageName{ Text = "bulgaaria", TranslationToLanguage = et,},              // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Bulgarian", TranslationToLanguage = eu,},              // #15 Language = "Basque"
//    //                new LanguageName{ Text = "بلغاری", TranslationToLanguage = fa,},                // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Bulgarian", TranslationToLanguage = fi,},              // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "bulgares", TranslationToLanguage = fr,},               // #18 Language = "French"
//    //                new LanguageName{ Text = "Bulgáiris", TranslationToLanguage = ga,},              // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Búlgaro", TranslationToLanguage = gl,},                // #20 Language = "Galician"
//    //                new LanguageName{ Text = "બલ્ગેરિયન", TranslationToLanguage = gu,},               // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "בולגרית", TranslationToLanguage = he,},               // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "बल्गेरियाई", TranslationToLanguage = hi,},               // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "bugarski", TranslationToLanguage = hr,},               // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "bulgarian", TranslationToLanguage = ht,},              // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "bolgár", TranslationToLanguage = hu,},                 // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "բուլղարացի", TranslationToLanguage = hy,},             // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Bulgaria", TranslationToLanguage = id,},               // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Búlgaríu", TranslationToLanguage = isLanguage,},       // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "bulgaro", TranslationToLanguage = it,},                // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ブルガリア語", TranslationToLanguage = ja,},            // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ბულგარეთის", TranslationToLanguage = ka,},           // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಅಲ್ಲಿಯ ಭಾಷೆ", TranslationToLanguage = kn,},              // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "불가리아 사람", TranslationToLanguage = ko,},           // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Bulgarica", TranslationToLanguage = la,},              // #35 Language = "Latin"
//    //                new LanguageName{ Text = "bulgarų", TranslationToLanguage = lt,},                // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Bulgārijas", TranslationToLanguage = lv,},             // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Бугарија", TranslationToLanguage = mk,},               // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bulgaria", TranslationToLanguage = ms,},               // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Bulgaru", TranslationToLanguage = mt,},                // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "bulgarian", TranslationToLanguage = nl,},              // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "bulgarske", TranslationToLanguage = no,},              // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "bułgarski", TranslationToLanguage = pl,},              // #43 Language = "Polish"
//    //                new LanguageName{ Text = "búlgaro", TranslationToLanguage = pt,},                // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "limba bulgară", TranslationToLanguage = ro,},          // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "болгарский", TranslationToLanguage = ru,},             // #46 Language = "Russian"
//    //                new LanguageName{ Text = "bulharský", TranslationToLanguage = sk,},              // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "bolgarski", TranslationToLanguage = sl,},              // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "bullgare", TranslationToLanguage = sq,},               // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "бугарски", TranslationToLanguage = sr,},               // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "bulgariska", TranslationToLanguage = sv,},             // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Bulgarian", TranslationToLanguage = sw,},              // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "பல்கேரியன்", TranslationToLanguage = ta,},           // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "బల్గేరియన్", TranslationToLanguage = te,},                // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "บัลแกเรีย", TranslationToLanguage = th,},                 // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Bulgar", TranslationToLanguage = tr,},                 // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Болгарська", TranslationToLanguage = uk,},             // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "بلغارین", TranslationToLanguage = ur,},               // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Bun-ga-ri", TranslationToLanguage = vi,},              // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "בולגאַריש", TranslationToLanguage = yi,},              // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "保加利亚语", TranslationToLanguage = zh,},              // #61 Language = "Chinese"
//    //            };


//    //        if (bn.Names.Count < 61)
//    //            bn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = en, },            // #1  Language = "English"
//    //                new LanguageName{ Text = "bengalí", TranslationToLanguage = es,},             // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = de,},             // #3  Language = "German"
//    //                new LanguageName{ Text = "بنغالي", TranslationToLanguage = ar,},             // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = af,},             // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Benqal", TranslationToLanguage = az,},              // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "бенгальскі", TranslationToLanguage = be,},          // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "бенгалски", TranslationToLanguage = bg,},           // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "বাঙ্গালী", TranslationToLanguage = bn,},               // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "bengalí", TranslationToLanguage = ca,},             // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "bengálský", TranslationToLanguage = cs,},           // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = cy,},             // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = da,},             // #13 Language = "Danish"
//    //                new LanguageName{ Text = "bengali", TranslationToLanguage = et,},             // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = eu,},             // #15 Language = "Basque"
//    //                new LanguageName{ Text = "بنگالی", TranslationToLanguage = fa,},             // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = fi,},             // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "bengali", TranslationToLanguage = fr,},             // #18 Language = "French"
//    //                new LanguageName{ Text = "Beangáilis", TranslationToLanguage = ga,},          // #19 Language = "Irish"
//    //                new LanguageName{ Text = "bengalí", TranslationToLanguage = gl,},             // #20 Language = "Galician"
//    //                new LanguageName{ Text = "બંગાળનું", TranslationToLanguage = gu,},              // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "בנגלית", TranslationToLanguage = he,},             // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "बंगाली", TranslationToLanguage = hi,},               // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "bengalski", TranslationToLanguage = hr,},           // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = ht,},             // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "bengáli", TranslationToLanguage = hu,},             // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "բենգալերեն", TranslationToLanguage = hy,},          // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = id,},             // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = isLanguage,},     // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "bengalese", TranslationToLanguage = it,},           // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ベンガル語", TranslationToLanguage = ja,},           // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ბენგალური", TranslationToLanguage = ka,},          // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಬಂಗಾಳಿ", TranslationToLanguage = kn,},              // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "벵골 사람", TranslationToLanguage = ko,},            // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Bengalica", TranslationToLanguage = la,},           // #35 Language = "Latin"
//    //                new LanguageName{ Text = "bengalų", TranslationToLanguage = lt,},             // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "bengāļu", TranslationToLanguage = lv,},             // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "бенгалски", TranslationToLanguage = mk,},           // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = ms,},             // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = mt,},             // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Bengalees", TranslationToLanguage = nl,},           // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = no,},             // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "bengalski", TranslationToLanguage = pl,},           // #43 Language = "Polish"
//    //                new LanguageName{ Text = "bengali", TranslationToLanguage = pt,},             // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "bengali", TranslationToLanguage = ro,},             // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "бенгальский", TranslationToLanguage = ru,},         // #46 Language = "Russian"
//    //                new LanguageName{ Text = "bengálsky", TranslationToLanguage = sk,},           // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = sl,},             // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "bengalisht", TranslationToLanguage = sq,},          // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "бенгалски", TranslationToLanguage = sr,},           // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = sv,},             // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kibengali", TranslationToLanguage = sw,},           // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "பெங்காலி", TranslationToLanguage = ta,},          // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "బెంగాలీ", TranslationToLanguage = te,},               // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "เบงกาลี", TranslationToLanguage = th,},               // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Bengal", TranslationToLanguage = tr,},              // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "бенгальська", TranslationToLanguage = uk,},         // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "بنگالی", TranslationToLanguage = ur,},             // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Bengali", TranslationToLanguage = vi,},             // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "בענגאַליש", TranslationToLanguage = yi,},           // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "孟加拉", TranslationToLanguage = zh,},              // #61 Language = "Chinese"
//    //            };


//    //        if (ca.Names.Count < 61)
//    //            ca.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Catalan", TranslationToLanguage = en, },                   // #1  Language = "English"
//    //                new LanguageName{ Text = "catalán", TranslationToLanguage = es,},                    // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Katalanisch", TranslationToLanguage = de,},                // #3  Language = "German"
//    //                new LanguageName{ Text = "الكاتالونية", TranslationToLanguage = ar,},              // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Katalaans", TranslationToLanguage = af,},                  // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Katalan", TranslationToLanguage = az,},                    // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Каталонскі", TranslationToLanguage = be,},                 // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "каталонски", TranslationToLanguage = bg,},                 // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "কাটালান", TranslationToLanguage = bn,},                     // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "català", TranslationToLanguage = ca,},                     // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "katalánština", TranslationToLanguage = cs,},               // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Catalaneg", TranslationToLanguage = cy,},                  // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "catalansk", TranslationToLanguage = da,},                  // #13 Language = "Danish"
//    //                new LanguageName{ Text = "Katalaani", TranslationToLanguage = et,},                  // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Katalana", TranslationToLanguage = eu,},                   // #15 Language = "Basque"
//    //                new LanguageName{ Text = "کاتالان", TranslationToLanguage = fa,},                    // #16 Language = "Persian"
//    //                new LanguageName{ Text = "katalaani", TranslationToLanguage = fi,},                  // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "catalane", TranslationToLanguage = fr,},                   // #18 Language = "French"
//    //                new LanguageName{ Text = "Catalóinis", TranslationToLanguage = ga,},                 // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Catalán", TranslationToLanguage = gl,},                    // #20 Language = "Galician"
//    //                new LanguageName{ Text = "કેટાલન", TranslationToLanguage = gu,},                      // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "קטלאנית", TranslationToLanguage = he,},                   // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "कैटलन", TranslationToLanguage = hi,},                      // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Catalan", TranslationToLanguage = hr,},                    // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Katalan", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "katalán", TranslationToLanguage = hu,},                    // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "կատալոներեն", TranslationToLanguage = hy,},               // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Katalan", TranslationToLanguage = id,},                    // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "katalónska", TranslationToLanguage = isLanguage,},         // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "catalano", TranslationToLanguage = it,},                   // #30 Language = "Italian"
//    //                new LanguageName{ Text = "カタロニア語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "კატალანური", TranslationToLanguage = ka,},                // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಕ್ಯಾಟಲಾನ್", TranslationToLanguage = kn,},                    // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "카탈로니아의", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Catalana", TranslationToLanguage = la,},                   // #35 Language = "Latin"
//    //                new LanguageName{ Text = "katalonų", TranslationToLanguage = lt,},                   // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Katalāņu", TranslationToLanguage = lv,},                   // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "каталонски", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Catalan", TranslationToLanguage = ms,},             // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Katalan", TranslationToLanguage = mt,},                    // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "catalan", TranslationToLanguage = nl,},                    // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "katalansk", TranslationToLanguage = no,},                  // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "Kataloński", TranslationToLanguage = pl,},                 // #43 Language = "Polish"
//    //                new LanguageName{ Text = "catalão", TranslationToLanguage = pt,},                    // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "catalan", TranslationToLanguage = ro,},                    // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "каталонский", TranslationToLanguage = ru,},                // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Katalánsky", TranslationToLanguage = sk,},                 // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Katalonski", TranslationToLanguage = sl,},                 // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "Katalonje", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "каталонски", TranslationToLanguage = sr,},                 // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "katalanska", TranslationToLanguage = sv,},                 // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kikatalani", TranslationToLanguage = sw,},                 // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "கடாலன்", TranslationToLanguage = ta,},                   // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "కాటలాన్", TranslationToLanguage = te,},                     // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "คาตาลัน", TranslationToLanguage = th,},                      // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Katalan", TranslationToLanguage = tr,},                    // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Каталонський", TranslationToLanguage = uk,},               // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "کاٹالانين", TranslationToLanguage = ur,},                  // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Catalan", TranslationToLanguage = vi,},                    // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "קאַטאַלאַניש", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "加泰罗尼亚", TranslationToLanguage = zh,},                  // #61 Language = "Chinese"
//    //            };

//    //        if (cs.Names.Count < 61)
//    //            cs.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Czech", TranslationToLanguage = en, },                                   // #1  Language = "English"
//    //                new LanguageName{ Text = "checo", TranslationToLanguage = es,},                                    // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Tschechisch", TranslationToLanguage = de,},                              // #3  Language = "German"
//    //                new LanguageName{ Text = "التشيكية", TranslationToLanguage = ar,},                                // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Tsjeggies", TranslationToLanguage = af,},                                // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Çex", TranslationToLanguage = az,},                                      // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Чэшскі", TranslationToLanguage = be,},                                   // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "чешки", TranslationToLanguage = bg,},                                    // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "চেকোশ্লোভাকিয়াবাসী স্লাভজাতির একটি শাখার লোক", TranslationToLanguage = bn,},    // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "txec", TranslationToLanguage = ca,},                                     // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "český", TranslationToLanguage = cs,},                                    // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Tsiec", TranslationToLanguage = cy,},                                    // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "tjekkisk", TranslationToLanguage = da,},                                 // #13 Language = "Danish"
//    //                new LanguageName{ Text = "tšehhi", TranslationToLanguage = et,},                                   // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Txekiar", TranslationToLanguage = eu,},                                  // #15 Language = "Basque"
//    //                new LanguageName{ Text = "چک", TranslationToLanguage = fa,},                                       // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Tšekin", TranslationToLanguage = fi,},                                   // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "tchèque", TranslationToLanguage = fr,},                                  // #18 Language = "French"
//    //                new LanguageName{ Text = "na Seice", TranslationToLanguage = ga,},                                 // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Checo", TranslationToLanguage = gl,},                                    // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ચેક", TranslationToLanguage = gu,},                                       // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "צ", TranslationToLanguage = he,},                                        // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "चेक", TranslationToLanguage = hi,},                                      // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "češki", TranslationToLanguage = hr,},                                    // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "czech", TranslationToLanguage = ht,},                                    // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "cseh", TranslationToLanguage = hu,},                                     // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "չեխ", TranslationToLanguage = hy,},                                      // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Ceko", TranslationToLanguage = id,},                                     // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Tékkland", TranslationToLanguage = isLanguage,},                         // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "ceco", TranslationToLanguage = it,},                                     // #30 Language = "Italian"
//    //                new LanguageName{ Text = "チェコ", TranslationToLanguage = ja,},                                   // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ჩეხეთის", TranslationToLanguage = ka,},                                 // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಜೆಕ್", TranslationToLanguage = kn,},                                      // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "체코어", TranslationToLanguage = ko,},                                   // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Bohemica", TranslationToLanguage = la,},                                 // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Čekijos", TranslationToLanguage = lt,},                                  // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Čehijas", TranslationToLanguage = lv,},                                  // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Чешка", TranslationToLanguage = mk,},                                    // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Czech", TranslationToLanguage = ms,},                                    // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ċeka", TranslationToLanguage = mt,},                                     // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Tsjechisch", TranslationToLanguage = nl,},                               // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "tsjekkiske", TranslationToLanguage = no,},                               // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "Czech", TranslationToLanguage = pl,},                                    // #43 Language = "Polish"
//    //                new LanguageName{ Text = "tcheco", TranslationToLanguage = pt,},                                   // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "ceh", TranslationToLanguage = ro,},                                      // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "чешский", TranslationToLanguage = ru,},                                  // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Český", TranslationToLanguage = sk,},                                    // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Češki", TranslationToLanguage = sl,},                                    // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "çek", TranslationToLanguage = sq,},                                      // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "чешки", TranslationToLanguage = sr,},                                    // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Tjeckien", TranslationToLanguage = sv,},                                 // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Czech", TranslationToLanguage = sw,},                                    // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "செக்", TranslationToLanguage = ta,},                                     // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "చెక్", TranslationToLanguage = te,},                                       // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "สาธารณรัฐเช็ก", TranslationToLanguage = th,},                                // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Çek", TranslationToLanguage = tr,},                                      // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "чеський", TranslationToLanguage = uk,},                                  // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "چیک", TranslationToLanguage = ur,},                                      // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Séc", TranslationToLanguage = vi,},                                      // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "טשעכיש", TranslationToLanguage = yi,},                                  // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "捷克", TranslationToLanguage = zh,},                                     // #61 Language = "Chinese"
//    //            };


//    //        if (cy.Names.Count < 61)
//    //            cy.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Welsh", TranslationToLanguage = en, },                                                // #1  Language = "English"
//    //                new LanguageName{ Text = "galés", TranslationToLanguage = es,},                                                 // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Walisisch", TranslationToLanguage = de,},                                             // #3  Language = "German"
//    //                new LanguageName{ Text = "تهرب من دفع الرهان", TranslationToLanguage = ar,},                                  // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Walliese", TranslationToLanguage = af,},                                              // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Uels", TranslationToLanguage = az,},                                                  // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Валійская", TranslationToLanguage = be,},                                             // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "уелски", TranslationToLanguage = bg,},                                                // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ত্তয়েল্স্দেশসম্বন্ধীয়", TranslationToLanguage = bn,},                                          // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Gal · lès", TranslationToLanguage = ca,},                                             // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "velšský", TranslationToLanguage = cs,},                                               // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Cymraeg", TranslationToLanguage = cy,},                                               // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "walisisk", TranslationToLanguage = da,},                                              // #13 Language = "Danish"
//    //                new LanguageName{ Text = "võlga maksmata jätma", TranslationToLanguage = et,},                                  // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Galesera", TranslationToLanguage = eu,},                                              // #15 Language = "Basque"
//    //                new LanguageName{ Text = "زیر قول زدن", TranslationToLanguage = fa,},                                          // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Walesin", TranslationToLanguage = fi,},                                               // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "Gallois", TranslationToLanguage = fr,},                                               // #18 Language = "French"
//    //                new LanguageName{ Text = "Breatnais", TranslationToLanguage = ga,},                                             // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Galés", TranslationToLanguage = gl,},                                                 // #20 Language = "Galician"
//    //                new LanguageName{ Text = "કરારભંગ કરવો", TranslationToLanguage = gu,},                                           // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "וולשית", TranslationToLanguage = he,},                                               // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "वेल्श", TranslationToLanguage = hi,},                                                   // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "velški", TranslationToLanguage = hr,},                                                // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "welsh", TranslationToLanguage = ht,},                                                 // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "walesi", TranslationToLanguage = hu,},                                                // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "ուելսացի", TranslationToLanguage = hy,},                                              // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Welsh", TranslationToLanguage = id,},                                                 // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "velska", TranslationToLanguage = isLanguage,},                                        // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "gallese", TranslationToLanguage = it,},                                               // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ウェールズ語", TranslationToLanguage = ja,},                                           // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "უელსური", TranslationToLanguage = ka,},                                             // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ವೇಲ್ಸಿನ ಜನರು ಯಾ ಭಾಷೆ", TranslationToLanguage = kn,},                                   // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "웨일스 사람", TranslationToLanguage = ko,},                                            // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Welsh", TranslationToLanguage = la,},                                                 // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Velso", TranslationToLanguage = lt,},                                                 // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "velsiešu", TranslationToLanguage = lv,},                                              // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "велшкиот", TranslationToLanguage = mk,},                                              // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Welsh", TranslationToLanguage = ms,},                                                 // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Welsh", TranslationToLanguage = mt,},                                                 // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "van Wales", TranslationToLanguage = nl,},                                             // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "walisisk", TranslationToLanguage = no,},                                              // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "walijski", TranslationToLanguage = pl,},                                              // #43 Language = "Polish"
//    //                new LanguageName{ Text = "galês", TranslationToLanguage = pt,},                                                 // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "Welsh", TranslationToLanguage = ro,},                                                 // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "валлийский", TranslationToLanguage = ru,},                                            // #46 Language = "Russian"
//    //                new LanguageName{ Text = "waleský", TranslationToLanguage = sk,},                                               // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "valižanščina", TranslationToLanguage = sl,},                                          // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "shkel premtimin", TranslationToLanguage = sq,},                                       // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "велшки", TranslationToLanguage = sr,},                                                // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "walesiska", TranslationToLanguage = sv,},                                             // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Welsh", TranslationToLanguage = sw,},                                                 // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "குதிரை பந்தயத்தில் பணம் கொடுக்காது ஓடி விடு", TranslationToLanguage = ta,},    // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "వెల్ష్", TranslationToLanguage = te,},                                                   // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาชาวเวลส์", TranslationToLanguage = th,},                                             // #55 Language = "Thai"
//    //                new LanguageName{ Text = "şartları yerine getirmemek", TranslationToLanguage = tr,},                            // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Валлійська", TranslationToLanguage = uk,},                                            // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "ويلش", TranslationToLanguage = ur,},                                                 // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "tiếng Wales", TranslationToLanguage = vi,},                                           // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "וועלש", TranslationToLanguage = yi,},                                                // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "威尔士", TranslationToLanguage = zh,},                                                // #61 Language = "Chinese"
//    //            };

//    //        if (da.Names.Count < 61)
//    //            da.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Danish", TranslationToLanguage = en, },                  // #1  Language = "English"
//    //                new LanguageName{ Text = "danés", TranslationToLanguage = es,},                    // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Dänisch", TranslationToLanguage = de,},                  // #3  Language = "German"
//    //                new LanguageName{ Text = "دانماركي", TranslationToLanguage = ar,},                // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Deens", TranslationToLanguage = af,},                    // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Danimarka", TranslationToLanguage = az,},                // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Дацкая", TranslationToLanguage = be,},                   // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "датски", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ডেনিশ", TranslationToLanguage = bn,},                    // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "danès", TranslationToLanguage = ca,},                    // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "dánský", TranslationToLanguage = cs,},                   // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Daneg", TranslationToLanguage = cy,},                    // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "danske", TranslationToLanguage = da,},                   // #13 Language = "Danish"
//    //                new LanguageName{ Text = "taani", TranslationToLanguage = et,},                    // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Danimarkako", TranslationToLanguage = eu,},              // #15 Language = "Basque"
//    //                new LanguageName{ Text = "دانمارکی", TranslationToLanguage = fa,},                // #16 Language = "Persian"
//    //                new LanguageName{ Text = "tanska", TranslationToLanguage = fi,},                   // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "danoises", TranslationToLanguage = fr,},                 // #18 Language = "French"
//    //                new LanguageName{ Text = "Danmhairgis", TranslationToLanguage = ga,},              // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Dinamarqués", TranslationToLanguage = gl,},              // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ડેનિશ ભાષા", TranslationToLanguage = gu,},                // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "דני", TranslationToLanguage = he,},                      // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "डेनिश", TranslationToLanguage = hi,},                     // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "danski", TranslationToLanguage = hr,},                   // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Danwa", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "dán", TranslationToLanguage = hu,},                      // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "դանիերեն", TranslationToLanguage = hy,},                // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Denmark", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Danska", TranslationToLanguage = isLanguage,},           // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "danese", TranslationToLanguage = it,},                   // #30 Language = "Italian"
//    //                new LanguageName{ Text = "デンマーク", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "დანიის", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಡೆನ್ಮಾರ್ಕ್ ದೇಶದ ಭಾಷೆ", TranslationToLanguage = kn,},        // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "덴마크의", TranslationToLanguage = ko,},                  // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Danish", TranslationToLanguage = la,},                   // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Danijos", TranslationToLanguage = lt,},                  // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Dānijas", TranslationToLanguage = lv,},                  // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "данскиот", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Denmark", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName{ Text = "danish", TranslationToLanguage = mt,},                   // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Deens", TranslationToLanguage = nl,},                    // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "danske", TranslationToLanguage = no,},                   // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "duński", TranslationToLanguage = pl,},                   // #43 Language = "Polish"
//    //                new LanguageName{ Text = "dinamarquês", TranslationToLanguage = pt,},              // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "danez", TranslationToLanguage = ro,},                    // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "датский", TranslationToLanguage = ru,},                  // #46 Language = "Russian"
//    //                new LanguageName{ Text = "dánsky", TranslationToLanguage = sk,},                   // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Danish", TranslationToLanguage = sl,},                   // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "danez", TranslationToLanguage = sq,},                    // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "дански", TranslationToLanguage = sr,},                   // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "danska", TranslationToLanguage = sv,},                   // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Denmark", TranslationToLanguage = sw,},                  // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "டானிஷ்", TranslationToLanguage = ta,},                 // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "డేనిష్", TranslationToLanguage = te,},                     // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาเดนมาร์ก", TranslationToLanguage = th,},                // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Danimarkalı", TranslationToLanguage = tr,},              // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Данська", TranslationToLanguage = uk,},                  // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "ڈينش", TranslationToLanguage = ur,},                    // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Đan Mạch", TranslationToLanguage = vi,},                 // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "דאַניש", TranslationToLanguage = yi,},                   // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "丹麦", TranslationToLanguage = zh,},                     // #61 Language = "Chinese"
//    //            };


//    //        if (et.Names.Count < 61)
//    //            et.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Estonian", TranslationToLanguage = en, },               // #1  Language = "English"
//    //                new LanguageName{ Text = "Estonia", TranslationToLanguage = es,},                 // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Estnisch", TranslationToLanguage = de,},                // #3  Language = "German"
//    //                new LanguageName{ Text = "الاستونية", TranslationToLanguage = ar,},               // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Estnies", TranslationToLanguage = af,},                 // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "eston", TranslationToLanguage = az,},                   // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "эстонскі", TranslationToLanguage = be,},                // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "естонски", TranslationToLanguage = bg,},                // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "এস্তোনিয়াদেশ - সংক্রান্ত", TranslationToLanguage = bn,},       // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Estònia", TranslationToLanguage = ca,},                 // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "estonština", TranslationToLanguage = cs,},              // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Estonia", TranslationToLanguage = cy,},                 // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "estisk", TranslationToLanguage = da,},                  // #13 Language = "Danish"
//    //                new LanguageName{ Text = "eesti", TranslationToLanguage = et,},                   // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Estonian", TranslationToLanguage = eu,},                // #15 Language = "Basque"
//    //                new LanguageName{ Text = "زبان استونی", TranslationToLanguage = fa,},            // #16 Language = "Persian"
//    //                new LanguageName{ Text = "virolainen", TranslationToLanguage = fi,},              // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "estonien", TranslationToLanguage = fr,},                // #18 Language = "French"
//    //                new LanguageName{ Text = "Eastóinis", TranslationToLanguage = ga,},               // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Estoniano", TranslationToLanguage = gl,},               // #20 Language = "Galician"
//    //                new LanguageName{ Text = "એસ્ટોનિયન", TranslationToLanguage = gu,},               // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "אסטוניה", TranslationToLanguage = he,},                // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "एस्तोनियावासी", TranslationToLanguage = hi,},             // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "estonski", TranslationToLanguage = hr,},                // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Estonyen", TranslationToLanguage = ht,},                // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "észt", TranslationToLanguage = hu,},                    // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "Էստոնիայի", TranslationToLanguage = hy,},              // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Estonia", TranslationToLanguage = id,},                 // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "eistneska", TranslationToLanguage = isLanguage,},       // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "estonian", TranslationToLanguage = it,},                // #30 Language = "Italian"
//    //                new LanguageName{ Text = "エストニア語", TranslationToLanguage = ja,},             // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ესტონეთის", TranslationToLanguage = ka,},              // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಎಸ್ಟೋನಿಯನ್", TranslationToLanguage = kn,},              // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "에스 토니아 사람", TranslationToLanguage = ko,},         // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Estonica", TranslationToLanguage = la,},                // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Estijos", TranslationToLanguage = lt,},                 // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Igaunijas", TranslationToLanguage = lv,},               // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Естонија", TranslationToLanguage = mk,},                // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Estonia", TranslationToLanguage = ms,},                 // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Estonjan", TranslationToLanguage = mt,},                // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Estlands", TranslationToLanguage = nl,},                // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "estisk", TranslationToLanguage = no,},                  // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "estoński", TranslationToLanguage = pl,},                // #43 Language = "Polish"
//    //                new LanguageName{ Text = "estoniano", TranslationToLanguage = pt,},               // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "limba estonă", TranslationToLanguage = ro,},            // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "эстонский", TranslationToLanguage = ru,},               // #46 Language = "Russian"
//    //                new LanguageName{ Text = "estónčina", TranslationToLanguage = sk,},               // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "estonski", TranslationToLanguage = sl,},                // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "estonez", TranslationToLanguage = sq,},                 // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "естонски", TranslationToLanguage = sr,},                // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "estniska", TranslationToLanguage = sv,},                // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiestonia", TranslationToLanguage = sw,},               // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "எஸ்தானியம்", TranslationToLanguage = ta,},           // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ఈస్టోనియను", TranslationToLanguage = te,},               // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "เอสโตเนีย", TranslationToLanguage = th,},                  // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Estonyalı", TranslationToLanguage = tr,},               // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Естонська", TranslationToLanguage = uk,},               // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "اسٹونين", TranslationToLanguage = ur,},                // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "tiếng Estonia", TranslationToLanguage = vi,},           // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "עסטיש", TranslationToLanguage = yi,},                  // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "爱沙尼亚语", TranslationToLanguage = zh,},               // #61 Language = "Chinese"

//    //            };

//    //        if (eu.Names.Count < 61)
//    //            eu.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = en, },                                 // #1  Language = "English"
//    //                new LanguageName{ Text = "vasco", TranslationToLanguage = es,},                                   // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Baskische", TranslationToLanguage = de,},                               // #3  Language = "German"
//    //                new LanguageName{ Text = "الباسكي", TranslationToLanguage = ar,},                                // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Baskies", TranslationToLanguage = af,},                                 // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Bask", TranslationToLanguage = az,},                                    // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "басконскі", TranslationToLanguage = be,},                               // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "баска", TranslationToLanguage = bg,},                                   // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "স্পেন ও ফ্রান্সের পিরেনিজ পর্বতাঞ্চলের অধিবাসী", TranslationToLanguage = bn,},     // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Basc", TranslationToLanguage = ca,},                                    // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = cs,},                                  // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Basgeg", TranslationToLanguage = cy,},                                  // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "Baskerlandet", TranslationToLanguage = da,},                            // #13 Language = "Danish"
//    //                new LanguageName{ Text = "baski", TranslationToLanguage = et,},                                   // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Euskal", TranslationToLanguage = eu,},                                  // #15 Language = "Basque"
//    //                new LanguageName{ Text = "باسک", TranslationToLanguage = fa,},                                   // #16 Language = "Persian"
//    //                new LanguageName{ Text = "baski", TranslationToLanguage = fi,},                                   // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "basques", TranslationToLanguage = fr,},                                 // #18 Language = "French"
//    //                new LanguageName{ Text = "Bascais", TranslationToLanguage = ga,},                                 // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = gl,},                                  // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ટૂંકી ઝાલરવાળી ચોળી", TranslationToLanguage = gu,},                       // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "הבסקים", TranslationToLanguage = he,},                                 // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "बस्क", TranslationToLanguage = hi,},                                    // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "baskijski", TranslationToLanguage = hr,},                               // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "basque", TranslationToLanguage = ht,},                                  // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "baszk", TranslationToLanguage = hu,},                                   // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "բասկա", TranslationToLanguage = hy,},                                  // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = id,},                                  // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "baskneska", TranslationToLanguage = isLanguage,},                       // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "basco", TranslationToLanguage = it,},                                   // #30 Language = "Italian"
//    //                new LanguageName{ Text = "バスク", TranslationToLanguage = ja,},                                  // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ბასკურ", TranslationToLanguage = ka,},                                  // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಬಾಸ್ಕ್", TranslationToLanguage = kn,},                                    // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "바스크 사람", TranslationToLanguage = ko,},                              // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Vasca", TranslationToLanguage = la,},                                   // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Baskų", TranslationToLanguage = lt,},                                   // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "basku", TranslationToLanguage = lv,},                                   // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Баскија", TranslationToLanguage = mk,},                                 // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = ms,},                                  // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Bask", TranslationToLanguage = mt,},                                    // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "basque", TranslationToLanguage = nl,},                                  // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = no,},                                  // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "baskijski", TranslationToLanguage = pl,},                               // #43 Language = "Polish"
//    //                new LanguageName{ Text = "basco", TranslationToLanguage = pt,},                                   // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "basc", TranslationToLanguage = ro,},                                    // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "баскский", TranslationToLanguage = ru,},                                // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = sk,},                                  // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = sl,},                                  // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "bask", TranslationToLanguage = sq,},                                    // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "баскијски", TranslationToLanguage = sr,},                               // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "baskiska", TranslationToLanguage = sv,},                                // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = sw,},                                  // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "பஸ்க்", TranslationToLanguage = ta,},                                  // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "బాస్క్", TranslationToLanguage = te,},                                    // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ชาวแบสค์", TranslationToLanguage = th,},                                  // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Bask", TranslationToLanguage = tr,},                                    // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Баскська", TranslationToLanguage = uk,},                                // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "باسکی", TranslationToLanguage = ur,},                                  // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Basque", TranslationToLanguage = vi,},                                  // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "באַסק", TranslationToLanguage = yi,},                                   // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "巴斯克", TranslationToLanguage = zh,},                                  // #61 Language = "Chinese"
//    //            };

//    //        if (fa.Names.Count < 61)
//    //            fa.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Persian", TranslationToLanguage = en, },                    // #1  Language = "English"
//    //                new LanguageName{ Text = "persa", TranslationToLanguage = es,},                       // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Persisch", TranslationToLanguage = de,},                    // #3  Language = "German"
//    //                new LanguageName{ Text = "اللغة الفارسية", TranslationToLanguage = ar,},            // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Persiese", TranslationToLanguage = af,},                    // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "fars", TranslationToLanguage = az,},                        // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Фарсі", TranslationToLanguage = be,},                       // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "персийски", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "পারসিক", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "persa", TranslationToLanguage = ca,},                       // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "perský", TranslationToLanguage = cs,},                      // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Perseg", TranslationToLanguage = cy,},                      // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "perser", TranslationToLanguage = da,},                      // #13 Language = "Danish"
//    //                new LanguageName{ Text = "Pärsia", TranslationToLanguage = et,},                      // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Persian", TranslationToLanguage = eu,},                     // #15 Language = "Basque"
//    //                new LanguageName{ Text = "فارسی", TranslationToLanguage = fa,},                      // #16 Language = "Persian"
//    //                new LanguageName{ Text = "persialainen", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "Persique", TranslationToLanguage = fr,},                    // #18 Language = "French"
//    //                new LanguageName{ Text = "Peirsis", TranslationToLanguage = ga,},                     // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Persa", TranslationToLanguage = gl,},                       // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ફારસી", TranslationToLanguage = gu,},                       // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "פרסי", TranslationToLanguage = he,},                       // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "फ़ारसी", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Perzijski", TranslationToLanguage = hr,},                   // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Pèsik", TranslationToLanguage = ht,},                       // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "perzsa", TranslationToLanguage = hu,},                      // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "պարսիկ", TranslationToLanguage = hy,},                     // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Persian", TranslationToLanguage = id,},                     // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "persneska", TranslationToLanguage = isLanguage,},           // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "persiano", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ペルシア語", TranslationToLanguage = ja,},                   // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "სპარსეთის", TranslationToLanguage = ka,},                  // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಪರ್ಷಿಯಾಕ್ಕೆ ಸಂಬಂಧಿಸಿದ", TranslationToLanguage = kn,},           // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "페르시아의", TranslationToLanguage = ko,},                   // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Persae", TranslationToLanguage = la,},                      // #35 Language = "Latin"
//    //                new LanguageName{ Text = "persų", TranslationToLanguage = lt,},                       // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "persiešu", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Персискиот", TranslationToLanguage = mk,},                  // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Parsi", TranslationToLanguage = ms,},                       // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Persjan", TranslationToLanguage = mt,},                     // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Perzisch", TranslationToLanguage = nl,},                    // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Persian", TranslationToLanguage = no,},                     // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "perski", TranslationToLanguage = pl,},                      // #43 Language = "Polish"
//    //                new LanguageName{ Text = "persa", TranslationToLanguage = pt,},                       // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "persană", TranslationToLanguage = ro,},                     // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "персидский", TranslationToLanguage = ru,},                  // #46 Language = "Russian"
//    //                new LanguageName{ Text = "perzský", TranslationToLanguage = sk,},                     // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Persian", TranslationToLanguage = sl,},                     // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "persisht", TranslationToLanguage = sq,},                    // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "персијски", TranslationToLanguage = sr,},                   // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Persiska", TranslationToLanguage = sv,},                    // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiajemi", TranslationToLanguage = sw,},                     // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "பர்ஸியன்", TranslationToLanguage = ta,},                  // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "పెర్షియన్", TranslationToLanguage = te,},                      // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ชาวเปอร์เซีย", TranslationToLanguage = th,},                    // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Farsça", TranslationToLanguage = tr,},                      // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Перська", TranslationToLanguage = uk,},                     // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "فارسی", TranslationToLanguage = ur,},                      // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Tiếng Ba Tư", TranslationToLanguage = vi,},                 // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "פּערסיש", TranslationToLanguage = yi,},                     // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "波斯语", TranslationToLanguage = zh,},                      // #61 Language = "Chinese"
//    //            };


//    //        if (fi.Names.Count < 61)
//    //            fi.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Finnish", TranslationToLanguage = en, },                   // #1  Language = "English"
//    //                new LanguageName{ Text = "finlandés", TranslationToLanguage = es,},                  // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Finnisch", TranslationToLanguage = de,},                   // #3  Language = "German"
//    //                new LanguageName{ Text = "فنلندية", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Finse", TranslationToLanguage = af,},                      // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "fin", TranslationToLanguage = az,},                        // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Фінская", TranslationToLanguage = be,},                    // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "фински", TranslationToLanguage = bg,},                     // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ফিনল্যাণ্ডের ভাষা", TranslationToLanguage = bn,},               // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Finès", TranslationToLanguage = ca,},                      // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "finský", TranslationToLanguage = cs,},                     // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Ffinneg", TranslationToLanguage = cy,},                    // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "finnish", TranslationToLanguage = da,},                    // #13 Language = "Danish"
//    //                new LanguageName{ Text = "soome", TranslationToLanguage = et,},                      // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Finlandiako", TranslationToLanguage = eu,},                // #15 Language = "Basque"
//    //                new LanguageName{ Text = "فنلاندی", TranslationToLanguage = fa,},                    // #16 Language = "Persian"
//    //                new LanguageName{ Text = "suomalainen", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "finlandaise", TranslationToLanguage = fr,},                // #18 Language = "French"
//    //                new LanguageName{ Text = "Fionlainnis", TranslationToLanguage = ga,},                // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Finés", TranslationToLanguage = gl,},                      // #20 Language = "Galician"
//    //                new LanguageName{ Text = "દેશની ભાષા", TranslationToLanguage = gu,},                 // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "פינית", TranslationToLanguage = he,},                     // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "फ़िन जातीय", TranslationToLanguage = hi,},                  // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "finski", TranslationToLanguage = hr,},                     // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "finnish", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "finn", TranslationToLanguage = hu,},                       // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "ֆիննական", TranslationToLanguage = hy,},                  // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Finlandia", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Finnska", TranslationToLanguage = isLanguage,},            // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "finlandese", TranslationToLanguage = it,},                 // #30 Language = "Italian"
//    //                new LanguageName{ Text = "フィンランド", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ფინეთის", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಫಿನ್ನಿಶ್", TranslationToLanguage = kn,},                       // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "핀란드의", TranslationToLanguage = ko,},                    // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Finnish", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName{ Text = "suomių", TranslationToLanguage = lt,},                     // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Somijas", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "финскиот", TranslationToLanguage = mk,},                   // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Finland", TranslationToLanguage = ms,},                    // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Finlandiż", TranslationToLanguage = mt,},                  // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Fins", TranslationToLanguage = nl,},                       // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "finske", TranslationToLanguage = no,},                     // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "fiński", TranslationToLanguage = pl,},                     // #43 Language = "Polish"
//    //                new LanguageName{ Text = "finlandês", TranslationToLanguage = pt,},                  // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "finlandeză", TranslationToLanguage = ro,},                 // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "финский", TranslationToLanguage = ru,},                    // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Fínsky", TranslationToLanguage = sk,},                     // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "finski", TranslationToLanguage = sl,},                     // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "finlandisht", TranslationToLanguage = sq,},                // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "фински", TranslationToLanguage = sr,},                     // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "finska", TranslationToLanguage = sv,},                     // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kifini", TranslationToLanguage = sw,},                     // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஃபின்னிஷ்", TranslationToLanguage = ta,},                // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ఫిన్నిష్", TranslationToLanguage = te,},                      // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ฟินแลนด์", TranslationToLanguage = th,},                     // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Fince", TranslationToLanguage = tr,},                      // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Фінська", TranslationToLanguage = uk,},                    // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "فننش", TranslationToLanguage = ur,},                      // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Phần Lan", TranslationToLanguage = vi,},                   // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "פֿיניש", TranslationToLanguage = yi,},                     // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "芬兰", TranslationToLanguage = zh,},                       // #61 Language = "Chinese"
//    //            };

//    //        if (fr.Names.Count < 61)
//    //            fr.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "French", TranslationToLanguage = en, },                   // #1  Language = "English"
//    //                new LanguageName{ Text = "francés", TranslationToLanguage = es,},                   // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Französisch", TranslationToLanguage = de,},               // #3  Language = "German"
//    //                new LanguageName{ Text = "فرنسي", TranslationToLanguage = ar,},                    // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Frans", TranslationToLanguage = af,},                     // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Fransız dili", TranslationToLanguage = az,},              // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "французская", TranslationToLanguage = be,},               // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "френски", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ফরাসি", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "francès", TranslationToLanguage = ca,},                   // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "francouzština", TranslationToLanguage = cs,},             // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Ffrangeg", TranslationToLanguage = cy,},                  // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "fransk", TranslationToLanguage = da,},                    // #13 Language = "Danish"
//    //                new LanguageName{ Text = "prantsuse", TranslationToLanguage = et,},                 // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Frantziako", TranslationToLanguage = eu,},                // #15 Language = "Basque"
//    //                new LanguageName{ Text = "فرانسوی", TranslationToLanguage = fa,},                  // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Ranskan", TranslationToLanguage = fi,},                   // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "française", TranslationToLanguage = fr,},                 // #18 Language = "French"
//    //                new LanguageName{ Text = "Fraincis", TranslationToLanguage = ga,},                  // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Francés", TranslationToLanguage = gl,},                   // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ફ્રાંસની ભાષા", TranslationToLanguage = gu,},                // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "צרפתי", TranslationToLanguage = he,},                    // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "फ्रांसीसी", TranslationToLanguage = hi,},                    // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "francuski", TranslationToLanguage = hr,},                 // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "franse", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "francia", TranslationToLanguage = hu,},                   // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "ֆրանսերեն", TranslationToLanguage = hy,},                // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Prancis", TranslationToLanguage = id,},                   // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Franska", TranslationToLanguage = isLanguage,},           // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "francese", TranslationToLanguage = it,},                  // #30 Language = "Italian"
//    //                new LanguageName{ Text = "フランス", TranslationToLanguage = ja,},                   // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "საფრანგეთის", TranslationToLanguage = ka,},              // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಫ್ರೆಂಚ್", TranslationToLanguage = kn,},                      // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "프랑스의", TranslationToLanguage = ko,},                   // #34 Language = "Korean"
//    //                new LanguageName{ Text = "French", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName{ Text = "prancūzų", TranslationToLanguage = lt,},                  // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "franču", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "францускиот", TranslationToLanguage = mk,},               // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Perancis", TranslationToLanguage = ms,},           // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Franċiż", TranslationToLanguage = mt,},                   // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Frans", TranslationToLanguage = nl,},                     // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "fransk", TranslationToLanguage = no,},                    // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "francuski", TranslationToLanguage = pl,},                 // #43 Language = "Polish"
//    //                new LanguageName{ Text = "francês", TranslationToLanguage = pt,},                   // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "franceză", TranslationToLanguage = ro,},                  // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "французский", TranslationToLanguage = ru,},               // #46 Language = "Russian"
//    //                new LanguageName{ Text = "francúzština", TranslationToLanguage = sk,},              // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "francoski", TranslationToLanguage = sl,},                 // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "frëngjisht", TranslationToLanguage = sq,},                // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "Французи", TranslationToLanguage = sr,},                  // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "franska", TranslationToLanguage = sv,},                   // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kifaransa", TranslationToLanguage = sw,},                 // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "பிரஞ்சு", TranslationToLanguage = ta,},                   // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ప్రాన్సుదేశభాష", TranslationToLanguage = te,},                // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาฝรั่งเศส", TranslationToLanguage = th,},                 // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Fransız", TranslationToLanguage = tr,},                   // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "французький", TranslationToLanguage = uk,},               // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "فرانسیسی", TranslationToLanguage = ur,},                 // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Pháp", TranslationToLanguage = vi,},                      // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "פראנצויזיש", TranslationToLanguage = yi,},               // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "法国", TranslationToLanguage = zh,},                       // #61 Language = "Chinese"
//    //            };

//    //        if (ga.Names.Count < 61)
//    //            ga.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Irish", TranslationToLanguage = en, },                 // #1  Language = "English"
//    //                new LanguageName{ Text = "irlandés", TranslationToLanguage = es,},               // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Irisch", TranslationToLanguage = de,},                 // #3  Language = "German"
//    //                new LanguageName{ Text = "الأيرلندية", TranslationToLanguage = ar,},             // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Ierse", TranslationToLanguage = af,},                  // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "irland", TranslationToLanguage = az,},                 // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Ірландскі", TranslationToLanguage = be,},              // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "ирландски", TranslationToLanguage = bg,},              // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "আয়াল্যাণ্ড সংক্রান্ত", TranslationToLanguage = bn,},           // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "irlandès", TranslationToLanguage = ca,},               // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "irský", TranslationToLanguage = cs,},                  // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Gwyddelig", TranslationToLanguage = cy,},              // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "irsk", TranslationToLanguage = da,},                   // #13 Language = "Danish"
//    //                new LanguageName{ Text = "iiri", TranslationToLanguage = et,},                   // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Gaelera", TranslationToLanguage = eu,},                // #15 Language = "Basque"
//    //                new LanguageName{ Text = "ایرلندی", TranslationToLanguage = fa,},               // #16 Language = "Persian"
//    //                new LanguageName{ Text = "irlantilainen", TranslationToLanguage = fi,},          // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "irlandaise", TranslationToLanguage = fr,},             // #18 Language = "French"
//    //                new LanguageName{ Text = "na hÉireann", TranslationToLanguage = ga,},            // #19 Language = "Irish"
//    //                new LanguageName{ Text = "irlandés", TranslationToLanguage = gl,},               // #20 Language = "Galician"
//    //                new LanguageName{ Text = "આયર્લેન્ડનું", TranslationToLanguage = gu,},               // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "אירית", TranslationToLanguage = he,},                 // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "आयरिश", TranslationToLanguage = hi,},                 // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "irski", TranslationToLanguage = hr,},                  // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Ilandè", TranslationToLanguage = ht,},                 // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "ír", TranslationToLanguage = hu,},                     // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "իռլանդական", TranslationToLanguage = hy,},            // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Irlandia", TranslationToLanguage = id,},               // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "írska", TranslationToLanguage = isLanguage,},          // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "irlandese", TranslationToLanguage = it,},              // #30 Language = "Italian"
//    //                new LanguageName{ Text = "アイリッシュ", TranslationToLanguage = ja,},            // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ირლანდიის", TranslationToLanguage = ka,},             // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಕೋಪ", TranslationToLanguage = kn,},                   // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "아일랜드", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Hibernica", TranslationToLanguage = la,},              // #35 Language = "Latin"
//    //                new LanguageName{ Text = "airių", TranslationToLanguage = lt,},                  // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Īrijas", TranslationToLanguage = lv,},                 // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Ирска", TranslationToLanguage = mk,},                  // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Ireland", TranslationToLanguage = ms,},                // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Irlandiż", TranslationToLanguage = mt,},               // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Iers", TranslationToLanguage = nl,},                   // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "irsk", TranslationToLanguage = no,},                   // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "irlandzki", TranslationToLanguage = pl,},              // #43 Language = "Polish"
//    //                new LanguageName{ Text = "irlandês", TranslationToLanguage = pt,},               // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "irlandez", TranslationToLanguage = ro,},               // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "ирландский", TranslationToLanguage = ru,},             // #46 Language = "Russian"
//    //                new LanguageName{ Text = "írsky", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "irski", TranslationToLanguage = sl,},                  // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "irlandez", TranslationToLanguage = sq,},               // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "Ирци", TranslationToLanguage = sr,},                   // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "irländska", TranslationToLanguage = sv,},              // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Ireland", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஐயர்லாந்தை சார்ந்த", TranslationToLanguage = ta,},  // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ఐరిష్", TranslationToLanguage = te,},                   // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ไอริช", TranslationToLanguage = th,},                    // #55 Language = "Thai"
//    //                new LanguageName{ Text = "İrlandalı", TranslationToLanguage = tr,},              // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Ірландський", TranslationToLanguage = uk,},            // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "آئيرش", TranslationToLanguage = ur,},                 // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Ailen", TranslationToLanguage = vi,},                  // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "איריש", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "爱尔兰", TranslationToLanguage = zh,},                  // #61 Language = "Chinese"
//    //            };

//    //        if (gl.Names.Count < 61)
//    //            gl.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = en, },             // #1  Language = "English"
//    //                new LanguageName{ Text = "gallego", TranslationToLanguage = es,},               // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "galizischen", TranslationToLanguage = de,},           // #3  Language = "German"
//    //                new LanguageName{ Text = "الجاليكية", TranslationToLanguage = ar,},            // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Galicies", TranslationToLanguage = af,},              // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Qalisian", TranslationToLanguage = az,},              // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Галіцка", TranslationToLanguage = be,},               // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "галисийски", TranslationToLanguage = bg,},            // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "গ্যালিশিয়", TranslationToLanguage = bn,},                // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Gallego", TranslationToLanguage = ca,},               // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = cs,},              // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = cy,},              // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "galiciske", TranslationToLanguage = da,},             // #13 Language = "Danish"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = et,},              // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Galiziako", TranslationToLanguage = eu,},             // #15 Language = "Basque"
//    //                new LanguageName{ Text = "گالیسیایی", TranslationToLanguage = fa,},            // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = fi,},              // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "Galice", TranslationToLanguage = fr,},                // #18 Language = "French"
//    //                new LanguageName{ Text = "Gailísis", TranslationToLanguage = ga,},              // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Galego", TranslationToLanguage = gl,},                // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ગેલિશિયન", TranslationToLanguage = gu,},              // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "גליציאנית", TranslationToLanguage = he,},            // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "गैलिशियन्", TranslationToLanguage = hi,},              // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Galicijski", TranslationToLanguage = hr,},            // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Galisyen", TranslationToLanguage = ht,},              // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = hu,},              // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "գալիսերեն", TranslationToLanguage = hy,},             // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Galicia", TranslationToLanguage = id,},               // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "galisíska", TranslationToLanguage = isLanguage,},     // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "galiziano", TranslationToLanguage = it,},             // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ガリシア語", TranslationToLanguage = ja,},             // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "გალიციური", TranslationToLanguage = ka,},            // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಗ್ಯಾಲಿಶಿಯನ್", TranslationToLanguage = kn,},             // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "갈리시아어", TranslationToLanguage = ko,},             // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Gallaeca", TranslationToLanguage = la,},              // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Galicijos", TranslationToLanguage = lt,},             // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "galisiešu", TranslationToLanguage = lv,},             // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Галиција", TranslationToLanguage = mk,},              // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Galicia", TranslationToLanguage = ms,},        // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Galicia", TranslationToLanguage = mt,},               // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "galician", TranslationToLanguage = nl,},              // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "galiciske", TranslationToLanguage = no,},             // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "galicyjskiej", TranslationToLanguage = pl,},          // #43 Language = "Polish"
//    //                new LanguageName{ Text = "galego", TranslationToLanguage = pt,},                // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "galiciană", TranslationToLanguage = ro,},             // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "Галицко", TranslationToLanguage = ru,},               // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = sk,},              // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = sl,},              // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = sq,},              // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "галицијски", TranslationToLanguage = sr,},            // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "galiciska", TranslationToLanguage = sv,},             // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Galician", TranslationToLanguage = sw,},              // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "காலிசியன்", TranslationToLanguage = ta,},           // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "గెలీసియన్", TranslationToLanguage = te,},               // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "กาลิเซีย", TranslationToLanguage = th,},                 // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Galicia'ya ait", TranslationToLanguage = tr,},        // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Галицько", TranslationToLanguage = uk,},              // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "گاليشيائی", TranslationToLanguage = ur,},            // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Tiếng Galicia", TranslationToLanguage = vi,},         // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "גאליציאנער", TranslationToLanguage = yi,},           // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "加利西亚", TranslationToLanguage = zh,},               // #61 Language = "Chinese"
//    //            };



//    //        if (gu.Names.Count < 61)
//    //            gu.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = en, },            // #1  Language = "English"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = es,},             // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = de,},             // #3  Language = "German"
//    //                new LanguageName{ Text = "الغوجاراتية", TranslationToLanguage = ar,},        // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = af,},             // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = az,},             // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Гуяраці", TranslationToLanguage = be,},              // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "гуджарати", TranslationToLanguage = bg,},            // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "গুজরাটি", TranslationToLanguage = bn,},               // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = ca,},             // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = cs,},             // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Gwjarati", TranslationToLanguage = cy,},             // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = da,},             // #13 Language = "Danish"
//    //                new LanguageName{ Text = "gudžarati", TranslationToLanguage = et,},            // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = eu,},             // #15 Language = "Basque"
//    //                new LanguageName{ Text = "گجراتی", TranslationToLanguage = fa,},              // #16 Language = "Persian"
//    //                new LanguageName{ Text = "gudzarati", TranslationToLanguage = fi,},            // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "Goudjrati", TranslationToLanguage = fr,},            // #18 Language = "French"
//    //                new LanguageName{ Text = "Gúisearáitis", TranslationToLanguage = ga,},         // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Guzerate", TranslationToLanguage = gl,},             // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ગુજરાતી", TranslationToLanguage = gu,},               // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "גוג'ראטית", TranslationToLanguage = he,},           // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "गुजराती", TranslationToLanguage = hi,},               // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "gujarati", TranslationToLanguage = hr,},             // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = ht,},             // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "gudzsaráti", TranslationToLanguage = hu,},           // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "գուջարատերեն", TranslationToLanguage = hy,},        // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Gujarat", TranslationToLanguage = id,},              // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "gújaratí", TranslationToLanguage = isLanguage,},     // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "gujarati", TranslationToLanguage = it,},             // #30 Language = "Italian"
//    //                new LanguageName{ Text = "グジャラート語", TranslationToLanguage = ja,},        // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "გუჯარატი", TranslationToLanguage = ka,},            // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಗುಜರಾತಿ", TranslationToLanguage = kn,},               // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "구자라트어", TranslationToLanguage = ko,},            // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Guiaratica", TranslationToLanguage = la,},           // #35 Language = "Latin"
//    //                new LanguageName{ Text = "gudžarati", TranslationToLanguage = lt,},            // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "gudžaratu", TranslationToLanguage = lv,},            // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Гуџарати", TranslationToLanguage = mk,},             // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = ms,},             // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Guġarati", TranslationToLanguage = mt,},             // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = nl,},             // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "gujarati", TranslationToLanguage = no,},             // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "gudżarati", TranslationToLanguage = pl,},            // #43 Language = "Polish"
//    //                new LanguageName{ Text = "guzerate", TranslationToLanguage = pt,},             // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "gujarati", TranslationToLanguage = ro,},             // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "гуджарати", TranslationToLanguage = ru,},            // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = sk,},             // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "gudžaratščina", TranslationToLanguage = sl,},        // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "guxharati", TranslationToLanguage = sq,},            // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "гујарати", TranslationToLanguage = sr,},             // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = sv,},             // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kigujarati", TranslationToLanguage = sw,},           // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "குஜராத்தி", TranslationToLanguage = ta,},           // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "గుజరాతీ", TranslationToLanguage = te,},               // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "คุชราต", TranslationToLanguage = th,},                // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = tr,},             // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "гуджараті", TranslationToLanguage = uk,},            // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "گجراتی", TranslationToLanguage = ur,},              // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Gujarati", TranslationToLanguage = vi,},             // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "גודזשאַראַטי", TranslationToLanguage = yi,},          // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "古吉拉特语", TranslationToLanguage = zh,},            // #61 Language = "Chinese"
//    //            };

//    //        if (he.Names.Count < 61)
//    //            he.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Hebrew", TranslationToLanguage = en, },                    // #1  Language = "English"
//    //                new LanguageName{ Text = "hebreo", TranslationToLanguage = es,},                     // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Hebräisch", TranslationToLanguage = de,},                  // #3  Language = "German"
//    //                new LanguageName{ Text = "العبرية", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Hebreeus", TranslationToLanguage = af,},                   // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "İvrit", TranslationToLanguage = az,},                      // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "іўрыт", TranslationToLanguage = be,},                      // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "иврит", TranslationToLanguage = bg,},                      // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ইহুদি", TranslationToLanguage = bn,},                        // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "hebreu", TranslationToLanguage = ca,},                     // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "hebrejský", TranslationToLanguage = cs,},                  // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Hebraeg", TranslationToLanguage = cy,},                    // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "Hebrew", TranslationToLanguage = da,},                     // #13 Language = "Danish"
//    //                new LanguageName{ Text = "heebrea", TranslationToLanguage = et,},                    // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Hebrear", TranslationToLanguage = eu,},                    // #15 Language = "Basque"
//    //                new LanguageName{ Text = "زبان عبری", TranslationToLanguage = fa,},                 // #16 Language = "Persian"
//    //                new LanguageName{ Text = "heprealainen", TranslationToLanguage = fi,},               // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "l'hébreu", TranslationToLanguage = fr,},                   // #18 Language = "French"
//    //                new LanguageName{ Text = "Eabhrais", TranslationToLanguage = ga,},                   // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Hebreo", TranslationToLanguage = gl,},                     // #20 Language = "Galician"
//    //                new LanguageName{ Text = "યહુદી", TranslationToLanguage = gu,},                       // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "עברית", TranslationToLanguage = he,},                     // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "यहूदी", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Hebrejski", TranslationToLanguage = hr,},                  // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "lang ebre", TranslationToLanguage = ht,},                  // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "héber", TranslationToLanguage = hu,},                      // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "եբրայեցի", TranslationToLanguage = hy,},                   // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Ibrani", TranslationToLanguage = id,},                     // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "hebreska", TranslationToLanguage = isLanguage,},           // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "ebraico", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ヘブライ", TranslationToLanguage = ja,},                    // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ებრაული", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಹೀಬ್ರೂ", TranslationToLanguage = kn,},                      // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "히브리어", TranslationToLanguage = ko,},                    // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Hebrew", TranslationToLanguage = la,},                     // #35 Language = "Latin"
//    //                new LanguageName{ Text = "hebrajų", TranslationToLanguage = lt,},                    // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "ebreju", TranslationToLanguage = lv,},                     // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "хебрејски јазик", TranslationToLanguage = mk,},            // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Ibrani", TranslationToLanguage = ms,},                     // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ebrajk", TranslationToLanguage = mt,},                     // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Hebreeuws", TranslationToLanguage = nl,},                  // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "hebraisk", TranslationToLanguage = no,},                   // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "hebrajski", TranslationToLanguage = pl,},                  // #43 Language = "Polish"
//    //                new LanguageName{ Text = "hebraico", TranslationToLanguage = pt,},                   // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "evreiesc", TranslationToLanguage = ro,},                   // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "иврит", TranslationToLanguage = ru,},                      // #46 Language = "Russian"
//    //                new LanguageName{ Text = "hebrejský", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Hebrew", TranslationToLanguage = sl,},                     // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "Hebraisht", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "јеврејски", TranslationToLanguage = sr,},                  // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "hebreiska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiyahudi", TranslationToLanguage = sw,},                   // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "யூதர்", TranslationToLanguage = ta,},                      // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "యూదుల భాష", TranslationToLanguage = te,},                // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาฮิบรู", TranslationToLanguage = th,},                     // #55 Language = "Thai"
//    //                new LanguageName{ Text = "İbranice", TranslationToLanguage = tr,},                   // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Іврит", TranslationToLanguage = uk,},                      // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "عبرانی", TranslationToLanguage = ur,},                    // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "tiếng Do Thái,", TranslationToLanguage = vi,},             // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "העברעיש", TranslationToLanguage = yi,},                   // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "希伯来文", TranslationToLanguage = zh,},                    // #61 Language = "Chinese"
//    //            };

//    //        if (hi.Names.Count < 61)
//    //            hi.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = en, },                                        // #1  Language = "English"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = es,},                                         // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = de,},                                         // #3  Language = "German"
//    //                new LanguageName{ Text = "هندي", TranslationToLanguage = ar,},                                         // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = af,},                                         // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = az,},                                         // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "ня", TranslationToLanguage = be,},                                            // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "хинди", TranslationToLanguage = bg,},                                         // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "না", TranslationToLanguage = bn,},                                            // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = ca,},                                         // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "hindština", TranslationToLanguage = cs,},                                     // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = cy,},                                         // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = da,},                                         // #13 Language = "Danish"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = et,},                                         // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "ez", TranslationToLanguage = eu,},                                            // #15 Language = "Basque"
//    //                new LanguageName{ Text = "هندی", TranslationToLanguage = fa,},                                         // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = fi,},                                         // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = fr,},                                         // #18 Language = "French"
//    //                new LanguageName{ Text = "Hiondúis", TranslationToLanguage = ga,},                                      // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = gl,},                                         // #20 Language = "Galician"
//    //                new LanguageName{ Text = "નથી", TranslationToLanguage = gu,},                                           // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "הינדי", TranslationToLanguage = he,},                                        // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "हिंदी", TranslationToLanguage = hi,},                                           // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Hindski", TranslationToLanguage = hr,},                                       // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = ht,},                                         // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = hu,},                                         // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = hy,},                                         // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = id,},                                         // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = isLanguage,},                                 // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = it,},                                         // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ヒンディー語", TranslationToLanguage = ja,},                                   // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ჰინდი", TranslationToLanguage = ka,},                                         // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಹಿಂದಿ", TranslationToLanguage = kn,},                                          // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "힌디어", TranslationToLanguage = ko,},                                        // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = la,},                                         // #35 Language = "Latin"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = lt,},                                         // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = lv,},                                         // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "хинди", TranslationToLanguage = mk,},                                         // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = ms,},                                         // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ħindi", TranslationToLanguage = mt,},                                         // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = nl,},                                         // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = no,},                                         // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = pl,},                                         // #43 Language = "Polish"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = pt,},                                         // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = ro,},                                         // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "хинди", TranslationToLanguage = ru,},                                         // #46 Language = "Russian"
//    //                new LanguageName{ Text = "hindčina", TranslationToLanguage = sk,},                                      // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = sl,},                                         // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "hindi", TranslationToLanguage = sq,},                                         // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "хинди", TranslationToLanguage = sr,},                                         // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = sv,},                                         // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Hindi", TranslationToLanguage = sw,},                                         // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "இந்தியாவில் பரவலாக பேசப்படும் மொழி", TranslationToLanguage = ta,},   // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "హిందీభాష", TranslationToLanguage = te,},                                       // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาฮินดี", TranslationToLanguage = th,},                                        // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Hintçe", TranslationToLanguage = tr,},                                        // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Хінді", TranslationToLanguage = uk,},                                         // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "ہندی", TranslationToLanguage = ur,},                                         // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Tiếng Hin-ddi", TranslationToLanguage = vi,},                                 // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "הינדיש", TranslationToLanguage = yi,},                                       // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "印地文", TranslationToLanguage = zh,},                                        // #61 Language = "Chinese"

//    //            };



//    //        if (hr.Names.Count < 61)
//    //            hr.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Croatian", TranslationToLanguage = en, },                     // #1  Language = "English"
//    //                new LanguageName{ Text = "croata", TranslationToLanguage = es,},                        // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "kroatisch", TranslationToLanguage = de,},                     // #3  Language = "German"
//    //                new LanguageName{ Text = "الكرواتي", TranslationToLanguage = ar,},                     // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Kroaties", TranslationToLanguage = af,},                      // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Xorvat", TranslationToLanguage = az,},                        // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Харвацкая", TranslationToLanguage = be,},                     // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "хърватски", TranslationToLanguage = bg,},                     // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ক্রোয়েশিয়ান", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "croat", TranslationToLanguage = ca,},                         // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "chorvatský", TranslationToLanguage = cs,},                    // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Croateg", TranslationToLanguage = cy,},                       // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "Kroatisk", TranslationToLanguage = da,},                      // #13 Language = "Danish"
//    //                new LanguageName{ Text = "horvaatia", TranslationToLanguage = et,},                     // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Kroazierara", TranslationToLanguage = eu,},                   // #15 Language = "Basque"
//    //                new LanguageName{ Text = "کرواتی", TranslationToLanguage = fa,},                       // #16 Language = "Persian"
//    //                new LanguageName{ Text = "kroaatti", TranslationToLanguage = fi,},                      // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "croate", TranslationToLanguage = fr,},                        // #18 Language = "French"
//    //                new LanguageName{ Text = "Cróitis", TranslationToLanguage = ga,},                       // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Croata", TranslationToLanguage = gl,},                        // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ક્રોએશિયન", TranslationToLanguage = gu,},                      // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "קרואטית", TranslationToLanguage = he,},                      // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "क्रोएशियाई", TranslationToLanguage = hi,},                      // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "hrvatski", TranslationToLanguage = hr,},                      // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "kwoasyen", TranslationToLanguage = ht,},                      // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "horvát", TranslationToLanguage = hu,},                        // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "խորվաթական", TranslationToLanguage = hy,},                  // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Kroasia", TranslationToLanguage = id,},                       // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Króatíska", TranslationToLanguage = isLanguage,},             // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "croato", TranslationToLanguage = it,},                        // #30 Language = "Italian"
//    //                new LanguageName{ Text = "クロアチア語", TranslationToLanguage = ja,},                   // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ხორვატული", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಕ್ರೊಯೇಷಿಯಾದ", TranslationToLanguage = kn,},                   // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "크로아티아의", TranslationToLanguage = ko,},                   // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Illyrica", TranslationToLanguage = la,},                      // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Kroatijos", TranslationToLanguage = lt,},                     // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Horvātijas", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Хрватска", TranslationToLanguage = mk,},                      // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Croatia", TranslationToLanguage = ms,},                // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Kroat", TranslationToLanguage = mt,},                         // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Kroatisch", TranslationToLanguage = nl,},                     // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "kroatisk", TranslationToLanguage = no,},                      // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "chorwacki", TranslationToLanguage = pl,},                     // #43 Language = "Polish"
//    //                new LanguageName{ Text = "croata", TranslationToLanguage = pt,},                        // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "croat", TranslationToLanguage = ro,},                         // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "хорватский", TranslationToLanguage = ru,},                    // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Chorvátsky", TranslationToLanguage = sk,},                    // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "hrvaški", TranslationToLanguage = sl,},                       // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "kroate", TranslationToLanguage = sq,},                        // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "хрватски", TranslationToLanguage = sr,},                      // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "kroatiska", TranslationToLanguage = sv,},                     // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kikroeshia", TranslationToLanguage = sw,},                    // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "கரோஷியன்", TranslationToLanguage = ta,},                  // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "క్రొయేషియన్", TranslationToLanguage = te,},                     // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "โครเอเชีย", TranslationToLanguage = th,},                        // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Hırvat", TranslationToLanguage = tr,},                        // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Хорватська", TranslationToLanguage = uk,},                    // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "کروشین", TranslationToLanguage = ur,},                       // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Croatia", TranslationToLanguage = vi,},                       // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "קראָאַטיש", TranslationToLanguage = yi,},                      // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "克罗地亚", TranslationToLanguage = zh,},                       // #61 Language = "Chinese"
//    //            };

//    //        if (ht.Names.Count < 61)
//    //            ht.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Haitian Creole", TranslationToLanguage = en, },               // #1  Language = "English"
//    //                new LanguageName{ Text = "buenas Noticias", TranslationToLanguage = es,},               // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Good News", TranslationToLanguage = de,},                     // #3  Language = "German"
//    //                new LanguageName{ Text = "أخبار سارة", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "goeie Nuus", TranslationToLanguage = af,},                    // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "Xoş Xəbər", TranslationToLanguage = az,},                     // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "добрыя навіны", TranslationToLanguage = be,},                 // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "Добрата новина", TranslationToLanguage = bg,},                // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "সু - সংবাদ", TranslationToLanguage = bn,},                     // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "bones Notícies", TranslationToLanguage = ca,},                // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "dobré zprávy", TranslationToLanguage = cs,},                  // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Newyddion Da", TranslationToLanguage = cy,},                  // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "gode nyheder", TranslationToLanguage = da,},                  // #13 Language = "Danish"
//    //                new LanguageName{ Text = "head uudised", TranslationToLanguage = et,},                  // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Good News", TranslationToLanguage = eu,},                     // #15 Language = "Basque"
//    //                new LanguageName{ Text = "مژده", TranslationToLanguage = fa,},                         // #16 Language = "Persian"
//    //                new LanguageName{ Text = "hyviä uutisia", TranslationToLanguage = fi,},                 // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "French", TranslationToLanguage = fr,},                        // #18 Language = "French"
//    //                new LanguageName{ Text = "dea-Scéal", TranslationToLanguage = ga,},                     // #19 Language = "Irish"
//    //                new LanguageName{ Text = "boa Nova", TranslationToLanguage = gl,},                      // #20 Language = "Galician"
//    //                new LanguageName{ Text = "સારા સમાચાર", TranslationToLanguage = gu,},                   // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "חדשות טובות", TranslationToLanguage = he,},                  // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "अच्छी खबर", TranslationToLanguage = hi,},                     // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Haićanski kreolski", TranslationToLanguage = hr,},            // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "kreyòl ayisyen", TranslationToLanguage = ht,},                // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "jó hír", TranslationToLanguage = hu,},                        // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "Haitian կրեոլ", TranslationToLanguage = hy,},                 // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "baik Berita", TranslationToLanguage = id,},                   // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Góðar fréttir", TranslationToLanguage = isLanguage,},         // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "buona Novella", TranslationToLanguage = it,},                 // #30 Language = "Italian"
//    //                new LanguageName{ Text = "良いニュース", TranslationToLanguage = ja,},                   // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "Haitian Creole", TranslationToLanguage = ka,},                // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಹೈಟಿ ಕ್ರಿಯೋಲ", TranslationToLanguage = kn,},                    // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "아이티 크리올어 크리올", TranslationToLanguage = ko,},         // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Haitian Creole", TranslationToLanguage = la,},                // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Haičio kreolų", TranslationToLanguage = lt,},                 // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Haiti kreolu", TranslationToLanguage = lv,},                  // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Хаити креолски", TranslationToLanguage = mk,},                // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Haiti Creole", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ħaitjan", TranslationToLanguage = mt,},                       // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Haïtiaans Creools", TranslationToLanguage = nl,},             // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "haitisk kreolsk", TranslationToLanguage = no,},               // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "Haiti Creole", TranslationToLanguage = pl,},                  // #43 Language = "Polish"
//    //                new LanguageName{ Text = "crioulo haitiano", TranslationToLanguage = pt,},              // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "haitiană Creole", TranslationToLanguage = ro,},               // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "гаитянский креольский", TranslationToLanguage = ru,},         // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Haitian kreolský", TranslationToLanguage = sk,},              // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Haitian Creole", TranslationToLanguage = sl,},                // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "haitian Creole", TranslationToLanguage = sq,},                // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "хаићански креолски", TranslationToLanguage = sr,},            // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Haitisk kreol", TranslationToLanguage = sv,},                 // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Haitian Creole", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஹெய்டியன் கிரியோல்", TranslationToLanguage = ta,},       // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "హైటియన్ క్రియోల్", TranslationToLanguage = te,},                 // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ครีโอลเฮติ", TranslationToLanguage = th,},                       // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Haiti Creole", TranslationToLanguage = tr,},                  // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "гаїтянський креольський", TranslationToLanguage = uk,},       // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "ہیٹی Creole", TranslationToLanguage = ur,},                  // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Haiti", TranslationToLanguage = vi,},                         // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "האַיטיאַן קרעאָלע", TranslationToLanguage = yi,},              // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "海地克里奥尔语", TranslationToLanguage = zh,},                 // #61 Language = "Chinese"
//    //            };

//    //        if (hu.Names.Count < 61)
//    //            hu.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Hungarian", TranslationToLanguage = en, },                 // #1  Language = "English"
//    //                new LanguageName{ Text = "húngaro", TranslationToLanguage = es,},                    // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Ungarisch", TranslationToLanguage = de,},                  // #3  Language = "German"
//    //                new LanguageName{ Text = "المجري", TranslationToLanguage = ar,},                    // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Hongaars", TranslationToLanguage = af,},                   // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "macar", TranslationToLanguage = az,},                      // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Венгерская", TranslationToLanguage = be,},                 // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "унгарски", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "হাঙ্গেরীয়", TranslationToLanguage = bn,},                     // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "hongarès", TranslationToLanguage = ca,},                   // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "maďarština", TranslationToLanguage = cs,},                 // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Hwngari", TranslationToLanguage = cy,},                    // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "ungarsk", TranslationToLanguage = da,},                    // #13 Language = "Danish"
//    //                new LanguageName{ Text = "ungari", TranslationToLanguage = et,},                     // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Hungarian", TranslationToLanguage = eu,},                  // #15 Language = "Basque"
//    //                new LanguageName{ Text = "مجارستانی", TranslationToLanguage = fa,},                 // #16 Language = "Persian"
//    //                new LanguageName{ Text = "unkari", TranslationToLanguage = fi,},                     // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "hongroises", TranslationToLanguage = fr,},                 // #18 Language = "French"
//    //                new LanguageName{ Text = "Ungáiris", TranslationToLanguage = ga,},                   // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Húngaro", TranslationToLanguage = gl,},                    // #20 Language = "Galician"
//    //                new LanguageName{ Text = "હંગેરિયન", TranslationToLanguage = gu,},                    // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "הונגרי", TranslationToLanguage = he,},                    // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "हंगेरी", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "mađarski", TranslationToLanguage = hr,},                   // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Ongwa", TranslationToLanguage = ht,},                      // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "magyar", TranslationToLanguage = hu,},                     // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "հունգարերեն", TranslationToLanguage = hy,},                // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Hongaria", TranslationToLanguage = id,},                   // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Ungverska", TranslationToLanguage = isLanguage,},          // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "ungherese", TranslationToLanguage = it,},                  // #30 Language = "Italian"
//    //                new LanguageName{ Text = "ハンガリー", TranslationToLanguage = ja,},                  // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "უნგრეთის", TranslationToLanguage = ka,},                  // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಹಂಗೇರಿಯ", TranslationToLanguage = kn,},                    // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "헝가리어", TranslationToLanguage = ko,},                    // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Hungarica", TranslationToLanguage = la,},                  // #35 Language = "Latin"
//    //                new LanguageName{ Text = "vengrų", TranslationToLanguage = lt,},                     // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Ungārijas", TranslationToLanguage = lv,},                  // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "унгарскиот", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Bahasa Hungary", TranslationToLanguage = ms,},             // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ungeriż", TranslationToLanguage = mt,},                    // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Hongaars", TranslationToLanguage = nl,},                   // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Hungarian", TranslationToLanguage = no,},                  // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "węgierski", TranslationToLanguage = pl,},                  // #43 Language = "Polish"
//    //                new LanguageName{ Text = "húngaro", TranslationToLanguage = pt,},                    // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "limba maghiară", TranslationToLanguage = ro,},             // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "венгерский", TranslationToLanguage = ru,},                 // #46 Language = "Russian"
//    //                new LanguageName{ Text = "maďarčina", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "madžarski", TranslationToLanguage = sl,},                  // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "hungarez", TranslationToLanguage = sq,},                   // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "мађарски", TranslationToLanguage = sr,},                   // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Ungerska", TranslationToLanguage = sv,},                   // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Hungarian", TranslationToLanguage = sw,},                  // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஹங்கேரியன்", TranslationToLanguage = ta,},             // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "హన్గేరియన్", TranslationToLanguage = te,},                   // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาฮังการี", TranslationToLanguage = th,},                   // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Macar", TranslationToLanguage = tr,},                      // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Угорський", TranslationToLanguage = uk,},                  // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "ہنگیرین", TranslationToLanguage = ur,},                   // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Hungary", TranslationToLanguage = vi,},                    // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "אונגעריש", TranslationToLanguage = yi,},                  // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "匈牙利", TranslationToLanguage = zh,},                     // #61 Language = "Chinese"
//    //            };

//    //        if (hy.Names.Count < 61)
//    //            hy.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Armenian", TranslationToLanguage = en, },                  // #1  Language = "English"
//    //                new LanguageName{ Text = "armenio", TranslationToLanguage = es,},                    // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Armenisch", TranslationToLanguage = de,},                  // #3  Language = "German"
//    //                new LanguageName{ Text = "الأرميني", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Armeens", TranslationToLanguage = af,},                    // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "erməni", TranslationToLanguage = az,},                     // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "армянскі", TranslationToLanguage = be,},                   // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "арменски", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "আর্মেনিয়ান", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "armeni", TranslationToLanguage = ca,},                     // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "arménský", TranslationToLanguage = cs,},                   // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Armenia", TranslationToLanguage = cy,},                    // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "armenske", TranslationToLanguage = da,},                   // #13 Language = "Danish"
//    //                new LanguageName{ Text = "armeenia", TranslationToLanguage = et,},                   // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Armenian", TranslationToLanguage = eu,},                   // #15 Language = "Basque"
//    //                new LanguageName{ Text = "ارمنی", TranslationToLanguage = fa,},                     // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Armenian", TranslationToLanguage = fi,},                   // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "arméniens", TranslationToLanguage = fr,},                  // #18 Language = "French"
//    //                new LanguageName{ Text = "Airméinis", TranslationToLanguage = ga,},                  // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Armenio", TranslationToLanguage = gl,},                    // #20 Language = "Galician"
//    //                new LanguageName{ Text = "આર્મેનિયન", TranslationToLanguage = gu,},                   // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "ארמני", TranslationToLanguage = he,},                     // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "आर्मीनियाई", TranslationToLanguage = hi,},                   // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "Armenski", TranslationToLanguage = hr,},                   // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Amenyen", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "örmény", TranslationToLanguage = hu,},                     // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "հայերեն", TranslationToLanguage = hy,},                    // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Armenia", TranslationToLanguage = id,},                    // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "armenska", TranslationToLanguage = isLanguage,},           // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "armeno", TranslationToLanguage = it,},                     // #30 Language = "Italian"
//    //                new LanguageName{ Text = "アルメニア語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "სომხეთის", TranslationToLanguage = ka,},                  // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಅರ್ಮೇನಿಯನ್", TranslationToLanguage = kn,},                 // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "아르메니아의", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Armenius", TranslationToLanguage = la,},                   // #35 Language = "Latin"
//    //                new LanguageName{ Text = "armėnų", TranslationToLanguage = lt,},                     // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "armēņu", TranslationToLanguage = lv,},                     // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "ерменскиот", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Armenia", TranslationToLanguage = ms,},                    // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Armen", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Armeens", TranslationToLanguage = nl,},                    // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "armensk", TranslationToLanguage = no,},                    // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "Ormianin", TranslationToLanguage = pl,},                   // #43 Language = "Polish"
//    //                new LanguageName{ Text = "armênio", TranslationToLanguage = pt,},                    // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "armean", TranslationToLanguage = ro,},                     // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "армянский", TranslationToLanguage = ru,},                  // #46 Language = "Russian"
//    //                new LanguageName{ Text = "arménsky", TranslationToLanguage = sk,},                   // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "armenski", TranslationToLanguage = sl,},                   // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "armen", TranslationToLanguage = sq,},                      // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "јерменски", TranslationToLanguage = sr,},                  // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "armeniska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "muarmeni", TranslationToLanguage = sw,},                   // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஆர்மீனியன்", TranslationToLanguage = ta,},               // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "అర్మేనియా దేశస్తుడు", TranslationToLanguage = te,},            // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "อาร์เมเนีย", TranslationToLanguage = th,},                     // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Ermeni", TranslationToLanguage = tr,},                     // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "вірменський", TranslationToLanguage = uk,},                // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "آرمینیا", TranslationToLanguage = ur,},                   // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "tiếng Armenia", TranslationToLanguage = vi,},              // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "ארמאניש", TranslationToLanguage = yi,},                   // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "亚美尼亚", TranslationToLanguage = zh,},                    // #61 Language = "Chinese"
//    //            };



//    //        if (id.Names.Count < 61)
//    //            id.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Indonesian", TranslationToLanguage = en, },                   // #1  Language = "English"
//    //                new LanguageName{ Text = "indonesio", TranslationToLanguage = es,},                     // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Indonesier", TranslationToLanguage = de,},                    // #3  Language = "German"
//    //                new LanguageName{ Text = "الأندونيسية", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Indonesies", TranslationToLanguage = af,},                    // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "İndoneziya", TranslationToLanguage = az,},                    // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "інданезійская", TranslationToLanguage = be,},                 // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "индонезийски", TranslationToLanguage = bg,},                  // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ইন্দোনেশীয়", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "indonesi", TranslationToLanguage = ca,},                      // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "Indonésan", TranslationToLanguage = cs,},                     // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Indonesia", TranslationToLanguage = cy,},                     // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "indonesisk", TranslationToLanguage = da,},                    // #13 Language = "Danish"
//    //                new LanguageName{ Text = "indoneesia", TranslationToLanguage = et,},                    // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Indonesian", TranslationToLanguage = eu,},                    // #15 Language = "Basque"
//    //                new LanguageName{ Text = "اندونزی", TranslationToLanguage = fa,},                      // #16 Language = "Persian"
//    //                new LanguageName{ Text = "indonesialainen", TranslationToLanguage = fi,},               // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "indonésienne", TranslationToLanguage = fr,},                  // #18 Language = "French"
//    //                new LanguageName{ Text = "Indinéisis", TranslationToLanguage = ga,},                    // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Indonesio", TranslationToLanguage = gl,},                     // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ઇન્ડોનેશિયન", TranslationToLanguage = gu,},                     // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "אינדונזית", TranslationToLanguage = he,},                    // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "इंडोनेशिया का या उससे संबद्ध", TranslationToLanguage = hi,},       // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "indonezijski", TranslationToLanguage = hr,},                  // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Endonezyen", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creole"
//    //                new LanguageName{ Text = "indonéz", TranslationToLanguage = hu,},                       // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "ինդոնեզերեն", TranslationToLanguage = hy,},                   // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Bahasa Indonesia", TranslationToLanguage = id,},              // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "indónesísku", TranslationToLanguage = isLanguage,},           // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "indonesiano", TranslationToLanguage = it,},                   // #30 Language = "Italian"
//    //                new LanguageName{ Text = "インドネシア", TranslationToLanguage = ja,},                   // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ინდონეზიის", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಇಂಡೋನೀಷಿಯ ದೇಶಕ್ಕೆ ಸೇರಿದ", TranslationToLanguage = kn,},        // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "인도네시아의", TranslationToLanguage = ko,},                   // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Indonesiaca", TranslationToLanguage = la,},                   // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Indonezijos", TranslationToLanguage = lt,},                   // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Indonēzijas", TranslationToLanguage = lv,},                   // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "индонезиски", TranslationToLanguage = mk,},                   // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Indonesia", TranslationToLanguage = ms,},                     // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Indoneżjan", TranslationToLanguage = mt,},                    // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Indonesisch", TranslationToLanguage = nl,},                   // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "indonesisk", TranslationToLanguage = no,},                    // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "indonezyjski", TranslationToLanguage = pl,},                  // #43 Language = "Polish"
//    //                new LanguageName{ Text = "indonésio", TranslationToLanguage = pt,},                     // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "indoneziană", TranslationToLanguage = ro,},                   // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "индонезийский", TranslationToLanguage = ru,},                 // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Indonézan", TranslationToLanguage = sk,},                     // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "indonezijski", TranslationToLanguage = sl,},                  // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "indonezian", TranslationToLanguage = sq,},                    // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "индонезијски", TranslationToLanguage = sr,},                  // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "indonesiska", TranslationToLanguage = sv,},                   // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiindonesia", TranslationToLanguage = sw,},                   // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "இந்தோனேசிய", TranslationToLanguage = ta,},               // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ఇండోనేషియా", TranslationToLanguage = te,},                     // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ชาวอินโดนีเซีย", TranslationToLanguage = th,},                     // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Endonezyalı", TranslationToLanguage = tr,},                   // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Індонезійська", TranslationToLanguage = uk,},                 // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "انڈونیشی", TranslationToLanguage = ur,},                     // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Indonesia", TranslationToLanguage = vi,},                     // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "אינדאָנעזיש", TranslationToLanguage = yi,},                   // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "印度尼西亚", TranslationToLanguage = zh,},                     // #61 Language = "Chinese"
//    //            };

//    //        if (isLanguage.Names.Count < 61)
//    //            isLanguage.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Icelandic", TranslationToLanguage = en, },                   // #1  Language = "English"
//    //                new LanguageName{ Text = "islandés", TranslationToLanguage = es,},                     // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Isländisch", TranslationToLanguage = de,},                   // #3  Language = "German"
//    //                new LanguageName{ Text = "الأيسلندية", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Yslands", TranslationToLanguage = af,},                      // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "island", TranslationToLanguage = az,},                       // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Ісландская", TranslationToLanguage = be,},                   // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "исландски", TranslationToLanguage = bg,},                    // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "আইস্ল্যাণ্ডের ভাষা", TranslationToLanguage = bn,},                 // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "islandès", TranslationToLanguage = ca,},                     // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "islandský", TranslationToLanguage = cs,},                    // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Islandeg", TranslationToLanguage = cy,},                     // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "islandsk", TranslationToLanguage = da,},                     // #13 Language = "Danish"
//    //                new LanguageName{ Text = "islandi", TranslationToLanguage = et,},                      // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Islandiako", TranslationToLanguage = eu,},                   // #15 Language = "Basque"
//    //                new LanguageName{ Text = "زبان ایسلندی", TranslationToLanguage = fa,},               // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Islannin", TranslationToLanguage = fi,},                     // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "islandais", TranslationToLanguage = fr,},                    // #18 Language = "French"
//    //                new LanguageName{ Text = "Íoslainnis", TranslationToLanguage = ga,},                   // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Islandés", TranslationToLanguage = gl,},                     // #20 Language = "Galician"
//    //                new LanguageName{ Text = "આઇસલેન્ડિક", TranslationToLanguage = gu,},                    // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "איסלנדית", TranslationToLanguage = he,},                    // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "आइसलैंड का", TranslationToLanguage = hi,},                   // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "islandski", TranslationToLanguage = hr,},                    // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "icelandic", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creol
//    //                new LanguageName{ Text = "izlandi", TranslationToLanguage = hu,},                      // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "իսլանդերեն", TranslationToLanguage = hy,},                   // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Islandia", TranslationToLanguage = id,},                     // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Íslenska", TranslationToLanguage = isLanguage,},             // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "islandese", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName{ Text = "アイスランド語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "ისლანდიის", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಐಸ್ಲ್ಯಾಂಡಿಕ್", TranslationToLanguage = kn,},                     // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "아이슬란드의", TranslationToLanguage = ko,},                  // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Islandica", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Islandijos", TranslationToLanguage = lt,},                   // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "islandiešu", TranslationToLanguage = lv,},                   // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "Исланд", TranslationToLanguage = mk,},                       // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Iceland", TranslationToLanguage = ms,},                      // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Iceland", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "IJslands", TranslationToLanguage = nl,},                     // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "islandsk", TranslationToLanguage = no,},                     // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "islandzki", TranslationToLanguage = pl,},                    // #43 Language = "Polish"
//    //                new LanguageName{ Text = "islandês", TranslationToLanguage = pt,},                     // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "islandez", TranslationToLanguage = ro,},                     // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "исландский", TranslationToLanguage = ru,},                   // #46 Language = "Russian"
//    //                new LanguageName{ Text = "islandský", TranslationToLanguage = sk,},                    // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "islandski", TranslationToLanguage = sl,},                    // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "islandez", TranslationToLanguage = sq,},                     // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "исландски", TranslationToLanguage = sr,},                    // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "isländska", TranslationToLanguage = sv,},                    // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kiaislandi", TranslationToLanguage = sw,},                   // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஐஸ்லாந்து", TranslationToLanguage = ta,},                  // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ఐస్లాండిక్", TranslationToLanguage = te,},                       // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ไอซ์แลนด์", TranslationToLanguage = th,},                       // #55 Language = "Thai"
//    //                new LanguageName{ Text = "İzlanda", TranslationToLanguage = tr,},                      // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Ісландська", TranslationToLanguage = uk,},                   // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "آئس لینڈی", TranslationToLanguage = ur,},                   // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "tiếng Iceland", TranslationToLanguage = vi,},                // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "איסלענדיש", TranslationToLanguage = yi,},                   // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "冰岛", TranslationToLanguage = zh,},                         // #61 Language = "Chinese"
//    //            };

//    //        if (it.Names.Count < 61)
//    //            it.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Italian", TranslationToLanguage = en, },                         // #1  Language = "English"
//    //                new LanguageName{ Text = "italiano", TranslationToLanguage = es,},                         // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Italienisch", TranslationToLanguage = de,},                      // #3  Language = "German"
//    //                new LanguageName{ Text = "إيطالي", TranslationToLanguage = ar,},                          // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Italiaanse", TranslationToLanguage = af,},                       // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "italyan", TranslationToLanguage = az,},                          // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Італьянская", TranslationToLanguage = be,},                      // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "италиански", TranslationToLanguage = bg,},                       // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "ইতালিয়", TranslationToLanguage = bn,},                            // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "italià", TranslationToLanguage = ca,},                           // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "Ital", TranslationToLanguage = cs,},                             // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Eidaleg", TranslationToLanguage = cy,},                          // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "italiensk", TranslationToLanguage = da,},                        // #13 Language = "Danish"
//    //                new LanguageName{ Text = "itaalia", TranslationToLanguage = et,},                          // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Italian", TranslationToLanguage = eu,},                          // #15 Language = "Basque"
//    //                new LanguageName{ Text = "ایتالیایی", TranslationToLanguage = fa,},                       // #16 Language = "Persian"
//    //                new LanguageName{ Text = "Italian", TranslationToLanguage = fi,},                          // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "italienne", TranslationToLanguage = fr,},                        // #18 Language = "French"
//    //                new LanguageName{ Text = "Iodáilis", TranslationToLanguage = ga,},                         // #19 Language = "Irish"
//    //                new LanguageName{ Text = "Italiano", TranslationToLanguage = gl,},                         // #20 Language = "Galician"
//    //                new LanguageName{ Text = "ઇટાલીનું", TranslationToLanguage = gu,},                           // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "איטלקית", TranslationToLanguage = he,},                         // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "इतालवी", TranslationToLanguage = hi,},                           // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "talijanski", TranslationToLanguage = hr,},                       // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Italyen", TranslationToLanguage = ht,},                          // #25 Language = "Haitian Creol
//    //                new LanguageName{ Text = "olasz", TranslationToLanguage = hu,},                            // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "իտալերեն", TranslationToLanguage = hy,},                        // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Italia", TranslationToLanguage = id,},                           // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "Ítalska", TranslationToLanguage = isLanguage,},                  // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "italiano", TranslationToLanguage = it,},                         // #30 Language = "Italian"
//    //                new LanguageName{ Text = "イタリア", TranslationToLanguage = ja,},                          // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "იტალიური", TranslationToLanguage = ka,},                       // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಇಟಲಿಯವ", TranslationToLanguage = kn,},                          // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "이탈리아의", TranslationToLanguage = ko,},                        // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Italica", TranslationToLanguage = la,},                          // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Italijos", TranslationToLanguage = lt,},                         // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Itālijas", TranslationToLanguage = lv,},                         // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "италијански", TranslationToLanguage = mk,},                      // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Itali", TranslationToLanguage = ms,},                            // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Taljan", TranslationToLanguage = mt,},                           // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Italiaans", TranslationToLanguage = nl,},                        // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Italian", TranslationToLanguage = no,},                          // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "włoski", TranslationToLanguage = pl,},                           // #43 Language = "Polish"
//    //                new LanguageName{ Text = "italiano", TranslationToLanguage = pt,},                         // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "italian", TranslationToLanguage = ro,},                          // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "итальянский", TranslationToLanguage = ru,},                      // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Talian", TranslationToLanguage = sk,},                           // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "Italijanska", TranslationToLanguage = sl,},                      // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "italian", TranslationToLanguage = sq,},                          // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "италијански", TranslationToLanguage = sr,},                      // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "italienska", TranslationToLanguage = sv,},                       // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Italia", TranslationToLanguage = sw,},                           // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "இத்தாலிய நாட்டை சார்ந்த", TranslationToLanguage = ta,},      // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "ఇటాలియన్", TranslationToLanguage = te,},                         // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "อิตาเลียน", TranslationToLanguage = th,},                           // #55 Language = "Thai"
//    //                new LanguageName{ Text = "İtalyan", TranslationToLanguage = tr,},                          // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Італійська", TranslationToLanguage = uk,},                       // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "اطالوی", TranslationToLanguage = ur,},                          // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Ý", TranslationToLanguage = vi,},                                // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "איטאַליעניש", TranslationToLanguage = yi,},                      // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "意大利", TranslationToLanguage = zh,},                            // #61 Language = "Chinese"
//    //            };


//    //        if (ja.Names.Count < 61)
//    //            ja.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Japanese", TranslationToLanguage = en, },                     // #1  Language = "English"
//    //                new LanguageName{ Text = "japonés", TranslationToLanguage = es,},                       // #2  Language = "Spanish"
//    //                new LanguageName{ Text = "Japaner", TranslationToLanguage = de,},                       // #3  Language = "German"
//    //                new LanguageName{ Text = "اليابانية", TranslationToLanguage = ar,},                    // #4  Language = "Arabic"
//    //                new LanguageName{ Text = "Japannese", TranslationToLanguage = af,},                     // #5  Language = "Afrikaans"
//    //                new LanguageName{ Text = "yapon", TranslationToLanguage = az,},                         // #6  Language = "Azerbaijani"
//    //                new LanguageName{ Text = "Японская", TranslationToLanguage = be,},                      // #7  Language = "Belarusian"
//    //                new LanguageName{ Text = "японски", TranslationToLanguage = bg,},                       // #8  Language = "Bulgarian"
//    //                new LanguageName{ Text = "জাপানি", TranslationToLanguage = bn,},                         // #9  Language = "Bengali"
//    //                new LanguageName{ Text = "japonès", TranslationToLanguage = ca,},                       // #10 Language = "Catalan"
//    //                new LanguageName{ Text = "Japonec", TranslationToLanguage = cs,},                       // #11 Language = "Czech"
//    //                new LanguageName{ Text = "Siapaneaidd", TranslationToLanguage = cy,},                   // #12 Language = "Welsh"
//    //                new LanguageName{ Text = "Japansk", TranslationToLanguage = da,},                       // #13 Language = "Danish"
//    //                new LanguageName{ Text = "jaapani", TranslationToLanguage = et,},                       // #14 Language = "Estonian"
//    //                new LanguageName{ Text = "Japoniako", TranslationToLanguage = eu,},                     // #15 Language = "Basque"
//    //                new LanguageName{ Text = "ژاپنی", TranslationToLanguage = fa,},                        // #16 Language = "Persian"
//    //                new LanguageName{ Text = "japanilainen", TranslationToLanguage = fi,},                  // #17 Language = "Finnish"
//    //                new LanguageName{ Text = "japonaise", TranslationToLanguage = fr,},                     // #18 Language = "French"
//    //                new LanguageName{ Text = "Seapáinis", TranslationToLanguage = ga,},                     // #19 Language = "Irish"
//    //                new LanguageName{ Text = "xaponés", TranslationToLanguage = gl,},                       // #20 Language = "Galician"
//    //                new LanguageName{ Text = "જાપાનીઝ", TranslationToLanguage = gu,},                       // #21 Language = "Gujarati"
//    //                new LanguageName{ Text = "יפנית", TranslationToLanguage = he,},                        // #22 Language = "Hebrew"
//    //                new LanguageName{ Text = "जापानी", TranslationToLanguage = hi,},                         // #23 Language = "Hindi"
//    //                new LanguageName{ Text = "japanski", TranslationToLanguage = hr,},                      // #24 Language = "Croatian"
//    //                new LanguageName{ Text = "Japonè", TranslationToLanguage = ht,},                        // #25 Language = "Haitian Creol
//    //                new LanguageName{ Text = "japán", TranslationToLanguage = hu,},                         // #26 Language = "Hungarian"
//    //                new LanguageName{ Text = "ճապոնացի", TranslationToLanguage = hy,},                    // #27 Language = "Armenian"
//    //                new LanguageName{ Text = "Jepang", TranslationToLanguage = id,},                        // #28 Language = "Indonesian"
//    //                new LanguageName{ Text = "japanska", TranslationToLanguage = isLanguage,},              // #29 Language = "Icelandic"
//    //                new LanguageName{ Text = "giapponese", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName{ Text = "日本", TranslationToLanguage = ja,},                          // #31 Language = "Japanese"
//    //                new LanguageName{ Text = "იაპონური", TranslationToLanguage = ka,},                     // #32 Language = "Georgian"
//    //                new LanguageName{ Text = "ಜಪಾನಿನವನು", TranslationToLanguage = kn,},                     // #33 Language = "Kannada"
//    //                new LanguageName{ Text = "일본어", TranslationToLanguage = ko,},                        // #34 Language = "Korean"
//    //                new LanguageName{ Text = "Iaponica", TranslationToLanguage = la,},                      // #35 Language = "Latin"
//    //                new LanguageName{ Text = "Japonijos", TranslationToLanguage = lt,},                     // #36 Language = "Lithuanian"
//    //                new LanguageName{ Text = "Japānas", TranslationToLanguage = lv,},                       // #37 Language = "Latvian"
//    //                new LanguageName{ Text = "јапонски", TranslationToLanguage = mk,},                      // #38 Language = "Macedonian"
//    //                new LanguageName{ Text = "Jepun", TranslationToLanguage = ms,},                         // #39 Language = "Malay"
//    //                new LanguageName{ Text = "Ġappuniż", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName{ Text = "Japans", TranslationToLanguage = nl,},                        // #41 Language = "Dutch"
//    //                new LanguageName{ Text = "Japanese", TranslationToLanguage = no,},                      // #42 Language = "Norwegian"
//    //                new LanguageName{ Text = "japoński", TranslationToLanguage = pl,},                      // #43 Language = "Polish"
//    //                new LanguageName{ Text = "japonês", TranslationToLanguage = pt,},                       // #44 Language = "Portuguese"
//    //                new LanguageName{ Text = "japonez", TranslationToLanguage = ro,},                       // #45 Language = "Romanian"
//    //                new LanguageName{ Text = "японский", TranslationToLanguage = ru,},                      // #46 Language = "Russian"
//    //                new LanguageName{ Text = "Japonec", TranslationToLanguage = sk,},                       // #47 Language = "Slovak"
//    //                new LanguageName{ Text = "japonski", TranslationToLanguage = sl,},                      // #48 Language = "Slovenian"
//    //                new LanguageName{ Text = "japonisht", TranslationToLanguage = sq,},                     // #49 Language = "Albanian"
//    //                new LanguageName{ Text = "јапански", TranslationToLanguage = sr,},                      // #50 Language = "Serbian"
//    //                new LanguageName{ Text = "Japanska", TranslationToLanguage = sv,},                      // #51 Language = "Swedish"
//    //                new LanguageName{ Text = "Kijapani", TranslationToLanguage = sw,},                      // #52 Language = "Swahili"
//    //                new LanguageName{ Text = "ஜப்பனீஸ்", TranslationToLanguage = ta,},                    // #53 Language = "Tamil"
//    //                new LanguageName{ Text = "జాపనీస్", TranslationToLanguage = te,},                        // #54 Language = "Telugu"
//    //                new LanguageName{ Text = "ภาษาญี่ปุ่น", TranslationToLanguage = th,},                       // #55 Language = "Thai"
//    //                new LanguageName{ Text = "Japon", TranslationToLanguage = tr,},                         // #56 Language = "Turkish"
//    //                new LanguageName{ Text = "Японський", TranslationToLanguage = uk,},                     // #57 Language = "Ukrainian"
//    //                new LanguageName{ Text = "جاپانی", TranslationToLanguage = ur,},                       // #58 Language = "Urdu"
//    //                new LanguageName{ Text = "Nhật Bản", TranslationToLanguage = vi,},                      // #59 Language = "Vietnamese"
//    //                new LanguageName{ Text = "יאַפּאַניש", TranslationToLanguage = yi,},                      // #60 Language = "Yiddish"
//    //                new LanguageName{ Text = "日本", TranslationToLanguage = zh,},                          // #61 Language = "Chinese"
//    //            };

//    //        if (ka.Names.Count < 61)
//    //            ka.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Georgian", TranslationToLanguage = en,},                       // #1  Language = "English"
//    //                new LanguageName {Text = "georgiano", TranslationToLanguage = es,},                      // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Georgier", TranslationToLanguage = de,},                       // #3  Language = "German"
//    //                new LanguageName {Text = "الجورجية", TranslationToLanguage = ar,},                      // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Georgiaans", TranslationToLanguage = af,},                     // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "gürcü", TranslationToLanguage = az,},                          // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "грузінскі", TranslationToLanguage = be,},                      // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "грузински", TranslationToLanguage = bg,},                      // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "জর্জিয়াদেশীয়", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName {Text = "georgià", TranslationToLanguage = ca,},                        // #10 Language = "Catalan"
//    //                new LanguageName {Text = "gruzínský", TranslationToLanguage = cs,},                      // #11 Language = "Czech"
//    //                new LanguageName {Text = "Georgaidd", TranslationToLanguage = cy,},                      // #12 Language = "Welsh"
//    //                new LanguageName {Text = "georgisk", TranslationToLanguage = da,},                       // #13 Language = "Danish"
//    //                new LanguageName {Text = "gruusia", TranslationToLanguage = et,},                        // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Georgian", TranslationToLanguage = eu,},                       // #15 Language = "Basque"
//    //                new LanguageName {Text = "گرجی", TranslationToLanguage = fa,},                          // #16 Language = "Persian"
//    //                new LanguageName {Text = "georgialainen", TranslationToLanguage = fi,},                  // #17 Language = "Finnish"
//    //                new LanguageName {Text = "géorgienne", TranslationToLanguage = fr,},                     // #18 Language = "French"
//    //                new LanguageName {Text = "Seoirsis", TranslationToLanguage = ga,},                       // #19 Language = "Irish"
//    //                new LanguageName {Text = "xeorxiano", TranslationToLanguage = gl,},                      // #20 Language = "Galician"
//    //                new LanguageName {Text = "જ્યોર્જિયન", TranslationToLanguage = gu,},                       // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "גאורגיה", TranslationToLanguage = he,},                       // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "जार्जकालीन", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName {Text = "gruzijski", TranslationToLanguage = hr,},                      // #24 Language = "Croatian"
//    //                new LanguageName {Text = "georgian", TranslationToLanguage = ht,},                       // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "grúz", TranslationToLanguage = hu,},                           // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "վրացերեն", TranslationToLanguage = hy,},                      // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Georgia", TranslationToLanguage = id,},                        // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Georgian", TranslationToLanguage = isLanguage,},               // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "georgiano", TranslationToLanguage = it,},                      // #30 Language = "Italian"
//    //                new LanguageName {Text = "ジョージアン", TranslationToLanguage = ja,},                    // #31 Language = "Japanese"
//    //                new LanguageName {Text = "საქართველოს", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಜಾರ್ಜಿಯನ್", TranslationToLanguage = kn,},                       // #33 Language = "Kannada"
//    //                new LanguageName {Text = "그루지야의", TranslationToLanguage = ko,},                      // #34 Language = "Korean"
//    //                new LanguageName {Text = "Pontica", TranslationToLanguage = la,},                        // #35 Language = "Latin"
//    //                new LanguageName {Text = "gruzinų", TranslationToLanguage = lt,},                        // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "gruzīnu", TranslationToLanguage = lv,},                        // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Грузија", TranslationToLanguage = mk,},                        // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Georgia", TranslationToLanguage = ms,},                        // #39 Language = "Malay"
//    //                new LanguageName {Text = "Ġorġjan", TranslationToLanguage = mt,},                        // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Georgisch", TranslationToLanguage = nl,},                      // #41 Language = "Dutch"
//    //                new LanguageName {Text = "georgisk", TranslationToLanguage = no,},                       // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "gruziński", TranslationToLanguage = pl,},                      // #43 Language = "Polish"
//    //                new LanguageName {Text = "georgiano", TranslationToLanguage = pt,},                      // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "georgian", TranslationToLanguage = ro,},                       // #45 Language = "Romanian"
//    //                new LanguageName {Text = "грузинский", TranslationToLanguage = ru,},                     // #46 Language = "Russian"
//    //                new LanguageName {Text = "gruzínsky", TranslationToLanguage = sk,},                      // #47 Language = "Slovak"
//    //                new LanguageName {Text = "gruzijski", TranslationToLanguage = sl,},                      // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "gjeorgjian", TranslationToLanguage = sq,},                     // #49 Language = "Albanian"
//    //                new LanguageName {Text = "грузијски", TranslationToLanguage = sr,},                      // #50 Language = "Serbian"
//    //                new LanguageName {Text = "georgiska", TranslationToLanguage = sv,},                      // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kijiojia", TranslationToLanguage = sw,},                       // #52 Language = "Swahili"
//    //                new LanguageName {Text = "ஜோர்ஜிய", TranslationToLanguage = ta,},                     // #53 Language = "Tamil"
//    //                new LanguageName {Text = "జార్జియన్", TranslationToLanguage = te,},                         // #54 Language = "Telugu"
//    //                new LanguageName {Text = "จอร์เจีย", TranslationToLanguage = th,},                          // #55 Language = "Thai"
//    //                new LanguageName {Text = "Gürcü", TranslationToLanguage = tr,},                          // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Грузинський", TranslationToLanguage = uk,},                    // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "جارجیا", TranslationToLanguage = ur,},                        // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Georgia", TranslationToLanguage = vi,},                        // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "גרוזיניש", TranslationToLanguage = yi,},                      // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "格鲁吉亚", TranslationToLanguage = zh,},                        // #61 Language = "Chinese"
//    //            };

//    //        if (kn.Names.Count < 61)
//    //            kn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = en,},                // #1  Language = "English"
//    //                new LanguageName {Text = "Canarés", TranslationToLanguage = es,},                // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = de,},                // #3  Language = "German"
//    //                new LanguageName {Text = "الكانادا", TranslationToLanguage = ar,},              // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = af,},                // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = az,},                // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "каннада", TranslationToLanguage = be,},                // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "каннада", TranslationToLanguage = bg,},                // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "কন্নড", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = ca,},                // #10 Language = "Catalan"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = cs,},                // #11 Language = "Czech"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = cy,},                // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = da,},                // #13 Language = "Danish"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = et,},                // #14 Language = "Estonian"
//    //                new LanguageName {Text = "kannadera", TranslationToLanguage = eu,},              // #15 Language = "Basque"
//    //                new LanguageName {Text = "کانادهای", TranslationToLanguage = fa,},              // #16 Language = "Persian"
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = fr,},                // #18 Language = "French"
//    //                new LanguageName {Text = "Cannadais", TranslationToLanguage = ga,},              // #19 Language = "Irish"
//    //                new LanguageName {Text = "Canara", TranslationToLanguage = gl,},                 // #20 Language = "Galician"
//    //                new LanguageName {Text = "કન્નડા", TranslationToLanguage = gu,},                   // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "קנאדה", TranslationToLanguage = he,},                 // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "कन्नड़", TranslationToLanguage = hi,},                  // #23 Language = "Hindi"
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = hr,},                // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = ht,},                // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = hu,},                // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "կաննադա", TranslationToLanguage = hy,},              // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = id,},                // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = isLanguage,},        // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = it,},                // #30 Language = "Italian"
//    //                new LanguageName {Text = "カンナダ語", TranslationToLanguage = ja,},              // #31 Language = "Japanese"
//    //                new LanguageName {Text = "კანადა", TranslationToLanguage = ka,},                 // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಕನ್ನಡ", TranslationToLanguage = kn,},                   // #33 Language = "Kannada"
//    //                new LanguageName {Text = "칸나다어", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Kannadica", TranslationToLanguage = la,},              // #35 Language = "Latin"
//    //                new LanguageName {Text = "kanadų", TranslationToLanguage = lt,},                 // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = lv,},                // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Канада", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = ms,},                // #39 Language = "Malay"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = mt,},                // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = nl,},                // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = no,},                // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = pl,},                // #43 Language = "Polish"
//    //                new LanguageName {Text = "canará", TranslationToLanguage = pt,},                 // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = ro,},                // #45 Language = "Romanian"
//    //                new LanguageName {Text = "каннада", TranslationToLanguage = ru,},                // #46 Language = "Russian"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = sk,},                // #47 Language = "Slovak"
//    //                new LanguageName {Text = "kannada", TranslationToLanguage = sl,},                // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = sq,},                // #49 Language = "Albanian"
//    //                new LanguageName {Text = "каннада", TranslationToLanguage = sr,},                // #50 Language = "Serbian"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = sv,},                // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName {Text = "கன்னடம்", TranslationToLanguage = ta,},              // #53 Language = "Tamil"
//    //                new LanguageName {Text = "కన్నడ", TranslationToLanguage = te,},                  // #54 Language = "Telugu"
//    //                new LanguageName {Text = "กันนาดา", TranslationToLanguage = th,},                  // #55 Language = "Thai"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = tr,},                // #56 Language = "Turkish"
//    //                new LanguageName {Text = "каннада", TranslationToLanguage = uk,},                // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "کناڈا", TranslationToLanguage = ur,},                 // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Kannada", TranslationToLanguage = vi,},                // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "קאַנאַדאַ", TranslationToLanguage = yi,},                // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "卡纳达语", TranslationToLanguage = zh,},                // #61 Language = "Chinese"
//    //            };

//    //        if (ko.Names.Count < 61)
//    //            ko.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Korean", TranslationToLanguage = en,},                                  // #1  Language = "English"
//    //                new LanguageName {Text = "coreano", TranslationToLanguage = es,},                                 // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Koreanisch", TranslationToLanguage = de,},                              // #3  Language = "German"
//    //                new LanguageName {Text = "كوري", TranslationToLanguage = ar,},                                   // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Koreaanse", TranslationToLanguage = af,},                               // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Koreya", TranslationToLanguage = az,},                                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Карэйская", TranslationToLanguage = be,},                               // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "корейски", TranslationToLanguage = bg,},                                // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "কোরিয়ান", TranslationToLanguage = bn,},                                 // #9  Language = "Bengali"
//    //                new LanguageName {Text = "coreà", TranslationToLanguage = ca,},                                   // #10 Language = "Catalan"
//    //                new LanguageName {Text = "korejský", TranslationToLanguage = cs,},                                // #11 Language = "Czech"
//    //                new LanguageName {Text = "Corea", TranslationToLanguage = cy,},                                   // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Korean", TranslationToLanguage = da,},                                  // #13 Language = "Danish"
//    //                new LanguageName {Text = "korea", TranslationToLanguage = et,},                                   // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Korean", TranslationToLanguage = eu,},                                  // #15 Language = "Basque"
//    //                new LanguageName {Text = "کره ای", TranslationToLanguage = fa,},                                 // #16 Language = "Persian"
//    //                new LanguageName {Text = "Korean", TranslationToLanguage = fi,},                                  // #17 Language = "Finnish"
//    //                new LanguageName {Text = "coréenne", TranslationToLanguage = fr,},                                // #18 Language = "French"
//    //                new LanguageName {Text = "Cóiréis", TranslationToLanguage = ga,},                                 // #19 Language = "Irish"
//    //                new LanguageName {Text = "Coreano", TranslationToLanguage = gl,},                                 // #20 Language = "Galician"
//    //                new LanguageName {Text = "કોરિયન", TranslationToLanguage = gu,},                                  // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "קוריאה", TranslationToLanguage = he,},                                 // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "कोरियाई", TranslationToLanguage = hi,},                                  // #23 Language = "Hindi"
//    //                new LanguageName {Text = "korejski", TranslationToLanguage = hr,},                                // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Kore", TranslationToLanguage = ht,},                                    // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "koreai", TranslationToLanguage = hu,},                                  // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "կորեական", TranslationToLanguage = hy,},                               // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Korea", TranslationToLanguage = id,},                                   // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Kóreumaður", TranslationToLanguage = isLanguage,},                      // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "coreano", TranslationToLanguage = it,},                                 // #30 Language = "Italian"
//    //                new LanguageName {Text = "韓国", TranslationToLanguage = ja,},                                    // #31 Language = "Japanese"
//    //                new LanguageName {Text = "კორეის", TranslationToLanguage = ka,},                                  // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಕೊರಿಯಾ ದೇಶದವನು ಯಾ ಆ ದೇಶದ ಭಾಷೆ", TranslationToLanguage = kn,},        // #33 Language = "Kannada"
//    //                new LanguageName {Text = "한국의", TranslationToLanguage = ko,},                                  // #34 Language = "Korean"
//    //                new LanguageName {Text = "Lorem", TranslationToLanguage = la,},                                   // #35 Language = "Latin"
//    //                new LanguageName {Text = "korėjiečių", TranslationToLanguage = lt,},                              // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "korejiešu", TranslationToLanguage = lv,},                               // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Кореја", TranslationToLanguage = mk,},                                  // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Korea", TranslationToLanguage = ms,},                                   // #39 Language = "Malay"
//    //                new LanguageName {Text = "Koreana", TranslationToLanguage = mt,},                                 // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Koreaans", TranslationToLanguage = nl,},                                // #41 Language = "Dutch"
//    //                new LanguageName {Text = "koreansk", TranslationToLanguage = no,},                                // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "koreański", TranslationToLanguage = pl,},                               // #43 Language = "Polish"
//    //                new LanguageName {Text = "coreano", TranslationToLanguage = pt,},                                 // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "coreean", TranslationToLanguage = ro,},                                 // #45 Language = "Romanian"
//    //                new LanguageName {Text = "корейский", TranslationToLanguage = ru,},                               // #46 Language = "Russian"
//    //                new LanguageName {Text = "kórejský", TranslationToLanguage = sk,},                                // #47 Language = "Slovak"
//    //                new LanguageName {Text = "Korean", TranslationToLanguage = sl,},                                  // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "korean", TranslationToLanguage = sq,},                                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "корејски", TranslationToLanguage = sr,},                                // #50 Language = "Serbian"
//    //                new LanguageName {Text = "koreanska", TranslationToLanguage = sv,},                               // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kikorea", TranslationToLanguage = sw,},                                 // #52 Language = "Swahili"
//    //                new LanguageName {Text = "கொரிய", TranslationToLanguage = ta,},                                // #53 Language = "Tamil"
//    //                new LanguageName {Text = "కొరియా", TranslationToLanguage = te,},                                   // #54 Language = "Telugu"
//    //                new LanguageName {Text = "เกาหลี", TranslationToLanguage = th,},                                    // #55 Language = "Thai"
//    //                new LanguageName {Text = "Kore", TranslationToLanguage = tr,},                                    // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Корейська", TranslationToLanguage = uk,},                               // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "کورین", TranslationToLanguage = ur,},                                  // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Hàn Quốc", TranslationToLanguage = vi,},                                // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "קאָרעיִש", TranslationToLanguage = yi,},                                 // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "韩国", TranslationToLanguage = zh,},                                    // #61 Language = "Chinese"
//    //            };

//    //        if (la.Names.Count < 61)
//    //            la.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = en,},                       // #1  Language = "English"
//    //                new LanguageName {Text = "latino", TranslationToLanguage = es,},                      // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Latein", TranslationToLanguage = de,},                      // #3  Language = "German"
//    //                new LanguageName {Text = "لاتينية", TranslationToLanguage = ar,},                     // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Latyns-", TranslationToLanguage = af,},                     // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "latın", TranslationToLanguage = az,},                       // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "лацінскі", TranslationToLanguage = be,},                    // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "латински", TranslationToLanguage = bg,},                    // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "ল্যাটিন", TranslationToLanguage = bn,},                       // #9  Language = "Bengali"
//    //                new LanguageName {Text = "llatí", TranslationToLanguage = ca,},                       // #10 Language = "Catalan"
//    //                new LanguageName {Text = "latina", TranslationToLanguage = cs,},                      // #11 Language = "Czech"
//    //                new LanguageName {Text = "Lladin", TranslationToLanguage = cy,},                      // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = da,},                       // #13 Language = "Danish"
//    //                new LanguageName {Text = "ladina", TranslationToLanguage = et,},                      // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = eu,},                       // #15 Language = "Basque"
//    //                new LanguageName {Text = "لاتین", TranslationToLanguage = fa,},                       // #16 Language = "Persian"
//    //                new LanguageName {Text = "Latinalaisen", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName {Text = "latine", TranslationToLanguage = fr,},                      // #18 Language = "French"
//    //                new LanguageName {Text = "لاتین", TranslationToLanguage = ga,},                       // #19 Language = "Irish"
//    //                new LanguageName {Text = "latino", TranslationToLanguage = gl,},                      // #20 Language = "Galician"
//    //                new LanguageName {Text = "લેટિન", TranslationToLanguage = gu,},                        // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "לטינית", TranslationToLanguage = he,},                     // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "लातौनी", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName {Text = "latinski", TranslationToLanguage = hr,},                    // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Latin lan", TranslationToLanguage = ht,},                   // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "latin", TranslationToLanguage = hu,},                       // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "լատիներեն", TranslationToLanguage = hy,},                  // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = id,},                       // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = isLanguage,},               // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "latino", TranslationToLanguage = it,},                      // #30 Language = "Italian"
//    //                new LanguageName {Text = "ラテン", TranslationToLanguage = ja,},                      // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ლათინური", TranslationToLanguage = ka,},                  // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಪ್ರಾಚೀನ ರೋಮನರ", TranslationToLanguage = kn,},              // #33 Language = "Kannada"
//    //                new LanguageName {Text = "라틴어", TranslationToLanguage = ko,},                      // #34 Language = "Korean"
//    //                new LanguageName {Text = "latine", TranslationToLanguage = la,},                      // #35 Language = "Latin"
//    //                new LanguageName {Text = "lotynų", TranslationToLanguage = lt,},                      // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "latīņu", TranslationToLanguage = lv,},                      // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Латинска", TranslationToLanguage = mk,},                    // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = ms,},                       // #39 Language = "Malay"
//    //                new LanguageName {Text = "latin", TranslationToLanguage = mt,},                       // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Latijn", TranslationToLanguage = nl,},                      // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = no,},                       // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "łacina", TranslationToLanguage = pl,},                      // #43 Language = "Polish"
//    //                new LanguageName {Text = "latino", TranslationToLanguage = pt,},                      // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "latin", TranslationToLanguage = ro,},                       // #45 Language = "Romanian"
//    //                new LanguageName {Text = "латинский", TranslationToLanguage = ru,},                   // #46 Language = "Russian"
//    //                new LanguageName {Text = "latinčina", TranslationToLanguage = sk,},                   // #47 Language = "Slovak"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = sl,},                       // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "latinisht", TranslationToLanguage = sq,},                   // #49 Language = "Albanian"
//    //                new LanguageName {Text = "латински", TranslationToLanguage = sr,},                    // #50 Language = "Serbian"
//    //                new LanguageName {Text = "latinska", TranslationToLanguage = sv,},                    // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kilatini", TranslationToLanguage = sw,},                    // #52 Language = "Swahili"
//    //                new LanguageName {Text = "இலத்தீன் மொழி", TranslationToLanguage = ta,},           // #53 Language = "Tamil"
//    //                new LanguageName {Text = "లాటిన్", TranslationToLanguage = te,},                        // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ละติน", TranslationToLanguage = th,},                        // #55 Language = "Thai"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = tr,},                       // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Латинську", TranslationToLanguage = uk,},                   // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "لاطینی", TranslationToLanguage = ur,},                      // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Latin", TranslationToLanguage = vi,},                       // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "לאַטייַן", TranslationToLanguage = yi,},                     // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "拉丁美洲", TranslationToLanguage = zh,},                     // #61 Language = "Chinese"
//    //            };

//    //        if (lt.Names.Count < 61)
//    //            lt.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Lithuanian", TranslationToLanguage = en,},                                    // #1  Language = "English"
//    //                new LanguageName {Text = "lituano", TranslationToLanguage = es,},                                       // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Litauisch", TranslationToLanguage = de,},                                     // #3  Language = "German"
//    //                new LanguageName {Text = "لتواني", TranslationToLanguage = ar,},                                       // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Litaus", TranslationToLanguage = af,},                                        // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Litva", TranslationToLanguage = az,},                                         // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Літоўскі", TranslationToLanguage = be,},                                      // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "литовски", TranslationToLanguage = bg,},                                      // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "লিথুনিয়ান", TranslationToLanguage = bn,},                                       // #9  Language = "Bengali"
//    //                new LanguageName {Text = "lituà", TranslationToLanguage = ca,},                                         // #10 Language = "Catalan"
//    //                new LanguageName {Text = "litevský", TranslationToLanguage = cs,},                                      // #11 Language = "Czech"
//    //                new LanguageName {Text = "Lithwaneg", TranslationToLanguage = cy,},                                     // #12 Language = "Welsh"
//    //                new LanguageName {Text = "litauisk", TranslationToLanguage = da,},                                      // #13 Language = "Danish"
//    //                new LanguageName {Text = "leedu", TranslationToLanguage = et,},                                         // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Lithuaniera", TranslationToLanguage = eu,},                                   // #15 Language = "Basque"
//    //                new LanguageName {Text = "زبان لیتوانی", TranslationToLanguage = fa,},                                // #16 Language = "Persian"
//    //                new LanguageName {Text = "Liettuan", TranslationToLanguage = fi,},                                      // #17 Language = "Finnish"
//    //                new LanguageName {Text = "Lituanie", TranslationToLanguage = fr,},                                      // #18 Language = "French"
//    //                new LanguageName {Text = "Liotuáinis", TranslationToLanguage = ga,},                                    // #19 Language = "Irish"
//    //                new LanguageName {Text = "Lituano", TranslationToLanguage = gl,},                                       // #20 Language = "Galician"
//    //                new LanguageName {Text = "લિથુનિયન", TranslationToLanguage = gu,},                                      // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "ליטא", TranslationToLanguage = he,},                                         // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "लिथुअनिअन की भाषा", TranslationToLanguage = hi,},                             // #23 Language = "Hindi"
//    //                new LanguageName {Text = "litvanski", TranslationToLanguage = hr,},                                     // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Haitian", TranslationToLanguage = ht,},                                       // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "litván", TranslationToLanguage = hu,},                                        // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "լիտվական", TranslationToLanguage = hy,},                                     // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Lithuania", TranslationToLanguage = id,},                                     // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Lithuanian", TranslationToLanguage = isLanguage,},                            // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "lituano", TranslationToLanguage = it,},                                       // #30 Language = "Italian"
//    //                new LanguageName {Text = "リトアニア語", TranslationToLanguage = ja,},                                   // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ლიტვის", TranslationToLanguage = ka,},                                       // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ರಷ್ಯಾದ ಬಾಲ್ಟಿಕ್ ಗಣರಾಜ್ಯವಾದ ಲಿಥುಯೇನಿಯದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguage = kn,},   // #33 Language = "Kannada"
//    //                new LanguageName {Text = "리투아니아의", TranslationToLanguage = ko,},                                   // #34 Language = "Korean"
//    //                new LanguageName {Text = "Lithuaniae", TranslationToLanguage = la,},                                    // #35 Language = "Latin"
//    //                new LanguageName {Text = "Lietuvos", TranslationToLanguage = lt,},                                      // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Lietuvas", TranslationToLanguage = lv,},                                      // #37 Language = "Latvian"
//    //                new LanguageName {Text = "литвански", TranslationToLanguage = mk,},                                     // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Bahasa Lithuania", TranslationToLanguage = ms,},                              // #39 Language = "Malay"
//    //                new LanguageName {Text = "Litwana", TranslationToLanguage = mt,},                                       // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Litouws", TranslationToLanguage = nl,},                                       // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Lithuanian", TranslationToLanguage = no,},                                    // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "litewski", TranslationToLanguage = pl,},                                      // #43 Language = "Polish"
//    //                new LanguageName {Text = "lituano", TranslationToLanguage = pt,},                                       // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "limba lituaniană", TranslationToLanguage = ro,},                              // #45 Language = "Romanian"
//    //                new LanguageName {Text = "литовский", TranslationToLanguage = ru,},                                     // #46 Language = "Russian"
//    //                new LanguageName {Text = "litovský", TranslationToLanguage = sk,},                                      // #47 Language = "Slovak"
//    //                new LanguageName {Text = "litovski", TranslationToLanguage = sl,},                                      // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "lituanisht", TranslationToLanguage = sq,},                                    // #49 Language = "Albanian"
//    //                new LanguageName {Text = "литвански", TranslationToLanguage = sr,},                                     // #50 Language = "Serbian"
//    //                new LanguageName {Text = "litauiska", TranslationToLanguage = sv,},                                     // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kilithuania", TranslationToLanguage = sw,},                                   // #52 Language = "Swahili"
//    //                new LanguageName {Text = "லிதுயேனியன்", TranslationToLanguage = ta,},                                // #53 Language = "Tamil"
//    //                new LanguageName {Text = "లిథువేనియన్", TranslationToLanguage = te,},                                     // #54 Language = "Telugu"
//    //                new LanguageName {Text = "เกี่ยวกับประเทศลิธัวเนีย", TranslationToLanguage = th,},                               // #55 Language = "Thai"
//    //                new LanguageName {Text = "Litvanya", TranslationToLanguage = tr,},                                      // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Литовська", TranslationToLanguage = uk,},                                     // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "لتھواینین", TranslationToLanguage = ur,},                                    // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Lithuania", TranslationToLanguage = vi,},                                     // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "ליטוויש", TranslationToLanguage = yi,},                                      // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "立陶宛", TranslationToLanguage = zh,},                                         // #61 Language = "Chinese"
//    //            };

//    //        if (lv.Names.Count < 61)
//    //            lv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Latvian", TranslationToLanguage = en,},                                          // #1  Language = "English"
//    //                new LanguageName {Text = "lituano", TranslationToLanguage = es,},                                          // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Litauisch", TranslationToLanguage = de,},                                        // #3  Language = "German"
//    //                new LanguageName {Text = "لتواني", TranslationToLanguage = ar,},                                          // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Litaus", TranslationToLanguage = af,},                                           // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Litva", TranslationToLanguage = az,},                                            // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Літоўскі", TranslationToLanguage = be,},                                         // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "литовски", TranslationToLanguage = bg,},                                         // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "লিথুনিয়ান", TranslationToLanguage = bn,},                                          // #9  Language = "Bengali"
//    //                new LanguageName {Text = "lituà", TranslationToLanguage = ca,},                                            // #10 Language = "Catalan"
//    //                new LanguageName {Text = "litevský", TranslationToLanguage = cs,},                                         // #11 Language = "Czech"
//    //                new LanguageName {Text = "Lithwaneg", TranslationToLanguage = cy,},                                        // #12 Language = "Welsh"
//    //                new LanguageName {Text = "litauisk", TranslationToLanguage = da,},                                         // #13 Language = "Danish"
//    //                new LanguageName {Text = "leedu", TranslationToLanguage = et,},                                            // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Lithuaniera", TranslationToLanguage = eu,},                                      // #15 Language = "Basque"
//    //                new LanguageName {Text = "زبان لیتوانی", TranslationToLanguage = fa,},                                   // #16 Language = "Persian"
//    //                new LanguageName {Text = "Liettuan", TranslationToLanguage = fi,},                                         // #17 Language = "Finnish"
//    //                new LanguageName {Text = "Lituanie", TranslationToLanguage = fr,},                                         // #18 Language = "French"
//    //                new LanguageName {Text = "Liotuáinis", TranslationToLanguage = ga,},                                       // #19 Language = "Irish"
//    //                new LanguageName {Text = "Lituano", TranslationToLanguage = gl,},                                          // #20 Language = "Galician"
//    //                new LanguageName {Text = "લિથુનિયન", TranslationToLanguage = gu,},                                         // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "ליטא", TranslationToLanguage = he,},                                            // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "लिथुअनिअन की भाषा", TranslationToLanguage = hi,},                                // #23 Language = "Hindi"
//    //                new LanguageName {Text = "litvanski", TranslationToLanguage = hr,},                                        // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Haitian", TranslationToLanguage = ht,},                                          // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "litván", TranslationToLanguage = hu,},                                           // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "լիտվական", TranslationToLanguage = hy,},                                        // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Lithuania", TranslationToLanguage = id,},                                        // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Lithuanian", TranslationToLanguage = isLanguage,},                               // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "lituano", TranslationToLanguage = it,},                                          // #30 Language = "Italian"
//    //                new LanguageName {Text = "リトアニア語", TranslationToLanguage = ja,},                                      // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ლიტვის", TranslationToLanguage = ka,},                                          // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ರಷ್ಯಾದ ಬಾಲ್ಟಿಕ್ ಗಣರಾಜ್ಯವಾದ ಲಿಥುಯೇನಿಯದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguage = kn,},      // #33 Language = "Kannada"
//    //                new LanguageName {Text = "리투아니아의", TranslationToLanguage = ko,},                                      // #34 Language = "Korean"
//    //                new LanguageName {Text = "Lithuaniae", TranslationToLanguage = la,},                                       // #35 Language = "Latin"
//    //                new LanguageName {Text = "Lietuvos", TranslationToLanguage = lt,},                                         // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Lietuvas", TranslationToLanguage = lv,},                                         // #37 Language = "Latvian"
//    //                new LanguageName {Text = "литвански", TranslationToLanguage = mk,},                                        // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Bahasa Lithuania", TranslationToLanguage = ms,},                                 // #39 Language = "Malay"
//    //                new LanguageName {Text = "Litwana", TranslationToLanguage = mt,},                                          // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Litouws", TranslationToLanguage = nl,},                                          // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Lithuanian", TranslationToLanguage = no,},                                       // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "litewski", TranslationToLanguage = pl,},                                         // #43 Language = "Polish"
//    //                new LanguageName {Text = "lituano", TranslationToLanguage = pt,},                                          // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "limba lituaniană", TranslationToLanguage = ro,},                                 // #45 Language = "Romanian"
//    //                new LanguageName {Text = "литовский", TranslationToLanguage = ru,},                                        // #46 Language = "Russian"
//    //                new LanguageName {Text = "litovský", TranslationToLanguage = sk,},                                         // #47 Language = "Slovak"
//    //                new LanguageName {Text = "litovski", TranslationToLanguage = sl,},                                         // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "lituanisht", TranslationToLanguage = sq,},                                       // #49 Language = "Albanian"
//    //                new LanguageName {Text = "литвански", TranslationToLanguage = sr,},                                        // #50 Language = "Serbian"
//    //                new LanguageName {Text = "litauiska", TranslationToLanguage = sv,},                                        // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kilithuania", TranslationToLanguage = sw,},                                      // #52 Language = "Swahili"
//    //                new LanguageName {Text = "லிதுயேனியன்", TranslationToLanguage = ta,},                                   // #53 Language = "Tamil"
//    //                new LanguageName {Text = "లిథువేనియన్", TranslationToLanguage = te,},                                        // #54 Language = "Telugu"
//    //                new LanguageName {Text = "เกี่ยวกับประเทศลิธัวเนีย", TranslationToLanguage = th,},                                  // #55 Language = "Thai"
//    //                new LanguageName {Text = "Litvanya", TranslationToLanguage = tr,},                                         // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Литовська", TranslationToLanguage = uk,},                                        // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "لتھواینین", TranslationToLanguage = ur,},                                       // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Lithuania", TranslationToLanguage = vi,},                                        // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "ליטוויש", TranslationToLanguage = yi,},                                         // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "立陶宛", TranslationToLanguage = zh,},                                            // #61 Language = "Chinese"
//    //            };

//    //        if (mk.Names.Count < 61)
//    //            mk.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Macedonian", TranslationToLanguage = en,},                 // #1  Language = "English"
//    //                new LanguageName {Text = "macedonio", TranslationToLanguage = es,},                  // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Mazedonier", TranslationToLanguage = de,},                 // #3  Language = "German"
//    //                new LanguageName {Text = "المقدونية", TranslationToLanguage = ar,},                 // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Macedonies", TranslationToLanguage = af,},                 // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "makedoniyalı", TranslationToLanguage = az,},               // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "македонскай", TranslationToLanguage = be,},                // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "македонски", TranslationToLanguage = bg,},                 // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "মাসিডনের লোক", TranslationToLanguage = bn,},               // #9  Language = "Bengali"
//    //                new LanguageName {Text = "macedoni", TranslationToLanguage = ca,},                   // #10 Language = "Catalan"
//    //                new LanguageName {Text = "makedonský", TranslationToLanguage = cs,},                 // #11 Language = "Czech"
//    //                new LanguageName {Text = "Macedoneg", TranslationToLanguage = cy,},                  // #12 Language = "Welsh"
//    //                new LanguageName {Text = "makedonsk", TranslationToLanguage = da,},                  // #13 Language = "Danish"
//    //                new LanguageName {Text = "Makedoonia", TranslationToLanguage = et,},                 // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Mazedonierara", TranslationToLanguage = eu,},              // #15 Language = "Basque"
//    //                new LanguageName {Text = "مقدونی", TranslationToLanguage = fa,},                    // #16 Language = "Persian"
//    //                new LanguageName {Text = "Makedonian", TranslationToLanguage = fi,},                 // #17 Language = "Finnish"
//    //                new LanguageName {Text = "macédoniennes", TranslationToLanguage = fr,},              // #18 Language = "French"
//    //                new LanguageName {Text = "Macadóinis", TranslationToLanguage = ga,},                 // #19 Language = "Irish"
//    //                new LanguageName {Text = "Macedonia", TranslationToLanguage = gl,},                  // #20 Language = "Galician"
//    //                new LanguageName {Text = "મેસેડોનિયન", TranslationToLanguage = gu,},                  // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "מקדוניה", TranslationToLanguage = he,},                   // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "मेसीडोनियन", TranslationToLanguage = hi,},                  // #23 Language = "Hindi"
//    //                new LanguageName {Text = "makedonski", TranslationToLanguage = hr,},                 // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Masedonyen", TranslationToLanguage = ht,},                 // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "macedóniai", TranslationToLanguage = hu,},                 // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "մակեդոներեն", TranslationToLanguage = hy,},               // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Macedonia", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "makedónska", TranslationToLanguage = isLanguage,},         // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "macedone", TranslationToLanguage = it,},                   // #30 Language = "Italian"
//    //                new LanguageName {Text = "マケドニア語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName {Text = "მაკედონიის", TranslationToLanguage = ka,},                 // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಮೆಸಿಡೋನಿಯನ್", TranslationToLanguage = kn,},                // #33 Language = "Kannada"
//    //                new LanguageName {Text = "마케도니아의", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Macedonum", TranslationToLanguage = la,},                  // #35 Language = "Latin"
//    //                new LanguageName {Text = "Makedonijos", TranslationToLanguage = lt,},                // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "maķedoniešu", TranslationToLanguage = lv,},                // #37 Language = "Latvian"
//    //                new LanguageName {Text = "македонски", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Macedonia", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName {Text = "Maċedonjan", TranslationToLanguage = mt,},                 // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Macedonisch", TranslationToLanguage = nl,},                // #41 Language = "Dutch"
//    //                new LanguageName {Text = "makedonsk", TranslationToLanguage = no,},                  // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "macedoński", TranslationToLanguage = pl,},                 // #43 Language = "Polish"
//    //                new LanguageName {Text = "macedônia", TranslationToLanguage = pt,},                  // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "macedonean", TranslationToLanguage = ro,},                 // #45 Language = "Romanian"
//    //                new LanguageName {Text = "македонский", TranslationToLanguage = ru,},                // #46 Language = "Russian"
//    //                new LanguageName {Text = "macedónsky", TranslationToLanguage = sk,},                 // #47 Language = "Slovak"
//    //                new LanguageName {Text = "makedonski", TranslationToLanguage = sl,},                 // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "maqedonas", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "македонски", TranslationToLanguage = sr,},                 // #50 Language = "Serbian"
//    //                new LanguageName {Text = "makedonska", TranslationToLanguage = sv,},                 // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kimasedonia", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName {Text = "மாஸிடோனியன்", TranslationToLanguage = ta,},          // #53 Language = "Tamil"
//    //                new LanguageName {Text = "మసడోనియన్", TranslationToLanguage = te,},                 // #54 Language = "Telugu"
//    //                new LanguageName {Text = "มาซิโดเนีย", TranslationToLanguage = th,},                    // #55 Language = "Thai"
//    //                new LanguageName {Text = "Makedonya", TranslationToLanguage = tr,},                  // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Македонський", TranslationToLanguage = uk,},               // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "مقدونيائی", TranslationToLanguage = ur,},                 // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Tiếng Macedonia", TranslationToLanguage = vi,},            // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "מאַקעדאָניש", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "马其顿", TranslationToLanguage = zh,},                      // #61 Language = "Chinese"
//    //            };

//    //        if (ms.Names.Count < 61)
//    //            ms.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = en,},                        // #1  Language = "English"
//    //                new LanguageName {Text = "malayo", TranslationToLanguage = es,},                       // #2  Language = "Spanish"
//    //                new LanguageName {Text = "malaiisch", TranslationToLanguage = de,},                    // #3  Language = "German"
//    //                new LanguageName {Text = "الملايو", TranslationToLanguage = ar,},                      // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Maleis", TranslationToLanguage = af,},                       // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "malay", TranslationToLanguage = az,},                        // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Малайская", TranslationToLanguage = be,},                    // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "малайски", TranslationToLanguage = bg,},                     // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "মালয়দেশীয় লোক", TranslationToLanguage = bn,},                 // #9  Language = "Bengali"
//    //                new LanguageName {Text = "malai", TranslationToLanguage = ca,},                        // #10 Language = "Catalan"
//    //                new LanguageName {Text = "Malajská", TranslationToLanguage = cs,},                     // #11 Language = "Czech"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = cy,},                        // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = da,},                        // #13 Language = "Danish"
//    //                new LanguageName {Text = "malai", TranslationToLanguage = et,},                        // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Malaysiera", TranslationToLanguage = eu,},                   // #15 Language = "Basque"
//    //                new LanguageName {Text = "مالایا", TranslationToLanguage = fa,},                       // #16 Language = "Persian"
//    //                new LanguageName {Text = "malaiji", TranslationToLanguage = fi,},                      // #17 Language = "Finnish"
//    //                new LanguageName {Text = "malais", TranslationToLanguage = fr,},                       // #18 Language = "French"
//    //                new LanguageName {Text = "Malaeis", TranslationToLanguage = ga,},                      // #19 Language = "Irish"
//    //                new LanguageName {Text = "malaio", TranslationToLanguage = gl,},                       // #20 Language = "Galician"
//    //                new LanguageName {Text = "મલય", TranslationToLanguage = gu,},                         // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "מלאית", TranslationToLanguage = he,},                       // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "मलायी", TranslationToLanguage = hi,},                        // #23 Language = "Hindi"
//    //                new LanguageName {Text = "malajski", TranslationToLanguage = hr,},                     // #24 Language = "Croatian"
//    //                new LanguageName {Text = "malay", TranslationToLanguage = ht,},                        // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "maláj", TranslationToLanguage = hu,},                        // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "մալայական", TranslationToLanguage = hy,},                   // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Melayu", TranslationToLanguage = id,},                       // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = isLanguage,},                // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "malese", TranslationToLanguage = it,},                       // #30 Language = "Italian"
//    //                new LanguageName {Text = "マレー語", TranslationToLanguage = ja,},                      // #31 Language = "Japanese"
//    //                new LanguageName {Text = "მალაური", TranslationToLanguage = ka,},                     // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಮಲಯಾ ಭಾಷೆಯ", TranslationToLanguage = kn,},                // #33 Language = "Kannada"
//    //                new LanguageName {Text = "말레이 사람", TranslationToLanguage = ko,},                   // #34 Language = "Korean"
//    //                new LanguageName {Text = "Malaeorum", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName {Text = "malajų", TranslationToLanguage = lt,},                       // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "malajiešu valoda", TranslationToLanguage = lv,},             // #37 Language = "Latvian"
//    //                new LanguageName {Text = "малајски", TranslationToLanguage = mk,},                     // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Melayu", TranslationToLanguage = ms,},                       // #39 Language = "Malay"
//    //                new LanguageName {Text = "Malajan", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Maleis", TranslationToLanguage = nl,},                       // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = no,},                        // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "malajski", TranslationToLanguage = pl,},                     // #43 Language = "Polish"
//    //                new LanguageName {Text = "malaio", TranslationToLanguage = pt,},                       // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "malaezian", TranslationToLanguage = ro,},                    // #45 Language = "Romanian"
//    //                new LanguageName {Text = "малайский", TranslationToLanguage = ru,},                    // #46 Language = "Russian"
//    //                new LanguageName {Text = "malajzijská", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = sl,},                        // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "malajisht", TranslationToLanguage = sq,},                    // #49 Language = "Albanian"
//    //                new LanguageName {Text = "малајски", TranslationToLanguage = sr,},                     // #50 Language = "Serbian"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = sv,},                        // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = sw,},                        // #52 Language = "Swahili"
//    //                new LanguageName {Text = "மலாய்", TranslationToLanguage = ta,},                      // #53 Language = "Tamil"
//    //                new LanguageName {Text = "మలే", TranslationToLanguage = te,},                          // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ภาษามลายู", TranslationToLanguage = th,},                      // #55 Language = "Thai"
//    //                new LanguageName {Text = "Malay", TranslationToLanguage = tr,},                        // #56 Language = "Turkish"
//    //                new LanguageName {Text = "малайський", TranslationToLanguage = uk,},                   // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "مالائی", TranslationToLanguage = ur,},                       // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Mã Lai", TranslationToLanguage = vi,},                       // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "מאַלייַיש", TranslationToLanguage = yi,},                     // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "马来人", TranslationToLanguage = zh,},                        // #61 Language = "Chinese"
//    //            };

//    //        if (mt.Names.Count < 61)
//    //            mt.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Maltese", TranslationToLanguage = en,},                      // #1  Language = "English"
//    //                new LanguageName {Text = "maltés", TranslationToLanguage = es,},                       // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Malteser", TranslationToLanguage = de,},                     // #3  Language = "German"
//    //                new LanguageName {Text = "المالطية", TranslationToLanguage = ar,},                    // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Maltese", TranslationToLanguage = af,},                      // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Malta", TranslationToLanguage = az,},                        // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "мальтыйская", TranslationToLanguage = be,},                  // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "малтийски", TranslationToLanguage = bg,},                    // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "মালটার ভাষা", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "maltès", TranslationToLanguage = ca,},                       // #10 Language = "Catalan"
//    //                new LanguageName {Text = "maltese", TranslationToLanguage = cs,},                      // #11 Language = "Czech"
//    //                new LanguageName {Text = "Malta", TranslationToLanguage = cy,},                        // #12 Language = "Welsh"
//    //                new LanguageName {Text = "maltesiske", TranslationToLanguage = da,},                   // #13 Language = "Danish"
//    //                new LanguageName {Text = "Malta", TranslationToLanguage = et,},                        // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Maltako", TranslationToLanguage = eu,},                      // #15 Language = "Basque"
//    //                new LanguageName {Text = "اهل مالت", TranslationToLanguage = fa,},                    // #16 Language = "Persian"
//    //                new LanguageName {Text = "Maltan", TranslationToLanguage = fi,},                       // #17 Language = "Finnish"
//    //                new LanguageName {Text = "maltais", TranslationToLanguage = fr,},                      // #18 Language = "French"
//    //                new LanguageName {Text = "Mháltais", TranslationToLanguage = ga,},                     // #19 Language = "Irish"
//    //                new LanguageName {Text = "Maltés", TranslationToLanguage = gl,},                       // #20 Language = "Galician"
//    //                new LanguageName {Text = "માલ્ટી", TranslationToLanguage = gu,},                         // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "מלטה", TranslationToLanguage = he,},                        // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "मोलतिज़", TranslationToLanguage = hi,},                      // #23 Language = "Hindi"
//    //                new LanguageName {Text = "malteški", TranslationToLanguage = hr,},                     // #24 Language = "Croatian"
//    //                new LanguageName {Text = "maltese", TranslationToLanguage = ht,},                      // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "máltai", TranslationToLanguage = hu,},                       // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "մալթացի", TranslationToLanguage = hy,},                     // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Malta", TranslationToLanguage = id,},                        // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "maltneska", TranslationToLanguage = isLanguage,},            // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "maltese", TranslationToLanguage = it,},                      // #30 Language = "Italian"
//    //                new LanguageName {Text = "マルタ語", TranslationToLanguage = ja,},                      // #31 Language = "Japanese"
//    //                new LanguageName {Text = "მალტური", TranslationToLanguage = ka,},                    // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಮಾಲ್ಟಾ ದ್ವೀಪದ ನಿವಾಸಿ ಯಾ ಭಾಷೆ", TranslationToLanguage = kn,},    // #33 Language = "Kannada"
//    //                new LanguageName {Text = "몰티즈", TranslationToLanguage = ko,},                       // #34 Language = "Korean"
//    //                new LanguageName {Text = "Melitica", TranslationToLanguage = la,},                     // #35 Language = "Latin"
//    //                new LanguageName {Text = "Maltos", TranslationToLanguage = lt,},                       // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Maltas", TranslationToLanguage = lv,},                       // #37 Language = "Latvian"
//    //                new LanguageName {Text = "малтешки", TranslationToLanguage = mk,},                     // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Malta", TranslationToLanguage = ms,},                        // #39 Language = "Malay"
//    //                new LanguageName {Text = "Malti", TranslationToLanguage = mt,},                        // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Maltees", TranslationToLanguage = nl,},                      // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Maltese", TranslationToLanguage = no,},                      // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "maltański", TranslationToLanguage = pl,},                    // #43 Language = "Polish"
//    //                new LanguageName {Text = "maltês", TranslationToLanguage = pt,},                       // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "maltez", TranslationToLanguage = ro,},                       // #45 Language = "Romanian"
//    //                new LanguageName {Text = "мальтийский", TranslationToLanguage = ru,},                  // #46 Language = "Russian"
//    //                new LanguageName {Text = "Maltese", TranslationToLanguage = sk,},                      // #47 Language = "Slovak"
//    //                new LanguageName {Text = "malteški", TranslationToLanguage = sl,},                     // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "maltez", TranslationToLanguage = sq,},                       // #49 Language = "Albanian"
//    //                new LanguageName {Text = "малтешки", TranslationToLanguage = sr,},                     // #50 Language = "Serbian"
//    //                new LanguageName {Text = "maltesiska", TranslationToLanguage = sv,},                   // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kimalta", TranslationToLanguage = sw,},                      // #52 Language = "Swahili"
//    //                new LanguageName {Text = "மால்டிஸ்", TranslationToLanguage = ta,},                   // #53 Language = "Tamil"
//    //                new LanguageName {Text = "మాల్టీస్", TranslationToLanguage = te,},                        // #54 Language = "Telugu"
//    //                new LanguageName {Text = "เกี่ยวกับมอลตา", TranslationToLanguage = th,},                    // #55 Language = "Thai"
//    //                new LanguageName {Text = "Malta", TranslationToLanguage = tr,},                        // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Мальтійська", TranslationToLanguage = uk,},                  // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "مالٹی", TranslationToLanguage = ur,},                       // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Tiếng Malta", TranslationToLanguage = vi,},                  // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "מאלטיזיש", TranslationToLanguage = yi,},                    // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "马耳他语", TranslationToLanguage = zh,},                      // #61 Language = "Chinese"
//    //            };

//    //        if (nl.Names.Count < 61)
//    //            nl.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Dutch", TranslationToLanguage = en,},                    // #1  Language = "English"
//    //                new LanguageName {Text = "holandés", TranslationToLanguage = es,},                 // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Holländer", TranslationToLanguage = de,},                // #3  Language = "German"
//    //                new LanguageName {Text = "هولندي", TranslationToLanguage = ar,},                  // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Nederlandse", TranslationToLanguage = af,},              // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "holland", TranslationToLanguage = az,},                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Галандская", TranslationToLanguage = be,},               // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "холандски", TranslationToLanguage = bg,},                // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "ওলন্দাজি", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "holandès", TranslationToLanguage = ca,},                 // #10 Language = "Catalan"
//    //                new LanguageName {Text = "holandský", TranslationToLanguage = cs,},                // #11 Language = "Czech"
//    //                new LanguageName {Text = "Iseldireg", TranslationToLanguage = cy,},                // #12 Language = "Welsh"
//    //                new LanguageName {Text = "hollandsk", TranslationToLanguage = da,},                // #13 Language = "Danish"
//    //                new LanguageName {Text = "hollandi", TranslationToLanguage = et,},                 // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Nederlanderara", TranslationToLanguage = eu,},           // #15 Language = "Basque"
//    //                new LanguageName {Text = "هلندی", TranslationToLanguage = fa,},                   // #16 Language = "Persian"
//    //                new LanguageName {Text = "hollantilainen", TranslationToLanguage = fi,},           // #17 Language = "Finnish"
//    //                new LanguageName {Text = "hollandaise", TranslationToLanguage = fr,},              // #18 Language = "French"
//    //                new LanguageName {Text = "Ollainnis", TranslationToLanguage = ga,},                // #19 Language = "Irish"
//    //                new LanguageName {Text = "Holandés", TranslationToLanguage = gl,},                 // #20 Language = "Galician"
//    //                new LanguageName {Text = "હૉલાન્ડની ડચ ભાષા", TranslationToLanguage = gu,},         // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "הולנדי", TranslationToLanguage = he,},                  // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "डच", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName {Text = "nizozemski", TranslationToLanguage = hr,},               // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Olandè", TranslationToLanguage = ht,},                   // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "holland", TranslationToLanguage = hu,},                  // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "հոլանդական", TranslationToLanguage = hy,},              // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Belanda", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Hollenska", TranslationToLanguage = isLanguage,},        // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "olandese", TranslationToLanguage = it,},                 // #30 Language = "Italian"
//    //                new LanguageName {Text = "オランダ", TranslationToLanguage = ja,},                  // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ჰოლანდიელი", TranslationToLanguage = ka,},             // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಡಚ್", TranslationToLanguage = kn,},                      // #33 Language = "Kannada"
//    //                new LanguageName {Text = "네덜란드", TranslationToLanguage = ko,},                  // #34 Language = "Korean"
//    //                new LanguageName {Text = "Dutch", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName {Text = "Olandijos", TranslationToLanguage = lt,},                // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "holandiešu", TranslationToLanguage = lv,},               // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Холандија", TranslationToLanguage = mk,},                // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Belanda", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName {Text = "Olandiż", TranslationToLanguage = mt,},                  // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Nederlands", TranslationToLanguage = nl,},               // #41 Language = "Dutch"
//    //                new LanguageName {Text = "nederlandsk", TranslationToLanguage = no,},              // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "holenderski", TranslationToLanguage = pl,},              // #43 Language = "Polish"
//    //                new LanguageName {Text = "holandês", TranslationToLanguage = pt,},                 // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "olandez", TranslationToLanguage = ro,},                  // #45 Language = "Romanian"
//    //                new LanguageName {Text = "голландский", TranslationToLanguage = ru,},              // #46 Language = "Russian"
//    //                new LanguageName {Text = "Holandský", TranslationToLanguage = sk,},                // #47 Language = "Slovak"
//    //                new LanguageName {Text = "nizozemski", TranslationToLanguage = sl,},               // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "holandez", TranslationToLanguage = sq,},                 // #49 Language = "Albanian"
//    //                new LanguageName {Text = "холандски", TranslationToLanguage = sr,},                // #50 Language = "Serbian"
//    //                new LanguageName {Text = "nederländska", TranslationToLanguage = sv,},             // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kiholanzi", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName {Text = "உலாந்திய", TranslationToLanguage = ta,},               // #53 Language = "Tamil"
//    //                new LanguageName {Text = "డచ్", TranslationToLanguage = te,},                      // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ดัตช์", TranslationToLanguage = th,},                      // #55 Language = "Thai"
//    //                new LanguageName {Text = "Hollandalı", TranslationToLanguage = tr,},               // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Голландська", TranslationToLanguage = uk,},              // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "ڈچ", TranslationToLanguage = ur,},                       // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Hà Lan", TranslationToLanguage = vi,},                   // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "האָלענדיש", TranslationToLanguage = yi,},                // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "荷兰", TranslationToLanguage = zh,},                     // #61 Language = "Chinese"
//    //            };

//    //        if (no.Names.Count < 61)
//    //            no.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Norwegian", TranslationToLanguage = en,},                     // #1  Language = "English"
//    //                new LanguageName {Text = "noruego", TranslationToLanguage = es,},                       // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Norwegisch", TranslationToLanguage = de,},                    // #3  Language = "German"
//    //                new LanguageName {Text = "النرويجية", TranslationToLanguage = ar,},                    // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Noorse", TranslationToLanguage = af,},                        // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "norveçli", TranslationToLanguage = az,},                      // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Нарвежская", TranslationToLanguage = be,},                    // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "норвежки", TranslationToLanguage = bg,},                      // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "নরওয়েবাসী", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName {Text = "noruec", TranslationToLanguage = ca,},                        // #10 Language = "Catalan"
//    //                new LanguageName {Text = "norský", TranslationToLanguage = cs,},                        // #11 Language = "Czech"
//    //                new LanguageName {Text = "Norwy", TranslationToLanguage = cy,},                         // #12 Language = "Welsh"
//    //                new LanguageName {Text = "norske", TranslationToLanguage = da,},                        // #13 Language = "Danish"
//    //                new LanguageName {Text = "norra", TranslationToLanguage = et,},                         // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Norvegiako", TranslationToLanguage = eu,},                    // #15 Language = "Basque"
//    //                new LanguageName {Text = "نروژی", TranslationToLanguage = fa,},                        // #16 Language = "Persian"
//    //                new LanguageName {Text = "Norja", TranslationToLanguage = fi,},                         // #17 Language = "Finnish"
//    //                new LanguageName {Text = "norvégiennes", TranslationToLanguage = fr,},                  // #18 Language = "French"
//    //                new LanguageName {Text = "Ioruais", TranslationToLanguage = ga,},                       // #19 Language = "Irish"
//    //                new LanguageName {Text = "Noruego", TranslationToLanguage = gl,},                       // #20 Language = "Galician"
//    //                new LanguageName {Text = "નોર્વેજિયન", TranslationToLanguage = gu,},                      // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "נורווגית", TranslationToLanguage = he,},                     // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "नार्वेजियन", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName {Text = "norveški", TranslationToLanguage = hr,},                      // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Nòvejyen", TranslationToLanguage = ht,},                      // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "norvég", TranslationToLanguage = hu,},                        // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "նորվեգերեն", TranslationToLanguage = hy,},                    // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Norwegia", TranslationToLanguage = id,},                      // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Norska", TranslationToLanguage = isLanguage,},                // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "norvegese", TranslationToLanguage = it,},                     // #30 Language = "Italian"
//    //                new LanguageName {Text = "ノルウェー", TranslationToLanguage = ja,},                     // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ნორვეგიის", TranslationToLanguage = ka,},                    // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ನಾರ್ವೇ ದೇಶದಲ್ಲಿ ಹುಟ್ಟಿದವನು", TranslationToLanguage = kn,},         // #33 Language = "Kannada"
//    //                new LanguageName {Text = "노르웨이의", TranslationToLanguage = ko,},                     // #34 Language = "Korean"
//    //                new LanguageName {Text = "Norvegica", TranslationToLanguage = la,},                     // #35 Language = "Latin"
//    //                new LanguageName {Text = "Norvegijos", TranslationToLanguage = lt,},                    // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Norvēģijas", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Норвешка", TranslationToLanguage = mk,},                      // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Norway", TranslationToLanguage = ms,},                        // #39 Language = "Malay"
//    //                new LanguageName {Text = "Norveġiż", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Noors", TranslationToLanguage = nl,},                         // #41 Language = "Dutch"
//    //                new LanguageName {Text = "norske", TranslationToLanguage = no,},                        // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "norweski", TranslationToLanguage = pl,},                      // #43 Language = "Polish"
//    //                new LanguageName {Text = "norueguês", TranslationToLanguage = pt,},                     // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "norvegian", TranslationToLanguage = ro,},                     // #45 Language = "Romanian"
//    //                new LanguageName {Text = "норвежский", TranslationToLanguage = ru,},                    // #46 Language = "Russian"
//    //                new LanguageName {Text = "Nórsky", TranslationToLanguage = sk,},                        // #47 Language = "Slovak"
//    //                new LanguageName {Text = "norveški", TranslationToLanguage = sl,},                      // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "norvegjez", TranslationToLanguage = sq,},                     // #49 Language = "Albanian"
//    //                new LanguageName {Text = "норвешки", TranslationToLanguage = sr,},                      // #50 Language = "Serbian"
//    //                new LanguageName {Text = "norska", TranslationToLanguage = sv,},                        // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Norway", TranslationToLanguage = sw,},                        // #52 Language = "Swahili"
//    //                new LanguageName {Text = "நார்வேஜியன்", TranslationToLanguage = ta,},                 // #53 Language = "Tamil"
//    //                new LanguageName {Text = "నార్వేజియన్", TranslationToLanguage = te,},                     // #54 Language = "Telugu"
//    //                new LanguageName {Text = "นอร์เวย์", TranslationToLanguage = th,},                         // #55 Language = "Thai"
//    //                new LanguageName {Text = "Norveç", TranslationToLanguage = tr,},                        // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Норвезька", TranslationToLanguage = uk,},                     // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "ناروے", TranslationToLanguage = ur,},                        // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Na Uy", TranslationToLanguage = vi,},                         // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "נאָרוועגיש", TranslationToLanguage = yi,},                    // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "挪威", TranslationToLanguage = zh,},                          // #61 Language = "Chinese"
//    //            };

//    //        if (pl.Names.Count < 61)
//    //            pl.Names = new List<LanguageName>
//    //            {

//    //                new LanguageName {Text = "Polish", TranslationToLanguage = en,},                                               // #1  Language = "English"
//    //                new LanguageName {Text = "polaco", TranslationToLanguage = es,},                                               // #2  Language = "Spanish"
//    //                new LanguageName {Text = "polnisch", TranslationToLanguage = de,},                                             // #3  Language = "German"
//    //                new LanguageName {Text = "بولندي", TranslationToLanguage = ar,},                                              // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Pools", TranslationToLanguage = af,},                                                // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "sürtmək", TranslationToLanguage = az,},                                              // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Польскі", TranslationToLanguage = be,},                                              // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "лак", TranslationToLanguage = bg,},                                                  // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "পালিশ", TranslationToLanguage = bn,},                                                 // #9  Language = "Bengali"
//    //                new LanguageName {Text = "polonès", TranslationToLanguage = ca,},                                              // #10 Language = "Catalan"
//    //                new LanguageName {Text = "polský", TranslationToLanguage = cs,},                                               // #11 Language = "Czech"
//    //                new LanguageName {Text = "Pwyleg", TranslationToLanguage = cy,},                                               // #12 Language = "Welsh"
//    //                new LanguageName {Text = "polsk", TranslationToLanguage = da,},                                                // #13 Language = "Danish"
//    //                new LanguageName {Text = "poola", TranslationToLanguage = et,},                                                // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Polonierara", TranslationToLanguage = eu,},                                          // #15 Language = "Basque"
//    //                new LanguageName {Text = "لهستانی", TranslationToLanguage = fa,},                                             // #16 Language = "Persian"
//    //                new LanguageName {Text = "puola", TranslationToLanguage = fi,},                                                // #17 Language = "Finnish"
//    //                new LanguageName {Text = "polonaise", TranslationToLanguage = fr,},                                            // #18 Language = "French"
//    //                new LanguageName {Text = "Polainnis", TranslationToLanguage = ga,},                                            // #19 Language = "Irish"
//    //                new LanguageName {Text = "Polaco", TranslationToLanguage = gl,},                                               // #20 Language = "Galician"
//    //                new LanguageName {Text = "ઘસીને ઘસાઈને લીસું ચકચકિત કરવું કે થવું ઓપવું કે ઓપાવવું", TranslationToLanguage = gu,},   // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "פולני", TranslationToLanguage = he,},                                               // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "पोलिश", TranslationToLanguage = hi,},                                                // #23 Language = "Hindi"
//    //                new LanguageName {Text = "poljski", TranslationToLanguage = hr,},                                              // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Polonè", TranslationToLanguage = ht,},                                               // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "lengyel", TranslationToLanguage = hu,},                                              // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "հղկում", TranslationToLanguage = hy,},                                               // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Polandia", TranslationToLanguage = id,},                                             // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "pólskur", TranslationToLanguage = isLanguage,},                                      // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "polacco", TranslationToLanguage = it,},                                              // #30 Language = "Italian"
//    //                new LanguageName {Text = "ポーランド", TranslationToLanguage = ja,},                                            // #31 Language = "Japanese"
//    //                new LanguageName {Text = "პოლონეთის", TranslationToLanguage = ka,},                                          // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಒಪ್ಪಹಾಕು", TranslationToLanguage = kn,},                                              // #33 Language = "Kannada"
//    //                new LanguageName {Text = "폴란드의", TranslationToLanguage = ko,},                                              // #34 Language = "Korean"
//    //                new LanguageName {Text = "Polonica", TranslationToLanguage = la,},                                             // #35 Language = "Latin"
//    //                new LanguageName {Text = "Lenkijos", TranslationToLanguage = lt,},                                             // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Polijas", TranslationToLanguage = lv,},                                              // #37 Language = "Latvian"
//    //                new LanguageName {Text = "полски", TranslationToLanguage = mk,},                                               // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Bahasa Poland", TranslationToLanguage = ms,},                                        // #39 Language = "Malay"
//    //                new LanguageName {Text = "Pollakk", TranslationToLanguage = mt,},                                              // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Pools", TranslationToLanguage = nl,},                                                // #41 Language = "Dutch"
//    //                new LanguageName {Text = "polsk", TranslationToLanguage = no,},                                                // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "polski", TranslationToLanguage = pl,},                                               // #43 Language = "Polish"
//    //                new LanguageName {Text = "polonês", TranslationToLanguage = pt,},                                              // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "polonez", TranslationToLanguage = ro,},                                              // #45 Language = "Romanian"
//    //                new LanguageName {Text = "польский", TranslationToLanguage = ru,},                                             // #46 Language = "Russian"
//    //                new LanguageName {Text = "Poľský", TranslationToLanguage = sk,},                                               // #47 Language = "Slovak"
//    //                new LanguageName {Text = "poljski", TranslationToLanguage = sl,},                                              // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "polonisht", TranslationToLanguage = sq,},                                            // #49 Language = "Albanian"
//    //                new LanguageName {Text = "пољски", TranslationToLanguage = sr,},                                               // #50 Language = "Serbian"
//    //                new LanguageName {Text = "polska", TranslationToLanguage = sv,},                                               // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kipolishi", TranslationToLanguage = sw,},                                            // #52 Language = "Swahili"
//    //                new LanguageName {Text = "மினுக்கு", TranslationToLanguage = ta,},                                             // #53 Language = "Tamil"
//    //                new LanguageName {Text = "సంస్కరించు", TranslationToLanguage = te,},                                             // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ขัด", TranslationToLanguage = th,},                                                   // #55 Language = "Thai"
//    //                new LanguageName {Text = "Polonya", TranslationToLanguage = tr,},                                              // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Польський", TranslationToLanguage = uk,},                                            // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "پولش", TranslationToLanguage = ur,},                                                // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Ba Lan", TranslationToLanguage = vi,},                                               // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "פּויליש", TranslationToLanguage = yi,},                                              // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "波兰", TranslationToLanguage = zh,},                                                 // #61 Language = "Chinese"
//    //            };

//    //        if (pt.Names.Count < 61)
//    //            pt.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Portuguese", TranslationToLanguage = en,},                    // #1  Language = "English"
//    //                new LanguageName {Text = "portugués", TranslationToLanguage = es,},                     // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Portugiesisch", TranslationToLanguage = de,},                 // #3  Language = "German"
//    //                new LanguageName {Text = "البرتغالية", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Portugees", TranslationToLanguage = af,},                     // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Portuqaliya", TranslationToLanguage = az,},                   // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Партугальская", TranslationToLanguage = be,},                 // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "португалски", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "পটুর্গালদেশীয়", TranslationToLanguage = bn,},                     // #9  Language = "Bengali"
//    //                new LanguageName {Text = "portuguès", TranslationToLanguage = ca,},                     // #10 Language = "Catalan"
//    //                new LanguageName {Text = "portugalština", TranslationToLanguage = cs,},                 // #11 Language = "Czech"
//    //                new LanguageName {Text = "Portiwgaleg", TranslationToLanguage = cy,},                   // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Portugisisk", TranslationToLanguage = da,},                   // #13 Language = "Danish"
//    //                new LanguageName {Text = "portugali", TranslationToLanguage = et,},                     // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Portuguese", TranslationToLanguage = eu,},                    // #15 Language = "Basque"
//    //                new LanguageName {Text = "پرتغالی", TranslationToLanguage = fa,},                      // #16 Language = "Persian"
//    //                new LanguageName {Text = "portugali", TranslationToLanguage = fi,},                     // #17 Language = "Finnish"
//    //                new LanguageName {Text = "portugaise", TranslationToLanguage = fr,},                    // #18 Language = "French"
//    //                new LanguageName {Text = "Portaingéilis", TranslationToLanguage = ga,},                 // #19 Language = "Irish"
//    //                new LanguageName {Text = "Portugués", TranslationToLanguage = gl,},                     // #20 Language = "Galician"
//    //                new LanguageName {Text = "પોર્ટુગીઝ ભાષા", TranslationToLanguage = gu,},                   // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "פורטוגזית", TranslationToLanguage = he,},                    // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "पुर्तगाली", TranslationToLanguage = hi,},                        // #23 Language = "Hindi"
//    //                new LanguageName {Text = "portugalisht", TranslationToLanguage = hr,},                  // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Pòtigè", TranslationToLanguage = ht,},                        // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "portugál", TranslationToLanguage = hu,},                      // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "պորտուգալերեն", TranslationToLanguage = hy,},                // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Portugis", TranslationToLanguage = id,},                      // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Portúgalska", TranslationToLanguage = isLanguage,},           // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "portoghese", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName {Text = "ポルトガル", TranslationToLanguage = ja,},                     // #31 Language = "Japanese"
//    //                new LanguageName {Text = "პორტუგალიის", TranslationToLanguage = ka,},                 // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಪೋರ್ಚುಗೀಸ್", TranslationToLanguage = kn,},                     // #33 Language = "Kannada"
//    //                new LanguageName {Text = "포르투갈어", TranslationToLanguage = ko,},                     // #34 Language = "Korean"
//    //                new LanguageName {Text = "Portuguese", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName {Text = "portugalų", TranslationToLanguage = lt,},                     // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "portugāļu", TranslationToLanguage = lv,},                     // #37 Language = "Latvian"
//    //                new LanguageName {Text = "португалски", TranslationToLanguage = mk,},                   // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Bahasa Portugis", TranslationToLanguage = ms,},               // #39 Language = "Malay"
//    //                new LanguageName {Text = "Portugiż", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Portugees", TranslationToLanguage = nl,},                     // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Portugisisk", TranslationToLanguage = no,},                   // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "portugalski", TranslationToLanguage = pl,},                   // #43 Language = "Polish"
//    //                new LanguageName {Text = "português", TranslationToLanguage = pt,},                     // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "portughez", TranslationToLanguage = ro,},                     // #45 Language = "Romanian"
//    //                new LanguageName {Text = "португальский", TranslationToLanguage = ru,},                 // #46 Language = "Russian"
//    //                new LanguageName {Text = "portugalčina", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName {Text = "portugalski", TranslationToLanguage = sl,},                   // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "portugalisht", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "португалски", TranslationToLanguage = sr,},                   // #50 Language = "Serbian"
//    //                new LanguageName {Text = "portugisiska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kireno", TranslationToLanguage = sw,},                        // #52 Language = "Swahili"
//    //                new LanguageName {Text = "போர்த்துகீசியம்", TranslationToLanguage = ta,},              // #53 Language = "Tamil"
//    //                new LanguageName {Text = "పోర్చుగీసు", TranslationToLanguage = te,},                       // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ภาษาโปรตุเกส", TranslationToLanguage = th,},                     // #55 Language = "Thai"
//    //                new LanguageName {Text = "Portekizce", TranslationToLanguage = tr,},                    // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Португальська", TranslationToLanguage = uk,},                 // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "پرتگالی", TranslationToLanguage = ur,},                      // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Bồ Đào Nha", TranslationToLanguage = vi,},                    // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "פּאָרטוגעזיש", TranslationToLanguage = yi,},                   // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "葡萄牙语", TranslationToLanguage = zh,},                       // #61 Language = "Chinese"
//    //            };

//    //        if (ro.Names.Count < 61)
//    //            ro.Names = new List<LanguageName>
//    //            {

//    //                new LanguageName {Text = "Romanian", TranslationToLanguage = en,},               // #1  Language = "English"
//    //                new LanguageName {Text = "rumano", TranslationToLanguage = es,},                 // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Rumänisch", TranslationToLanguage = de,},              // #3  Language = "German"
//    //                new LanguageName {Text = "رومانيا", TranslationToLanguage = ar,},               // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Roemeens", TranslationToLanguage = af,},               // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "rumın", TranslationToLanguage = az,},                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "румынская", TranslationToLanguage = be,},              // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "румънски", TranslationToLanguage = bg,},               // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "রোমানিয়ান", TranslationToLanguage = bn,},               // #9  Language = "Bengali"
//    //                new LanguageName {Text = "romanès", TranslationToLanguage = ca,},                // #10 Language = "Catalan"
//    //                new LanguageName {Text = "rumunský", TranslationToLanguage = cs,},               // #11 Language = "Czech"
//    //                new LanguageName {Text = "Rwmaneg", TranslationToLanguage = cy,},                // #12 Language = "Welsh"
//    //                new LanguageName {Text = "rumænsk", TranslationToLanguage = da,},                // #13 Language = "Danish"
//    //                new LanguageName {Text = "rumeenia", TranslationToLanguage = et,},               // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Errumanierara", TranslationToLanguage = eu,},          // #15 Language = "Basque"
//    //                new LanguageName {Text = "رومانیایی", TranslationToLanguage = fa,},             // #16 Language = "Persian"
//    //                new LanguageName {Text = "Romanian", TranslationToLanguage = fi,},               // #17 Language = "Finnish"
//    //                new LanguageName {Text = "roumaine", TranslationToLanguage = fr,},               // #18 Language = "French"
//    //                new LanguageName {Text = "Rómáinis", TranslationToLanguage = ga,},               // #19 Language = "Irish"
//    //                new LanguageName {Text = "Romanés", TranslationToLanguage = gl,},                // #20 Language = "Galician"
//    //                new LanguageName {Text = "રોમાનિયન", TranslationToLanguage = gu,},               // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "רומניה", TranslationToLanguage = he,},                // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "रोमानियाई", TranslationToLanguage = hi,},               // #23 Language = "Hindi"
//    //                new LanguageName {Text = "Rumunjski", TranslationToLanguage = hr,},              // #24 Language = "Croatian"
//    //                new LanguageName {Text = "romanian", TranslationToLanguage = ht,},               // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "román", TranslationToLanguage = hu,},                  // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "ռումիներեն", TranslationToLanguage = hy,},             // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Rumania", TranslationToLanguage = id,},                // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "rúmensku", TranslationToLanguage = isLanguage,},       // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "rumeno", TranslationToLanguage = it,},                 // #30 Language = "Italian"
//    //                new LanguageName {Text = "ルーマニア語", TranslationToLanguage = ja,},            // #31 Language = "Japanese"
//    //                new LanguageName {Text = "რუმინეთის", TranslationToLanguage = ka,},             // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ರೊಮೇನಿಯನ್", TranslationToLanguage = kn,},             // #33 Language = "Kannada"
//    //                new LanguageName {Text = "루마니아 사람", TranslationToLanguage = ko,},           // #34 Language = "Korean"
//    //                new LanguageName {Text = "Romanian", TranslationToLanguage = la,},               // #35 Language = "Latin"
//    //                new LanguageName {Text = "rumunų", TranslationToLanguage = lt,},                 // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "rumāņu", TranslationToLanguage = lv,},                 // #37 Language = "Latvian"
//    //                new LanguageName {Text = "романскиот", TranslationToLanguage = mk,},             // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Bahasa Romania", TranslationToLanguage = ms,},         // #39 Language = "Malay"
//    //                new LanguageName {Text = "Rumen", TranslationToLanguage = mt,},                  // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Roemeense", TranslationToLanguage = nl,},              // #41 Language = "Dutch"
//    //                new LanguageName {Text = "rumensk", TranslationToLanguage = no,},                // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "rumuński", TranslationToLanguage = pl,},               // #43 Language = "Polish"
//    //                new LanguageName {Text = "romeno", TranslationToLanguage = pt,},                 // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "român", TranslationToLanguage = ro,},                  // #45 Language = "Romanian"
//    //                new LanguageName {Text = "румынский", TranslationToLanguage = ru,},              // #46 Language = "Russian"
//    //                new LanguageName {Text = "Rumunský", TranslationToLanguage = sk,},               // #47 Language = "Slovak"
//    //                new LanguageName {Text = "romunski", TranslationToLanguage = sl,},               // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "rumun", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "румунски", TranslationToLanguage = sr,},               // #50 Language = "Serbian"
//    //                new LanguageName {Text = "rumänska", TranslationToLanguage = sv,},               // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kiromania", TranslationToLanguage = sw,},              // #52 Language = "Swahili"
//    //                new LanguageName {Text = "ரோமானியம்", TranslationToLanguage = ta,},          // #53 Language = "Tamil"
//    //                new LanguageName {Text = "రోమేనియన్", TranslationToLanguage = te,},               // #54 Language = "Telugu"
//    //                new LanguageName {Text = "โรมาเนีย", TranslationToLanguage = th,},                 // #55 Language = "Thai"
//    //                new LanguageName {Text = "Romanya", TranslationToLanguage = tr,},                // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Румунська", TranslationToLanguage = uk,},              // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "رومانیہ", TranslationToLanguage = ur,},               // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Rumani", TranslationToLanguage = vi,},                 // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "רומעניש", TranslationToLanguage = yi,},               // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "罗马尼亚", TranslationToLanguage = zh,},                // #61 Language = "Chinese"
//    //            };

//    //        if (ru.Names.Count < 61)
//    //            ru.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Russian", TranslationToLanguage = en,},                // #1  Language = "English"
//    //                new LanguageName {Text = "ruso", TranslationToLanguage = es,},                   // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Russisch", TranslationToLanguage = de,},               // #3  Language = "German"
//    //                new LanguageName {Text = "الروسية", TranslationToLanguage = ar,},               // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Russiese", TranslationToLanguage = af,},               // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "rus", TranslationToLanguage = az,},                    // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "рускі", TranslationToLanguage = be,},                  // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "руски", TranslationToLanguage = bg,},                  // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "রাশিয়ান", TranslationToLanguage = bn,},                 // #9  Language = "Bengali"
//    //                new LanguageName {Text = "rus", TranslationToLanguage = ca,},                    // #10 Language = "Catalan"
//    //                new LanguageName {Text = "Rus", TranslationToLanguage = cs,},                    // #11 Language = "Czech"
//    //                new LanguageName {Text = "Rwsia", TranslationToLanguage = cy,},                  // #12 Language = "Welsh"
//    //                new LanguageName {Text = "russisk", TranslationToLanguage = da,},                // #13 Language = "Danish"
//    //                new LanguageName {Text = "vene", TranslationToLanguage = et,},                   // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Errusierara", TranslationToLanguage = eu,},            // #15 Language = "Basque"
//    //                new LanguageName {Text = "روسی", TranslationToLanguage = fa,},                  // #16 Language = "Persian"
//    //                new LanguageName {Text = "Venäjän", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName {Text = "russes", TranslationToLanguage = fr,},                 // #18 Language = "French"
//    //                new LanguageName {Text = "Rúisis", TranslationToLanguage = ga,},                 // #19 Language = "Irish"
//    //                new LanguageName {Text = "Ruso", TranslationToLanguage = gl,},                   // #20 Language = "Galician"
//    //                new LanguageName {Text = "રશિયાનું", TranslationToLanguage = gu,},                 // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "רוסי", TranslationToLanguage = he,},                  // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "रूसी", TranslationToLanguage = hi,},                    // #23 Language = "Hindi"
//    //                new LanguageName {Text = "ruski", TranslationToLanguage = hr,},                  // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Ris", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "orosz", TranslationToLanguage = hu,},                  // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "ռուսերեն", TranslationToLanguage = hy,},               // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Rusia", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Rússneska", TranslationToLanguage = isLanguage,},      // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "russo", TranslationToLanguage = it,},                  // #30 Language = "Italian"
//    //                new LanguageName {Text = "ロシア語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName {Text = "რუსეთის", TranslationToLanguage = ka,},               // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ರಶಿಯನ್", TranslationToLanguage = kn,},                 // #33 Language = "Kannada"
//    //                new LanguageName {Text = "러시아의", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Russian", TranslationToLanguage = la,},                // #35 Language = "Latin"
//    //                new LanguageName {Text = "Rusijos", TranslationToLanguage = lt,},                // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "krievu", TranslationToLanguage = lv,},                 // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Руската", TranslationToLanguage = mk,},                // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Rusia", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName {Text = "Russu", TranslationToLanguage = mt,},                  // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Russisch", TranslationToLanguage = nl,},               // #41 Language = "Dutch"
//    //                new LanguageName {Text = "russiske", TranslationToLanguage = no,},               // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "rosyjski", TranslationToLanguage = pl,},               // #43 Language = "Polish"
//    //                new LanguageName {Text = "russo", TranslationToLanguage = pt,},                  // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "rusesc", TranslationToLanguage = ro,},                 // #45 Language = "Romanian"
//    //                new LanguageName {Text = "русский", TranslationToLanguage = ru,},                // #46 Language = "Russian"
//    //                new LanguageName {Text = "Rus", TranslationToLanguage = sk,},                    // #47 Language = "Slovak"
//    //                new LanguageName {Text = "ruski", TranslationToLanguage = sl,},                  // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "rusisht", TranslationToLanguage = sq,},                // #49 Language = "Albanian"
//    //                new LanguageName {Text = "руски", TranslationToLanguage = sr,},                  // #50 Language = "Serbian"
//    //                new LanguageName {Text = "ryska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kirusi", TranslationToLanguage = sw,},                 // #52 Language = "Swahili"
//    //                new LanguageName {Text = "ரஷியன்", TranslationToLanguage = ta,},               // #53 Language = "Tamil"
//    //                new LanguageName {Text = "రష్యన్", TranslationToLanguage = te,},                   // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ภาษารัสเซีย", TranslationToLanguage = th,},               // #55 Language = "Thai"
//    //                new LanguageName {Text = "Rus", TranslationToLanguage = tr,},                    // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Російська", TranslationToLanguage = uk,},              // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "روسی", TranslationToLanguage = ur,},                  // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Nga", TranslationToLanguage = vi,},                    // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "רוסיש", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "俄罗斯", TranslationToLanguage = zh,},                 // #61 Language = "Chinese"
//    //            };

//    //        if (sk.Names.Count < 61)
//    //            sk.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Slovak", TranslationToLanguage = en,},                        // #1  Language = "English"
//    //                new LanguageName {Text = "eslovaco", TranslationToLanguage = es,},                      // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Slowakisch", TranslationToLanguage = de,},                    // #3  Language = "German"
//    //                new LanguageName {Text = "السلوفاكية", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Slowaakse", TranslationToLanguage = af,},                     // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Slovak", TranslationToLanguage = az,},                        // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Славацкая", TranslationToLanguage = be,},                     // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "словашки", TranslationToLanguage = bg,},                      // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "স্লোভাক", TranslationToLanguage = bn,},                         // #9  Language = "Bengali"
//    //                new LanguageName {Text = "eslovac", TranslationToLanguage = ca,},                       // #10 Language = "Catalan"
//    //                new LanguageName {Text = "slovenský", TranslationToLanguage = cs,},                     // #11 Language = "Czech"
//    //                new LanguageName {Text = "Slofacia", TranslationToLanguage = cy,},                      // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Slovakiske", TranslationToLanguage = da,},                    // #13 Language = "Danish"
//    //                new LanguageName {Text = "slovaki", TranslationToLanguage = et,},                       // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Eslovakierara", TranslationToLanguage = eu,},                 // #15 Language = "Basque"
//    //                new LanguageName {Text = "اسلواکی", TranslationToLanguage = fa,},                      // #16 Language = "Persian"
//    //                new LanguageName {Text = "Slovakian", TranslationToLanguage = fi,},                     // #17 Language = "Finnish"
//    //                new LanguageName {Text = "slovaques", TranslationToLanguage = fr,},                     // #18 Language = "French"
//    //                new LanguageName {Text = "Slóvaicis", TranslationToLanguage = ga,},                     // #19 Language = "Irish"
//    //                new LanguageName {Text = "Eslovaco", TranslationToLanguage = gl,},                      // #20 Language = "Galician"
//    //                new LanguageName {Text = "સ્લોવાક", TranslationToLanguage = gu,},                        // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "סלובקית", TranslationToLanguage = he,},                      // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "स्लोवाक", TranslationToLanguage = hi,},                        // #23 Language = "Hindi"
//    //                new LanguageName {Text = "slovački", TranslationToLanguage = hr,},                      // #24 Language = "Croatian"
//    //                new LanguageName {Text = "slovak", TranslationToLanguage = ht,},                        // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "szlovák", TranslationToLanguage = hu,},                       // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "սլովակ", TranslationToLanguage = hy,},                        // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Slovakia", TranslationToLanguage = id,},                      // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Slovak", TranslationToLanguage = isLanguage,},                // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "slovacco", TranslationToLanguage = it,},                      // #30 Language = "Italian"
//    //                new LanguageName {Text = "スロバキア語", TranslationToLanguage = ja,},                   // #31 Language = "Japanese"
//    //                new LanguageName {Text = "სლოვაკეთის", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಸ್ಲೋವ್ಯಾಕ್ ಭಾಷೆ ಯಾ ಜನಾಂಗದವರು", TranslationToLanguage = kn,},   // #33 Language = "Kannada"
//    //                new LanguageName {Text = "슬로바키아어", TranslationToLanguage = ko,},                   // #34 Language = "Korean"
//    //                new LanguageName {Text = "Moravica", TranslationToLanguage = la,},                      // #35 Language = "Latin"
//    //                new LanguageName {Text = "Slovakijos", TranslationToLanguage = lt,},                    // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Slovākijas", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Словачка", TranslationToLanguage = mk,},                      // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Slovakia", TranslationToLanguage = ms,},                      // #39 Language = "Malay"
//    //                new LanguageName {Text = "Slovakka", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Slowaaks", TranslationToLanguage = nl,},                      // #41 Language = "Dutch"
//    //                new LanguageName {Text = "slovakisk", TranslationToLanguage = no,},                     // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "słowacki", TranslationToLanguage = pl,},                      // #43 Language = "Polish"
//    //                new LanguageName {Text = "eslovaco", TranslationToLanguage = pt,},                      // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "slovac", TranslationToLanguage = ro,},                        // #45 Language = "Romanian"
//    //                new LanguageName {Text = "словацкий", TranslationToLanguage = ru,},                     // #46 Language = "Russian"
//    //                new LanguageName {Text = "slovenských", TranslationToLanguage = sk,},                   // #47 Language = "Slovak"
//    //                new LanguageName {Text = "Slovaški", TranslationToLanguage = sl,},                      // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "sllovak", TranslationToLanguage = sq,},                       // #49 Language = "Albanian"
//    //                new LanguageName {Text = "словачки", TranslationToLanguage = sr,},                      // #50 Language = "Serbian"
//    //                new LanguageName {Text = "slovak", TranslationToLanguage = sv,},                        // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kislovakia", TranslationToLanguage = sw,},                    // #52 Language = "Swahili"
//    //                new LanguageName {Text = "ஸ்லோவாக்", TranslationToLanguage = ta,},                  // #53 Language = "Tamil"
//    //                new LanguageName {Text = "స్లోవక్", TranslationToLanguage = te,},                          // #54 Language = "Telugu"
//    //                new LanguageName {Text = "สโลวัก", TranslationToLanguage = th,},                          // #55 Language = "Thai"
//    //                new LanguageName {Text = "Slovak", TranslationToLanguage = tr,},                        // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Словацька", TranslationToLanguage = uk,},                     // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "سلاواکی", TranslationToLanguage = ur,},                       // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Tiếng Slovak", TranslationToLanguage = vi,},                  // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "סלאָוואַקיש", TranslationToLanguage = yi,},                    // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "斯洛伐克", TranslationToLanguage = zh,},                       // #61 Language = "Chinese"
//    //            };

//    //        if (sl.Names.Count < 61)
//    //            sl.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Slovenian", TranslationToLanguage = en,},                  // #1  Language = "English"
//    //                new LanguageName {Text = "esloveno", TranslationToLanguage = es,},                   // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Slowenisch", TranslationToLanguage = de,},                 // #3  Language = "German"
//    //                new LanguageName {Text = "سلوفيني", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Sloweens", TranslationToLanguage = af,},                   // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Sloven", TranslationToLanguage = az,},                     // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Славенская", TranslationToLanguage = be,},                 // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "словенски", TranslationToLanguage = bg,},                  // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "স্লোভেনিয়ান", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "eslovè", TranslationToLanguage = ca,},                     // #10 Language = "Catalan"
//    //                new LanguageName {Text = "slovinský", TranslationToLanguage = cs,},                  // #11 Language = "Czech"
//    //                new LanguageName {Text = "Slofenia", TranslationToLanguage = cy,},                   // #12 Language = "Welsh"
//    //                new LanguageName {Text = "slovenske", TranslationToLanguage = da,},                  // #13 Language = "Danish"
//    //                new LanguageName {Text = "sloveeni", TranslationToLanguage = et,},                   // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Eslovenierara", TranslationToLanguage = eu,},              // #15 Language = "Basque"
//    //                new LanguageName {Text = "اسلوونیایی", TranslationToLanguage = fa,},                // #16 Language = "Persian"
//    //                new LanguageName {Text = "Slovenian", TranslationToLanguage = fi,},                  // #17 Language = "Finnish"
//    //                new LanguageName {Text = "slovènes", TranslationToLanguage = fr,},                   // #18 Language = "French"
//    //                new LanguageName {Text = "Slóivéinis", TranslationToLanguage = ga,},                 // #19 Language = "Irish"
//    //                new LanguageName {Text = "Esloveno", TranslationToLanguage = gl,},                   // #20 Language = "Galician"
//    //                new LanguageName {Text = "સ્લોવેનિયન", TranslationToLanguage = gu,},                  // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "סלובנית", TranslationToLanguage = he,},                   // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "स्लॉवेनियन", TranslationToLanguage = hi,},                   // #23 Language = "Hindi"
//    //                new LanguageName {Text = "Slovenski", TranslationToLanguage = hr,},                  // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Sloveni", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "szlovén", TranslationToLanguage = hu,},                    // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "սլովեներեն", TranslationToLanguage = hy,},                 // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Slovenia", TranslationToLanguage = id,},                   // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "slóvensku", TranslationToLanguage = isLanguage,},          // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "sloveno", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName {Text = "スロベニア語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName {Text = "სლოვენიის", TranslationToLanguage = ka,},                 // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಸ್ಲೋವೇನಿಯಾದ ಜನರ ಯಾ ಭಾಷೆಯ", TranslationToLanguage = kn,}, // #33 Language = "Kannada"
//    //                new LanguageName {Text = "슬로베니아", TranslationToLanguage = ko,},                  // #34 Language = "Korean"
//    //                new LanguageName {Text = "Carnica", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName {Text = "Slovėnijos", TranslationToLanguage = lt,},                 // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "slovēņu", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Словенија", TranslationToLanguage = mk,},                  // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Slovenia", TranslationToLanguage = ms,},                   // #39 Language = "Malay"
//    //                new LanguageName {Text = "Sloven", TranslationToLanguage = mt,},                     // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Sloveens", TranslationToLanguage = nl,},                   // #41 Language = "Dutch"
//    //                new LanguageName {Text = "slovensk", TranslationToLanguage = no,},                   // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "słoweński", TranslationToLanguage = pl,},                  // #43 Language = "Polish"
//    //                new LanguageName {Text = "esloveno", TranslationToLanguage = pt,},                   // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "sloven", TranslationToLanguage = ro,},                     // #45 Language = "Romanian"
//    //                new LanguageName {Text = "словенский", TranslationToLanguage = ru,},                 // #46 Language = "Russian"
//    //                new LanguageName {Text = "Slovinský", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName {Text = "slovenski", TranslationToLanguage = sl,},                  // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "slloven", TranslationToLanguage = sq,},                    // #49 Language = "Albanian"
//    //                new LanguageName {Text = "словеначки", TranslationToLanguage = sr,},                 // #50 Language = "Serbian"
//    //                new LanguageName {Text = "slovenska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kislovenia", TranslationToLanguage = sw,},                 // #52 Language = "Swahili"
//    //                new LanguageName {Text = "ஸ்லோவேனியன்", TranslationToLanguage = ta,},         // #53 Language = "Tamil"
//    //                new LanguageName {Text = "స్లోవేనియన్", TranslationToLanguage = te,},                   // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ภาษาสโลเวเนีย", TranslationToLanguage = th,},                 // #55 Language = "Thai"
//    //                new LanguageName {Text = "Slovenya", TranslationToLanguage = tr,},                   // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Словенська", TranslationToLanguage = uk,},                 // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "سلووینیا", TranslationToLanguage = ur,},                  // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Tiếng Slovenia", TranslationToLanguage = vi,},             // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "סלאוועניש", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "斯洛文尼亚", TranslationToLanguage = zh,},                  // #61 Language = "Chinese"
//    //            };

//    //        if (sq.Names.Count < 61)
//    //            sq.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Albanian", TranslationToLanguage = en,},          // #1  Language = "English"
//    //                new LanguageName {Text = "albanés", TranslationToLanguage = es,},           // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Albanisch", TranslationToLanguage = de,},         // #3  Language = "German"
//    //                new LanguageName {Text = "الألبانية", TranslationToLanguage = ar,},         // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Albanees", TranslationToLanguage = af,},          // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "alban", TranslationToLanguage = az,},             // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "албанская", TranslationToLanguage = be,},         // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "албански", TranslationToLanguage = bg,},          // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "আলবেনিয়ান", TranslationToLanguage = bn,},         // #9  Language = "Bengali"
//    //                new LanguageName {Text = "Albanès", TranslationToLanguage = ca,},           // #10 Language = "Catalan"
//    //                new LanguageName {Text = "albánský", TranslationToLanguage = cs,},          // #11 Language = "Czech"
//    //                new LanguageName {Text = "Albaneg", TranslationToLanguage = cy,},           // #12 Language = "Welsh"
//    //                new LanguageName {Text = "albansk", TranslationToLanguage = da,},           // #13 Language = "Danish"
//    //                new LanguageName {Text = "albaania", TranslationToLanguage = et,},          // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Albanian", TranslationToLanguage = eu,},          // #15 Language = "Basque"
//    //                new LanguageName {Text = "آلبانیایی", TranslationToLanguage = fa,},        // #16 Language = "Persian"
//    //                new LanguageName {Text = "Albanian", TranslationToLanguage = fi,},          // #17 Language = "Finnish"
//    //                new LanguageName {Text = "albanaises", TranslationToLanguage = fr,},        // #18 Language = "French"
//    //                new LanguageName {Text = "Albáinis", TranslationToLanguage = ga,},          // #19 Language = "Irish"
//    //                new LanguageName {Text = "Albanés", TranslationToLanguage = gl,},           // #20 Language = "Galician"
//    //                new LanguageName {Text = "અલ્બેનિયન", TranslationToLanguage = gu,},         // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "אלבנית", TranslationToLanguage = he,},           // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "अल्बेनियन्", TranslationToLanguage = hi,},          // #23 Language = "Hindi"
//    //                new LanguageName {Text = "अल्बेनियन्", TranslationToLanguage = hr,},          // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Haitian", TranslationToLanguage = ht,},           // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "albán", TranslationToLanguage = hu,},             // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "ալբանական", TranslationToLanguage = hy,},       // #27 Language = "Armenian"
//    //                new LanguageName {Text = "bahasa Albania", TranslationToLanguage = id,},    // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Albanska", TranslationToLanguage = isLanguage,},  // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "albanese", TranslationToLanguage = it,},          // #30 Language = "Italian"
//    //                new LanguageName {Text = "アルバニア語", TranslationToLanguage = ja,},       // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ალბანური", TranslationToLanguage = ka,},         // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಅಲ್ಬೇನಿಯನ್", TranslationToLanguage = kn,},         // #33 Language = "Kannada"
//    //                new LanguageName {Text = "알바니아", TranslationToLanguage = ko,},           // #34 Language = "Korean"
//    //                new LanguageName {Text = "Illyrica", TranslationToLanguage = la,},          // #35 Language = "Latin"
//    //                new LanguageName {Text = "albanų", TranslationToLanguage = lt,},            // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "albāņu", TranslationToLanguage = lv,},            // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Албанци", TranslationToLanguage = mk,},           // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Albania", TranslationToLanguage = ms,},           // #39 Language = "Malay"
//    //                new LanguageName {Text = "Albaniż", TranslationToLanguage = mt,},           // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Albanees", TranslationToLanguage = nl,},          // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Albansk", TranslationToLanguage = no,},           // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "albański", TranslationToLanguage = pl,},          // #43 Language = "Polish"
//    //                new LanguageName {Text = "albanês", TranslationToLanguage = pt,},           // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "albanez", TranslationToLanguage = ro,},           // #45 Language = "Romanian"
//    //                new LanguageName {Text = "албанский", TranslationToLanguage = ru,},         // #46 Language = "Russian"
//    //                new LanguageName {Text = "Albánsky", TranslationToLanguage = sk,},          // #47 Language = "Slovak"
//    //                new LanguageName {Text = "albanski", TranslationToLanguage = sl,},          // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "shqiptar", TranslationToLanguage = sq,},          // #49 Language = "Albanian"
//    //                new LanguageName {Text = "албански", TranslationToLanguage = sr,},          // #50 Language = "Serbian"
//    //                new LanguageName {Text = "albanska", TranslationToLanguage = sv,},          // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kialbeni", TranslationToLanguage = sw,},          // #52 Language = "Swahili"
//    //                new LanguageName {Text = "அல்பானியன்", TranslationToLanguage = ta,},     // #53 Language = "Tamil"
//    //                new LanguageName {Text = "అల్బేనియా దేశస్థుడు", TranslationToLanguage = te,},   // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ชาวแอลเบเนีย", TranslationToLanguage = th,},         // #55 Language = "Thai"
//    //                new LanguageName {Text = "Arnavut", TranslationToLanguage = tr,},           // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Албанська", TranslationToLanguage = uk,},         // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "البانی", TranslationToLanguage = ur,},           // #58 Language = "Urdu"
//    //                new LanguageName {Text = "tiếng An-ba-ni", TranslationToLanguage = vi,},    // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "אַלבאַניש", TranslationToLanguage = yi,},          // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "阿尔巴尼亚人", TranslationToLanguage = zh,},       // #61 Language = "Chinese"
//    //            };

//    //        if (sr.Names.Count < 61)
//    //            sr.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Serbian", TranslationToLanguage = en,},                               // #1  Language = "English"
//    //                new LanguageName {Text = "serbio", TranslationToLanguage = es,},                                // #2  Language = "Spanish"
//    //                new LanguageName {Text = "serbisch", TranslationToLanguage = de,},                              // #3  Language = "German"
//    //                new LanguageName {Text = "صربي", TranslationToLanguage = ar,},                                 // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Serwies", TranslationToLanguage = af,},                               // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "serb", TranslationToLanguage = az,},                                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Сербская", TranslationToLanguage = be,},                              // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "сръбски", TranslationToLanguage = bg,},                               // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "সার্বিয়া সংক্রান্ত", TranslationToLanguage = bn,},                           // #9  Language = "Bengali"
//    //                new LanguageName {Text = "serbi", TranslationToLanguage = ca,},                                 // #10 Language = "Catalan"
//    //                new LanguageName {Text = "srbský", TranslationToLanguage = cs,},                                // #11 Language = "Czech"
//    //                new LanguageName {Text = "Serbeg", TranslationToLanguage = cy,},                                // #12 Language = "Welsh"
//    //                new LanguageName {Text = "serbisk", TranslationToLanguage = da,},                               // #13 Language = "Danish"
//    //                new LanguageName {Text = "serbia", TranslationToLanguage = et,},                                // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Serbian", TranslationToLanguage = eu,},                               // #15 Language = "Basque"
//    //                new LanguageName {Text = "صرب", TranslationToLanguage = fa,},                                   // #16 Language = "Persian"
//    //                new LanguageName {Text = "Serbian", TranslationToLanguage = fi,},                               // #17 Language = "Finnish"
//    //                new LanguageName {Text = "serbes", TranslationToLanguage = fr,},                                // #18 Language = "French"
//    //                new LanguageName {Text = "Seirbis", TranslationToLanguage = ga,},                               // #19 Language = "Irish"
//    //                new LanguageName {Text = "Serbian", TranslationToLanguage = gl,},                               // #20 Language = "Galician"
//    //                new LanguageName {Text = "સર્બિયન", TranslationToLanguage = gu,},                                // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "הסרבי", TranslationToLanguage = he,},                                // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "सर्बिया की (भाषा या निवासी)", TranslationToLanguage = hi,},               // #23 Language = "Hindi"
//    //                new LanguageName {Text = "srpski", TranslationToLanguage = hr,},                                // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Sèb", TranslationToLanguage = ht,},                                   // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "szerb", TranslationToLanguage = hu,},                                 // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "սերբերեն", TranslationToLanguage = hy,},                              // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Serbia", TranslationToLanguage = id,},                                // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "serbneska", TranslationToLanguage = isLanguage,},                     // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "serbo", TranslationToLanguage = it,},                                 // #30 Language = "Italian"
//    //                new LanguageName {Text = "セルビア語", TranslationToLanguage = ja,},                             // #31 Language = "Japanese"
//    //                new LanguageName {Text = "სერბეთის", TranslationToLanguage = ka,},                             // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಸರ್ಬಿಯ ರಾಷ್ಟ್ರದ ಯಾ ಅದರ ಭಾಷೆಗೆ ಸಂಬಂಧಿಸಿದ", TranslationToLanguage = kn,},  // #33 Language = "Kannada"
//    //                new LanguageName {Text = "세르비아의", TranslationToLanguage = ko,},                             // #34 Language = "Korean"
//    //                new LanguageName {Text = "Serbiae", TranslationToLanguage = la,},                               // #35 Language = "Latin"
//    //                new LanguageName {Text = "Serbijos", TranslationToLanguage = lt,},                              // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Serbijas", TranslationToLanguage = lv,},                              // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Србија", TranslationToLanguage = mk,},                                // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Serbia", TranslationToLanguage = ms,},                                // #39 Language = "Malay"
//    //                new LanguageName {Text = "Serb", TranslationToLanguage = mt,},                                  // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Servisch", TranslationToLanguage = nl,},                              // #41 Language = "Dutch"
//    //                new LanguageName {Text = "serbisk", TranslationToLanguage = no,},                               // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "serbski", TranslationToLanguage = pl,},                               // #43 Language = "Polish"
//    //                new LanguageName {Text = "sérvio", TranslationToLanguage = pt,},                                // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "sârb", TranslationToLanguage = ro,},                                  // #45 Language = "Romanian"
//    //                new LanguageName {Text = "сербский", TranslationToLanguage = ru,},                              // #46 Language = "Russian"
//    //                new LanguageName {Text = "srbský", TranslationToLanguage = sk,},                                // #47 Language = "Slovak"
//    //                new LanguageName {Text = "srbski", TranslationToLanguage = sl,},                                // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "serb", TranslationToLanguage = sq,},                                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "српски", TranslationToLanguage = sr,},                                // #50 Language = "Serbian"
//    //                new LanguageName {Text = "serbiska", TranslationToLanguage = sv,},                              // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Serbia", TranslationToLanguage = sw,},                                // #52 Language = "Swahili"
//    //                new LanguageName {Text = "செர்பியாவை சார்ந்த", TranslationToLanguage = ta,},                 // #53 Language = "Tamil"
//    //                new LanguageName {Text = "సెర్బియా", TranslationToLanguage = te,},                                // #54 Language = "Telugu"
//    //                new LanguageName {Text = "เซอร์เบีย", TranslationToLanguage = th,},                                 // #55 Language = "Thai"
//    //                new LanguageName {Text = "Sırp", TranslationToLanguage = tr,},                                  // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Сербська", TranslationToLanguage = uk,},                              // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "سربیا", TranslationToLanguage = ur,},                                // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Serbia", TranslationToLanguage = vi,},                                // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "סערביש", TranslationToLanguage = yi,},                               // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "塞尔维亚", TranslationToLanguage = zh,},                               // #61 Language = "Chinese"
//    //            };

//    //        if (sv.Names.Count < 61)
//    //            sv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Swedish", TranslationToLanguage = en,},                                // #1  Language = "English"
//    //                new LanguageName {Text = "sueco", TranslationToLanguage = es,},                                  // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Schwedisch", TranslationToLanguage = de,},                             // #3  Language = "German"
//    //                new LanguageName {Text = "السويدية", TranslationToLanguage = ar,},                              // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Sweedse", TranslationToLanguage = af,},                                // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "İsveç", TranslationToLanguage = az,},                                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Шведская", TranslationToLanguage = be,},                               // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "шведски", TranslationToLanguage = bg,},                                // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "সুইডেনের ভাষা", TranslationToLanguage = bn,},                            // #9  Language = "Bengali"
//    //                new LanguageName {Text = "suec", TranslationToLanguage = ca,},                                   // #10 Language = "Catalan"
//    //                new LanguageName {Text = "švédský", TranslationToLanguage = cs,},                                // #11 Language = "Czech"
//    //                new LanguageName {Text = "Sweden", TranslationToLanguage = cy,},                                 // #12 Language = "Welsh"
//    //                new LanguageName {Text = "svensk", TranslationToLanguage = da,},                                 // #13 Language = "Danish"
//    //                new LanguageName {Text = "rootsi", TranslationToLanguage = et,},                                 // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Suedierara", TranslationToLanguage = eu,},                             // #15 Language = "Basque"
//    //                new LanguageName {Text = "سوئد", TranslationToLanguage = fa,},                                  // #16 Language = "Persian"
//    //                new LanguageName {Text = "ruotsi", TranslationToLanguage = fi,},                                 // #17 Language = "Finnish"
//    //                new LanguageName {Text = "Suède", TranslationToLanguage = fr,},                                  // #18 Language = "French"
//    //                new LanguageName {Text = "Sualainnis", TranslationToLanguage = ga,},                             // #19 Language = "Irish"
//    //                new LanguageName {Text = "Sueco", TranslationToLanguage = gl,},                                  // #20 Language = "Galician"
//    //                new LanguageName {Text = "સ્વીડીશ", TranslationToLanguage = gu,},                                 // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "שוודית", TranslationToLanguage = he,},                                // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "स्वीडिश", TranslationToLanguage = hi,},                                  // #23 Language = "Hindi"
//    //                new LanguageName {Text = "švedski", TranslationToLanguage = hr,},                                // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Syèd", TranslationToLanguage = ht,},                                   // #25 Language = "Haitian Creol
//    //                new LanguageName {Text = "svéd", TranslationToLanguage = hu,},                                   // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "շվեդերեն", TranslationToLanguage = hy,},                               // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Swedia", TranslationToLanguage = id,},                                 // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Sænska", TranslationToLanguage = isLanguage,},                         // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "svedese", TranslationToLanguage = it,},                                // #30 Language = "Italian"
//    //                new LanguageName {Text = "スウェーデン", TranslationToLanguage = ja,},                            // #31 Language = "Japanese"
//    //                new LanguageName {Text = "შვედეთის", TranslationToLanguage = ka,},                              // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಸ್ವೀಡಿಷ್", TranslationToLanguage = kn,},                                  // #33 Language = "Kannada"
//    //                new LanguageName {Text = "스웨덴의", TranslationToLanguage = ko,},                                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Suecica", TranslationToLanguage = la,},                                // #35 Language = "Latin"
//    //                new LanguageName {Text = "Švedijos", TranslationToLanguage = lt,},                               // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Zviedru", TranslationToLanguage = lv,},                                // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Шведската", TranslationToLanguage = mk,},                              // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Sweden", TranslationToLanguage = ms,},                                 // #39 Language = "Malay"
//    //                new LanguageName {Text = "Svediż", TranslationToLanguage = mt,},                                 // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Zweeds", TranslationToLanguage = nl,},                                 // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Svenske", TranslationToLanguage = no,},                                // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "szwedzki", TranslationToLanguage = pl,},                               // #43 Language = "Polish"
//    //                new LanguageName {Text = "sueco", TranslationToLanguage = pt,},                                  // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "suedez", TranslationToLanguage = ro,},                                 // #45 Language = "Romanian"
//    //                new LanguageName {Text = "шведский", TranslationToLanguage = ru,},                               // #46 Language = "Russian"
//    //                new LanguageName {Text = "Švédsky", TranslationToLanguage = sk,},                                // #47 Language = "Slovak"
//    //                new LanguageName {Text = "švedski", TranslationToLanguage = sl,},                                // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "suedez", TranslationToLanguage = sq,},                                 // #49 Language = "Albanian"
//    //                new LanguageName {Text = "шведски", TranslationToLanguage = sr,},                                // #50 Language = "Serbian"
//    //                new LanguageName {Text = "svenska", TranslationToLanguage = sv,},                                // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Swedish", TranslationToLanguage = sw,},                                // #52 Language = "Swahili"
//    //                new LanguageName {Text = "ஸ்வீடன் நாட்டு மொழி, மக்கள்", TranslationToLanguage = ta,},       // #53 Language = "Tamil"
//    //                new LanguageName {Text = "స్వీడిష్", TranslationToLanguage = te,},                                  // #54 Language = "Telugu"
//    //                new LanguageName {Text = "สวีเดน", TranslationToLanguage = th,},                                   // #55 Language = "Thai"
//    //                new LanguageName {Text = "İsveç", TranslationToLanguage = tr,},                                  // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Шведська", TranslationToLanguage = uk,},                               // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "سويڈش", TranslationToLanguage = ur,},                                 // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Thụy Điển", TranslationToLanguage = vi,},                              // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "שוועדיש", TranslationToLanguage = yi,},                               // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "瑞典语", TranslationToLanguage = zh,},                                  // #61 Language = "Chinese"
//    //            };

//    //        if (sw.Names.Count < 61)
//    //            sw.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = en,},               // #1  Language = "English"
//    //                new LanguageName {Text = "swahili", TranslationToLanguage = es,},               // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = de,},               // #3  Language = "German"
//    //                new LanguageName {Text = "السواحيلية", TranslationToLanguage = ar,},           // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = af,},               // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Suahili", TranslationToLanguage = az,},               // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "суахілі", TranslationToLanguage = be,},               // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "суахили", TranslationToLanguage = bg,},               // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "সোয়াহিলি", TranslationToLanguage = bn,},               // #9  Language = "Bengali"
//    //                new LanguageName {Text = "suahili", TranslationToLanguage = ca,},               // #10 Language = "Catalan"
//    //                new LanguageName {Text = "svahilština", TranslationToLanguage = cs,},           // #11 Language = "Czech"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = cy,},               // #12 Language = "Welsh"
//    //                new LanguageName {Text = "swahili", TranslationToLanguage = da,},               // #13 Language = "Danish"
//    //                new LanguageName {Text = "suahiili", TranslationToLanguage = et,},              // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = eu,},               // #15 Language = "Basque"
//    //                new LanguageName {Text = "سواحیلی", TranslationToLanguage = fa,},              // #16 Language = "Persian"
//    //                new LanguageName {Text = "Suahili", TranslationToLanguage = fi,},               // #17 Language = "Finnish"
//    //                new LanguageName {Text = "swahili", TranslationToLanguage = fr,},               // #18 Language = "French"
//    //                new LanguageName {Text = "Svahaílis", TranslationToLanguage = ga,},             // #19 Language = "Irish"
//    //                new LanguageName {Text = "suahili", TranslationToLanguage = gl,},               // #20 Language = "Galician"
//    //                new LanguageName {Text = "સ્વાહિલી", TranslationToLanguage = gu,},                // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "סוואהילית", TranslationToLanguage = he,},            // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "स्वाहिली", TranslationToLanguage = hi,},                // #23 Language = "Hindi"
//    //                new LanguageName {Text = "svahili", TranslationToLanguage = hr,},               // #24 Language = "Croatian"
//    //                new LanguageName {Text = "swahili", TranslationToLanguage = ht,},               // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "szuahéli", TranslationToLanguage = hu,},              // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "սուահիլի", TranslationToLanguage = hy,},              // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = id,},               // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "svahílí", TranslationToLanguage = isLanguage,},       // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "swahili", TranslationToLanguage = it,},               // #30 Language = "Italian"
//    //                new LanguageName {Text = "スワヒリ語", TranslationToLanguage = ja,},             // #31 Language = "Japanese"
//    //                new LanguageName {Text = "სუაჰილი", TranslationToLanguage = ka,},              // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಸ್ವಾಹಿಲಿ", TranslationToLanguage = kn,},                 // #33 Language = "Kannada"
//    //                new LanguageName {Text = "스와힐리어", TranslationToLanguage = ko,},             // #34 Language = "Korean"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = la,},               // #35 Language = "Latin"
//    //                new LanguageName {Text = "svahili", TranslationToLanguage = lt,},               // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "svahili", TranslationToLanguage = lv,},               // #37 Language = "Latvian"
//    //                new LanguageName {Text = "свахили", TranslationToLanguage = mk,},               // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Bahasa Swahili", TranslationToLanguage = ms,},        // #39 Language = "Malay"
//    //                new LanguageName {Text = "Swaħili", TranslationToLanguage = mt,},               // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = nl,},               // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = no,},               // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "suahili", TranslationToLanguage = pl,},               // #43 Language = "Polish"
//    //                new LanguageName {Text = "suaíli", TranslationToLanguage = pt,},                // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = ro,},               // #45 Language = "Romanian"
//    //                new LanguageName {Text = "суахили", TranslationToLanguage = ru,},               // #46 Language = "Russian"
//    //                new LanguageName {Text = "svahilština", TranslationToLanguage = sk,},           // #47 Language = "Slovak"
//    //                new LanguageName {Text = "svahili", TranslationToLanguage = sl,},               // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "suahilisht", TranslationToLanguage = sq,},            // #49 Language = "Albanian"
//    //                new LanguageName {Text = "Свахили", TranslationToLanguage = sr,},               // #50 Language = "Serbian"
//    //                new LanguageName {Text = "swahili", TranslationToLanguage = sv,},               // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = sw,},               // #52 Language = "Swahili"
//    //                new LanguageName {Text = "சுவாஹிலி", TranslationToLanguage = ta,},           // #53 Language = "Tamil"
//    //                new LanguageName {Text = "స్వాహిలి", TranslationToLanguage = te,},                // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ภาษาสวาฮีลี", TranslationToLanguage = th,},              // #55 Language = "Thai"
//    //                new LanguageName {Text = "Swahili", TranslationToLanguage = tr,},               // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Суахілі", TranslationToLanguage = uk,},               // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "سواہیلی", TranslationToLanguage = ur,},              // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Tiếng Swahili", TranslationToLanguage = vi,},         // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "סוואַהילי", TranslationToLanguage = yi,},             // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "斯瓦希里", TranslationToLanguage = zh,},               // #61 Language = "Chinese"
//    //            };

//    //        if (ta.Names.Count < 61)
//    //            ta.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = en,},                  // #1  Language = "English"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = es,},                  // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Tamilisch", TranslationToLanguage = de,},              // #3  Language = "German"
//    //                new LanguageName {Text = "التاميل", TranslationToLanguage = ar,},               // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = af,},                  // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = az,},                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "тамільская", TranslationToLanguage = be,},             // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "тамилски", TranslationToLanguage = bg,},               // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "তামিল", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "tàmil", TranslationToLanguage = ca,},                  // #10 Language = "Catalan"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = cs,},                  // #11 Language = "Czech"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = cy,},                  // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = da,},                  // #13 Language = "Danish"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = et,},                  // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = eu,},                  // #15 Language = "Basque"
//    //                new LanguageName {Text = "تامیل", TranslationToLanguage = fa,},                 // #16 Language = "Persian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = fi,},                  // #17 Language = "Finnish"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = fr,},                  // #18 Language = "French"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = ga,},                  // #19 Language = "Irish"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = gl,},                  // #20 Language = "Galician"
//    //                new LanguageName {Text = "તમિળ", TranslationToLanguage = gu,},                  // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "טמילית", TranslationToLanguage = he,},                // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "तमिल", TranslationToLanguage = hi,},                  // #23 Language = "Hindi"
//    //                new LanguageName {Text = "tamilski", TranslationToLanguage = hr,},               // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = ht,},                  // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "tamil", TranslationToLanguage = hu,},                  // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "թամիլերեն", TranslationToLanguage = hy,},              // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = isLanguage,},          // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = it,},                  // #30 Language = "Italian"
//    //                new LanguageName {Text = "タミル語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ტამილური", TranslationToLanguage = ka,},             // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ತಮಿಳು", TranslationToLanguage = kn,},                  // #33 Language = "Kannada"
//    //                new LanguageName {Text = "타밀 사람", TranslationToLanguage = ko,},               // #34 Language = "Korean"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = la,},                  // #35 Language = "Latin"
//    //                new LanguageName {Text = "tamilų", TranslationToLanguage = lt,},                 // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "tamilu", TranslationToLanguage = lv,},                 // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Тамилските", TranslationToLanguage = mk,},             // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName {Text = "tamil", TranslationToLanguage = mt,},                  // #40 Language = "Maltese"
//    //                new LanguageName {Text = "tamil", TranslationToLanguage = nl,},                  // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = no,},                  // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = pl,},                  // #43 Language = "Polish"
//    //                new LanguageName {Text = "tâmil", TranslationToLanguage = pt,},                  // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "tamilă", TranslationToLanguage = ro,},                 // #45 Language = "Romanian"
//    //                new LanguageName {Text = "тамильский", TranslationToLanguage = ru,},             // #46 Language = "Russian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = sl,},                  // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "tamil", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "тамилски", TranslationToLanguage = sr,},               // #50 Language = "Serbian"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = sw,},                  // #52 Language = "Swahili"
//    //                new LanguageName {Text = "தமிழ்", TranslationToLanguage = ta,},                  // #53 Language = "Tamil"
//    //                new LanguageName {Text = "తమిళ్", TranslationToLanguage = te,},                   // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ทมิฬ", TranslationToLanguage = th,},                    // #55 Language = "Thai"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = tr,},                  // #56 Language = "Turkish"
//    //                new LanguageName {Text = "тамільська", TranslationToLanguage = uk,},             // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "تامل", TranslationToLanguage = ur,},                  // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Tamil", TranslationToLanguage = vi,},                  // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "טאַמיל", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "泰米尔人", TranslationToLanguage = zh,},                // #61 Language = "Chinese"
//    //            };

//    //        if (te.Names.Count < 61)
//    //            te.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = en,},           // #1  Language = "English"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = es,},           // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = de,},           // #3  Language = "German"
//    //                new LanguageName {Text = "التيلجو", TranslationToLanguage = ar,},         // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = af,},           // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Teluqu", TranslationToLanguage = az,},           // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "тэлугу", TranslationToLanguage = be,},           // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "телугу", TranslationToLanguage = bg,},           // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "তেলুগু", TranslationToLanguage = bn,},             // #9  Language = "Bengali"
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = ca,},           // #10 Language = "Catalan"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = cs,},           // #11 Language = "Czech"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = cy,},           // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = da,},           // #13 Language = "Danish"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = et,},           // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = eu,},           // #15 Language = "Basque"
//    //                new LanguageName {Text = "تلوگو", TranslationToLanguage = fa,},           // #16 Language = "Persian"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = fi,},           // #17 Language = "Finnish"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = fr,},           // #18 Language = "French"
//    //                new LanguageName {Text = "Teileagúis", TranslationToLanguage = ga,},       // #19 Language = "Irish"
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = gl,},           // #20 Language = "Galician"
//    //                new LanguageName {Text = "તેલુગુ", TranslationToLanguage = gu,},             // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "טלוגו", TranslationToLanguage = he,},           // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "तेलुगु", TranslationToLanguage = hi,},             // #23 Language = "Hindi"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = hr,},           // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = ht,},           // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = hu,},           // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "տելուգու", TranslationToLanguage = hy,},         // #27 Language = "Armenian"
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = id,},           // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "telúgú", TranslationToLanguage = isLanguage,},   // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = it,},           // #30 Language = "Italian"
//    //                new LanguageName {Text = "テルグ語", TranslationToLanguage = ja,},          // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ტელუგუ", TranslationToLanguage = ka,},         // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ತೆಲುಗು", TranslationToLanguage = kn,},            // #33 Language = "Kannada"
//    //                new LanguageName {Text = "텔루구어", TranslationToLanguage = ko,},          // #34 Language = "Korean"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = la,},           // #35 Language = "Latin"
//    //                new LanguageName {Text = "telugų", TranslationToLanguage = lt,},           // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = lv,},           // #37 Language = "Latvian"
//    //                new LanguageName {Text = "телугу", TranslationToLanguage = mk,},           // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = ms,},           // #39 Language = "Malay"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = mt,},           // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = nl,},           // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = no,},           // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = pl,},           // #43 Language = "Polish"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = pt,},           // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = ro,},           // #45 Language = "Romanian"
//    //                new LanguageName {Text = "телугу", TranslationToLanguage = ru,},           // #46 Language = "Russian"
//    //                new LanguageName {Text = "telugu", TranslationToLanguage = sk,},           // #47 Language = "Slovak"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = sl,},           // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = sq,},           // #49 Language = "Albanian"
//    //                new LanguageName {Text = "телугу", TranslationToLanguage = sr,},           // #50 Language = "Serbian"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = sv,},           // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = sw,},           // #52 Language = "Swahili"
//    //                new LanguageName {Text = "தெலுங்கு", TranslationToLanguage = ta,},        // #53 Language = "Tamil"
//    //                new LanguageName {Text = "తెలుగు", TranslationToLanguage = te,},            // #54 Language = "Telugu"
//    //                new LanguageName {Text = "เตลูกู", TranslationToLanguage = th,},              // #55 Language = "Thai"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = tr,},           // #56 Language = "Turkish"
//    //                new LanguageName {Text = "телугу", TranslationToLanguage = uk,},           // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "تيلوگو", TranslationToLanguage = ur,},          // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Telugu", TranslationToLanguage = vi,},           // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "טעלוגו", TranslationToLanguage = yi,},          // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "泰卢固语", TranslationToLanguage = zh,},         // #61 Language = "Chinese"
//    //            };

//    //        if (th.Names.Count < 61)
//    //            th.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = en,},                  // #1  Language = "English"
//    //                new LanguageName {Text = "tailandés", TranslationToLanguage = es,},             // #2  Language = "Spanish"
//    //                new LanguageName {Text = "thailändisch", TranslationToLanguage = de,},          // #3  Language = "German"
//    //                new LanguageName {Text = "التايلاندية", TranslationToLanguage = ar,},           // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = af,},                  // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Tay", TranslationToLanguage = az,},                   // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Тайская", TranslationToLanguage = be,},               // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "тайландски", TranslationToLanguage = bg,},            // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "থাই", TranslationToLanguage = bn,},                    // #9  Language = "Bengali"
//    //                new LanguageName {Text = "tailandès", TranslationToLanguage = ca,},             // #10 Language = "Catalan"
//    //                new LanguageName {Text = "thajské", TranslationToLanguage = cs,},               // #11 Language = "Czech"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = cy,},                  // #12 Language = "Welsh"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = da,},                  // #13 Language = "Danish"
//    //                new LanguageName {Text = "tai", TranslationToLanguage = et,},                   // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = eu,},                  // #15 Language = "Basque"
//    //                new LanguageName {Text = "تایلندی", TranslationToLanguage = fa,},              // #16 Language = "Persian"
//    //                new LanguageName {Text = "thaimaalainen", TranslationToLanguage = fi,},         // #17 Language = "Finnish"
//    //                new LanguageName {Text = "thaïlandaise", TranslationToLanguage = fr,},          // #18 Language = "French"
//    //                new LanguageName {Text = "Téalainnis", TranslationToLanguage = ga,},            // #19 Language = "Irish"
//    //                new LanguageName {Text = "Tailandés", TranslationToLanguage = gl,},             // #20 Language = "Galician"
//    //                new LanguageName {Text = "થાઈ", TranslationToLanguage = gu,},                   // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "תאילנדי", TranslationToLanguage = he,},              // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "थाई", TranslationToLanguage = hi,},                   // #23 Language = "Hindi"
//    //                new LanguageName {Text = "Tajlandski", TranslationToLanguage = hr,},            // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Tayi", TranslationToLanguage = ht,},                  // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = hu,},                  // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "թայերեն", TranslationToLanguage = hy,},               // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Thailand", TranslationToLanguage = id,},              // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = isLanguage,},          // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = it,},                  // #30 Language = "Italian"
//    //                new LanguageName {Text = "タイ", TranslationToLanguage = ja,},                  // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ტაილანდური", TranslationToLanguage = ka,},          // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಥಾಯ್", TranslationToLanguage = kn,},                 // #33 Language = "Kannada"
//    //                new LanguageName {Text = "타이어", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = la,},                  // #35 Language = "Latin"
//    //                new LanguageName {Text = "Tailando", TranslationToLanguage = lt,},              // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "Taizemes", TranslationToLanguage = lv,},              // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Тајланд", TranslationToLanguage = mk,},               // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = ms,},                  // #39 Language = "Malay"
//    //                new LanguageName {Text = "Tajlandiż", TranslationToLanguage = mt,},             // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = nl,},                  // #41 Language = "Dutch"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = no,},                  // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "tajski", TranslationToLanguage = pl,},                // #43 Language = "Polish"
//    //                new LanguageName {Text = "tailandês", TranslationToLanguage = pt,},             // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "tailandez", TranslationToLanguage = ro,},             // #45 Language = "Romanian"
//    //                new LanguageName {Text = "тайский", TranslationToLanguage = ru,},               // #46 Language = "Russian"
//    //                new LanguageName {Text = "Thajské", TranslationToLanguage = sk,},               // #47 Language = "Slovak"
//    //                new LanguageName {Text = "tajski", TranslationToLanguage = sl,},                // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "Тајландски", TranslationToLanguage = sr,},            // #50 Language = "Serbian"
//    //                new LanguageName {Text = "thailändska", TranslationToLanguage = sv,},           // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Thai", TranslationToLanguage = sw,},                  // #52 Language = "Swahili"
//    //                new LanguageName {Text = "தாய்", TranslationToLanguage = ta,},                 // #53 Language = "Tamil"
//    //                new LanguageName {Text = "థాయ్", TranslationToLanguage = te,},                  // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ไทย", TranslationToLanguage = th,},                   // #55 Language = "Thai"
//    //                new LanguageName {Text = "Tayland", TranslationToLanguage = tr,},               // #56 Language = "Turkish"
//    //                new LanguageName {Text = "тайський", TranslationToLanguage = uk,},              // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "تھائی", TranslationToLanguage = ur,},                // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Thái", TranslationToLanguage = vi,},                  // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "טייַלענדיש", TranslationToLanguage = yi,},            // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "泰国", TranslationToLanguage = zh,},                  // #61 Language = "Chinese"
//    //            };

//    //        if (tr.Names.Count < 61)
//    //            tr.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Turkish", TranslationToLanguage = en,},               // #1  Language = "English"
//    //                new LanguageName {Text = "turco", TranslationToLanguage = es,},                 // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Türkisch", TranslationToLanguage = de,},              // #3  Language = "German"
//    //                new LanguageName {Text = "تركي", TranslationToLanguage = ar,},                 // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Turkse", TranslationToLanguage = af,},                // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Türk", TranslationToLanguage = az,},                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Турэцкая", TranslationToLanguage = be,},              // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "турски", TranslationToLanguage = bg,},                // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "তুর্কী", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "turc", TranslationToLanguage = ca,},                  // #10 Language = "Catalan"
//    //                new LanguageName {Text = "turečtina", TranslationToLanguage = cs,},             // #11 Language = "Czech"
//    //                new LanguageName {Text = "Twrcaidd", TranslationToLanguage = cy,},              // #12 Language = "Welsh"
//    //                new LanguageName {Text = "tyrkisk", TranslationToLanguage = da,},               // #13 Language = "Danish"
//    //                new LanguageName {Text = "türgi", TranslationToLanguage = et,},                 // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Turkish", TranslationToLanguage = eu,},               // #15 Language = "Basque"
//    //                new LanguageName {Text = "ترکی", TranslationToLanguage = fa,},                 // #16 Language = "Persian"
//    //                new LanguageName {Text = "turkki", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName {Text = "turque", TranslationToLanguage = fr,},                // #18 Language = "French"
//    //                new LanguageName {Text = "Tuircis", TranslationToLanguage = ga,},               // #19 Language = "Irish"
//    //                new LanguageName {Text = "Turco", TranslationToLanguage = gl,},                 // #20 Language = "Galician"
//    //                new LanguageName {Text = "ટર્કિશ", TranslationToLanguage = gu,},                  // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "טורקית", TranslationToLanguage = he,},               // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "तुर्की का", TranslationToLanguage = hi,},                // #23 Language = "Hindi"
//    //                new LanguageName {Text = "turski", TranslationToLanguage = hr,},                // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Tik", TranslationToLanguage = ht,},                   // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "török", TranslationToLanguage = hu,},                 // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "թուրքական", TranslationToLanguage = hy,},            // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Turki", TranslationToLanguage = id,},                 // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "Tyrkneska", TranslationToLanguage = isLanguage,},     // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "turco", TranslationToLanguage = it,},                 // #30 Language = "Italian"
//    //                new LanguageName {Text = "トルコ", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName {Text = "თურქეთის", TranslationToLanguage = ka,},            // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಟರ್ಕಿಷ್", TranslationToLanguage = kn,},                 // #33 Language = "Kannada"
//    //                new LanguageName {Text = "터키어", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Turkish", TranslationToLanguage = la,},               // #35 Language = "Latin"
//    //                new LanguageName {Text = "Turkijos", TranslationToLanguage = lt,},              // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "turku", TranslationToLanguage = lv,},                 // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Турција", TranslationToLanguage = mk,},               // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Turki", TranslationToLanguage = ms,},                 // #39 Language = "Malay"
//    //                new LanguageName {Text = "Tork", TranslationToLanguage = mt,},                  // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Turks", TranslationToLanguage = nl,},                 // #41 Language = "Dutch"
//    //                new LanguageName {Text = "tyrkisk", TranslationToLanguage = no,},               // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "turecki", TranslationToLanguage = pl,},               // #43 Language = "Polish"
//    //                new LanguageName {Text = "turco", TranslationToLanguage = pt,},                 // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "turc", TranslationToLanguage = ro,},                  // #45 Language = "Romanian"
//    //                new LanguageName {Text = "турецкий", TranslationToLanguage = ru,},              // #46 Language = "Russian"
//    //                new LanguageName {Text = "turečtina", TranslationToLanguage = sk,},             // #47 Language = "Slovak"
//    //                new LanguageName {Text = "turečtina", TranslationToLanguage = sl,},             // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "turk", TranslationToLanguage = sq,},                  // #49 Language = "Albanian"
//    //                new LanguageName {Text = "турски", TranslationToLanguage = sr,},                // #50 Language = "Serbian"
//    //                new LanguageName {Text = "turkiska", TranslationToLanguage = sv,},              // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kituruki", TranslationToLanguage = sw,},              // #52 Language = "Swahili"
//    //                new LanguageName {Text = "டர்கிஷ்", TranslationToLanguage = ta,},               // #53 Language = "Tamil"
//    //                new LanguageName {Text = "టర్కిష్", TranslationToLanguage = te,},                  // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ตุรกี", TranslationToLanguage = th,},                   // #55 Language = "Thai"
//    //                new LanguageName {Text = "Türk", TranslationToLanguage = tr,},                  // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Турецька", TranslationToLanguage = uk,},              // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "ترکی", TranslationToLanguage = ur,},                 // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Thổ Nhĩ Kỳ", TranslationToLanguage = vi,},            // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "טערקיש", TranslationToLanguage = yi,},               // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "土耳其", TranslationToLanguage = zh,},                // #61 Language = "Chinese"
//    //            };

//    //        if (uk.Names.Count < 61)
//    //            uk.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Ukrainian", TranslationToLanguage = en,},                  // #1  Language = "English"
//    //                new LanguageName {Text = "ucranio", TranslationToLanguage = es,},                    // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Ukrainisch", TranslationToLanguage = de,},                 // #3  Language = "German"
//    //                new LanguageName {Text = "الأوكراني", TranslationToLanguage = ar,},                  // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Oekraïens", TranslationToLanguage = af,},                  // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Ukrayna", TranslationToLanguage = az,},                    // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Украінскі", TranslationToLanguage = be,},                  // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "украински", TranslationToLanguage = bg,},                  // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "ইউক্রেরিয়ান", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "ucraïnès", TranslationToLanguage = ca,},                   // #10 Language = "Catalan"
//    //                new LanguageName {Text = "ukrajinský", TranslationToLanguage = cs,},                 // #11 Language = "Czech"
//    //                new LanguageName {Text = "Wcreineg", TranslationToLanguage = cy,},                   // #12 Language = "Welsh"
//    //                new LanguageName {Text = "ukrainsk", TranslationToLanguage = da,},                   // #13 Language = "Danish"
//    //                new LanguageName {Text = "ukraina", TranslationToLanguage = et,},                    // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Ukrainian", TranslationToLanguage = eu,},                  // #15 Language = "Basque"
//    //                new LanguageName {Text = "اوکراین", TranslationToLanguage = fa,},                   // #16 Language = "Persian"
//    //                new LanguageName {Text = "ukrainalainen", TranslationToLanguage = fi,},              // #17 Language = "Finnish"
//    //                new LanguageName {Text = "ukrainiens", TranslationToLanguage = fr,},                 // #18 Language = "French"
//    //                new LanguageName {Text = "Úcráinis", TranslationToLanguage = ga,},                   // #19 Language = "Irish"
//    //                new LanguageName {Text = "Ucraíno", TranslationToLanguage = gl,},                    // #20 Language = "Galician"
//    //                new LanguageName {Text = "યુક્રેનિયન", TranslationToLanguage = gu,},                    // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "אוקראינית", TranslationToLanguage = he,},                 // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "यूक्रेनी", TranslationToLanguage = hi,},                       // #23 Language = "Hindi"
//    //                new LanguageName {Text = "ukrajinski", TranslationToLanguage = hr,},                 // #24 Language = "Croatian"
//    //                new LanguageName {Text = "ukrainian", TranslationToLanguage = ht,},                  // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "ukrán", TranslationToLanguage = hu,},                      // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "ուկրաիներեն", TranslationToLanguage = hy,},                // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Ukraina", TranslationToLanguage = id,},                    // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "úkraínska", TranslationToLanguage = isLanguage,},          // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "ucraino", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName {Text = "ウクライナ語", TranslationToLanguage = ja,},                // #31 Language = "Japanese"
//    //                new LanguageName {Text = "უკრაინის", TranslationToLanguage = ka,},                   // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಉಕ್ರೇನ್ ಪ್ರಾಂತ ಯಾ ಅದರ ಭಾಷೆ", TranslationToLanguage = kn,},  // #33 Language = "Kannada"
//    //                new LanguageName {Text = "우크라이나의", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Ukrainian", TranslationToLanguage = la,},                  // #35 Language = "Latin"
//    //                new LanguageName {Text = "Ukrainos", TranslationToLanguage = lt,},                   // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "ukraiņu", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName {Text = "Украина", TranslationToLanguage = mk,},                    // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Ukraine", TranslationToLanguage = ms,},                    // #39 Language = "Malay"
//    //                new LanguageName {Text = "Ukraina", TranslationToLanguage = mt,},                    // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Oekraïens", TranslationToLanguage = nl,},                  // #41 Language = "Dutch"
//    //                new LanguageName {Text = "ukrainsk", TranslationToLanguage = no,},                   // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "ukraiński", TranslationToLanguage = pl,},                  // #43 Language = "Polish"
//    //                new LanguageName {Text = "ucraniano", TranslationToLanguage = pt,},                  // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "ucrainean", TranslationToLanguage = ro,},                  // #45 Language = "Romanian"
//    //                new LanguageName {Text = "украинский", TranslationToLanguage = ru,},                 // #46 Language = "Russian"
//    //                new LanguageName {Text = "Ukrajinský", TranslationToLanguage = sk,},                 // #47 Language = "Slovak"
//    //                new LanguageName {Text = "ukrajinski", TranslationToLanguage = sl,},                 // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "ukrainisht", TranslationToLanguage = sq,},                 // #49 Language = "Albanian"
//    //                new LanguageName {Text = "украјински", TranslationToLanguage = sr,},                 // #50 Language = "Serbian"
//    //                new LanguageName {Text = "ukrainska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kiukreni", TranslationToLanguage = sw,},                   // #52 Language = "Swahili"
//    //                new LanguageName {Text = "உக்ரேனியன்", TranslationToLanguage = ta,},              // #53 Language = "Tamil"
//    //                new LanguageName {Text = "యుక్రేయిన్", TranslationToLanguage = te,},                   // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ภาษายูเครน", TranslationToLanguage = th,},                   // #55 Language = "Thai"
//    //                new LanguageName {Text = "Ukrayna", TranslationToLanguage = tr,},                    // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Український", TranslationToLanguage = uk,},                // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "يوکرينی", TranslationToLanguage = ur,},                   // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Ucraina", TranslationToLanguage = vi,},                    // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "אוקרייניש", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "乌克兰", TranslationToLanguage = zh,},                     // #61 Language = "Chinese"
//    //            };

//    //        if (ur.Names.Count < 61)
//    //            ur.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = en,},                    // #1  Language = "English"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = es,},                    // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = de,},                    // #3  Language = "German"
//    //                new LanguageName {Text = "الأردية", TranslationToLanguage = ar,},                 // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Oerdoe", TranslationToLanguage = af,},                  // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = az,},                    // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Мова урду", TranslationToLanguage = be,},               // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "урду", TranslationToLanguage = bg,},                    // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "উর্দু", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = ca,},                    // #10 Language = "Catalan"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = cs,},                    // #11 Language = "Czech"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = cy,},                    // #12 Language = "Welsh"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = da,},                    // #13 Language = "Danish"
//    //                new LanguageName {Text = "urdu keel", TranslationToLanguage = et,},               // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = eu,},                    // #15 Language = "Basque"
//    //                new LanguageName {Text = "زبان اردو", TranslationToLanguage = fa,},              // #16 Language = "Persian"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = fi,},                    // #17 Language = "Finnish"
//    //                new LanguageName {Text = "ourdou", TranslationToLanguage = fr,},                  // #18 Language = "French"
//    //                new LanguageName {Text = "Urdais", TranslationToLanguage = ga,},                  // #19 Language = "Irish"
//    //                new LanguageName {Text = "urdú", TranslationToLanguage = gl,},                    // #20 Language = "Galician"
//    //                new LanguageName {Text = "ઉર્દુ", TranslationToLanguage = gu,},                      // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "אורדו", TranslationToLanguage = he,},                  // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "उर्दू", TranslationToLanguage = hi,},                      // #23 Language = "Hindi"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = hr,},                    // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = ht,},                    // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = hu,},                    // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "ուրդու", TranslationToLanguage = hy,},                   // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = id,},                    // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "úrdú", TranslationToLanguage = isLanguage,},            // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = it,},                    // #30 Language = "Italian"
//    //                new LanguageName {Text = "ウルドゥー語", TranslationToLanguage = ja,},             // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ურდული", TranslationToLanguage = ka,},                // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಉರ್ದು ಭಾಷೆ", TranslationToLanguage = kn,},               // #33 Language = "Kannada"
//    //                new LanguageName {Text = "우르 두말", TranslationToLanguage = ko,},                // #34 Language = "Korean"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = la,},                    // #35 Language = "Latin"
//    //                new LanguageName {Text = "urdų kalba", TranslationToLanguage = lt,},              // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName {Text = "урду", TranslationToLanguage = mk,},                    // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = ms,},                    // #39 Language = "Malay"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = mt,},                    // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = nl,},                    // #41 Language = "Dutch"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = no,},                    // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = pl,},                    // #43 Language = "Polish"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = pt,},                    // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = ro,},                    // #45 Language = "Romanian"
//    //                new LanguageName {Text = "язык урду", TranslationToLanguage = ru,},               // #46 Language = "Russian"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = sk,},                    // #47 Language = "Slovak"
//    //                new LanguageName {Text = "urdu", TranslationToLanguage = sl,},                    // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "urduisht", TranslationToLanguage = sq,},                // #49 Language = "Albanian"
//    //                new LanguageName {Text = "урду", TranslationToLanguage = sr,},                    // #50 Language = "Serbian"
//    //                new LanguageName {Text = "Urdu", TranslationToLanguage = sv,},                    // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kiurdu", TranslationToLanguage = sw,},                  // #52 Language = "Swahili"
//    //                new LanguageName {Text = "முள்ளம் பன்றி", TranslationToLanguage = ta,},         // #53 Language = "Tamil"
//    //                new LanguageName {Text = "ఉర్దూ భాష", TranslationToLanguage = te,},                // #54 Language = "Telugu"
//    //                new LanguageName {Text = "ภาษาอิรดู", TranslationToLanguage = th,},                  // #55 Language = "Thai"
//    //                new LanguageName {Text = "Urduca", TranslationToLanguage = tr,},                  // #56 Language = "Turkish"
//    //                new LanguageName {Text = "мова урду", TranslationToLanguage = uk,},               // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "اردو", TranslationToLanguage = ur,},                   // #58 Language = "Urdu"
//    //                new LanguageName {Text = "tiếng Urdu", TranslationToLanguage = vi,},              // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "אורדו", TranslationToLanguage = yi,},                  // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "乌尔都语", TranslationToLanguage = zh,},                 // #61 Language = "Chinese"
//    //            };

//    //        if (vi.Names.Count < 61)
//    //            vi.Names = new List<LanguageName>
//    //            {

//    //                new LanguageName {Text = "Vietnamese", TranslationToLanguage = en,},                      // #1  Language = "English"
//    //                new LanguageName {Text = "vietnamita", TranslationToLanguage = es,},                      // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Vietnamese", TranslationToLanguage = de,},                      // #3  Language = "German"
//    //                new LanguageName {Text = "الفيتنامية", TranslationToLanguage = ar,},                     // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Viëtnamees", TranslationToLanguage = af,},                      // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Vyetnam", TranslationToLanguage = az,},                         // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "в'етнамскі", TranslationToLanguage = be,},                      // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "виетнамски", TranslationToLanguage = bg,},                      // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "ভিএত্নেম লোক", TranslationToLanguage = bn,},                      // #9  Language = "Bengali"
//    //                new LanguageName {Text = "vietnamita", TranslationToLanguage = ca,},                      // #10 Language = "Catalan"
//    //                new LanguageName {Text = "vietnamský", TranslationToLanguage = cs,},                      // #11 Language = "Czech"
//    //                new LanguageName {Text = "Fietnameg", TranslationToLanguage = cy,},                       // #12 Language = "Welsh"
//    //                new LanguageName {Text = "vietnamesisk", TranslationToLanguage = da,},                    // #13 Language = "Danish"
//    //                new LanguageName {Text = "vietnami", TranslationToLanguage = et,},                        // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Vietnamese", TranslationToLanguage = eu,},                      // #15 Language = "Basque"
//    //                new LanguageName {Text = "ویتنامی", TranslationToLanguage = fa,},                        // #16 Language = "Persian"
//    //                new LanguageName {Text = "vietnam", TranslationToLanguage = fi,},                         // #17 Language = "Finnish"
//    //                new LanguageName {Text = "vietnamienne", TranslationToLanguage = fr,},                    // #18 Language = "French"
//    //                new LanguageName {Text = "Vítneaimis", TranslationToLanguage = ga,},                      // #19 Language = "Irish"
//    //                new LanguageName {Text = "Vietnamita", TranslationToLanguage = gl,},                      // #20 Language = "Galician"
//    //                new LanguageName {Text = "વિયેતનામીઝ", TranslationToLanguage = gu,},                      // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "ויאטנמית", TranslationToLanguage = he,},                       // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "वियतनामी", TranslationToLanguage = hi,},                        // #23 Language = "Hindi"
//    //                new LanguageName {Text = "vijetnamski", TranslationToLanguage = hr,},                     // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Vyetnamyen", TranslationToLanguage = ht,},                      // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "vietnami", TranslationToLanguage = hu,},                        // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "վիետնամերեն", TranslationToLanguage = hy,},                    // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Vietnam", TranslationToLanguage = id,},                         // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "víetnamska", TranslationToLanguage = isLanguage,},              // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "vietnamita", TranslationToLanguage = it,},                      // #30 Language = "Italian"
//    //                new LanguageName {Text = "ベトナム", TranslationToLanguage = ja,},                         // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ვიეტნამური", TranslationToLanguage = ka,},                     // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ವಿಯೆಟ್ನಾಂ ದೇಶದ ವ್ಯಕ್ತಿ ಯಾ ಪ್ರಜೆ ಯಾ ಭಾಷೆ", TranslationToLanguage = kn,}, // #33 Language = "Kannada"
//    //                new LanguageName {Text = "베트남 사람", TranslationToLanguage = ko,},                      // #34 Language = "Korean"
//    //                new LanguageName {Text = "Vietnamica", TranslationToLanguage = la,},                      // #35 Language = "Latin"
//    //                new LanguageName {Text = "vietnamiečių", TranslationToLanguage = lt,},                    // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "vjetnamiešu", TranslationToLanguage = lv,},                     // #37 Language = "Latvian"
//    //                new LanguageName {Text = "виетнамски", TranslationToLanguage = mk,},                      // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Vietnam", TranslationToLanguage = ms,},                         // #39 Language = "Malay"
//    //                new LanguageName {Text = "Vjetnamiż", TranslationToLanguage = mt,},                       // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Vietnamese", TranslationToLanguage = nl,},                      // #41 Language = "Dutch"
//    //                new LanguageName {Text = "vietnamesisk", TranslationToLanguage = no,},                    // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "wietnamski", TranslationToLanguage = pl,},                      // #43 Language = "Polish"
//    //                new LanguageName {Text = "vietnamita", TranslationToLanguage = pt,},                      // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "vietnameză", TranslationToLanguage = ro,},                      // #45 Language = "Romanian"
//    //                new LanguageName {Text = "вьетнамский", TranslationToLanguage = ru,},                     // #46 Language = "Russian"
//    //                new LanguageName {Text = "vietnamský", TranslationToLanguage = sk,},                      // #47 Language = "Slovak"
//    //                new LanguageName {Text = "Vietnamese", TranslationToLanguage = sl,},                      // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "vietnamisht", TranslationToLanguage = sq,},                     // #49 Language = "Albanian"
//    //                new LanguageName {Text = "вијетнамски", TranslationToLanguage = sr,},                     // #50 Language = "Serbian"
//    //                new LanguageName {Text = "vietnamese", TranslationToLanguage = sv,},                      // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kivietinamu", TranslationToLanguage = sw,},                     // #52 Language = "Swahili"
//    //                new LanguageName {Text = "வியட்னாமீஸ்", TranslationToLanguage = ta,},                  // #53 Language = "Tamil"
//    //                new LanguageName {Text = "వియత్నమీస్", TranslationToLanguage = te,},                       // #54 Language = "Telugu"
//    //                new LanguageName {Text = "เวียตนาม", TranslationToLanguage = th,},                          // #55 Language = "Thai"
//    //                new LanguageName {Text = "Vietnam", TranslationToLanguage = tr,},                         // #56 Language = "Turkish"
//    //                new LanguageName {Text = "В'єтнамський", TranslationToLanguage = uk,},                    // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "ويتنامی", TranslationToLanguage = ur,},                        // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Việt", TranslationToLanguage = vi,},                            // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "וויעטנאַמעזיש", TranslationToLanguage = yi,},                  // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "越南", TranslationToLanguage = zh,},                            // #61 Language = "Chinese"
//    //            };

//    //        if (yi.Names.Count < 61)
//    //            yi.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Yiddish", TranslationToLanguage = en,},                // #1  Language = "English"
//    //                new LanguageName {Text = "yídish", TranslationToLanguage = es,},                 // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Jiddisch", TranslationToLanguage = de,},               // #3  Language = "German"
//    //                new LanguageName {Text = "اليديشية", TranslationToLanguage = ar,},              // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Jiddisj", TranslationToLanguage = af,},                // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "Yidiş", TranslationToLanguage = az,},                  // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "ідыш", TranslationToLanguage = be,},                   // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "идиш", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "ইডীশ্", TranslationToLanguage = bn,},                   // #9  Language = "Bengali"
//    //                new LanguageName {Text = "Jiddisch", TranslationToLanguage = ca,},               // #10 Language = "Catalan"
//    //                new LanguageName {Text = "jidiš", TranslationToLanguage = cs,},                  // #11 Language = "Czech"
//    //                new LanguageName {Text = "Iddeweg", TranslationToLanguage = cy,},                // #12 Language = "Welsh"
//    //                new LanguageName {Text = "jiddisch", TranslationToLanguage = da,},               // #13 Language = "Danish"
//    //                new LanguageName {Text = "Jidiš", TranslationToLanguage = et,},                  // #14 Language = "Estonian"
//    //                new LanguageName {Text = "Yiddish", TranslationToLanguage = eu,},                // #15 Language = "Basque"
//    //                new LanguageName {Text = "ییدیش", TranslationToLanguage = fa,},                 // #16 Language = "Persian"
//    //                new LanguageName {Text = "Jiddiš", TranslationToLanguage = fi,},                 // #17 Language = "Finnish"
//    //                new LanguageName {Text = "yiddish", TranslationToLanguage = fr,},                // #18 Language = "French"
//    //                new LanguageName {Text = "Giúdais", TranslationToLanguage = ga,},                // #19 Language = "Irish"
//    //                new LanguageName {Text = "Yiddish", TranslationToLanguage = gl,},                // #20 Language = "Galician"
//    //                new LanguageName {Text = "યીદ્દીશ", TranslationToLanguage = gu,},                  // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "אידיש", TranslationToLanguage = he,},                 // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "यहूदी", TranslationToLanguage = hi,},                   // #23 Language = "Hindi"
//    //                new LanguageName {Text = "jidiš", TranslationToLanguage = hr,},                  // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Yiddish", TranslationToLanguage = ht,},                // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "jiddis", TranslationToLanguage = hu,},                 // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "իդիշ", TranslationToLanguage = hy,},                   // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Yiddi", TranslationToLanguage = id,},                  // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "jiddíska", TranslationToLanguage = isLanguage,},       // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "yiddish", TranslationToLanguage = it,},                // #30 Language = "Italian"
//    //                new LanguageName {Text = "イディッシュ語", TranslationToLanguage = ja,},          // #31 Language = "Japanese"
//    //                new LanguageName {Text = "იდიშზე", TranslationToLanguage = ka,},                // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಯಿಡ್ಡಿಷ್", TranslationToLanguage = kn,},                  // #33 Language = "Kannada"
//    //                new LanguageName {Text = "이디시 말의 뜻", TranslationToLanguage = ko,},          // #34 Language = "Korean"
//    //                new LanguageName {Text = "Yiddish", TranslationToLanguage = la,},                // #35 Language = "Latin"
//    //                new LanguageName {Text = "jidiš", TranslationToLanguage = lt,},                  // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "jidišs", TranslationToLanguage = lv,},                 // #37 Language = "Latvian"
//    //                new LanguageName {Text = "јидски", TranslationToLanguage = mk,},                 // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Bahasa Yiddish", TranslationToLanguage = ms,},         // #39 Language = "Malay"
//    //                new LanguageName {Text = "Jiddix", TranslationToLanguage = mt,},                 // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Jiddisch", TranslationToLanguage = nl,},               // #41 Language = "Dutch"
//    //                new LanguageName {Text = "jiddisch", TranslationToLanguage = no,},               // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "jidysz", TranslationToLanguage = pl,},                 // #43 Language = "Polish"
//    //                new LanguageName {Text = "ídiche", TranslationToLanguage = pt,},                 // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "idiş", TranslationToLanguage = ro,},                   // #45 Language = "Romanian"
//    //                new LanguageName {Text = "идиш", TranslationToLanguage = ru,},                   // #46 Language = "Russian"
//    //                new LanguageName {Text = "jidiš", TranslationToLanguage = sk,},                  // #47 Language = "Slovak"
//    //                new LanguageName {Text = "jidiš", TranslationToLanguage = sl,},                  // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "jidish", TranslationToLanguage = sq,},                 // #49 Language = "Albanian"
//    //                new LanguageName {Text = "јидиш", TranslationToLanguage = sr,},                  // #50 Language = "Serbian"
//    //                new LanguageName {Text = "jiddisch", TranslationToLanguage = sv,},               // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Yiddish", TranslationToLanguage = sw,},                // #52 Language = "Swahili"
//    //                new LanguageName {Text = "ஈத்திஷ", TranslationToLanguage = ta,},               // #53 Language = "Tamil"
//    //                new LanguageName {Text = "యిడ్డిష్", TranslationToLanguage = te,},                  // #54 Language = "Telugu"
//    //                new LanguageName {Text = "Yiddish", TranslationToLanguage = th,},                // #55 Language = "Thai"
//    //                new LanguageName {Text = "Eskenazi dili", TranslationToLanguage = tr,},          // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Ідиш", TranslationToLanguage = uk,},                   // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "يادش", TranslationToLanguage = ur,},                  // #58 Language = "Urdu"
//    //                new LanguageName {Text = "tiếng Yiddish", TranslationToLanguage = vi,},          // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "ייִדיש", TranslationToLanguage = yi,},                 // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "意第绪语", TranslationToLanguage = zh,},                // #61 Language = "Chinese"
//    //            };

//    //        if (zh.Names.Count < 61)
//    //            zh.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName {Text = "Chinese", TranslationToLanguage = en,},                    // #1  Language = "English"
//    //                new LanguageName {Text = "chino", TranslationToLanguage = es,},                      // #2  Language = "Spanish"
//    //                new LanguageName {Text = "Chinese", TranslationToLanguage = de,},                    // #3  Language = "German"
//    //                new LanguageName {Text = "الصينية", TranslationToLanguage = ar,},                   // #4  Language = "Arabic"
//    //                new LanguageName {Text = "Chinese", TranslationToLanguage = af,},                    // #5  Language = "Afrikaans"
//    //                new LanguageName {Text = "çin", TranslationToLanguage = az,},                        // #6  Language = "Azerbaijani"
//    //                new LanguageName {Text = "Кітайскі", TranslationToLanguage = be,},                   // #7  Language = "Belarusian"
//    //                new LanguageName {Text = "китайски", TranslationToLanguage = bg,},                   // #8  Language = "Bulgarian"
//    //                new LanguageName {Text = "চীনা", TranslationToLanguage = bn,},                        // #9  Language = "Bengali"
//    //                new LanguageName {Text = "xinès", TranslationToLanguage = ca,},                      // #10 Language = "Catalan"
//    //                new LanguageName {Text = "Číňan", TranslationToLanguage = cs,},                      // #11 Language = "Czech"
//    //                new LanguageName {Text = "Tseiniaidd", TranslationToLanguage = cy,},                 // #12 Language = "Welsh"
//    //                new LanguageName {Text = "kinesisk", TranslationToLanguage = da,},                   // #13 Language = "Danish"
//    //                new LanguageName {Text = "hiina", TranslationToLanguage = et,},                      // #14 Language = "Estonian"
//    //                new LanguageName {Text = "txinera", TranslationToLanguage = eu,},                    // #15 Language = "Basque"
//    //                new LanguageName {Text = "چینی", TranslationToLanguage = fa,},                      // #16 Language = "Persian"
//    //                new LanguageName {Text = "kiinalainen", TranslationToLanguage = fi,},                // #17 Language = "Finnish"
//    //                new LanguageName {Text = "chinoise", TranslationToLanguage = fr,},                   // #18 Language = "French"
//    //                new LanguageName {Text = "Sínis", TranslationToLanguage = ga,},                      // #19 Language = "Irish"
//    //                new LanguageName {Text = "chinés", TranslationToLanguage = gl,},                     // #20 Language = "Galician"
//    //                new LanguageName {Text = "ચિની", TranslationToLanguage = gu,},                       // #21 Language = "Gujarati"
//    //                new LanguageName {Text = "סינית", TranslationToLanguage = he,},                     // #22 Language = "Hebrew"
//    //                new LanguageName {Text = "चीनी", TranslationToLanguage = hi,},                        // #23 Language = "Hindi"
//    //                new LanguageName {Text = "kineski", TranslationToLanguage = hr,},                    // #24 Language = "Croatian"
//    //                new LanguageName {Text = "Chinwa", TranslationToLanguage = ht,},                     // #25 Language = "Haitian Creo
//    //                new LanguageName {Text = "kínai", TranslationToLanguage = hu,},                      // #26 Language = "Hungarian"
//    //                new LanguageName {Text = "չինացի", TranslationToLanguage = hy,},                     // #27 Language = "Armenian"
//    //                new LanguageName {Text = "Cina", TranslationToLanguage = id,},                       // #28 Language = "Indonesian"
//    //                new LanguageName {Text = "kínverska", TranslationToLanguage = isLanguage,},          // #29 Language = "Icelandic"
//    //                new LanguageName {Text = "cinese", TranslationToLanguage = it,},                     // #30 Language = "Italian"
//    //                new LanguageName {Text = "中国", TranslationToLanguage = ja,},                       // #31 Language = "Japanese"
//    //                new LanguageName {Text = "ჩინური", TranslationToLanguage = ka,},                    // #32 Language = "Georgian"
//    //                new LanguageName {Text = "ಚೀನೀ", TranslationToLanguage = kn,},                       // #33 Language = "Kannada"
//    //                new LanguageName {Text = "중국어", TranslationToLanguage = ko,},                     // #34 Language = "Korean"
//    //                new LanguageName {Text = "Sinica", TranslationToLanguage = la,},                     // #35 Language = "Latin"
//    //                new LanguageName {Text = "Kinijos", TranslationToLanguage = lt,},                    // #36 Language = "Lithuanian"
//    //                new LanguageName {Text = "ķīniešu", TranslationToLanguage = lv,},                    // #37 Language = "Latvian"
//    //                new LanguageName {Text = "кинески", TranslationToLanguage = mk,},                    // #38 Language = "Macedonian"
//    //                new LanguageName {Text = "Cina", TranslationToLanguage = ms,},                       // #39 Language = "Malay"
//    //                new LanguageName {Text = "Ċiniż", TranslationToLanguage = mt,},                      // #40 Language = "Maltese"
//    //                new LanguageName {Text = "Chinees", TranslationToLanguage = nl,},                    // #41 Language = "Dutch"
//    //                new LanguageName {Text = "kinesiske", TranslationToLanguage = no,},                  // #42 Language = "Norwegian"
//    //                new LanguageName {Text = "chiński", TranslationToLanguage = pl,},                    // #43 Language = "Polish"
//    //                new LanguageName {Text = "chinês", TranslationToLanguage = pt,},                     // #44 Language = "Portuguese"
//    //                new LanguageName {Text = "chinezesc", TranslationToLanguage = ro,},                  // #45 Language = "Romanian"
//    //                new LanguageName {Text = "китайский", TranslationToLanguage = ru,},                  // #46 Language = "Russian"
//    //                new LanguageName {Text = "Číňan", TranslationToLanguage = sk,},                      // #47 Language = "Slovak"
//    //                new LanguageName {Text = "kitajski", TranslationToLanguage = sl,},                   // #48 Language = "Slovenian"
//    //                new LanguageName {Text = "kinez", TranslationToLanguage = sq,},                      // #49 Language = "Albanian"
//    //                new LanguageName {Text = "кинески", TranslationToLanguage = sr,},                    // #50 Language = "Serbian"
//    //                new LanguageName {Text = "kinesiska", TranslationToLanguage = sv,},                  // #51 Language = "Swedish"
//    //                new LanguageName {Text = "Kichina", TranslationToLanguage = sw,},                    // #52 Language = "Swahili"
//    //                new LanguageName {Text = "சீன", TranslationToLanguage = ta,},                       // #53 Language = "Tamil"
//    //                new LanguageName {Text = "చైనీస్", TranslationToLanguage = te,},                       // #54 Language = "Telugu"
//    //                new LanguageName {Text = "จีน", TranslationToLanguage = th,},                         // #55 Language = "Thai"
//    //                new LanguageName {Text = "Çin", TranslationToLanguage = tr,},                        // #56 Language = "Turkish"
//    //                new LanguageName {Text = "Китайський", TranslationToLanguage = uk,},                 // #57 Language = "Ukrainian"
//    //                new LanguageName {Text = "چینی", TranslationToLanguage = ur,},                      // #58 Language = "Urdu"
//    //                new LanguageName {Text = "Trung Quốc", TranslationToLanguage = vi,},                 // #59 Language = "Vietnamese"
//    //                new LanguageName {Text = "כינעזיש", TranslationToLanguage = yi,},                   // #60 Language = "Yiddish"
//    //                new LanguageName {Text = "中国", TranslationToLanguage = zh,},                       // #61 Language = "Chinese"
//    //            };


//    //        //Languages with only 1 translation

//    //        if (aa.Names.Count < 1)
//    //            aa.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Afar", TranslationToLanguage = en, },
//    //            };


//    //        if (ab.Names.Count < 1)
//    //            ab.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Abkhazian", TranslationToLanguage = en, },
//    //            };

//    //        if (ak.Names.Count < 1)
//    //            ak.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Akan", TranslationToLanguage = en, },
//    //            };

//    //        if (am.Names.Count < 1)
//    //            am.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Amharic", TranslationToLanguage = en, },
//    //            };

//    //        if (an.Names.Count < 1)
//    //            an.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Aragonese", TranslationToLanguage = en, },
//    //            };

//    //        if (asLanguage.Names.Count < 1)
//    //            asLanguage.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Assamese", TranslationToLanguage = en, },
//    //            };

//    //        if (av.Names.Count < 1)
//    //            av.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Avaric", TranslationToLanguage = en, },
//    //            };

//    //        if (ay.Names.Count < 1)
//    //            ay.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Aymara", TranslationToLanguage = en, },
//    //            };

//    //        if (ba.Names.Count < 1)
//    //            ba.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Bashkir", TranslationToLanguage = en, },
//    //            };

//    //        if (bh.Names.Count < 1)
//    //            bh.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Bihari languages", TranslationToLanguage = en, },
//    //            };

//    //        if (bi.Names.Count < 1)
//    //            bi.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Bislama", TranslationToLanguage = en, },
//    //            };

//    //        if (bm.Names.Count < 1)
//    //            bm.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Bambara", TranslationToLanguage = en, },
//    //            };


//    //        if (bo.Names.Count < 1)
//    //            bo.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tibetan", TranslationToLanguage = en, },
//    //            };

//    //        if (br.Names.Count < 1)
//    //            br.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Breton", TranslationToLanguage = en, },
//    //            };

//    //        if (bs.Names.Count < 1)
//    //            bs.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Bosnian", TranslationToLanguage = en, },
//    //            };


//    //        if (ce.Names.Count < 1)
//    //            ce.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Chechen", TranslationToLanguage = en, },
//    //            };

//    //        if (ch.Names.Count < 1)
//    //            ch.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Chamorro", TranslationToLanguage = en, },
//    //            };

//    //        if (co.Names.Count < 1)
//    //            co.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Corsican", TranslationToLanguage = en, },
//    //            };

//    //        if (cr.Names.Count < 1)
//    //            cr.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Cree", TranslationToLanguage = en, },
//    //            };



//    //        if (cu.Names.Count < 1)
//    //            cu.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Church Slavic", TranslationToLanguage = en, },
//    //            };

//    //        if (cv.Names.Count < 1)
//    //            cv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Chuvash", TranslationToLanguage = en, },
//    //            };



//    //        if (dv.Names.Count < 1)
//    //            dv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Dhivehi", TranslationToLanguage = en, },
//    //            };

//    //        if (dz.Names.Count < 1)
//    //            dz.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Dzongkha", TranslationToLanguage = en, },
//    //            };

//    //        if (ee.Names.Count < 1)
//    //            ee.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Ewe", TranslationToLanguage = en, },
//    //            };

//    //        if (el.Names.Count < 1)
//    //            el.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Modern Greek (1453-)", TranslationToLanguage = en, },
//    //            };

//    //        if (eo.Names.Count < 1)
//    //            eo.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Esperanto", TranslationToLanguage = en, },
//    //            };

//    //        if (ff.Names.Count < 1)
//    //            ff.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Fulah", TranslationToLanguage = en, },
//    //            };

//    //        if (fj.Names.Count < 1)
//    //            fj.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Fijian", TranslationToLanguage = en, },
//    //            };

//    //        if (fo.Names.Count < 1)
//    //            fo.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Faroese", TranslationToLanguage = en, },
//    //            };

//    //        if (fy.Names.Count < 1)
//    //            fy.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Western Frisian", TranslationToLanguage = en, },
//    //            };

//    //        if (gd.Names.Count < 1)
//    //            gd.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Scottish Gaelic", TranslationToLanguage = en, },
//    //            };

//    //        if (gn.Names.Count < 1)
//    //            gn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Guarani", TranslationToLanguage = en, },
//    //            };

//    //        if (gv.Names.Count < 1)
//    //            gv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Manx", TranslationToLanguage = en, },
//    //            };

//    //        if (ha.Names.Count < 1)
//    //            ha.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Hausa", TranslationToLanguage = en, },
//    //            };

//    //        if (ho.Names.Count < 1)
//    //            ho.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Hiri Motu", TranslationToLanguage = en, },
//    //            };

//    //        if (hz.Names.Count < 1)
//    //            hz.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Herero", TranslationToLanguage = en, },
//    //            };

//    //        if (ia.Names.Count < 1)
//    //            ia.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Interlingua (International Auxiliary Language Association)", TranslationToLanguage = en, },
//    //            };

//    //        if (ie.Names.Count < 1)
//    //            ie.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Interlingue", TranslationToLanguage = en, },
//    //            };

//    //        if (ig.Names.Count < 1)
//    //            ig.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Igbo", TranslationToLanguage = en, },
//    //            };

//    //        if (ii.Names.Count < 1)
//    //            ii.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Sichuan Yi", TranslationToLanguage = en, },
//    //            };

//    //        if (io.Names.Count < 1)
//    //            io.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Ido", TranslationToLanguage = en, },
//    //            };


//    //        if (iu.Names.Count < 1)
//    //            iu.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Inuktitut", TranslationToLanguage = en, },
//    //            };

//    //        if (jv.Names.Count < 1)
//    //            jv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Javanese", TranslationToLanguage = en, },
//    //            };

//    //        if (kg.Names.Count < 1)
//    //            kg.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kongo", TranslationToLanguage = en, },
//    //            };

//    //        if (ki.Names.Count < 1)
//    //            ki.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kikuyu", TranslationToLanguage = en, },
//    //            };

//    //        if (kj.Names.Count < 1)
//    //            kj.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kuanyama" , TranslationToLanguage = en, },
//    //            };

//    //        if (kk.Names.Count < 1)
//    //            kk.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kazakh", TranslationToLanguage = en, },
//    //            };

//    //        if (kl.Names.Count < 1)
//    //            kl.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kalaallisut", TranslationToLanguage = en, },
//    //            };

//    //        if (km.Names.Count < 1)
//    //            km.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Central Khmer", TranslationToLanguage = en, },
//    //            };

//    //        if (ks.Names.Count < 1)
//    //            ks.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kashmiri", TranslationToLanguage = en, },
//    //            };

//    //        if (ku.Names.Count < 1)
//    //            ku.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kurdish", TranslationToLanguage = en, },
//    //            };

//    //        if (kv.Names.Count < 1)
//    //            kv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Komi", TranslationToLanguage = en, },
//    //            };

//    //        if (kw.Names.Count < 1)
//    //            kw.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Cornish", TranslationToLanguage = en, },
//    //            };

//    //        if (ky.Names.Count < 1)
//    //            ky.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kirghiz", TranslationToLanguage = en, },
//    //            };

//    //        if (lb.Names.Count < 1)
//    //            lb.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Luxembourgish", TranslationToLanguage = en, },
//    //            };

//    //        if (lg.Names.Count < 1)
//    //            lg.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Ganda", TranslationToLanguage = en, },
//    //            };

//    //        if (li.Names.Count < 1)
//    //            li.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Limburgan", TranslationToLanguage = en, },
//    //            };

//    //        if (ln.Names.Count < 1)
//    //            ln.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Lingala", TranslationToLanguage = en, },
//    //            };

//    //        if (lo.Names.Count < 1)
//    //            lo.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Lao", TranslationToLanguage = en, },
//    //            };

//    //        if (mg.Names.Count < 1)
//    //            mg.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Malagasy", TranslationToLanguage = en, },
//    //            };

//    //        if (mh.Names.Count < 1)
//    //            mh.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Marshallese", TranslationToLanguage = en, },
//    //            };

//    //        if (mi.Names.Count < 1)
//    //            mi.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Maori", TranslationToLanguage = en, },
//    //            };

//    //        if (ml.Names.Count < 1)
//    //            ml.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Malayalam", TranslationToLanguage = en, },
//    //            };

//    //        if (mn.Names.Count < 1)
//    //            mn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Mongolian", TranslationToLanguage = en, },
//    //            };


//    //        if (mr.Names.Count < 1)
//    //            mr.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Marathi", TranslationToLanguage = en, },
//    //            };

//    //        if (my.Names.Count < 1)
//    //            my.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Burmese", TranslationToLanguage = en, },
//    //            };

//    //        if (na.Names.Count < 1)
//    //            na.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Nauru", TranslationToLanguage = en, },
//    //            };

//    //        if (nb.Names.Count < 1)
//    //            nb.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Norwegian Bokmål", TranslationToLanguage = en, },
//    //            };

//    //        if (ne.Names.Count < 1)
//    //            ne.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Nepali", TranslationToLanguage = en, },
//    //            };

//    //        if (ng.Names.Count < 1)
//    //            ng.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Ndonga", TranslationToLanguage = en, },
//    //            };

//    //        if (nn.Names.Count < 1)
//    //            nn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Norwegian Nynorsk", TranslationToLanguage = en, },
//    //            };


//    //        if (nv.Names.Count < 1)
//    //            nv.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Navajo", TranslationToLanguage = en, },
//    //            };

//    //        if (ny.Names.Count < 1)
//    //            ny.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Nyanja", TranslationToLanguage = en, },
//    //            };

//    //        if (oc.Names.Count < 1)
//    //            oc.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Occitan (post 1500)", TranslationToLanguage = en, },
//    //            };

//    //        if (om.Names.Count < 1)
//    //            om.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Oromo", TranslationToLanguage = en, },
//    //            };


//    //        if (or.Names.Count < 1)
//    //            or.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Oriya", TranslationToLanguage = en, },
//    //            };

//    //        if (os.Names.Count < 1)
//    //            os.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Ossetian", TranslationToLanguage = en, },
//    //            };

//    //        if (pa.Names.Count < 1)
//    //            pa.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Panjabi", TranslationToLanguage = en, },
//    //            };

//    //        if (pi.Names.Count < 1)
//    //            pi.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Pali", TranslationToLanguage = en, },
//    //            };

//    //        if (ps.Names.Count < 1)
//    //            ps.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Pushto", TranslationToLanguage = en, },
//    //            };

//    //        if (qu.Names.Count < 1)
//    //            qu.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Quechua", TranslationToLanguage = en, },
//    //            };

//    //        if (rm.Names.Count < 1)
//    //            rm.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Romansh", TranslationToLanguage = en, },
//    //            };

//    //        if (rn.Names.Count < 1)
//    //            rn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Rundi", TranslationToLanguage = en, },
//    //            };

//    //        if (rw.Names.Count < 1)
//    //            rw.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Kinyarwanda", TranslationToLanguage = en, },
//    //            };


//    //        if (sa.Names.Count < 1)
//    //            sa.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Sanskrit", TranslationToLanguage = en, },
//    //            };


//    //        if (sc.Names.Count < 1)
//    //            sc.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Sardinian", TranslationToLanguage = en, },
//    //            };


//    //        if (sd.Names.Count < 1)
//    //            sd.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Sindhi", TranslationToLanguage = en, },
//    //            };


//    //        if (se.Names.Count < 1)
//    //            se.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Northern Sami", TranslationToLanguage = en, },
//    //            };


//    //        if (sg.Names.Count < 1)
//    //            sg.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Sango", TranslationToLanguage = en, },
//    //            };


//    //        if (sh.Names.Count < 1)
//    //            sh.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Serbo-Croatian", TranslationToLanguage = en, },
//    //            };


//    //        if (si.Names.Count < 1)
//    //            si.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Sinhala", TranslationToLanguage = en, },
//    //            };


//    //        if (sm.Names.Count < 1)
//    //            sm.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Samoan", TranslationToLanguage = en, },
//    //            };


//    //        if (sn.Names.Count < 1)
//    //            sn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Shona", TranslationToLanguage = en, },
//    //            };


//    //        if (so.Names.Count < 1)
//    //            so.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Somali", TranslationToLanguage = en, },
//    //            };


//    //        if (ss.Names.Count < 1)
//    //            ss.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Swati", TranslationToLanguage = en, },
//    //            };


//    //        if (st.Names.Count < 1)
//    //            st.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Southern Sotho", TranslationToLanguage = en, },
//    //            };


//    //        if (su.Names.Count < 1)
//    //            su.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Sundanese", TranslationToLanguage = en, },
//    //            };


//    //        if (tg.Names.Count < 1)
//    //            tg.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tajik", TranslationToLanguage = en, },
//    //            };

//    //        if (ti.Names.Count < 1)
//    //            ti.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tigrinya", TranslationToLanguage = en, },
//    //            };

//    //        if (tk.Names.Count < 1)
//    //            tk.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Turkmen", TranslationToLanguage = en, },
//    //            };

//    //        if (tl.Names.Count < 1)
//    //            tl.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tagalog", TranslationToLanguage = en, },
//    //            };

//    //        if (tn.Names.Count < 1)
//    //            tn.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tswana", TranslationToLanguage = en, },
//    //            };

//    //        if (to.Names.Count < 1)
//    //            to.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tonga (Tonga Islands)", TranslationToLanguage = en, },
//    //            };

//    //        if (ts.Names.Count < 1)
//    //            ts.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tsonga", TranslationToLanguage = en, },
//    //            };

//    //        if (tt.Names.Count < 1)
//    //            tt.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tatar", TranslationToLanguage = en, },
//    //            };

//    //        if (tw.Names.Count < 1)
//    //            tw.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Twi", TranslationToLanguage = en, },
//    //            };

//    //        if (ty.Names.Count < 1)
//    //            ty.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Tahitian", TranslationToLanguage = en, },
//    //            };

//    //        if (ug.Names.Count < 1)
//    //            ug.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Uighur", TranslationToLanguage = en, },
//    //            };

//    //        if (uz.Names.Count < 1)
//    //            uz.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Uzbek", TranslationToLanguage = en, },
//    //            };

//    //        if (ve.Names.Count < 1)
//    //            ve.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Venda", TranslationToLanguage = en, },
//    //            };

//    //        if (vo.Names.Count < 1)
//    //            vo.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Volapük", TranslationToLanguage = en, },
//    //            };

//    //        if (wa.Names.Count < 1)
//    //            wa.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Walloon", TranslationToLanguage = en, },
//    //            };

//    //        if (wo.Names.Count < 1)
//    //            wo.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Wolof", TranslationToLanguage = en, },
//    //            };

//    //        if (xh.Names.Count < 1)
//    //            xh.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Xhosa", TranslationToLanguage = en, },
//    //            };

//    //        if (yo.Names.Count < 1)
//    //            yo.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Yoruba", TranslationToLanguage = en, },
//    //            };

//    //        if (za.Names.Count < 1)
//    //            za.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Zhuang", TranslationToLanguage = en, },
//    //            };

//    //        if (zu.Names.Count < 1)
//    //            zu.Names = new List<LanguageName>
//    //            {
//    //                new LanguageName{ Text = "Zulu", TranslationToLanguage = en, },
//    //            };

//    //        Context.SaveChanges();

//    //        #endregion
//    //    }
//    //}
//}