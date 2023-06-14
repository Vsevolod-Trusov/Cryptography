import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

public class Main {
    public static void main(String[] args) {
        StringBuilder plaintext = readFromFile("D:\\Univer\\3курс\\2сем\\Криптография\\Labs\\lab04\\file.txt");

        String key = "Feiglinge";

        //Viszener
        System.out.println("Viszener:\n\nEncrypt plaintext");
        String ciphertext = encrypt(plaintext.toString(), key.toLowerCase(), 26, 'a');
        System.out.println(ciphertext);
        System.out.println("Key: " + key.toLowerCase());
        decrypt(ciphertext, key.toLowerCase(), 26, 'a');
        System.out.println("Decrypt plaintext");
        //Port
        String plainText = plaintext.toString();
        byte[] keyValue = "Feiglinge".getBytes();
        // Encrypt the data
        long startEncrypt = System.currentTimeMillis();
        byte[] encryptedData = encrypt(plainText.getBytes(), keyValue);
        long finishEncrypt = System.currentTimeMillis();
        // Print the original text and the decrypted text
        System.out.println("Port:\n\nEncrypt using Port cypher");
        System.out.println("Port: encrypt time:" + (finishEncrypt - startEncrypt));

        // Decrypt the encrypted data
        startEncrypt = System.currentTimeMillis();
        decrypt(encryptedData, keyValue);
        finishEncrypt = System.currentTimeMillis();

        System.out.println("Decrypt using Port cypher");
        System.out.println("Port: encrypt time:" + (finishEncrypt - startEncrypt));
    }
    public static StringBuilder readFromFile(String path){
        StringBuilder text = new StringBuilder();
        try (BufferedReader br = new BufferedReader(new FileReader(path))) {
            String line;
            while ((line = br.readLine()) != null) {
                text.append(line.toLowerCase().replaceAll("\\W+", ""));
            }
            return text;
        } catch (IOException e) {
            System.err.println("Error reading file: " + e.getMessage());
            return new StringBuilder();
        }
    }
    public static String encrypt(String plaintext, String key, int alpabetSize, int startAlphabetSymbolCode) {
        int keyIndex = 0;
        StringBuilder ciphertext = new StringBuilder();
        Map<Character, Integer> charFrequencyMap = new HashMap<>();

        long startEncrypt = System.currentTimeMillis();
        for (int i = 0; i < plaintext.length(); i++) {

            char c = plaintext.charAt(i);
            char k = key.charAt(keyIndex);
            int shift = (int) k - startAlphabetSymbolCode; // convert key character to shift value
            char encrypted = (char) (((int) c + shift - startAlphabetSymbolCode) % alpabetSize + startAlphabetSymbolCode); // apply shift and wrap around
            ciphertext.append(encrypted);

            if (Character.isLetterOrDigit(c)) { // exclude non-alphanumeric characters
                charFrequencyMap.put(c, charFrequencyMap.getOrDefault(c, 0) + 1);
            }

            keyIndex = (keyIndex + 1) % key.length(); // move to next key character
        }

        long finishEncrypt = System.currentTimeMillis();
        System.out.println("Encrypt time: " + (finishEncrypt - startEncrypt));


        int plaintextLength = plaintext.length();
        System.out.println("Character frequency count:");
        for (Map.Entry<Character, Integer> entry : charFrequencyMap.entrySet()) {
            System.out.println(entry.getKey() + " -> " + (double)entry.getValue()/plaintextLength);
        }

        return ciphertext.toString();
    }

    public static String decrypt(String ciphertext, String key, int alpabetSize, int startAlphabetSymbolCode) {
        int keyIndex = 0;
        StringBuilder plaintext = new StringBuilder();
        Map<Character, Integer> charFrequencyMap = new HashMap<>();

        long startEncrypt = System.currentTimeMillis();
        for (int i = 0; i < ciphertext.length(); i++) {
            char c = ciphertext.charAt(i);
            char k = key.charAt(keyIndex);
            int shift = (int) k - startAlphabetSymbolCode; // index in alphabet
            char decrypted = (char) (((int) c - shift - startAlphabetSymbolCode + alpabetSize) % alpabetSize + startAlphabetSymbolCode); // apply shift and wrap around
            plaintext.append(decrypted);


            if (Character.isLetterOrDigit(c)) { // exclude non-alphanumeric characters
                charFrequencyMap.put(c, charFrequencyMap.getOrDefault(c, 0) + 1);
            }
            keyIndex = (keyIndex + 1) % key.length(); // move to next key character
        }

        long finishEncrypt = System.currentTimeMillis();
        System.out.println("Decrypt time: " + (finishEncrypt - startEncrypt));

        int plaintextLength = plaintext.length();
        System.out.println("Character frequency count:");
        for (Map.Entry<Character, Integer> entry : charFrequencyMap.entrySet()) {
            System.out.println(entry.getKey() + " -> " + (double)entry.getValue()/plaintextLength);
        }
        return plaintext.toString();
    }

    public static byte[] encrypt(byte[] data, byte[] key) {
        byte[] encryptedData = new byte[data.length];
        for (int i = 0; i < data.length; i++) {
            encryptedData[i] = (byte) (data[i] ^ key[i % key.length]);
            //System.out.print((char)encryptedData[i]);
        }
        return encryptedData;
    }

    // Decrypt encrypted data using the same key
    public static byte[] decrypt(byte[] encryptedData, byte[] key) {
        return encrypt(encryptedData, key);
    }
}