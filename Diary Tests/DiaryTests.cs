using Microsoft.VisualStudio.TestTools.UnitTesting;
using Diary;

namespace Diary_Tests
{
    [TestClass]
    public class DiaryTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddRecordNullReturnedMessage()
        {
            //arrange
            string[] line = GetLine();
            string expected = "¬ходные данные некорректны";
            //act
            DiaryStruct d = new DiaryStruct();
            d.AddRecord(line, out string actual);
            //assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string[] GetLine()
        {
            return new string[] { };
        }
    }
}
