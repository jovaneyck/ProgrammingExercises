package test

class Functions extends TestBase{
	behavior of "functions" 
  
	it should "accept functions as argument" in{
		def increment(n:Int) = n + 1
		
		def perform(number : Int, something: Int=>Int) : Int = //takes a function as argument
		  something(number)
		  
		perform(1, increment) should be(2) //pass in a function argument
	}
	
	it should "return functions" in {
	  def adder : Int=>Int = (el : Int) => el + 1 //return a function
	  
	  adder(3) should be(4)
	}
	
	it should "be partially applied (Currying)" in {
	  def doStuff(a:Int)(b:Int) = a + b
	  
	  def plusFive = doStuff(5) _ //partially apply a function!
	  
	  plusFive(3) should be(8)
	}

	it should "have a nice shorthand notation" in {
	  def largerThan(a:Int,b:Int) = a > b
	  List(1,2,3) sortWith largerThan should be(List(3,2,1)) //long
	  
	  List(1,2,3) sortWith ((a,b) => a > b) should be(List(3,2,1)) //short
	  
	  List(1,2,3) sortWith (_>_) should be(List(3,2,1)) //shorter
	}
	
}