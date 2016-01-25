using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Common;
using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Enumerations;
using MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.Exceptions;

namespace WorkflowActivityLibrary.Tests
{
    /// <summary>
    /// This test class is meant to test the public/protected interfaces to the WorkflowActivityLibrary.ExpressionFunction class.
    /// </summary>
    /// <remarks>
    /// If this doesn't work for you then you may have to add a public key verification entry to your system with the following commands (replace [xxx] with real values):
    /// C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\sn.exe -T [path to solution]MIMWAL\src\WorkflowActivityLibrary\bin\Debug\MicrosoftServices.IdentityManagement.WorkflowActivityLibrary.dll
    /// C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\sn.exe -Vr *,[public key]
    /// </remarks>
    [TestClass]
    public class ExpressionFunctionTests
    {
        [TestMethod]
        public void AddFunctionResolveWorksTest()
        {
            const string function = "add";
            const EvaluationMode evaluationMode = EvaluationMode.Resolve;

            #region 1L + 1L
            var parameters1 = new ArrayList {1L, 1L};
            var expressionFunction1 = new ExpressionFunction(function, parameters1, evaluationMode);
            Assert.IsNotNull(expressionFunction1);

            var result1 = expressionFunction1.Run();

            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(long));
            Assert.AreEqual(2L, result1);
            #endregion

            #region 10L + 10L
            var parameters2 = new ArrayList { 10L, 10L };
            var expressionFunction2 = new ExpressionFunction(function, parameters2, evaluationMode);
            Assert.IsNotNull(expressionFunction2);

            var result2 = expressionFunction2.Run();

            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(long));
            Assert.AreEqual(20L, result2);
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFunctionFormatException))]
        public void AddFunctionResolveStringInvalidInputFormatTest()
        {
            const string function = "add";
            const EvaluationMode evaluationMode = EvaluationMode.Resolve;

            #region 10 string + 10 string
            var parameters1 = new ArrayList { "10", "10" };
            var expresssionFunction1 = new ExpressionFunction(function, parameters1, evaluationMode);
            expresssionFunction1.Run();
            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFunctionFormatException))]
        public void AddFunctionResolveCharInvalidInputFormatTest()
        {
            const string function = "add";
            const EvaluationMode evaluationMode = EvaluationMode.Resolve;

            #region 1 char + 1 char
            var parameters1 = new ArrayList { '1', '1' };
            var expresssionFunction1 = new ExpressionFunction(function, parameters1, evaluationMode);
            expresssionFunction1.Run();
            #endregion
        }
    }
}