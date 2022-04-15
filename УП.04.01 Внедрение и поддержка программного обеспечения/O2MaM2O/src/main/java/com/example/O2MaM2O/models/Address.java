package com.example.O2MaM2O.models;

import javax.persistence.*;
import java.util.Collection;

@Entity
public class Address {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String city, street, building;
    @OneToMany(mappedBy = "address", fetch = FetchType.EAGER)
    private Collection<Person> tenants;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getStreet() {
        return street;
    }

    public void setStreet(String street) {
        this.street = street;
    }

    public String getBuilding() {
        return building;
    }

    public void setBuilding(String building) {
        this.building = building;
    }

    public Collection<Person> getTenants() {
        return tenants;
    }

    public void setTenants(Collection<Person> tenants) {
        this.tenants = tenants;
    }

    public Address(String city, String street, String building, Collection<Person> tenants) {
        this.city = city;
        this.street = street;
        this.building = building;
        this.tenants = tenants;
    }

    public Address() {
    }
}
