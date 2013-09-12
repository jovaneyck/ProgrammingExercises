class StringCalculator {
	def add(input : String) : Int = 
	  if (input isEmpty) 0
	  else asListOfNumbers(input) sum
	  
	private def asListOfNumbers(list : String) : List[Int] = 
	  (list.split(",") toList) map (el => Integer.parseInt(el))
}


