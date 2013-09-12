class StringCalculator {
	def add(input : String) : Int = 
	  if (input isEmpty) 0
	  else asListOfNumbers(input) sum
	  
	private def asListOfNumbers(list : String) : List[Int] = 
	  toListOfNumbers(list) map (el => Integer.parseInt(el))
	  
	private def toListOfNumbers(input : String): List[String] =
  		((input.split("\n") toList) flatMap (el => el.split(",") toList))
}


