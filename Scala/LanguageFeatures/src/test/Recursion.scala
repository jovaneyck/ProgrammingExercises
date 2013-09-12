package test

class Recursion extends TestBase{
	//tail recursion!
  
  behavior of "recursion" 
  
  it should "end" in{
    def factorial(n:Int) : Int =
      if(n==1) 1
      else n * factorial(n-1) //current stack must be maintained
    
    factorial(4) should be(24)
  }
  
  it should "support tail recursion" in {
    def factorial(n:Int) : Int = {
      def factorialTailRecursive(accumulator: Int, n: Int) : Int =
        if (n == 1) accumulator
        else factorialTailRecursive(accumulator * n, n -1)
        
      factorialTailRecursive(1, n) //tail-recursive call: can re-use its stack-frame!
    }
    
    factorial(4) should be(24)
  }
}