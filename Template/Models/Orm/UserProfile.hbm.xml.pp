<?xml version="1.0" encoding="utf-8"?>

<hibernate-mapping xmlns="urn:nhibernate-mapping-2.2">
  <class name="$rootnamespace$.Models.UserProfile, $rootnamespace$" table="Profiles">
    <id name="Id">
      <generator class="identity" />
    </id>

    <property name="Name" length="100" not-null="true" unique="true" />
    <property name="Active" not-null="true" />
    <property name="Created" not-null="true" />
    <property name="Updated" not-null="true" />

    <bag name="Functionalities" table="Profiles_x_Functionalities" lazy="true">
      <key column="ProfileId" />
      <many-to-many class="$rootnamespace$.Models.Functionality, $rootnamespace$" column="FunctionalityId" />
    </bag>
  </class>
</hibernate-mapping>