using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class RecipeBook : Node2D {
    [Export] public Node2D leftPage;
    [Export] public Node2D rightPage;

    private int currentPage = 0;
    private int pageCount;

    public override void _Ready () {
        pageCount = Mathf.CeilToInt(BrewHandler.potionCodex.Count * 0.5);

        CreateUI();
    }

    public void CreateUI() {
        // Left page
        List<Sprite2D> lNodes = new();
        for(int i = 0; i < leftPage.GetChildCount(); i++) {
            Node currentNode = leftPage.GetChild(i);

            if (currentNode.Name.ToString().StartsWith("Ing"))
                lNodes.Add((Sprite2D) currentNode);
        }

        PotionData leftPotion = BrewHandler.potionCodex[currentPage * 2];

        var potionNode = leftPage.GetNode<Sprite2D>("Potion");
        potionNode.Texture = leftPotion.Texture;
        potionNode.GetChild<RichTextLabel>(0).Text = "[center]" + leftPotion.Name;

        for (int i = 0; i < leftPotion.Formula.Count; i++) {
            lNodes[i].Texture = leftPotion.Formula[i].Texture;
        }


        // Right page
        if (currentPage == pageCount - 1 &&
            BrewHandler.potionCodex.Count % 2 != 0)
            return;

        List<Sprite2D> rNodes = new();
        for (int i = 0; i < rightPage.GetChildCount(); i++) {
            Node currentNode = rightPage.GetChild(i);

            if (!currentNode.Name.ToString().StartsWith("Ing"))
                continue;

            rNodes.Add(currentNode as Sprite2D);
        }

        PotionData rightPotion = BrewHandler.potionCodex[currentPage * 2 + 1];

        var rightPotionNode = rightPage.GetNode<Sprite2D>("Potion");
        rightPotionNode.Texture = rightPotion.Texture;
        rightPotionNode.GetChild<RichTextLabel>(0).Text = "[center]" + rightPotion.Name;

        for (int i = 0; i < rightPotion.Formula.Count; i++) {
            rNodes[i].Texture = rightPotion.Formula[i].Texture;
        }
    }

    public void DestroyUI() {
        foreach (var node in leftPage.GetChildren().Cast<Sprite2D>()) {
            if(node.Name == "Potion") {
                node.GetChild<RichTextLabel>(0).Text = string.Empty;
            }

            node.Texture = null;
        }

        foreach (var node in rightPage.GetChildren().Cast<Sprite2D>()) {
            if (node.Name == "Potion") {
                node.GetChild<RichTextLabel>(0).Text = string.Empty;
            }

            node.Texture = null;
        }
    }

    public void UpdateUI() {
        DestroyUI();
        CreateUI();
    }

    public void NextPage() {
        if (currentPage < pageCount - 1)
            currentPage++;

        UpdateUI();
    }

    public void PrevPage () {
        if(currentPage > 0)
            currentPage--;

        UpdateUI();
    }
}