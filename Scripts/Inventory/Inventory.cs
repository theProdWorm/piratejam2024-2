using Godot;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[GlobalClass]
public abstract partial class Inventory : Node2D {
    public static Dictionary<string, IngredientData> ingredientCodex;
    public List<IngredientData> ingredients;

    public override void _Ready () {
        string[] ingredientPaths = Directory.GetFiles("Items/Ingredients");

        ingredientCodex = new();

        foreach(var path in ingredientPaths) {
            var ingredient = ResourceLoader.Load<IngredientData>(path);

            ingredientCodex.Add(ingredient.Name, ingredient);
        }
    }

    public IngredientData GetRandomItem () {
        int index = GD.RandRange(0, ingredientCodex.Values.Count - 1);

        float haggleValue = 0; // TODO: change when HaggleValue is relevant

        IngredientData item = new(ingredientCodex.Values.ToArray()[index]) {
            HaggleValue = haggleValue
        };

        item.RandomizeQuality();

        return item;
    }

    #region UI
    /// <summary>
    /// Create the ingredient UI.<br></br>
    /// Hierarchy has the ui as the first child of inventory,
    /// the children of which are positional nodes for easy editing.<br></br>
    /// Item nodes are then childed to the positional nodes.
    /// </summary>
    public virtual void CreateIngredientUI () {
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

    protected void DisplayPrices () {
        var ui = (Node2D) GetNode("Ingredients");

        foreach (var node in ui.GetChildren().Cast<Node2D>()) {
            if (node.GetChildCount() == 0)
                continue;

            var priceTag = node.GetChild(0).GetNode<RichTextLabel>("Price tag");

            priceTag.Visible = true;
        }
    }
    #endregion

    public static void TransferIngredient (int index, Inventory from, Inventory to) {
        to.ingredients.Add(from.ingredients[index]);
        from.ingredients.RemoveAt(index);

        from.UpdateUI();
        to.UpdateUI();
    }
}