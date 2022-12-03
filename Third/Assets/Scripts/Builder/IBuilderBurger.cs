public interface IBuilderBurger
{
    public Burger MyBurger { get; set; }
    
    void AddCheese();
    void AddHam();

    void AddOnion();
    void AddLettuce();

    void AddKetchup();
    void AddMayonnaise();

    void AddChickenCutlet();
    void AddBeefCutlet();
}