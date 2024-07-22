using Godot;
using System.Collections.Generic;
using System.IO;

[GlobalClass]
public abstract partial class Inventory : Node2D {
    private IngredientData[] ingredientCodex;
    public List<IngredientData> ingredients;

    public override void _Ready () {
        string[] ingredientPaths = Directory.GetFiles("Items/Ingredients");

        ingredientCodex = new IngredientData[ingredientPaths.Length];

        for (int i = 0; i < ingredientPaths.Length; i++) {
            var ingredient = ResourceLoader.Load<IngredientData>(ingredientPaths[i]);
            ingredientCodex[i] = ingredient;
        }
    }

    #region Random item generation
    public IngredientData GetRandomItem () {
        int index = GD.RandRange(0, ingredientCodex.Length - 1);

        Quality quality = GetRandomQuality();

        float haggleValue = 0; // TODO: change when HaggleValue is relevant

        IngredientData item = new(ingredientCodex[index]) {
            ItemQuality = quality,
            HaggleValue = haggleValue
        };

        return item;
    }

    public Quality GetRandomQuality () {
        Quality quality = Quality.Normal;

        if (GD.RandRange(0, 2) == 0) {
            quality = Quality.Good;
            if (GD.RandRange(0, 2) == 0) {
                quality = Quality.Great;
                if (GD.RandRange(0, 2) == 0) {
                    quality = Quality.Exquisite;
                }
            }
        }

        return quality;
    }
    #endregion

    #region UI
    /// <summary>
    /// Create the ingredient UI.<br></br>
    /// Hierarchy has the ui as the first child of inventory,
    /// the children of which are positional nodes for easy editing.<br></br>
    /// Item nodes are then childed to the positional nodes.
    /// </summary>
    public void CreateIngredientUI () {
        Node2D ui = (Node2D) GetNode("Ingredients");

        // Create ingredient nodes.
        // These are the nodes that will later be childed to the positional nodes in 'ui'
        List<IngredientNode> ingredientNodes = new();
        ingredients.ForEach(ing => {
            var prefab = ResourceLoader.Load<PackedScene>("Prefabs/Ingredient.tscn");
            var node = prefab.Instantiate<IngredientNode>();

            node.data = ing;
            node.Name = ing.Name;

            ingredientNodes.Add(node);
        });

        for (int i = 0; i < ingredientNodes.Count; i++) {
            var posNode = ui.GetChild(i);
            var ingredientNode = ingredientNodes[i];

            posNode.AddChild(ingredientNode);
        }
    }

    public virtual void DestroyUI() {
        foreach (var ui in GetChildren()) {
            foreach (var posNode in ui.GetChildren()) {
                if(posNode.GetChildCount() > 0)
                    posNode.GetChild(0).QueueFree();
            }
        }
    }

    public virtual void UpdateUI() {
        DestroyUI();
        CreateIngredientUI();
    }
    #endregion

    public static void TransferIngredient (int index, Inventory from, Inventory to) {
        to.ingredients.Add(from.ingredients[index]);
        from.ingredients.RemoveAt(index);

        from.UpdateUI();
        to.UpdateUI();
    }
}