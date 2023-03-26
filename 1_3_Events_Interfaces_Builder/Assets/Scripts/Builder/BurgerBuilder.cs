public class BurgerBuilder : IBuilderBurger
{
    public Burger MyBurger { get; set; }

    // private void CreateBurger() => _burger = new Burger();
    // public Burger GetBurger() => _burger;

    public void AddCheese() => MyBurger.AddCheese();
    public void AddHam() => MyBurger.AddHam();

    public void AddOnion() => MyBurger.AddOnion();
    public void AddLettuce() => MyBurger.AddLettuce();

    public void AddKetchup() => MyBurger.AddKetchup();
    public void AddMayonnaise() => MyBurger.AddMayonnaise();

    public void AddChickenCutlet() => MyBurger.AddChickenCutlet();
    public void AddBeefCutlet() => MyBurger.AddBeefCutlet();
}
