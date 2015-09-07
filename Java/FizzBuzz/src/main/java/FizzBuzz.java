import java.util.LinkedHashMap;

/**
 * Created by Jo.VanEyck on 20/08/2015.
 */
public class FizzBuzz {
    public static LinkedHashMap<Integer, String> specialNumberToMagicWord = new LinkedHashMap<Integer, String>(){{
        put(3, "Fizz");
        put(5, "Buzz");
     }};

    public static String Of(int i) {
        String result = "";

        for(int specialNumber : specialNumberToMagicWord.keySet())
            if(i % specialNumber == 0)
                result += specialNumberToMagicWord.get(specialNumber);

        if(result != "")
            return result;

        return String.valueOf(i);
    }
}
