using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField]
    private Difficulty difficulty;

    public static DifficultyController instance;

    void Awake() {
        if (instance) {
            Destroy(this);
            return;
        }
        
        instance = this;
    }

    public Difficulty getDifficulty() {
        return this.difficulty;
    }

    public string DifficultyToString() {
        switch (this.difficulty) { 
            case Difficulty.facil: return "facil";
            case Difficulty.medio: return "medio";
            case Difficulty.dificil: return "dificil";
            default: return "facil";
        } 
    }

    public void setDifficulty(Difficulty newDifficulty) {
        this.difficulty = newDifficulty;
    }

    public float getDifficultyDamageScaler() {
        switch (this.difficulty) {
            case Difficulty.facil: return 0.9f;
            case Difficulty.medio: return 1f;
            case Difficulty.dificil: return 1.1f;
            default: return 0.9f;
        }
    }
}
