public class Main {
    public static void main(String[] args) {

        String plainText = "Hello world";
        int numRows = 2;
        int numCols = 5;

        String cipherText = encrypt(plainText, numRows, numCols);
        System.out.println(cipherText);

         plainText = decrypt(cipherText, numRows, numCols);
        System.out.println(plainText);
    }

    public static String decrypt(String cipherText, int numRows, int numCols) {
        char[][] matrix = createDecryptMatrix(cipherText, numRows, numCols); // create matrix
        StringBuilder plainText = new StringBuilder(); // to store the decrypted text

        // traverse the matrix in a snake-like pattern
        for (int i = 0; i < numCols; i++) { // iterate over columns
            if (i % 2 == 0) { // even column -> top to bottom
                for (int j = 0; j < numRows; j++) {
                    plainText.append(matrix[j][i]);
                }
            } else { // odd column -> bottom to top
                for (int j = numRows - 1; j >= 0; j--) {
                    plainText.append(matrix[j][i]);
                }
            }
        }

        return plainText.toString().trim(); // remove any padding spaces
    }

    // Create matrix from cipher text
    private static char[][] createDecryptMatrix(String cipherText, int numRows, int numCols) {
        char[][] matrix = new char[numRows][numCols];
        int k = 0;

        for (int i = 0; i < numRows; i++) {
            if (i % 2 == 0) { // even row -> left to right
                for (int j = 0; j < numCols; j++) {
                    matrix[i][j] = cipherText.charAt(k++);
                }
            } else { // odd row -> right to left
                for (int j = numCols - 1; j >= 0; j--) {
                    matrix[i][j] = cipherText.charAt(k++);
                }
            }
        }

        return matrix;
    }
    // Encrypt the plain text using route shuffling algorithm with snake route
    public static String encrypt(String plainText, int numRows, int numCols) {
        char[][] matrix = createDecryptMatrix(plainText, numRows, numCols); // create matrix
        StringBuilder cipherText = new StringBuilder(); // to store the encrypted text

        // traverse the matrix in a snake-like pattern
        for (int i = 0; i < numCols; i++) { // iterate over columns
            if (i % 2 == 0) { // even column -> top to bottom
                for (int j = 0; j < numRows; j++) {
                    cipherText.append(matrix[j][i]);
                }
            } else { // odd column -> bottom to top
                for (int j = numRows - 1; j >= 0; j--) {
                    cipherText.append(matrix[j][i]);
                }
            }
        }

        return cipherText.toString();
    }

    // Create matrix from plain text
    private static char[][] createMatrix(String plainText, int numRows, int numCols) {
        char[][] matrix = new char[numRows][numCols];
        int k = 0;

        for (int i = 0; i < numRows; i++) {
            for (int j = 0; j < numCols; j++) {
                if (k < plainText.length()) {
                    matrix[i][j] = plainText.charAt(k++);
                } else {
                    matrix[i][j] = ' '; // pad with spaces if plain text is shorter than matrix size
                }
            }
        }

        return matrix;
    }
}
