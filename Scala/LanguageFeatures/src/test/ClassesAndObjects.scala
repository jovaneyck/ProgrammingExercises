package test

import org.scalatest.exceptions.TestFailedException

class ClassesAndObjects extends TestBase{
	behavior of "classes"
	
	class Person(val name : String, val age : Int)
	val me = new Person("Jo", 23)
	
	it should "have properties" in {
	  me.age should be(23)
	}
	
	it should "support read-only attributes" in {
	  //me.age = 45 //compiler does not allow reassignment to val's
	}
	
	it should "support mutable state" in {
	  class Mutable(var mutableProperty : Int = 3) //var means mutable
	  
	  val mutableObject = new Mutable();
	  mutableObject.mutableProperty += 1
	  
	  mutableObject.mutableProperty should be(4)
	}
	
	it should "support private methods" in {
	  object Superman {
	    def whenDrunk = identity
	    
	    private def identity = "Clark Kent"
	  }
	  
	  Superman.whenDrunk //no problem here
	  //Superman.identity //compiler says no
	}
	
	it should "support invariants and preconditions" in {
	  class Account(val balance : Double) {
	    require(balance > 0.0) //check an invariant/ or precondition
	  }
	  
	  an [IllegalArgumentException] should be thrownBy{
		  val negative = new Account(-2)
	  }
	}
	
	it should "support post-condition checking" in {
	  def add(a: Int, b: Int) = {
	    val result = a + b
	    assert(result > 0)
	    result
	  }
	  
	  a [TestFailedException] should be thrownBy {
		  add(-4, -2)
	  }
	}
	
	behavior of "case classes"
	
	it should "not require the new keyword, handy for pattern matching!" in {
  	  case class Room(number : Int)
	  val myRoom = Room(4) //notice the lack of "new" here!
	}
	
	behavior of "objects"
	
	it should "act as a singleton" in {
	  object Superman{
	    def identity = "Clark Kent"
	  }
	  
	  Superman.identity should be("Clark Kent") //again, notice the lack of new
	}
	
	behavior of "companion objects"
	
	it should "act as a factory for classes" in {
	  class Book(title : String) {
	    private def secret = "secret"
	  }
	  
	  object Book {
	    def apply(t : String) = new Book(t)
	  }
	  
	  val myBook = Book("A hitchhiker's guide to the galaxy") //clean, no cluttering with "BookFactory"
	}
	
}