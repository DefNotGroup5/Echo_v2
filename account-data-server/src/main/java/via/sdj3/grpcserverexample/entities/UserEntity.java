package via.sdj3.grpcserverexample.entities;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table
public class UserEntity {
  @Id
  @Column
  private int id;
  @Column
  private String email;
  @Column
  private String firstName;
  @Column
  private String lastName;
  @Column
  private String password;
  @Column
  private String address;
  @Column
  private String city;
  @Column
  private int postalCode;
  @Column
  private String country;
  @Column
  private boolean isSeller;


  public UserEntity() {}

  public UserEntity(int id, String email, String firstName, String lastName,
      String password, String address, String city, int postalCode,
      String country, boolean isSeller)
  {
    this.id = id;
    this.email = email;
    this.firstName = firstName;
    this.lastName = lastName;
    this.password = password;
    this.address = address;
    this.city = city;
    this.postalCode = postalCode;
    this.country = country;
    this.isSeller = isSeller;
  }

  public int getId()
  {
    return id;
  }

  public void setId(int id)
  {
    this.id = id;
  }

  public String getEmail()
  {
    return email;
  }

  public void setEmail(String email)
  {
    this.email = email;
  }

  public String getFirstName()
  {
    return firstName;
  }

  public void setFirstName(String firstName)
  {
    this.firstName = firstName;
  }

  public String getLastName()
  {
    return lastName;
  }

  public void setLastName(String lastName)
  {
    this.lastName = lastName;
  }

  public String getPassword()
  {
    return password;
  }

  public void setPassword(String password)
  {
    this.password = password;
  }

  public String getAddress()
  {
    return address;
  }

  public void setAddress(String address)
  {
    this.address = address;
  }

  public String getCity()
  {
    return city;
  }

  public void setCity(String city)
  {
    this.city = city;
  }

  public int getPostalCode()
  {
    return postalCode;
  }

  public void setPostalCode(int postalCode)
  {
    this.postalCode = postalCode;
  }

  public String getCountry()
  {
    return country;
  }

  public void setCountry(String country)
  {
    this.country = country;
  }

  public boolean isSeller()
  {
    return isSeller;
  }

  public void setSeller(boolean seller)
  {
    isSeller = seller;
  }

}
