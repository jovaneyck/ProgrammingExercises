package test

class DesignPatterns extends TestBase{
  behavior of "visitor" 
  
  it should "be easily implemented using pattern matching" in{
    trait Tree
    object Empty extends Tree
    case class NonEmpty(value : Int, left : Tree, right : Tree) extends Tree
    
    val tree = NonEmpty(1,Empty,NonEmpty(2,Empty,Empty))
    
    object SumVisitor{
      def visit(t : Tree) : Int = t match{
        case Empty => 0
        case NonEmpty(v,l,r) => v + visit(l) + visit(r)
      }
    } 
    
    SumVisitor.visit(tree) should be(3)
  }
  
  behavior of "singleton"
  
  it should "be implemented by an object" in {
    object Singleton{
      var value = 1
    }
    
    val x = Singleton
    val y = Singleton
    
    x.value = 200
    
    y.value should be(200)
  }
  
  behavior of "factory method"
  
  it should "be implemented with companion objects to avoid cluttering the namespace" in {
    class Dog(val name : String)
    object Dog{
      def apply(name : String) = new Dog(name)
    }
    
    val max = Dog("Max")
  }
    
  behavior of "template method"
  
  it should "be clear that functional programming was MADE for easy function composition" in {
    class Logger(logListener : String=>Unit) {
      private var id = 0
      
      def log(message : String) = { //oooh, template method action!
		  id = id + 1
		  logListener(id+"-"+message)
      }  
    }
    
    var loggedMessage : String = ""
    def fileSystemLogListener(message : String) : Unit = loggedMessage = message 
    
    object FileSystemLogger extends Logger(fileSystemLogListener){}
    
    FileSystemLogger.log("hello")
    
    loggedMessage should be("1-hello")
    
  }
}