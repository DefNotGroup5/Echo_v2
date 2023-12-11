package via.sdj3.grpcserverexample.entities;

import jakarta.persistence.*;

@Entity
@Table
public class WishlistEntity
{
  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  @Column(name = "wishlist_id")
  private int id;

  @ManyToOne
  @JoinColumn(name = "item_id", nullable = false)
  private ItemEntity item;

  @ManyToOne
  @JoinColumn(name = "user_id", nullable = false)
  private UserEntity user;

  public WishlistEntity() {}
  public WishlistEntity(int id, ItemEntity item, UserEntity user)
  {
    this.id = id;
    this.item = item;
    this.user = user;
  }

  public int getId()
  {
    return id;
  }

  public void setId(int id)
  {
    this.id = id;
  }

  public ItemEntity getItem()
  {
    return item;
  }

  public void setItem(ItemEntity item)
  {
    this.item = item;
  }

  public UserEntity getUser()
  {
    return user;
  }

  public void setUser(UserEntity user)
  {
    this.user = user;
  }
}
