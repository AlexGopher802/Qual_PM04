package com.example.O2MaM2O.repo;

import com.example.O2MaM2O.models.Person;
import org.springframework.data.repository.CrudRepository;

public interface PersonRepo extends CrudRepository<Person, Long> {
}
