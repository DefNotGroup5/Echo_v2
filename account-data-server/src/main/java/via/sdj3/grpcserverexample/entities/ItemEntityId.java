package via.sdj3.grpcserverexample.entities;

import java.io.Serializable;

public class ItemEntityId implements Serializable
{
  private int sellerId;
  private int itemId;

  public ItemEntityId(){}
  public ItemEntityId(int sellerId, int itemId)
  {
    this.sellerId = sellerId;
    this.itemId = itemId;
  }

  public int getSellerId()
  {
    return sellerId;
  }

  public void setSellerId(int sellerId)
  {
    this.sellerId = sellerId;
  }

  public int getItemId()
  {
    return itemId;
  }

  public void setItemId(int itemId)
  {
    this.itemId = itemId;
  }
}
