using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    #region Private Variables ---------------------------------------------
    // Private variables representing the collection of collectible letters 
    public CollectedLetter A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z;
    private int _TotalCollectedLetters;
    #endregion Private Variables ------------------------------------------

    #region Properties ----------------------------------------------------
    /// <summary>
    /// The total number of collected numbers
    /// </summary>
    public int TotalCollectedLetters
    {
        get
        {
            return _TotalCollectedLetters;
        }
        set
        {
            _TotalCollectedLetters = value;
        }
    }

    #endregion Properties -------------------------------------------------

    #region Functions ---------------------------------------------
    /// <summary>
    /// Updates the inventory's count of letters
    /// </summary>
    private void UpdateQuantityCollectedLetters()
    {
        _TotalCollectedLetters 
            = A.Count + B.Count + C.Count + D.Count + E.Count + F.Count + G.Count + H.Count + I.Count + J.Count 
            + K.Count + L.Count + M.Count + N.Count + O.Count + P.Count + Q.Count + R.Count + S.Count + T.Count
            + U.Count + V.Count + W.Count + X.Count + Y.Count + Z.Count;
    }

    /// <summary>
    /// Adds a collected letter to the inventory and update the total letters count
    /// </summary>
    public void AddCollectedLetter(string letter)
    {
        IncrementLetter(letter);
        UpdateQuantityCollectedLetters();
    }

    /// <summary>
    /// Subtracts a collected letter from the inventory and update the total letters count
    /// </summary>
    public void SubtractCollectedLetter(string letter)
    {
        DecrementLetter(letter);
        UpdateQuantityCollectedLetters();
    }

    /// <summary>
    /// Retrieves specified letter count
    /// </summary>
    public int GetLetterCount(string letter)
    {
        int count = 0;
        switch (letter)
        {
            case "A":
                count = A.Count;
                break;
            case "B":
                count = B.Count;
                break;
            case "C":
                count = C.Count;
                break;
            case "D":
                count = D.Count;
                break;
            case "E":
                count = E.Count;
                break;
            case "F":
                count = F.Count;
                break;
            case "G":
                count = G.Count;
                break;
            case "H":
                count = H.Count;
                break;
            case "I":
                count = I.Count;
                break;
            case "J":
                count = J.Count;
                break;
            case "K":
                count = K.Count;
                break;
            case "L":
                count = L.Count;
                break;
            case "M":
                count = M.Count;
                break;
            case "N":
                count = N.Count;
                break;
            case "O":
                count = O.Count;
                break;
            case "P":
                count = P.Count;
                break;
            case "Q":
                count = Q.Count;
                break;
            case "R":
                count = R.Count;
                break;
            case "S":
                count = S.Count;
                break;
            case "T":
                count = T.Count;
                break;
            case "U":
                count = U.Count;
                break;
            case "V":
                count = V.Count;
                break;
            case "W":
                count = W.Count;
                break;
            case "X":
                count = X.Count;
                break;
            case "Y":
                count = Y.Count;
                break;
            case "Z":
                count = Z.Count;
                break;
            default:
                break;
        }
        return count;
    }

    /// <summary>
    /// Increments count of letter
    /// </summary>
    private void IncrementLetter(string letter)
    {
        switch (letter)
        {
            case "A":
                A.Count++;
                break;
            case "B":
                B.Count++;
                break;
            case "C":
                C.Count++;
                break;
            case "D":
                D.Count++;
                break;
            case "E":
                E.Count++;
                break;
            case "F":
                F.Count++;
                break;
            case "G":
                G.Count++;
                break;
            case "H":
                H.Count++;
                break;
            case "I":
                I.Count++;
                break;
            case "J":
                J.Count++;
                break;
            case "K":
                K.Count++;
                break;
            case "L":
                L.Count++;
                break;
            case "M":
                M.Count++;
                break;
            case "N":
                N.Count++;
                break;
            case "O":
                O.Count++;
                break;
            case "P":
                P.Count++;
                break;
            case "Q":
                Q.Count++;
                break;
            case "R":
                R.Count++;
                break;
            case "S":
                S.Count++;
                break;
            case "T":
                T.Count++;
                break;
            case "U":
                U.Count++;
                break;
            case "V":
                V.Count++;
                break;
            case "W":
                W.Count++;
                break;
            case "X":
                X.Count++;
                break;
            case "Y":
                Y.Count++;
                break;
            case "Z":
                Z.Count++;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Decrements count of letter
    /// </summary>
    private void DecrementLetter(string letter)
    {
        switch (letter)
        {
            case "A":
                A.Count--;
                break;
            case "B":
                B.Count--;
                break;
            case "C":
                C.Count--;
                break;
            case "D":
                D.Count--;
                break;
            case "E":
                E.Count--;
                break;
            case "F":
                F.Count--;
                break;
            case "G":
                G.Count--;
                break;
            case "H":
                H.Count--;
                break;
            case "I":
                I.Count--;
                break;
            case "J":
                J.Count--;
                break;
            case "K":
                K.Count--;
                break;
            case "L":
                L.Count--;
                break;
            case "M":
                M.Count--;
                break;
            case "N":
                N.Count--;
                break;
            case "O":
                O.Count--;
                break;
            case "P":
                P.Count--;
                break;
            case "Q":
                Q.Count--;
                break;
            case "R":
                R.Count--;
                break;
            case "S":
                S.Count--;
                break;
            case "T":
                T.Count--;
                break;
            case "U":
                U.Count--;
                break;
            case "V":
                V.Count--;
                break;
            case "W":
                W.Count--;
                break;
            case "X":
                X.Count--;
                break;
            case "Y":
                Y.Count--;
                break;
            case "Z":
                Z.Count--;
                break;
            default:
                break;
        }
    }

    #endregion Functions ------------------------------------------

    #region Constructors --------------------------------------------------
    /// <summary>
    /// Stores all the collected letters in one location. Initialized each letter, 0 of each
    /// </summary>
    public Inventory()
    {
        _TotalCollectedLetters = 0;
        A = new CollectedLetter("A");
        B = new CollectedLetter("B");
        C = new CollectedLetter("C");
        D = new CollectedLetter("D");
        E = new CollectedLetter("E");
        F = new CollectedLetter("F");
        G = new CollectedLetter("G");
        H = new CollectedLetter("H");
        I = new CollectedLetter("I");
        J = new CollectedLetter("J");
        K = new CollectedLetter("K");
        L = new CollectedLetter("L");
        M = new CollectedLetter("M");
        N = new CollectedLetter("N");
        O = new CollectedLetter("O");
        P = new CollectedLetter("P");
        Q = new CollectedLetter("Q");
        R = new CollectedLetter("R");
        S = new CollectedLetter("S");
        T = new CollectedLetter("T");
        U = new CollectedLetter("U");
        V = new CollectedLetter("V");
        W = new CollectedLetter("W");
        X = new CollectedLetter("X");
        Y = new CollectedLetter("Y");
        Z = new CollectedLetter("Z");
    }
    #endregion Constructors -----------------------------------------------


    public void take_letterAway(string letter) { 
       DecrementLetter( letter);
    }

  
}
