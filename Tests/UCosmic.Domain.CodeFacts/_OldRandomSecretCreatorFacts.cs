//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Should;

//namespace UCosmic.Domain.Email
//{
//    // ReSharper disable UnusedMember.Global
//    public class RandomSecretCreatorFacts
//    // ReSharper restore UnusedMember.Global
//    {
//        [TestClass]
//        public class CreateSecretMethod_StringArg
//        {
//            [TestMethod]
//            public void ReturnsString_TenCharactersLong()
//            {
//                var secret = RandomSecretCreator.CreateSecret(10);
//                secret.ShouldNotBeNull();
//                secret.Length.ShouldEqual(10);
//            }
//        }

//        [TestClass]
//        public class CreateSecretMethod_StringArg_StringArg
//        {
//            [TestMethod]
//            public void ReturnsString_Between8And12CharactersLong()
//            {
//                var secret = RandomSecretCreator.CreateSecret(8, 12);
//                secret.ShouldNotBeNull();
//                secret.Length.ShouldBeInRange(8, 12);
//            }
//        }
//    }
//}
