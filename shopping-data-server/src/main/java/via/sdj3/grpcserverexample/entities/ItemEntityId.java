package via.sdj3.grpcserverexample.entities;

import jakarta.persistence.*;

import java.io.Serializable;

@Embeddable
@Table
public class ItemEntityId implements Serializable
{
  @Column
  private int sellerId;
  @Id
  @GeneratedValue
  @Column
  private int itemId;

  public ItemEntityId(){}
  public ItemEntityId(int sellerId) {
    this.sellerId = sellerId;
  }
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
