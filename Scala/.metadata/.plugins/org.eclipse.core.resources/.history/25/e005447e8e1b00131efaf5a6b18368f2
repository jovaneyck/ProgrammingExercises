package test

class Implicits extends TestBase{
	//similar to c# extension methods
  behavior of "implicits" 
  
  it should "act as extension methods in c#, for when a class needs that one extra method" in{
	object Extensions { //implicit classes must be defined within another scope
	    implicit class IntegerExtensions(v:Int){
	    	def plusOne = v + 1
	    }   
	}
	
	import Extensions._ //you need an explicit import to avoid black magic
	
    (4 plusOne) should be(5)
  }
}