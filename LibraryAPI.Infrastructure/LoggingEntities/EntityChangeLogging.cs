using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Infrastructure.LoggingEntities
{
    public class EntityChangeLogging(ILogger<EntityChangeLogging> logger)
    {
        private readonly ILogger<EntityChangeLogging> _entityLogger = logger ?? throw new ArgumentNullException(nameof(logger));

        public void LogChanges(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                var entityName = entry.Entity.GetType().Name;
                var state = entry.State.ToString();
                var eventTime = DateTime.Now;
                
                _entityLogger.LogInformation($"[{eventTime}] Сущность: {entityName}, Состояние: {state}");
                
                if (entry.State == EntityState.Modified)
                {
                    foreach (var property in entry.Properties)
                    {
                        if (property.IsModified)
                        {
                            var propertyName = property.Metadata.Name;
                            var oldValue = property.OriginalValue;
                            var newValue = property.CurrentValue;

                            _entityLogger.LogInformation(
                                $"[{eventTime}] Свойство: {propertyName}, Старое значение: {oldValue}, Новое значение: {newValue}");
                        }
                    }
                }

                if (entry.State == EntityState.Added)
                {
                    _entityLogger.LogInformation($"[{eventTime}] Добавлена сущность: {entityName}");
                    foreach (var property in entry.Properties)
                    {
                        var propertyName = property.Metadata.Name;
                        var newValue = property.CurrentValue;

                        _entityLogger.LogInformation(
                            $"[{eventTime}] Новое свойство: {propertyName}, Значение: {newValue}");
                    }
                }
                
                if (entry.State == EntityState.Deleted)
                {
                    _entityLogger.LogInformation($"[{eventTime}] Удалена сущность: {entityName}");
                    foreach (var property in entry.Properties)
                    {
                        var propertyName = property.Metadata.Name;
                        var oldValue = property.OriginalValue;

                        _entityLogger.LogInformation(
                            $"[{eventTime}] Удаленное свойство: {propertyName}, Значение: {oldValue}");
                    }
                }
            }
        }
    }
}