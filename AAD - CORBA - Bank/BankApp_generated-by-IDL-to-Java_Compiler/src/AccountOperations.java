package BankApp;


/**
* BankApp/AccountOperations.java .
* Generated by the IDL-to-Java compiler (portable), version "3.1"
* from banker.idl
* Friday, June 18, 2004 8:45:41 PM IST
*/

public interface AccountOperations 
{
  int withDraw (float amt, String acctno);
  int deposit (float amt, String acctno);

  //AcctDetails getAcctDetails(in string acctno);
  float balanceQuery (String acctno);
} // interface AccountOperations
