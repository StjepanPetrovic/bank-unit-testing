using Banka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankTests
{
    [TestClass]
    public class TransactionManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidBankAccountException))]
        public void TransferFunds_PassedNonExistentIBAN_ThrowException()
        {
            // arange
            UpraviteljTransakcijama transactionManager = new UpraviteljTransakcijama();

            //act
            transactionManager.PrebaciSredstva("HR77", "HR11", 120);

            //asert

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAmountException))]
        public void TransferFunds_PassedNegativAmount_ThrowException()
        {
            // arange
            UpraviteljTransakcijama transactionManager = new UpraviteljTransakcijama();

            //act
            transactionManager.PrebaciSredstva("HR11", "HR22", -3);

            //asert

        }

        [TestMethod]
        public void TransferFunds_PassedCorrectTransactionHR11toHR22_CorrectAmountInAccount()
        {
            // arange
            UpraviteljTransakcijama transactionManager = new UpraviteljTransakcijama();

            //act
            Transakcija transakcija = transactionManager.PrebaciSredstva("HR11", "HR22", 30000);

            //asert
            Assert.IsTrue(transakcija.Izvor.Stanje == 70000 && transakcija.Odrediste.Stanje == 80000);
        }


        [TestMethod]
        public void TransferFunds_PassedCorrectTransactionHR11toHR22_CorrectPayedAmountAndRemainingAmountToPay()
        {
            // arange
            UpraviteljTransakcijama transactionManager = new UpraviteljTransakcijama();

            //act
            Transakcija transakcija = transactionManager.PrebaciSredstva("HR11", "HR22", 30000);

            //asert
            Assert.IsTrue(transakcija.Naplaceno == 30000 && transakcija.PreostaloNaplatiti == 0);
        }

        [TestMethod]
        public void TransferFunds_PassedCorrectTransactionHR66toHR55_CorrectAmountInAccount()
        {
            // arange
            UpraviteljTransakcijama transactionManager = new UpraviteljTransakcijama();

            //act
            Transakcija transakcija = transactionManager.PrebaciSredstva("HR66", "HR55", 3500);

            //asert
            Assert.IsTrue(transakcija.Izvor.Stanje == 0 && transakcija.Odrediste.Stanje == 10000);
        }

        [TestMethod]
        public void TransferFunds_PayedNotEnoughMoneyfromHR66toHR55_ChargeAsMuchAsPossibleWithoutNegativeAmount()
        {
            // arange
            UpraviteljTransakcijama transactionManager = new UpraviteljTransakcijama();

            //act
            Transakcija transakcija = transactionManager.PrebaciSredstva("HR66", "HR55", 3500);

            //asert
            Assert.IsTrue(transakcija.Naplaceno <= transakcija.Iznos && transakcija.Naplaceno <= transakcija.Iznos);
        }
    }
}
