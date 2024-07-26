using Godot;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class BrewHandler : Node {
    public static List<PotionData> potionCodex;

    public override void _Ready () {
        potionCodex = new();

        string[] potionPaths = Directory.GetFiles("Items/Potions");

        foreach (var path in potionPaths) {
            var potion = ResourceLoader.Load<PotionData>(path);

            potionCodex.Add(potion);
        }
    }

    public static bool TryBrew (List<IngredientData> input, out PotionData resultPotion) {
        foreach(var potion in potionCodex) {
            if (input.Count != potion.Formula.Count)
                continue;

            bool isMatch = true;

            foreach(var ingredient in potion.Formula) {
                int amountInFormula = potion.Formula.Count(ing => ing.Name == ingredient.Name);
                int amountInInput = input.Count(ing => ing.Name == ingredient.Name);

                if (amountInFormula != amountInInput){
                    isMatch = false;
                    break;
                }
            }

            if(!isMatch)
                continue;

            resultPotion = potion;
            resultPotion.IngredientsUsed = input;
            return true;
        }

        resultPotion = null;
        return false;
    }
}