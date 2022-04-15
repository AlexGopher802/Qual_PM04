package com.example.O2MaM2O.repo;

import com.example.O2MaM2O.models.Address;
import org.springframework.data.repository.CrudRepository;

public interface AddressRepo extends CrudRepository<Address, Long> {
    Address findByStreet(String street);
}
