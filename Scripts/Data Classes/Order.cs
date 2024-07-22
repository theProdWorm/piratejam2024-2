using Godot;
using System.Collections.Generic;

public partial class Order : Resource {
    public List<PotionData> requestedPotions;
    public List<PotionData> packedPotions;

    public int Reward {
        get {
            int reward = 0;

            if (requestedPotions.Count == packedPotions.Count) {
                foreach (var potion in packedPotions) {
                    reward += potion.GetRealPrice();
                }
            }
            else {
                foreach (var potion in requestedPotions) {
                    reward += potion.Price;
                }
            }

            return reward;
        }
    }
}